using Google.Cloud.Vision.V1;
using System;
using System.Windows.Forms;
//using Elasticsearch.Net;
using Nest;
using System.Collections.Generic;

namespace AI_GOOGLE_API_TEST
{
    public partial class Form1 : Form
    {
        FoodDetector fd = new FoodDetector();
        List<Food> results = new List<Food>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            addIngredient(openFileDialog1.FileName);
            LstIngredients.Items.Clear();
            foreach(Request r in fd.GetRequests())
            {
                foreach(string s in r.Ingredients)
                {
                    LstIngredients.Items.Add(s);
                }
            }
        }

        private void addIngredient(string filename)
        {
            var image = Image.FromFile(filename);
            fd.AddIngredient(image);
        }

        private void detectImage(string filePath)
        {
            var image = Image.FromFile(filePath);
            FoodDetector fd = new FoodDetector();
            //var response = fd.GetRecipesFromImage(image);
            var response = fd.DetectImage(image);
            //foreach(Food s in response)
            //{
            //    textBox1.Text += s.Name;
            //}
        }

        //private void connectToDB()
        //{
        //    try
        //    {
        //        var conn = new ConnectionConfiguration(new Uri("https://56641ecc747adc2063fe8577b8a09d3b.us-east-1.aws.found.io:9243/recipes?pretty -u elastic:z0nqsrlDbJKN5VhQTwXKGq1"));
        //        var client = new ElasticLowLevelClient(conn);

                
        //        TR result = client.Search<TR>("apple");
        //        var response = result.Response;
        //        Console.WriteLine("response is: " + result.title);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}

        void conToDB()
        {
            var node = new Uri("https://elastic:Z0nqsrlDbJKN5VhQTwXKGq1@56641ecc747adc2063fe8577b8a09d3b.us-east-1.aws.found.io:9243/recipes?pretty");
            //var settings = new ConnectionSettings(node,
            //var client = new ElasticClient(node);
            var connectionSet = new ConnectionSettings(node).DefaultIndex("recipes");
                
            var client = new ElasticClient(connectionSet);
            PingResponse pr = (PingResponse) client.Ping();
            Console.WriteLine(pr.ToString());
            /*var searchResults = client.Search<Recipe>(s => s
            .From(0)
            .Size(10)
            .Query(q => q
                .Term(p => p.ing, "beef"));*/

            //var searchResults = client.Get<Recipe>(1);
            var sr = client.Search<Recipe>(s => s
            .AllIndices());
            Console.WriteLine(sr.Hits.ToString());
            Console.WriteLine(sr.Fields.Count);
            //Console.WriteLine("hi " + searchResults.Fields.ToString());
            //foreach (var fv in searchResults.Fields)
            //{
            //    Console.WriteLine(fv.ToString());
            //}
            ////Console.WriteLine( searchResults.Documents.ToString());

            
        }

        void getRecipe()
        {
            Foodi foodi = new Foodi();
            SeeFood seeFood = new SeeFood();
            //Recipe recipe = foodi.getRecipeByName("lasagna");
            //recipe.Display();
            //foodi.test_getIngredientIdsFromNames();
            //foodi.test_getRecipeIdFromIngredient(6);
            //foodi.Test_GetRecipeFromId(1);
            //foodi.Test_GetRecipeFromIngredients();
            //foodi.Test_containsIngredients();
            //foodi.Test_GetRecipeFromIngredients();
            seeFood.TEST();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fd.NewRequest();
            LstIngredients.Items.Clear();
            LstRecipes.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            results = fd.Search();
            LstRecipes.Items.Clear();
            if (results.Count == 0)
            {
                Console.WriteLine("No recipes with the specified ingredients");
            }
            foreach(Food f in results)
            {
                LstRecipes.Items.Add(f.Name);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fd.PopulateDatabaseFromFile(openFileDialog1.FileName, 2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fd.PopulateDatabaseFromFile(openFileDialog1.FileName, -1);
        }

        private void LstRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstRecipes.SelectedIndex == -1) return;
            FrmRecipe r = new FrmRecipe(results[LstRecipes.SelectedIndex]);
            r.Show();
        }
    }
}
