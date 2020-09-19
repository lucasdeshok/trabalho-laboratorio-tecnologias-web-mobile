using App.Models;
using SQLite;
using System.Collections.Generic;

namespace App.Repository
{
    public class RecipeRepository
    {
        public RecipeRepository()
        {
            CreateAllTablesForMyDatabase();
        }

        private void CreateAllTablesForMyDatabase()
        {
            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                db.CreateTable<Recipe>();
                db.CreateTable<Ingredient>();
                db.CreateTable<PreparationMode>();
                db.CreateTable<OtherInfo>();
                db.CreateTable<Favorite>();

                db.Close();
            }
        }

        public bool Save(Recipe recipe)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                SaveIngredient(recipe.Ingredient);
                SavePreparationMode(recipe.PreparationMode);
                SaveOtherInfo(recipe.OtherInfo);
                numberAffectedItems = db.Insert(recipe);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool SaveIngredient(List<Ingredient> ingredient)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.InsertAll(ingredient);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool SavePreparationMode(List<PreparationMode> preparationMode)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.InsertAll(preparationMode);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool SaveOtherInfo(List<OtherInfo> otherInfo)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.InsertAll(otherInfo);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Delete(Recipe recipe)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                db.Query<Ingredient>("delete from ingredient where recipe_id = ?", recipe.Id);
                db.Query<PreparationMode>("delete from preparation_mode where recipe_id = ?", recipe.Id);
                db.Query<OtherInfo>("delete from other_info where recipe_id = ?", recipe.Id);
                numberAffectedItems = db.Delete(recipe);

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

        public bool Update(Recipe recipe)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {                
                numberAffectedItems = db.Update(recipe);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool SetFavorite(Recipe recipe)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.InsertOrReplace(recipe.Favorite);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool CheckExists(Recipe recipe)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.Table<Recipe>().Where(i => i.Id == recipe.Id).Count();
                db.Close();                
            }

            return numberAffectedItems > 0;
        }
    }
}
