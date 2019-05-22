using System;

namespace Tvättmaskinen
{
    public class RandomFirstNameGenerator
    {      
        private string[] nameList = { "Adam", "Simon", "Karin", "Bengt", "Josef", "Anna", "Casper", "Linus", "Alexander", "Magnus", "Tommy", "Niclas", "Sabine", "Rolf", "Yngve", "Jesper", "Sara", "Jenny", "Maria", "Kalle" };
        public string name { get; set; }

        public RandomFirstNameGenerator()
        {
            int rand = new Random().Next(0, 19);
            name = nameList[rand];
        }
    }
}
