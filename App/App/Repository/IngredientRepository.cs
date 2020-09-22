using App.Models;
using SQLite;
using System.Collections.Generic;

namespace App.Repository
{
    public class IngredientRepository
    {
        public IngredientRepository()
        {
            CreateTableInMyDatabase();
        }

        private void CreateTableInMyDatabase()
        {
            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                db.CreateTable<Ingredient>();                
                db.Close();
            }
        }

        public bool Save(List<Ingredient> ingredient)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.InsertAll(ingredient);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Delete(List<Ingredient> ingredient)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.Delete<List<Ingredient>>(ingredient);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public Recipe Get()
        {
            return new Recipe();
        }

        public Recipe Get(string pk)
        {
            return new Recipe();
        }

        public List<Recipe> Get(string pk, string pg)
        {
            return new List<Recipe>();
        }
    }
}
