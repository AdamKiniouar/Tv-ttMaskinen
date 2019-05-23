using System;
using Microsoft.Extensions.DependencyInjection;

namespace Tvättmaskinen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var anonymizedSurName = "TestPerson";

            var file = @"C:\Users\Adam_\Desktop\MiP";
            var serviceProvider = ConfigureService();
            var sortering = serviceProvider.GetService<ISortering>();

            sortering.Sort(file, anonymizedSurName);


            Console.Read();
        }

        private static ServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection()
              .AddTransient<ISortering, Sortering>()
              .AddTransient<IMisLife16, MisLife16>()
              .AddTransient<IMisLife17, MisLife17>()
              .AddTransient<IMisLifepDoc, MisLifepDoc>()
         .BuildServiceProvider();

            return serviceCollection;
        }
    }
}