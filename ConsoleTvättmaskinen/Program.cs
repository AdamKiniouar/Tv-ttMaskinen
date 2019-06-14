using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tvättmaskinen;

namespace ConsoleTvättmaskinen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Tvättmaskinen is booting up...");
            var anonymizedSurName = "";

            var filePath = "";

            if (args.Length != 2 || args.Where(argument => argument == "help").Any())
            {
                LogHelpText();
                return; 
            }
            
            foreach(var arg in args)
            {
                if (arg.ToLower().Contains("path"))
                {
                    filePath = arg.Split('=')[1];
                }

                if(arg.ToLower().Contains("name")){
                    anonymizedSurName = arg.Split('=')[1];
                }
            }

            var serviceProvider = ConfigureService();
            var sortering = serviceProvider.GetService<ISortering>();

            try
            {
                sortering.SavePath(filePath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            sortering.Sort(filePath, anonymizedSurName);

            Console.Read();
        }

        private static void LogHelpText()
        {
            Console.WriteLine("Args till tvätt");
            Console.WriteLine("path     sökväg till svarsfiler");
            Console.WriteLine("name     namn personer kommer döpas till");
            Console.WriteLine("");
            Console.WriteLine("Exempel: ConsoleTvättmaskinen.exe path=D:\\tvätt\\alecta namn=Adam");
        }

        private static ServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection()
              .AddTransient<ISortering, Sortering>()
              .AddTransient<IMisLife162, MisLife162>()
              .AddTransient<IMisLife171, MisLife171>()
              .AddTransient<IMisLife172, MisLife172>()              
              .AddTransient<IMisLife173, MisLife173>()
              .AddTransient<IMisLife174, MisLife174>()
              .AddTransient<IMisLife175, MisLife175>()
              .AddTransient<IMisLife176, MisLife176>()
              .AddTransient<IMisLife20, MisLife20>()
              .AddTransient<IMisLifepDoc, MisLifepDoc>()
            .BuildServiceProvider();

            return serviceCollection;
        }
    }
}