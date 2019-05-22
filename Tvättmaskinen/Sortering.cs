using System.IO;
using System.Linq;
using System.Xml;


namespace Tvättmaskinen
{
    public class Sortering : ISortering
    {
        private IMisLifepDoc _misLifepDoc;
        private IMisLife16 _misLife16;
        private IMisLife17 _misLife17;

        public Sortering(IMisLifepDoc misLifepDoc, IMisLife16 misLife16, IMisLife17 misLife17)
        {
            _misLife17 = misLife17;
            _misLife16 = misLife16;
            _misLifepDoc = misLifepDoc;
        }

        public string _savePath;

        public void Sort(string filePath)
        {
            _savePath = filePath + "/Tvättade/";

            var di = new DirectoryInfo(filePath);

            var files = di.GetFiles("*.xml");
            var fileList = files.GroupBy(x => x.Name.Substring(0, 13)).ToList();

            fileList.ForEach(fileGrouping => 
                CleanAndSave(fileGrouping.Key, fileGrouping.Select(fileGroup => fileGroup).ToArray()));
        }

        public void CleanAndSave(string personnummer, FileInfo[] files)
        {
            string name = new RandomFirstNameGenerator().name;
            foreach (var file in files)
            {
                 
                 var doc = new XmlDocument();
                 doc.Load(file.FullName);
            
                var mislifeList = doc.GetElementsByTagName("mislife");
                var pensionsDocList = doc.GetElementsByTagName("Pensionsinstitut");

                var fileName = "";

                if (mislifeList.Count > 0)
                {
                    for (int i = 0; i < mislifeList.Count; i++)
                    {
                        var mislifeVersion = mislifeList[i].Attributes["version"].Value;

                        switch (mislifeVersion)
                        {
                            case "mislife-1.7.2":
                                fileName = _misLife17.CleanFile(doc, name);
                                break;

                            case "mislife162":
                                fileName = _misLife16.CleanFile(doc, name);
                                break;
                        }
                    }
                }
                if (pensionsDocList.Count > 0)
                {
                    fileName = _misLifepDoc.CleanFile(doc);
                }

                if (!Directory.Exists(_savePath))
                {
                    Directory.CreateDirectory(_savePath);
                }
                doc.Save(_savePath + fileName + file.Name.Remove(0, 13));
            }
        }
    }
}
