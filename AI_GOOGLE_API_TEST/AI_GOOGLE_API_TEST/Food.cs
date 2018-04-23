using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOOGLE_API_TEST
{
    public class Food
    {
        private int id;
        public string Name;
        public string Ingredients;
        public string Instructions;

        public Food(int id)
        {
            this.id = id;
        }

        public Food(int id, string name, string ingredients, string instructions)
        {
            this.id = id;
            Name = name;
            Ingredients = ingredients;
            Instructions = instructions;
        }

        public void Display()
        {
            Console.WriteLine("id : " + id);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Ingredient: " + Ingredients);
            Console.WriteLine("Instructions: " + Instructions);
        }
    }
}
