using Microsoft.Extensions.DependencyInjection;
using System;
using Tvättmaskinen;

namespace ConsoleTvättmaskinen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Tvättmaskinen is booting up...");
            var anonymizedSurName = "TestPerson";

            var file = @"C:\Users\Adam_\Desktop\MiP";
            var serviceProvider = ConfigureService();
            var sortering = serviceProvider.GetService<ISortering>();

            sortering.SavePath(file);
            sortering.Sort(file, anonymizedSurName);

            Console.Read();
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