using System;
using System.Xml;

namespace Tvättmaskinen
{
    public class MisLifepDoc
    {
        public string CleanFile(XmlDocument doc)
        {
            var fileName = "";

            var PensionIDList = doc.GetElementsByTagName("Pensionsdokument");
            foreach (XmlNode pID in PensionIDList)
            {
                pID.Attributes["TxId"].Value = Guid.NewGuid().ToString();
            }

            var IndividList = doc.GetElementsByTagName("Individ");
            foreach (XmlNode individList in IndividList)
            {
                var lastButOneDigitInPersoalNumber = individList.Attributes["Personnummer"].Value[10];

                if (lastButOneDigitInPersoalNumber % 2 == 0)
                {
                    fileName = individList.Attributes["Personnummer"].Value.Substring(0, individList.Attributes["Personnummer"].Value.Length - 6) + "014321";
                    individList.Attributes["Personnummer"].Value = fileName;
                }
                else
                {
                    fileName = individList.Attributes["Personnummer"].Value.Substring(0, individList.Attributes["Personnummer"].Value.Length - 6) + "011234";
                    individList.Attributes["Personnummer"].Value = fileName;
                }
            }

            return fileName;
        }
    }
}

