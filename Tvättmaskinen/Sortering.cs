using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace Tvättmaskinen
{
    class Sortering
    {
        public Sortering() { }

        public void Sort(string filePath, string fileSave, int degree, string förnamn, string efternamn)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);

            FileInfo[] files = di.GetFiles("*.xml");//Läser in alla xml filer
            foreach (FileInfo file in files)
            {
                var doc = new XmlDocument();
                doc.Load(file.FullName); ;

                XmlNodeList mislifeList = doc.GetElementsByTagName("mislife"); //mislife 1.6 och 1.7
                XmlNodeList pensionsDocList = doc.GetElementsByTagName("Pensionsinstitut"); //Läser in Pensionsdokument

                if (mislifeList.Count > 0)
                {
                    for (int i = 0; i < mislifeList.Count; i++)
                    {
                        var mislifeVersion = mislifeList[i].Attributes["version"].Value; //Kollar version av mislife

                        switch (mislifeVersion)
                        {
                            case "mislife-1.7.2":
                                var reader17 = new MisLife17();
                                reader17.Wash(doc, degree, förnamn, efternamn);
                                break;

                            case "mislife162":
                                var reader16 = new MisLife16();
                                reader16.Wash(doc, degree, förnamn, efternamn);
                                break;
                        }
                    }
                }
                if (pensionsDocList.Count > 0)
                {
                    var readerLifepDoc = new MisLifepDoc();
                    readerLifepDoc.Wash(doc, degree);
                }

                var savePathForFile = fileSave + file.Name.Remove(file.Name.Length - 4, 4) + "-tvättad.xml";//Sparar ned den nya filen med -tvättad.xml tillagd i slutet.

                if (Directory.Exists(savePathForFile))
                {
                    Directory.CreateDirectory(savePathForFile);
                }

                doc.Save(savePathForFile);
            }
        }
    }
}
