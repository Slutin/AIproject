using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOOGLE_API_TEST
{
    public class Recipe
    {
        private int id;
        public string name;
        public string directions;
        public List<Ingredient> ingredients;

        public Recipe()
        {
            name = "";
            directions = "";
            ingredients = new List<Ingredient>();
        }

        public Recipe(int id)
        {
            this.id = id;
            ingredients = new List<Ingredient>();
        }

        public Recipe(int id, string name, string directions, List<Ingredient> ingeridients)
        {
            this.id = id;
            this.name = name;
            this.directions = directions;
            this.ingredients = ingeridients;
        }

        public int GetId() { return id; }

        public void AddIngredient(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
        }

        public void Display()
        {
            Console.WriteLine("Id: " + id);
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Directions: " + directions);
            Console.WriteLine("Ingredients:");
            foreach(Ingredient i in ingredients)
            {
                i.Display();
            }
        }
    }
}
