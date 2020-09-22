using App.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository
{
    public class UserRepository
    {
        public UserRepository()
        {
            CreateTableInMyDatabase();
        }

        private void CreateTableInMyDatabase()
        {
            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                db.CreateTable<User>();
                db.Close();
            }
        }

        public bool Auth(User user)
        {
            int numberAffectedRows = 0;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedRows = db.Table<User>()
                    .Where(item => item.Email == user.Email && item.Password == user.Password)
                    .Count();
                db.Close();
            }

            return numberAffectedRows > 0;
        }

        public bool Save(User user)
        {
            int numberAffectedRows = 0;

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedRows = db.InsertOrReplace(user);                
                db.Close();
            }

            return numberAffectedRows > 0;
        }

        public List<User> Get()
        {
            var users = new List<User>();

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                users = db.Table<User>().ToList();
                db.Close();
            }

            return users;
        }

        public User Get(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            
            var user = new User();

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                user = db.Table<User>().Where(x => x.Email.Equals(email)).FirstOrDefault();
                db.Close();
            }

            return user;
        }

        public User Get(bool rememberMe)
        {
            if (!rememberMe)
                return null;

            var user = new User();

            using (var db = new SQLiteConnection(App.DatabasePath))
            {
                user = db.Query<User>("select * from user where remember_me = ?", true).FirstOrDefault();                                              
                db.Close();
            }

            return user;
        }
    }
}
