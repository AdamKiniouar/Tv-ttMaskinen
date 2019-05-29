using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Tvättmaskinen
{
    public interface IMisLife162
    {
        string CleanFile(XmlDocument doc, string anonymizedSurname);
    }
}
