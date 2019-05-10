using System;
using System.Xml;
using System.IO;
   

namespace Tvättmaskinen
{
    class Program
    {
        static void Main(string[] args)
        {

            var reader = new Reader();

            var file = @"C:\Users\Adam_\Desktop\MiP";
            var fileSave = @"C:\Users\Adam_\Desktop\MiP\172\";

            reader.Wash(file, fileSave);


            Console.Read();

        }
    }
}