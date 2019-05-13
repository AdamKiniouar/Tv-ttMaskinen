using System;
using System.Xml;
using System.IO;
   

namespace Tvättmaskinen
{
    class Program
    {
        static void Main(string[] args)
        {

            string förnamn = "Standard Förnamn";
            string efternamn = "Standard Efternamn";

            var sortera = new Sortering();

            var file = @"C:\Users\Adam_\Desktop\MiP";
            var fileSave = @"C:\Users\Adam_\Desktop\MiP\Tvättad\";

            sortera.Sort(file, fileSave, förnamn, efternamn);


            Console.Read();

        }
    }
}