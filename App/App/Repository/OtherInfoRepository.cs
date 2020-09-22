using App.Models;
using SQLite;
using System.Collections.Generic;

namespace App.Repository
{
    public class OtherInfoRepository
    {
        public OtherInfoRepository()
        {
            CreateTableInMyDatabase();
        }

        private void CreateTableInMyDatabase()
        {
            using (var db = new SQLiteConnection(App.DatabasePath))
            {               
                db.CreateTable<OtherInfo>();                
                db.Close();
            }
        }

        public bool Save(List<OtherInfo> otherInfo)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.InsertAll(otherInfo);
                db.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Delete(List<OtherInfo> other)
        {
            int numberAffectedItems;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = db.Delete<List<OtherInfo>>(other);
                db.Close();
            }

            return numberAffectedItems > 0;
        }
    }
}
