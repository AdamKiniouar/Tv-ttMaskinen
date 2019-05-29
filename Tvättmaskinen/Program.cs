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

            sortering.SavePath(file);
            sortering.Sort(file, anonymizedSurName);


            Console.Read();
        }

        private static ServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection()
              .AddTransient<ISortering, Sortering>()
              .AddTransient<IMisLife162, MisLife162>()
              .AddTransient<IMisLife172, MisLife172>()
              .AddTransient<IMisLifepDoc, MisLifepDoc>()
              .AddTransient<IMisLife171, MisLife171>()
              .AddTransient<IMisLife173, MisLife173>()
              .AddTransient<IMisLife174, MisLife174>()
              .AddTransient<IMisLife175, MisLife175>()
         .BuildServiceProvider();

            return serviceCollection;
        }
    }
}