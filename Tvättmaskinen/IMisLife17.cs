using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Tvättmaskinen
{
    public interface IMisLife17
    {
        string CleanFile(XmlDocument doc, string anonymizedSurname);
    }
}
