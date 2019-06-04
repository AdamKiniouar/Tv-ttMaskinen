using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLife20 : IMisLife20 
    {
        public string CleanFile(XmlDocument doc, string anonymizedSurname)
        {
            var fileName = "";
            var anonymizedLastname = "";

            var organisationsNamnList = doc.GetElementsByTagName("mis20:organisationsnamn");
            foreach (XmlNode organisationsNamn in organisationsNamnList)
            {
                anonymizedLastname = organisationsNamn.InnerText;
            }

            var forsakringsNummerList = doc.GetElementsByTagName("mis20: forsakringsnummer");
            foreach (XmlNode forsakringsNummer in forsakringsNummerList)
            {
                forsakringsNummer.InnerText = Guid.NewGuid().ToString();
            }      

            var fornamnsList = doc.GetElementsByTagName("mis20:fornamn");
            foreach (XmlNode fornamn in fornamnsList)
            {
                fornamn.InnerText = anonymizedSurname;
            }

            var efternamnsList = doc.GetElementsByTagName("mis20:efternamn");
            foreach (XmlNode Efternamn in efternamnsList)
            {
                Efternamn.InnerText = anonymizedLastname;
            }

            var personNummerList = doc.GetElementsByTagName("mis20:personnummer");
            foreach (XmlNode personNummer in personNummerList)
            {
                var lastButOneDigitInPersonalNumber = personNummer.InnerText[11];

                if (lastButOneDigitInPersonalNumber % 2 == 0)
                {
                    fileName = personNummer.InnerText.Remove(personNummer.InnerText.Length - 7, 7) + "01-4321";
                    personNummer.InnerText = fileName;
                }
                else
                {
                    fileName = personNummer.InnerText.Remove(personNummer.InnerText.Length - 7, 7) + "01-1234";
                    personNummer.InnerText = fileName;
                }
            }
            return fileName;
        }
    }
}
