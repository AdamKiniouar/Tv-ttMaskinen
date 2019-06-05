using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLife171 : IMisLife171
    {
        public string CleanFile(XmlDocument doc, string anonymizedSurname)
        {
            var fileName = "";
            var anonymizedLastname = "";

            var anslutningsList = doc.GetElementsByTagName("ml:Anslutning");
            foreach (XmlNode anslutning in anslutningsList)
            {
                anonymizedLastname = anslutning.Attributes["AvtalKod"].Value;
            }

            var anslutningsIdList = doc.GetElementsByTagName("ml:Anslutning");
            foreach (XmlNode anslutningsId in anslutningsIdList)
            {
                anslutningsId.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var momentList = doc.GetElementsByTagName("ml:Moment");
            foreach (XmlNode momentId in momentList)
            {
                momentId.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var fornamnsList = doc.GetElementsByTagName("ml:Fornamn");
            foreach (XmlNode fornamn in fornamnsList)
            {
                fornamn.InnerText = anonymizedSurname;
            }

            var efternamnsList = doc.GetElementsByTagName("ml:Efternamn");
            foreach (XmlNode Efternamn in efternamnsList)
            {
                Efternamn.InnerText = anonymizedLastname;
            }

            var personNummerList = doc.GetElementsByTagName("ml:Personnummer");
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
