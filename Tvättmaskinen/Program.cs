using System;

namespace Tvättmaskinen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string anonymizedSurname = "Standard Förnamn";
            string anonymizedLastname = "Standard Efternamn";

            var sortera = new Sortering();

            var file = @"C:\Users\Adam_\Desktop\MiP";

            sortera.Sort(file, anonymizedSurname, anonymizedLastname);

            Console.Read();
        }
    }
}