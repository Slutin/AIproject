using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOOGLE_API_TEST
{
    public class Request
    {
        public List<string> Ingredients;

        public Request()
        {
            Ingredients = new List<string>();
        }

        public Request(List<string> ingredients)
        {
            Ingredients = ingredients;
        }

        public void AddIngredient(string name)
        {
            Ingredients.Add(name);
        }

        public void Display()
        {
            foreach(string s in Ingredients)
            {
                Console.WriteLine("Request ingredient: " + s);
            }
        }
    }
}
