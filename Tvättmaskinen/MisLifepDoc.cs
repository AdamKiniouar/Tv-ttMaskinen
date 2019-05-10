using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Tvättmaskinen
{
    class MisLifepDoc
    {
        public MisLifepDoc() { }

        public XmlDocument Wash(XmlDocument doc, int degree)
        {
                if (degree == 30 || degree == 90)
                {
                    XmlNodeList PensionIDList = doc.GetElementsByTagName("Pensionsdokument");
                    for (int i = 0; i < PensionIDList.Count; i++)
                    {
                        PensionIDList[i].Attributes["TxId"].Value = Guid.NewGuid().ToString(); //Pensionsdokument TxId till nytt guid
                    }
                }

                if (degree == 60 || degree == 90)
                {
                    XmlNodeList PensionList = doc.GetElementsByTagName("Individ");//Personnummer
                    for (int i = 0; i < PensionList.Count; i++)
                    {
                        string a = PensionList[i].Attributes["Personnummer"].Value.Remove(0, 10);
                        string b = a.Substring(0, 1);
                        int n = int.Parse(b);

                        if (n % 2 == 0)
                        {
                            PensionList[i].Attributes["Personnummer"].Value = PensionList[i].Attributes["Personnummer"].Value.Substring(0, PensionList[i].Attributes["Personnummer"].Value.Length - 6) + "014321";
                        }
                        else
                        {
                            PensionList[i].Attributes["Personnummer"].Value = PensionList[i].Attributes["Personnummer"].Value.Substring(0, PensionList[i].Attributes["Personnummer"].Value.Length - 6) + "011234";
                        }
                    }
                }
         return doc;
        }
    }
}

