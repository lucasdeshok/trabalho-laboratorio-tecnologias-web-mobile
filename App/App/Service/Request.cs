using Android.Text.Method;
using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

                return ToModel(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static List<Recipe> ToModel(List<RecipeJson> recipeJsons)
        {
            var recipes = new List<Recipe>();

            foreach (var item in recipeJsons)
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
