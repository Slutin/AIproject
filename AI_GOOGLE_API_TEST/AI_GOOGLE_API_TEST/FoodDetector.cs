using System;
using System.Collections.Generic;
using Google.Cloud.Vision.V1;

namespace AI_GOOGLE_API_TEST
{
    public class FoodDetector
    {
        SeeFood seeFood;
        List<Request> requests = new List<Request>();

        public FoodDetector()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "AiG5-016bd8dd3313.json");
            seeFood = new SeeFood();
        }

        public void AddIngredient(Image image)
        {
            Request ingredients = new Request();
            var response = DetectImage(image);
            foreach (var annotation in response)
            {
                Console.WriteLine(annotation.Score * 100);
                if (annotation.Score * 100 < 80) continue;
                ingredients.AddIngredient(annotation.Description);
            }
            requests.Add(ingredients);
        }

        public List<Food> Search()
        {
            return seeFood.GetRecipeListFromRequests(requests);
        }

        public IReadOnlyList<EntityAnnotation> DetectImage(Image image)
        {
            var client = ImageAnnotatorClient.Create();
            return client.DetectLabels(image);
        }

        public void PopulateDatabaseFromFile(string filename, int numberOfFieldToRead)
        {
            seeFood.PopulateDatabaseFromFile(filename, numberOfFieldToRead);
        }

        public List<Request> GetRequests()
        {
            return requests;
        }

        public void NewRequest()
        {
            requests = new List<Request>();
        }

        public void TEST()
        {
            seeFood.TEST();
        }
    }
}
