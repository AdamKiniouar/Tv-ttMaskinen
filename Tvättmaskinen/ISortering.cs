using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Tvättmaskinen
{
    public interface ISortering
    {
        List<IGrouping<string, FileInfo>> GroupFiles(FileInfo[] files);

        FileInfo[] GetAllXmlFiles(string Path);

        string SavePath(string path);

        void Sort(string filePath, string anonymizedSurName);

        void CleanAndSave(string personnummer, string anonymizedSurName, FileInfo[] files);
    }
}
