using Android.Text.Method;
using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace App.Service
{
    public class Request
    {
        public static List<Recipe> RecipesGetFromApi()
        {
            var list = new List<RecipeJson>();
            var request = WebRequest.CreateHttp("https://github.com/adrianosferreira/afrodite.json/raw/master/afrodite.json");

            request.Method = "GET";
            request.UserAgent = "recipeApp";

            try
            {
                using (var response = request.GetResponse())
                {
                    var data = response.GetResponseStream();

                    StreamReader streamReader = new StreamReader(data);
                    object objectResponse = streamReader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<RecipeJson>>(objectResponse.ToString());
                    data.Close();
                    response.Close();
                }

                return ParseRecipeJson(list);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async static Task<List<RecipeJson>> GetRecipeApiAsync()
        {

            var json = new List<RecipeJson>();
            string url = "https://github.com/adrianosferreira/afrodite.json/raw/master/afrodite.json";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    json = JsonConvert.DeserializeObject<List<RecipeJson>>(await response.Content.ReadAsStringAsync());
                }
            }

            return json;
        }

        private static List<Recipe> ParseRecipeJson(List<RecipeJson> json)
        {
            var recipes = new List<Recipe>();

            foreach (var item in json)
            {
                var recipe = new Recipe();

                recipe.Id = item.Id.ObjectIdValue;
                recipe.Name = item.Name;

                foreach (var sessionItem in item.Session)
                {
                    if (sessionItem.Name.Trim() == "Ingredientes")
                    {
                        foreach (var contentItem in sessionItem.Content)
                        {
                            var ingredientItem = new Ingredient();

                            ingredientItem.RecipeId = item.Id.ObjectIdValue;
                            ingredientItem.Description = contentItem.Trim();
                            recipe.Ingredient.Add(ingredientItem);
                        }
                    }

                    if (sessionItem.Name.Trim() == "Modo de Preparo")
                    {
                        foreach (var contentItem in sessionItem.Content)
                        {
                            var preparationModeItem = new PreparationMode();

                            preparationModeItem.RecipeId = item.Id.ObjectIdValue;
                            preparationModeItem.Description = contentItem.Trim();
                            recipe.PreparationMode.Add(preparationModeItem);
                        }
                    }

                    if (sessionItem.Name.Trim() == "Outras informações")
                    {
                        foreach (var contentItem in sessionItem.Content)
                        {
                            var otherInfoItem = new OtherInfo();

                            otherInfoItem.RecipeId = item.Id.ObjectIdValue;
                            otherInfoItem.Description = contentItem.Trim();
                            recipe.OtherInfo.Add(otherInfoItem);
                        }
                    }
                }

                recipes.Add(recipe);
            }

            return recipes;
        }
    }
}
