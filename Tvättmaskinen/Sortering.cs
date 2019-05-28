﻿using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;


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
                                fileName = _misLife17.CleanFile(doc, anonymizedSurName);
                                break;

                            case "mislife162":
                                fileName = _misLife16.CleanFile(doc, anonymizedSurName);
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
