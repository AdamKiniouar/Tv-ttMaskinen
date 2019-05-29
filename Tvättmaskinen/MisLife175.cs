using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLife175 : IMisLife175
    {
        public string CleanFile(XmlDocument doc, string anonymizedSurname)
        {
            var fileName = "";
            var anonymizedLastname = "";

            var ForsakringsList = doc.GetElementsByTagName("ml:Forsakring");
            foreach (XmlNode forsakring in ForsakringsList)
            {
                anonymizedLastname = forsakring.Attributes["KollektivavtalKod"].Value;
            }

            var forsakringsList = doc.GetElementsByTagName("ml:Forsakring");
            foreach (XmlNode forsakringsiD in forsakringsList)
            {
                forsakringsiD.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var momentList = doc.GetElementsByTagName("ml:Moment");
            foreach (XmlNode momentsId in momentList)
            {
                momentsId.Attributes["Id"].Value = Guid.NewGuid().ToString();
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

            var personNummerList = doc.GetElementsByTagName("ml:Personnummer");
            foreach (XmlNode personNummer in personNummerList)
            {
                var lastButOneDigitInPersoalNumber = personNummer.InnerText[11];

                if (lastButOneDigitInPersoalNumber % 2 == 0)
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
