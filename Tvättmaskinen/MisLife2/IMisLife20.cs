using System.Xml;

namespace Tvättmaskinen
{
    public interface IMisLife20 
    {
        string CleanFile(XmlDocument doc, string anonymizedFörnamn);
    }
}
