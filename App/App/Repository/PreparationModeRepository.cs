using App.Models;
using SQLite;
using System.Collections.Generic;

namespace App.Repository
{
    public class PreparationModeRepository
    {
        public PreparationModeRepository()
        {
            CreateTableInMyDatabase();
        }

        private void CreateTableInMyDatabase()
        {
            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                db.CreateTable<PreparationMode>();
                db.Close();
            }
        }

        public bool Save(List<PreparationMode> preparation)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.InsertAll(preparation);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Delete(List<PreparationMode> preparation)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.Delete<List<PreparationMode>>(preparation);
                db.Close();
            }

            return numberAffectedItems > 0;
        }
    }
}
