using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLife16 : IMisLife16
    {
        public string CleanFile(XmlDocument doc, string anonymizedSurname)
        {
            var fileName = "";
            var anonymizedLastname = "";

            var indicatorList = doc.GetElementsByTagName("indicator");
            foreach (XmlNode indicator in indicatorList)
            {
                if (indicator.Attributes["itype"].Value == "PKMP")
                {
                    anonymizedLastname = indicator.Attributes["ind"].Value;
                }
            }

            var numberList = doc.GetElementsByTagName("number");
            foreach (XmlNode numberPin in numberList)
            {
                if (numberPin.Attributes["ntype"].Value == "PIN")
                {
                    numberPin.InnerText = Guid.NewGuid().ToString();
                }
            }

            var firstnameList = doc.GetElementsByTagName("firstname");
            foreach (XmlNode firstname in firstnameList)
            {
                firstname.InnerText = anonymizedSurname;
            }

            var lastnameList = doc.GetElementsByTagName("lastname");
            foreach (XmlNode lastname in lastnameList)
            {
                lastname.InnerText = anonymizedLastname;
            }

            var streetpobList = doc.GetElementsByTagName("streetpob");
            foreach (XmlNode streetname in streetpobList)
            {
                streetname.InnerText = "";
            }

            var partyList = doc.GetElementsByTagName("party");
            foreach (XmlNode party in partyList)
            {
                if (party.Attributes["ptype"].Value == "IP" || party.Attributes["ptype"].Value == "IN")
                {
                    var lastButOneDigitInPersoalNumber = party.Attributes["pno"].Value[11];

                    if (lastButOneDigitInPersoalNumber % 2 == 0)
                    {
                        fileName = party.Attributes["pno"].Value.Substring(0, party.Attributes["pno"].Value.Length - 7) + "01-4321";
                        party.Attributes["pno"].Value = fileName;
                    }
                    else
                    {
                        fileName = party.Attributes["pno"].Value.Substring(0, party.Attributes["pno"].Value.Length - 7) + "01-1234";
                        party.Attributes["pno"].Value = fileName;
                    }
                }
            }
            return fileName;
        }
    }
}

