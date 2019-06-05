using System.Xml;

namespace Tvättmaskinen
{
    public interface IMisLife172
    {
        string CleanFile(XmlDocument doc, string anonymizedSurname);
    }
}
