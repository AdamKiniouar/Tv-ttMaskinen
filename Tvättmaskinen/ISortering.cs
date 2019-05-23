using System.IO;

namespace Tvättmaskinen
{
    public interface ISortering
    {
        void Sort(string filePath, string anonymizedSurName);

        void CleanAndSave(string personnummer, string anonymizedSurName, FileInfo[] files);
    }
}
