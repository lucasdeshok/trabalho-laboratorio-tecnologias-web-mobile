using App.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository
{
    public class RecipeRepository
    {
        OtherInfoRepository infoRepository = new OtherInfoRepository();
        IngredientRepository ingredientRepository = new IngredientRepository();
        PreparationModeRepository modeRepository = new PreparationModeRepository();        

        public RecipeRepository()
        {
            CreateTableInMyDatabase();
        }

        private void CreateTableInMyDatabase()
        {
            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                db.CreateTable<Recipe>();                
                db.Close();
            }
        }

        public bool Save(Recipe recipe)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {                                                  
                infoRepository.Save(recipe.OtherInfo);
                modeRepository.Save(recipe.PreparationMode);
                ingredientRepository.Save(recipe.Ingredient);
                numberAffectedItems = db.InsertOrReplace(recipe);

                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Delete(Recipe recipe)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.Delete<Recipe>(recipe);               
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public List<Recipe> GetAll()
        {
            var result = new List<Recipe>();

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                result = db.Table<Recipe>().ToList();                
                db.Close();
            }

            return result;
        }

        public Recipe Get(Recipe recipe)
        {
            var result = new Recipe();

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                result = db.Get<Recipe>(recipe);
                db.Close();
            }

            return result;
        }

        public Recipe Get(string id)
        {
            var result = new Recipe();

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                result = db.Query<Recipe>("select * from recipe where id = ?", id).FirstOrDefault();
                db.Close();
            }

            return result;
        }

        public Recipe GetDetails(string recipeId)
        {
            var result = new Recipe();

            using (var db = new SQLiteConnection(App.DatabasePath))
            {                
                result = db.Table<Recipe>().Where(i => i.Id == recipeId).FirstOrDefault();
                result.Ingredient = db.Table<Ingredient>().Where(i => i.RecipeId == recipeId).ToList();
                result.PreparationMode = db.Table<PreparationMode>().Where(i => i.RecipeId == recipeId).ToList();
                result.OtherInfo = db.Table<OtherInfo>().Where(i => i.RecipeId == recipeId).ToList();
                db.Close();
            }

            return result;
        }

    }
}
