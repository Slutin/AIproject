using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AI_GOOGLE_API_TEST
{
    public class SeeFood
    {
        private const string V = "'";
        private MsqlConnector connector;
        private MySqlDataReader reader;
        
        private string cmd;

        public SeeFood()
        {
            connector = new MsqlConnector();
        }

        private Food GetRecipeFromOneIngredient(string ing_name)
        {
            cmd = "select * from recipe where ingredients like '%" + ing_name + "%'";
            try
            {
                reader = connector.ExecuteReader(cmd);
                reader.Read();
                Food retval = new Food(reader.GetInt32("id"));
                retval.Name = reader.GetString("name");
                retval.Instructions = reader.GetString("directions");
                retval.Ingredients = reader.GetString("ingredients");
                reader.Close();
                return retval;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine("NORMAL EXCEPTION");
                Console.WriteLine(ex);
            }

            return null;
        }

        public List<Food> GetRecipeListFromIngredientList(List<string> ingredients)
        {
            cmd = "select * from recipe where(";
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (i == ingredients.Count - 1)
                {
                    cmd += "ingredients like '%" + ingredients[i] + "%')";
                    break;
                }
                cmd += "ingredients like '%" + ingredients[i] + "%' and ";
            }
            try
            {
                reader = connector.ExecuteReader(cmd);
                List<Food> retval = new List<Food>();
                while (reader.Read())
                {
                    Food f = new Food(reader.GetInt32("id"));
                    f.Name = reader.GetString("name");
                    f.Instructions = reader.GetString("directions");
                    f.Ingredients = reader.GetString("ingredients");
                    retval.Add(f);
                }
                reader.Close();
                return retval;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public List<Food> GetRecipeListFromRequests(List<Request> requests)
        {
            foreach(Request req in requests)
            {
                req.Display();
            }
            cmd = "select * from recipe where";
            for (int i = 0; i < requests.Count; i++)
            {
                cmd += "(";
                for (int b = 0; b < requests[i].Ingredients.Count; b++)
                {
                    if (b == requests[i].Ingredients.Count - 1)
                    {
                        cmd += " ingredients like '%" + requests[i].Ingredients[b] + "%'";
                        
                        continue;
                    }
                    cmd += "ingredients like '%" + requests[i].Ingredients[b] + "%' or ";
                }
                if (i == requests.Count - 1)
                {
                    cmd += ")";
                    continue;
                }
                cmd += ") and ";
            }
            try
            {
                reader = connector.ExecuteReader(cmd);
                List<Food> retval = new List<Food>();
                while (reader.Read())
                {
                    Food f = new Food(reader.GetInt32("id"));
                    f.Name = reader.GetString("name");
                    f.Instructions = reader.GetString("directions");
                    f.Ingredients = reader.GetString("ingredients");
                    retval.Add(f);
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

        public void AddFoodToDatabase(Food recipe)
        {
            cmd = "insert into recipe values (0, '" +
                recipe.Name + "','" +
                recipe.Ingredients + "' , '" +
                recipe.Instructions + "')";
            try
            {
                connector.ExecuteCommand(cmd);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void PopulateDatabaseFromFile(string filename, int numberOfRecipesToRead)
        {
            int readSofar = 0;
            string text = System.IO.File.ReadAllText(filename);
            int ptr;
            int numToRead = numberOfRecipesToRead;
            string r_name;
            string r_ing;
            string r_directions;
            bool readToEnd = false;
            if (numberOfRecipesToRead == -1)
            {
                readToEnd = true;
                numToRead = 1;
            }
            //while (readSofar < numberOfRecipesToRead)
            //{
            //    // Get the name
            //    ptr = text.IndexOf(",", prev_ptr);
            //    r_name = text.Substring(prev_ptr, ptr - prev_ptr);
            //    prev_ptr = ptr + 1;
            //    Console.WriteLine("Recipe Name = " + r_name);

            //    // Get the ingeridients
            //    ptr = text.IndexOf(",", prev_ptr);
            //    r_ing = text.Substring(prev_ptr, ptr - prev_ptr);
            //    prev_ptr = (ptr + 1);
            //    Console.WriteLine("Recipe ingeridients = " + r_ing); 

            //    // Get directions
            //    ptr = text.IndexOf(",", 594);
            //    r_directions = text.Substring(prev_ptr, ptr - prev_ptr);
            //    prev_ptr = ptr + 1;
            //    Console.WriteLine("Recipe Directions = " + r_ing);

            //    // Discard the last one, we don't need it
            //    ptr = text.IndexOf("/n", prev_ptr);
            //    prev_ptr = ptr + 1;

            //    //AddFoodToDatabase(new Food(0, r_name, r_ing, r_directions));

            //    readSofar++;
            //}

            while (readSofar < numToRead)
            {
                // Get the name
                ptr = text.IndexOf("Food Network,");
                r_name = text.Substring(0, ptr).Trim();
                r_name = r_name.Replace("'", "");
                text = text.Substring(ptr + 13);
                //Console.WriteLine("Recipe Name = " + r_name);

                // Get the ingeridients
                ptr = text.IndexOf(",");
                r_ing = text.Substring(0, ptr).Trim();
                r_ing = r_ing.Replace("'", "");
                text = text.Substring(ptr + 1);
                //Console.WriteLine("Recipe ingeridients = " + r_ing);

                // Get directions
                ptr = text.IndexOf(",");
                r_directions = text.Substring(0, ptr).Trim();
                r_directions = r_directions.Replace("'", "");
                text = text.Substring(ptr + 1);
                //Console.WriteLine("Recipe Directions = " + r_directions);

                // Discard the last one, we don't need it
                ptr = text.IndexOf("/n/n");
                text = text.Substring(ptr + 4);

                if (text.Length < 1)
                {
                    break;
                }

                AddFoodToDatabase(new Food(0, r_name, r_ing, r_directions));

                readSofar++;
                if (readToEnd) numToRead++;
            }

            Console.WriteLine(readSofar + " fields added to the database");
        }

        private void Test_GetRecipeListFromIngredientList()
        {
            List<string> i = new List<string>();
            i.Add("salt");
            i.Add("cheese");
            List<Food> foods = GetRecipeListFromIngredientList(i);
            Console.WriteLine("Number of hits " + foods.Count);
            foreach(Food f in foods)
            {
                f.Display();
            }
        }

        private void Test_AddFoodToDatabasee()
        {
            Food r = new Food(0, "some random shit", "just do whatever", "apples grapes salt oregano");
            AddFoodToDatabase(r);
        }

        public void TEST()
        {
            //Test_GetRecipeListFromIngredientList();
            Test_AddFoodToDatabasee();
        }
    }
}
