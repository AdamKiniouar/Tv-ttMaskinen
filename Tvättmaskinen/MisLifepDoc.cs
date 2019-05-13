using System;
using System.Xml;

namespace Tvättmaskinen
{
    class MisLifepDoc
    {
        public MisLifepDoc() { }

        public XmlDocument Wash(XmlDocument doc)
        {

            XmlNodeList PensionIDList = doc.GetElementsByTagName("Pensionsdokument");
            foreach (XmlNode pID in PensionIDList)
            {
                pID.Attributes["TxId"].Value = Guid.NewGuid().ToString(); //Pensionsdokument TxId till nytt guid
            }
                

            XmlNodeList PensionList = doc.GetElementsByTagName("Individ");//Personnummer
            foreach (XmlNode pList in PensionList)
            {
                string a = pList.Attributes["Personnummer"].Value.Remove(0, 10);
                string b = a.Substring(0, 1);
                int n = int.Parse(b);

                if (n % 2 == 0)
                {
                    pList.Attributes["Personnummer"].Value = pList.Attributes["Personnummer"].Value.Substring(0, pList.Attributes["Personnummer"].Value.Length - 6) + "014321";
                }
                else
                {
                    pList.Attributes["Personnummer"].Value = pList.Attributes["Personnummer"].Value.Substring(0, pList.Attributes["Personnummer"].Value.Length - 6) + "011234";
                }
            }
                
         return doc;
        }
    }
}

