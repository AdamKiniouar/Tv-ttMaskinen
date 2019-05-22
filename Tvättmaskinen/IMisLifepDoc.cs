using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Tvättmaskinen
{
    public interface IMisLifepDoc
    {
        string CleanFile(XmlDocument doc);
    }
}
