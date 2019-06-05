using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLife172 : IMisLife172
    {
        public string CleanFile(XmlDocument doc, string anonymizedSurname)
        {
            var fileName = "";
            var anonymizedLastname = "";

            var ForsakringsList = doc.GetElementsByTagName("Forsakring");
            foreach (XmlNode forsakring in ForsakringsList)
            {
                    anonymizedLastname = forsakring.Attributes["ProduktKod"].Value;
            }

            var forsakringsList = doc.GetElementsByTagName("Forsakring");
            foreach (XmlNode forsakringsiD in forsakringsList)
            {
                forsakringsiD.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var administrationsList = doc.GetElementsByTagName("Administration");
            foreach (XmlNode administrationsId in administrationsList)
            {
                administrationsId.Attributes["Forsakringsnummer"].Value = Guid.NewGuid().ToString();
            }

            var momentList = doc.GetElementsByTagName("Moment");
            foreach (XmlNode momentId in momentList)
            {
                momentId.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var fornamnsList = doc.GetElementsByTagName("Fornamn");
            foreach (XmlNode fornamn in fornamnsList)
            {
                fornamn.InnerText = anonymizedSurname;
            }

            var efternamnsList = doc.GetElementsByTagName("Efternamn");
            foreach (XmlNode Efternamn in efternamnsList)
            {
                Efternamn.InnerText = anonymizedLastname;
            }

            var personNummerList = doc.GetElementsByTagName("Personnummer");
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

