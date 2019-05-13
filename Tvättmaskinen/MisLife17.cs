using System;
using System.Xml;

namespace Tvättmaskinen
{
    class MisLife17
    {
        private readonly string personnummer = "Personnummer";//1.7.2

        public MisLife17() { }

        public XmlDocument Wash(XmlDocument doc, string förnamn, string efternamn)
        {
            XmlNodeList idList = doc.GetElementsByTagName("Forsakring");
            foreach (XmlNode iD in idList)
            {
                iD.Attributes["Id"].Value = Guid.NewGuid().ToString();//Försäkringd ID till nytt guid
            }

            XmlNodeList forNRList = doc.GetElementsByTagName("Administration");
            foreach (XmlNode forsakringsNR in forNRList)
            {
                forsakringsNR.Attributes["Forsakringsnummer"].Value = Guid.NewGuid().ToString();//Försäkringsnummer till nytt guid
            }
            

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
                foreach (XmlNode forsakringsNr in ForsakringsNRList)
                {
                        forsakringsNr.Attributes["Forsakringsnummer"].Value = Guid.NewGuid().ToString();
                }
            }
                
         return doc;
        }
    }
}

