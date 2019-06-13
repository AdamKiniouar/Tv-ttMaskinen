﻿using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;

namespace Tvättmaskinen
{
    public class MisLife20 : IMisLife20 
    {
        public string CleanFile(XmlDocument doc, string anonymizedSurname)
        {
            //var firstchild = doc.ChildNodes[0].NamespaceURI;
            //var mgr = new XmlNamespaceManager(doc.NameTable);
            //mgr.AddNamespace("ml", firstchild);
            //var fornamn = doc.SelectNodes("//ml:fornamn", mgr);
            //foreach (XmlNode fornamnet in fornamn)
            //{
            //    fornamnet.InnerText = anonymizedSurname;
            //}

            var fileName = "";
            var anonymizedLastname = "";
            var testaaa = doc.FirstChild.Prefix;

            var organisationsNamnList = doc.GetElementsByTagName("mis20:organisationsnamn");
            foreach (XmlNode organisationsNamn in organisationsNamnList)
            {
                anonymizedLastname = organisationsNamn.InnerText;
            }

            var forsakringsNummerList = doc.GetElementsByTagName("mis20:forsakringsnummer");
            foreach (XmlNode forsakringsNummer in forsakringsNummerList)
            {
                forsakringsNummer.InnerText = Guid.NewGuid().ToString();
            }

            var fornamnsList = doc.GetElementsByTagName("mis20:fornamn");
            foreach (XmlNode fornamn in fornamnsList)
            {
                fornamn.InnerText = anonymizedSurname;
            }

            var fornamnssList = doc.GetElementsByTagName("fornamn");
            foreach (XmlNode fornamnn in fornamnssList)
            {
                fornamnn.InnerText = anonymizedSurname;
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