using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Tvättmaskinen
{
    class MisLife17
    {
        private readonly string personnummer = "Personnummer";//1.7.2

        public MisLife17() { }

        public XmlDocument Wash(XmlDocument doc, int degree, string förnamn, string efternamn)
        {
                if (degree == 30 || degree == 90)
                {
                    XmlNodeList idList = doc.GetElementsByTagName("Forsakring");
                    for (int i = 0; i < idList.Count; i++)
                    {
                        idList[i].Attributes["Id"].Value = Guid.NewGuid().ToString();//Försäkringd ID till nytt guid
                    }

                    XmlNodeList adminNRList = doc.GetElementsByTagName("Administration");
                    for (int i = 0; i < adminNRList.Count; i++)
                    {
                        adminNRList[i].Attributes["Forsakringsnummer"].Value = Guid.NewGuid().ToString();//Försäkringsnummer till nytt guid
                    }
            }

                if (degree == 40 || degree == 90)
                {
                    var fornamnsList = doc.GetElementsByTagName("Fornamn");
                    foreach (XmlNode fornamn in fornamnsList)
                    {
                        fornamn.InnerText = förnamn;
                    }

                    var efternamnsList = doc.GetElementsByTagName("Efternamn");
                    foreach (XmlNode efternamnet in efternamnsList)
                    {
                        efternamnet.InnerText = efternamn;
                    }
                }

                if (degree == 60 || degree == 90)
                {
                    var personNummerList = doc.GetElementsByTagName(personnummer);
                    foreach (XmlNode personNummer in personNummerList)
                    {
                            string a = personNummer.InnerText.Remove(0, 11);
                            string b = a.Substring(0, 1);
                            int n = int.Parse(b);

                            if (n % 2 == 0)
                            {
                             personNummer.InnerText = personNummer.InnerText.Remove(personNummer.InnerText.Length - 7, 7) + "01-4321";
                            }
                            else
                            {
                                personNummer.InnerText = personNummer.InnerText.Remove(personNummer.InnerText.Length - 7, 7) + "01-1234";
                            }

                       XmlNodeList ForsakringsNRList = doc.GetElementsByTagName("Administration");//Försäkringsnummer som också är ett personnummer får ett nytt guid
                       for (int i = 0; i < ForsakringsNRList.Count; i++)
                       {
                                ForsakringsNRList[i].Attributes["Forsakringsnummer"].Value = Guid.NewGuid().ToString();
                       }
                    }
                }
         return doc;
        }
    }
}

