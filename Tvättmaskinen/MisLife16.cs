using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Tvättmaskinen
{
    class MisLife16
    {
        public MisLife16() { }

        public XmlDocument Wash(XmlDocument doc, int degree, string förnamn, string efternamn)
        {
                if (degree == 30 || degree == 90)
                {
                    var pinList = doc.GetElementsByTagName("number");
                    foreach (XmlNode pin in pinList)
                    {
                        pin.InnerText = Guid.NewGuid().ToString(); //Pin till nytt guid
                    }
                }

                if (degree == 40 || degree == 90)
                {
                    var firstnamesList = doc.GetElementsByTagName("firstname");
                    foreach (XmlNode firstname in firstnamesList)
                    {
                        firstname.InnerText = förnamn;
                }
                    var lastnamesList = doc.GetElementsByTagName("lastname");
                    foreach (XmlNode lastname in lastnamesList)
                    {
                        lastname.InnerText = efternamn;
                    }
                }

                if (degree == 60 || degree == 90)
                {                   
                    XmlNodeList pnoList = doc.GetElementsByTagName("party");
                    for (int i = 0; i < pnoList.Count; i++)
                    {
                        if (pnoList[i].Attributes["ptype"].Value == "IP")//Personnummer
                        {
                            string a = pnoList[i].Attributes["pno"].Value.Remove(0, 11);
                            string b = a.Substring(0, 1);
                            int n = int.Parse(b);

                            if (n % 2 == 0)
                            {
                                pnoList[i].Attributes["pno"].Value = pnoList[i].Attributes["pno"].Value.Substring(0, pnoList[i].Attributes["pno"].Value.Length - 7) + "01-4321";
                            }
                            else
                            {
                                pnoList[i].Attributes["pno"].Value = pnoList[i].Attributes["pno"].Value.Substring(0, pnoList[i].Attributes["pno"].Value.Length - 7) + "01-1234";
                            }
                        }
                        if (pnoList[i].Attributes["ptype"].Value == "IN")//Personnummer
                        {
                            string a = pnoList[i].Attributes["pno"].Value.Remove(0, 11);
                            string b = a.Substring(0, 1);
                            int n = int.Parse(b);

                            if (n % 2 == 0)
                            {
                                pnoList[i].Attributes["pno"].Value = pnoList[i].Attributes["pno"].Value.Substring(0, pnoList[i].Attributes["pno"].Value.Length - 7) + "01-4321";
                            }
                            else
                            {
                                pnoList[i].Attributes["pno"].Value = pnoList[i].Attributes["pno"].Value.Substring(0, pnoList[i].Attributes["pno"].Value.Length - 7) + "01-1234";
                            }
                        }
                }
                }
         return doc;
        }
    }
}   

