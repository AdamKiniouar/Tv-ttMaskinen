﻿using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLife171 : IMisLife171
    {
        public string CleanFile(XmlDocument doc, string anonymizedFörnamn)
        {
            var fileName = "";
            var anonymizedEfternamn = "";

            var anslutningList = doc.GetElementsByTagName("ml:Anslutning");
            foreach (XmlNode anslutning in anslutningList)
            {
                anonymizedEfternamn = anslutning.Attributes["AvtalKod"].Value;
            }

            foreach (XmlNode anslutningId in anslutningList)
            {
                anslutningId.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var momentList = doc.GetElementsByTagName("ml:Moment");
            foreach (XmlNode momentId in momentList)
            {
                momentId.Attributes["Id"].Value = Guid.NewGuid().ToString();
            }

            var förnamnList = doc.GetElementsByTagName("ml:Fornamn");
            foreach (XmlNode förnamn in förnamnList)
            {
                förnamn.InnerText = anonymizedFörnamn;
            }

            var efternamnList = doc.GetElementsByTagName("ml:Efternamn");
            foreach (XmlNode Efternamn in efternamnList)
            {
                Efternamn.InnerText = anonymizedEfternamn;
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
