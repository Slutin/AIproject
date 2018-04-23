using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AI_GOOGLE_API_TEST
{
    [Obsolete]
    public class Foodi
    {
        private MsqlConnector connector;
        private MySqlDataReader reader;
        private string cmd;

        public Foodi()
        {
            connector = new MsqlConnector();
            Console.WriteLine(connector.IsConnected());
        }

        public Foodi(MsqlConnector connector)
        {
            this.connector = connector;
        }

        public Recipe GetRecipeByName(string name)
        {
            cmd = "select * from recipe where name = '" + name + "'";
            try
            {
                reader = connector.ExecuteReader(cmd);
                if (reader == null)
                {
                    Console.WriteLine("could not read");
                    return null;
                }
                reader.Read();
                Recipe retval = new Recipe((int)reader["id"]);
                retval.name = reader["name"].ToString();
                reader.Close();
                // Get all the ingredients
                retval.ingredients = GetIngredients(retval.GetId());
                return retval;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public List<Recipe> GetRecipeFromIngredients(List<string> ingredientNames)
        {
            List<int> recipeIds = new List<int>();
            List<int> ingredientIds = GetIngredientIdsFromNames(ingredientNames);
            List<Recipe> recipes = new List<Recipe>();
            foreach(int i in ingredientIds)
            {
                recipeIds.AddRange(GetAllRecipeIdFromIngredient(i)); // Adds all the recipes for all the ingredients
            }
            recipeIds = recipeIds.Distinct<int>().ToList();
            foreach(int i in recipeIds)
            {
                recipes.Add(GetRecipeFromId(i));
            }
            List<Recipe> hits = CheckRecipesForIngredients(recipes, ingredientNames);
            return hits;
        }

        private List<Recipe> CheckRecipesForIngredients(List<Recipe> recipes, List<string> ingredients)
        {
            List<Recipe> retval = new List<Recipe>();
            foreach(Recipe r in recipes)
            {
                if (containsIngredients(r, ingredients))
                {
                    retval.Add(r);
                }
            }

            return retval;
        }

        private bool containsIngredients(Recipe recipe, List<string> ingredients)
        {
            int cNum = 0;
            foreach(Ingredient ing in recipe.ingredients)
            {
                foreach(string s in ingredients)
                {
                    if (ing.name == s)
                    {
                        cNum++;
                    }
                }
            }
            return !(cNum < ingredients.Count);
        }

        private Recipe GetRecipeFromId(int id)
        {
            Recipe retval = new Recipe(id);
            cmd = "select * from recipe where id = " + id;
            try
            {
                reader = connector.ExecuteReader(cmd);
                reader.Read();
                retval.name = reader.GetString("name");
                retval.directions = reader.GetString("directions");
                reader.Close();
                retval.ingredients = GetIngredients(id);
                return retval;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        private List<Ingredient> GetIngredients(int recipeId)
        {
            List<Ingredient> retval = new List<Ingredient>();
            cmd = "SELECT * FROM ingredient I INNER JOIN recipe_ingeridients F2I ON I.id = F2I.ingeridient_id WHERE " + 
            "F2I.recipe_id =" +  recipeId ;
            try
            {
                reader = connector.ExecuteReader(cmd);
                while (reader.Read())
                {
                    Ingredient i = new Ingredient((int)reader["id"], reader.GetString("name"));
                    retval.Add(i);
                }
                reader.Close();
                return retval;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        private List<int> GetIngredientIdsFromNames(List<string> ingredientNames)
        {
            cmd = "select ingredient.id " +
                "from ingredient " +
                "where ";
            bool firstOne = true;
            foreach (string name in ingredientNames)
            {
                if (firstOne)
                {
                    firstOne = false;
                    cmd += " name = '" + name + "' ";
                }
                else
                {
                    cmd += " or" + " name = '" + name + "'";
                }
            }
            Console.WriteLine("****** CMD = " + cmd);
            try
            {
                reader = connector.ExecuteReader(cmd);
                List<int> retval = new List<int>();
                while (reader.Read())
                {
                    retval.Add(reader.GetInt32("id"));
                }
                reader.Close();
                return retval;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        private List<int> GetAllRecipeIdFromIngredient(int ingredientId)
        {
            cmd = "SELECT recipe_id FROM ingredient I INNER JOIN recipe_ingeridients F2I ON I.id = F2I.ingeridient_id WHERE " +
            "F2I.ingeridient_id = '" + ingredientId + "'";
            List<int> retval = new List<int>();
            try
            {
                reader = connector.ExecuteReader(cmd);
                while (reader.Read())
                {
                    retval.Add(reader.GetInt32(0));
                }
                reader.Close();
                return retval;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public void Test_getIngredientIdsFromNames()
        {
            List<string> names = new List<string>();
            names.Add("peanut butter");
            names.Add("salt");
            List<int> ids = GetIngredientIdsFromNames(names);
            foreach (int id in ids)
            {
                Console.WriteLine("Id = " + id);
            }
        }

        public void Test_getRecipeIdFromIngredient(int ingredientName)
        {
            List<int> RIDs = new List<int>();
            RIDs = GetAllRecipeIdFromIngredient(ingredientName);
            foreach(int id in RIDs)
            {
                Console.WriteLine("RID = " + id);
            }
        }

        public void Test_GetRecipeFromIngredients()
        {
            List<string> ingNames = new List<string>();
            ingNames.Add("salt");
            ingNames.Add("cheese");
            ingNames.Add("pepper");
            List<Recipe> hits = GetRecipeFromIngredients(ingNames);
            Console.WriteLine("Number of hits: " + hits.Count);
            foreach(Recipe hit in hits)
            {
                hit.Display();
            }
        }

        public void Test_GetRecipeFromId(int id)
        {
            Recipe r = GetRecipeFromId(id);
            r.Display();
        }

        public void Test_containsIngredients()
        {
            List<string> ing = new List<string>();
            List<Ingredient> ingList = new List<Ingredient>();
            ingList.Add(new Ingredient(0, "jelly"));
            ingList.Add(new Ingredient(0, "salt"));
            ing.Add("jelly");
            ing.Add("salt");
            ing.Add("pepper");
            Recipe r = new Recipe(0, "name", "directions", ingList);
            Console.WriteLine(containsIngredients(r, ing));
        }
    }
}
