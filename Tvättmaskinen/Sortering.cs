using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;


namespace Tvättmaskinen
{
    public class Sortering : ISortering
    {
        private IMisLifepDoc _misLifepDoc;
        private IMisLife162 _misLife162;
        private IMisLife172 _misLife172;
        private IMisLife171 _misLife171;
        private IMisLife173 _misLife173;
        private IMisLife174 _misLife174;
        private IMisLife175 _misLife175;

        public Sortering(IMisLifepDoc misLifepDoc, IMisLife162 misLife162, IMisLife172 misLife172, IMisLife171 misLife171, IMisLife173 misLife173, IMisLife174 misLife174, IMisLife175 misLife175)
        {           
            _misLife162 = misLife162;
            _misLifepDoc = misLifepDoc;
            _misLife171 = misLife171;
            _misLife172 = misLife172;
            _misLife173 = misLife173;
            _misLife174 = misLife174;
            _misLife175 = misLife175;

        }

        public string savePath;

        public string SavePath(string path)
        {
            var savePath = path + "/Tvättade/";

            return savePath;
        }

        public void Sort(string Path, string anonymizedSurName)
        {
            savePath = SavePath(Path);

            FileInfo[] files = GetAllXmlFiles(Path);

            List<IGrouping<string, FileInfo>> fileList = GroupFiles(files);

            for (int i = 0; i < fileList.Count; i++)
            {
                CleanAndSave(fileList[i].Key, anonymizedSurName + (i + 1), fileList[i].ToArray());
            }
        }

        public List<IGrouping<string, FileInfo>> GroupFiles(FileInfo[] files)
        {
            return files.GroupBy(x => x.Name.Substring(0, 13)).ToList();
        }

        public FileInfo[] GetAllXmlFiles(string Path)
        {
            var di = new DirectoryInfo(Path);

            var files = di.GetFiles("*.xml");
            return files;
        }

        public void CleanAndSave(string personnummer, string anonymizedSurName, FileInfo[] files)
        {
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
                                fileName = _misLife172.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife162":
                                fileName = _misLife162.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.1":
                                fileName = _misLife171.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.3":
                                fileName = _misLife173.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.4":
                                fileName = _misLife174.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.5":
                                fileName = _misLife175.CleanFile(doc, anonymizedSurName);
                                break;
                        }
                    }
                }
                if (pensionsDocList.Count > 0)
                {
                    fileName = _misLifepDoc.CleanFile(doc);
                }

                CreateNewFolder(savePath);
                doc.Save(savePath + fileName + file.Name.Remove(0, 13));
            }
        }

        public string CreateNewFolder(string path)
        {
            savePath = path;

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            return savePath;
        }
    }
}
