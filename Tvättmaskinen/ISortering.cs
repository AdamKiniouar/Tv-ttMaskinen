using System.IO;

namespace Tvättmaskinen
{
    public interface ISortering
    {
        void Sort(string filePath);

        void CleanAndSave(string personnummer, FileInfo[] files);
    }
}
