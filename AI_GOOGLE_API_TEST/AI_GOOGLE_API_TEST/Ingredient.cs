using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOOGLE_API_TEST
{
    public class Ingredient
    {
        private int id;
        public string name;

        public Ingredient()
        {
            id = 0;
            name = "";
        }

        public Ingredient(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public void Display()
        {
            Console.WriteLine("----Id: " + id);
            Console.WriteLine("----Name: " + name);
        }
    }
}
