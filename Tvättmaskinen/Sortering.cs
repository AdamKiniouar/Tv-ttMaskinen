using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;
using System;

namespace Tvättmaskinen
{
    public class Sortering : ISortering
    {
        private IMisLife162 misLife162;
        private IMisLife171 misLife171;
        private IMisLife172 misLife172;
        private IMisLife173 misLife173;
        private IMisLife174 misLife174;
        private IMisLife175 misLife175;
        private IMisLife176 misLife176;
        private IMisLife20 misLife20;
        private IMisLifepDoc misLifepDoc;

        private string savePath;

        public Sortering(IMisLifepDoc misLifepDoc, IMisLife162 misLife162, IMisLife171 misLife171, IMisLife172 misLife172, IMisLife173 misLife173, IMisLife174 misLife174, IMisLife175 misLife175, IMisLife176 misLife176, IMisLife20 misLife20)
        {           
            this.misLife162 = misLife162;           
            this.misLife171 = misLife171;
            this.misLife172 = misLife172;
            this.misLife173 = misLife173;
            this.misLife174 = misLife174;
            this.misLife175 = misLife175;
            this.misLife176 = misLife176;
            this.misLife20 = misLife20;
            this.misLifepDoc = misLifepDoc;
        }       
        
        public string SavePath(string path)
        {
            var todaysDate = DateTime.Now.ToString("-yyyy-MM-dd");
            var savePath = path + "/Tvättade" + todaysDate + "/";

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
                var misLifeList20 = doc.GetElementsByTagName("mis20:mislife");

                var fileName = "";

                if (mislifeList.Count > 0)
                {
                    for (int i = 0; i < mislifeList.Count; i++)
                    {
                        var mislifeVersion = mislifeList[i].Attributes["version"].Value;

                        switch (mislifeVersion)
                        {                            
                            case "mislife162":
                                fileName = misLife162.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.1":
                                fileName = misLife171.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.2":
                                fileName = misLife172.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.3":
                                fileName = misLife173.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.4":
                                fileName = misLife174.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.5":
                                fileName = misLife175.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife-1.7.6":
                                fileName = misLife176.CleanFile(doc, anonymizedSurName);
                                break;
                        }
                    }
                }

                if (misLifeList20.Count > 0)
                {
                    for (int i = 0; i < misLifeList20.Count; i++)
                    {
                        var misLifeVersion = misLifeList20[i].Attributes["version"].Value;

                        switch (misLifeVersion)
                        {
                            case "mislife00":
                                fileName = misLife20.CleanFile(doc, anonymizedSurName);
                                break;
                        }
                    }
                }
                if (pensionsDocList.Count > 0)
                {
                    fileName = misLifepDoc.CleanFile(doc);
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
