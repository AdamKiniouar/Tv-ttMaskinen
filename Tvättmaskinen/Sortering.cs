using System.IO;
using System.Xml;
using System;

namespace Tvättmaskinen
{
    public class Sortering
    {
        public void Sort(string filePath, string anonymizedSurname, string anonymizedLastname)
        {

            var savePath = filePath + "/tvättade/";

            var di = new DirectoryInfo(filePath);

            FileInfo[] files = di.GetFiles("*.xml");
            foreach (FileInfo file in files)
            {
                var doc = new XmlDocument();
                doc.Load(file.FullName); ;

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
                                var reader17 = new MisLife17();
                                fileName = reader17.CleanFile(doc, anonymizedSurname, anonymizedLastname);
                                break;

                            case "mislife162":
                                var reader16 = new MisLife16();
                                fileName = reader16.CleanFile(doc, anonymizedSurname, anonymizedLastname);
                                break;
                        }
                    }
                }
                if (pensionsDocList.Count > 0)
                {
                    var readerLifepDoc = new MisLifepDoc();
                    fileName = readerLifepDoc.CleanFile(doc);
                }

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                doc.Save(savePath + fileName + file.Name.Remove(0, 13));
            }
        }
    }
}
