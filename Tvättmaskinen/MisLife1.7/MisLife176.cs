﻿using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLife176 : IMisLife176
    {
        public string CleanFile(XmlDocument doc, string anonymizedSurname)
        {
            var fileName = "";
            var anonymizedLastname = "";

            var forsakringList = doc.GetElementsByTagName("ml:Forsakring");
            foreach (XmlNode forsakring in forsakringList)
            {
                anonymizedLastname = forsakring.Attributes["KollektivavtalKod"].Value;
            }

            var forsakringIdList = doc.GetElementsByTagName("ml:Forsakring");
            foreach (XmlNode forsakringiD in forsakringIdList)
            {
                forsakringiD.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var momentList = doc.GetElementsByTagName("ml:Moment");
            foreach (XmlNode momentId in momentList)
            {
                momentId.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var fornamnList = doc.GetElementsByTagName("Fornamn");
            foreach (XmlNode fornamn in fornamnList)
            {
                fornamn.InnerText = anonymizedSurname;
            }

            var efternamnList = doc.GetElementsByTagName("Efternamn");
            foreach (XmlNode Efternamn in efternamnList)
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
