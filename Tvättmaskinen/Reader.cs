using System;
using System.IO;
using System.Xml;

namespace Tvättmaskinen
{
    class Reader
    {
        
        private readonly string fornamn = "Fornamn";//1.7.2
        private readonly string efternamn = "Efternamn";//1.7.2
        private readonly string personnummer = "Personnummer";//1.7.2

        private readonly string firstname = "firstname";//1.6.2
        private readonly string lastname = "lastname";//1.6.2
        private readonly string pno = "lastname";//1.6.2


        public Reader() { }

        public void Wash(string path, string savePath)
        {

            DirectoryInfo di = new DirectoryInfo(path);

            FileInfo[] files = di.GetFiles("*.xml");
            foreach (FileInfo file in files)
            {

            var doc = new XmlDocument();
            doc.Load(file.FullName);

            doc = Wash(doc);
            

            savePath = savePath + file.Name.Remove(file.Name.Length - 4, 4) + "-tvättad.xml";


                if (Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                doc.Save(savePath);
            }
        }
    

        private XmlDocument Wash(XmlDocument doc)
        {

            //var idList = doc.GetElementsByTagName(id);
            //foreach (XmlNode id in idList)
            //{
            //    id.InnerText = "HejhejID";
            //}

            //Förnamn 1.7.2
            var fornamnsList = doc.GetElementsByTagName(fornamn);
            foreach (XmlNode fornamn in fornamnsList)
            {
                fornamn.InnerText = "kalle";
            }

            //Efternamn 1.7.2
            var efternamnsList = doc.GetElementsByTagName(efternamn);
            foreach (XmlNode efternamn in efternamnsList)
            {
                efternamn.InnerText = "Andersson";
            }

            //Personnummer 1.7.2
            var personNummerList = doc.GetElementsByTagName(personnummer);
            foreach (XmlNode personNummer in personNummerList)
            {
                personNummer.InnerText = personNummer.InnerText.Remove(personNummer.InnerText.Length - 7, 7) + "01-1234";
            }

            //Id 1.7.2
            XmlNodeList elementList = doc.GetElementsByTagName("Forsakring");
            for (int i = 0; i < elementList.Count; i++)
            {
                elementList[i].Attributes["Id"].Value = "HejhejID";

            }


            //Förnamn 1.6.2
            var firstnamesList = doc.GetElementsByTagName(firstname);
            foreach (XmlNode firstname in firstnamesList)
            {
                firstname.InnerText = "Frida";
            }

            //Efternamn 1.6.2
            var lastnamesList = doc.GetElementsByTagName(lastname);
            foreach (XmlNode lastname in lastnamesList)
            {
                lastname.InnerText = "Karlsson";
            }

            ////Personnummer 1.6.2
            //var pnoList = doc.GetElementsByTagName(pno);
            //foreach (XmlNode pno in pnoList)
            //{
            //    doc.Attributes["pno"].Value = pno.InnerText = pno.InnerText.Remove(pno.InnerText.Length - 7, 7) + "01-1234";
            //    //pno.InnerText = pno.InnerText.Remove(pno.InnerText.Length - 7, 7) + "01-1234";
            //}

            return doc;
        }
    }
}