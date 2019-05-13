using System;
using System.Xml;

namespace Tvättmaskinen
{
    class MisLife16
    {
        public MisLife16() { }

        public XmlDocument Wash(XmlDocument doc, string förnamn, string efternamn)
        {
            // get its parent node
            XmlNode node = doc.SelectSingleNode("/party[name = 'address']");

            // remove the child node
            node.ParentNode.RemoveChild(node);

            var pinList = doc.GetElementsByTagName("number");
            foreach (XmlNode pin in pinList)
            {
                pin.InnerText = Guid.NewGuid().ToString(); //Pin till nytt guid
            }            

            var firstnamesList = doc.GetElementsByTagName("firstname");
            foreach (XmlNode firstname in firstnamesList)
            {
                firstname.InnerText = förnamn;
            }
            var lastnamesList = doc.GetElementsByTagName("lastname");
            foreach (XmlNode lastname in lastnamesList)
            {
                lastname.InnerText = efternamn;
            }

            var streetList = doc.GetElementsByTagName("streetpob");
            foreach (XmlNode streetname in streetList)
            {
                streetname.InnerText = "";
            }

            XmlNodeList personNummerList = doc.GetElementsByTagName("party");
            foreach (XmlNode personNummer in personNummerList)
            {
                if (personNummer.Attributes["ptype"].Value == "IP" || personNummer.Attributes["ptype"].Value == "IN")//Personnummer
                {
                    string a = personNummer.Attributes["pno"].Value.Remove(0, 11);
                    string b = a.Substring(0, 1);
                    int n = int.Parse(b);

                    if (n % 2 == 0)
                    {
                        personNummer.Attributes["pno"].Value = personNummer.Attributes["pno"].Value.Substring(0, personNummer.Attributes["pno"].Value.Length - 7) + "01-4321";
                    }
                    else
                    {
                        personNummer.Attributes["pno"].Value = personNummer.Attributes["pno"].Value.Substring(0, personNummer.Attributes["pno"].Value.Length - 7) + "01-1234";
                    }
                }               
            }

            return doc;
        }
    }
}

