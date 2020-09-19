using App.Models;
using SQLite;

namespace App.Repository
{
    public class UserRepository
    {
        public UserRepository()
        {
            CreateTableForMyDatabase();
        }

        private void CreateTableForMyDatabase()
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<User>();                
                connection.Close();
            }
        }

        public bool Create(User user)
        {
            int numberAffectedItems;

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {                
                numberAffectedItems = connection.Insert(user);
                connection.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Update(User user)
        {
            int numberAffectedItems;

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {                
                numberAffectedItems = connection.Update(user);
                connection.Close();
            }

            return numberAffectedItems > 0;
        }

        public User GetByEmail(string email)
        {
            var user = new User();

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {                
                user = connection.Table<User>().Where(u => u.Email == email).FirstOrDefault();
                connection.Close();
            }

            return user;
        }

        public bool CheckExists(string email)
        {
            int numberAffectedItems;

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                numberAffectedItems = connection.Table<User>().Where(u => u.Email == email).Count();
                connection.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Delete(User user)
        {
            int numberAffectedItems;

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {                
                numberAffectedItems = connection.Delete(user);
                connection.Close();
            }

            return numberAffectedItems > 0;
        }

        public bool Auth(User user)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {                
                user = connection.Table<User>().Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
                connection.Close();
            }

            return user != null;
        }
    }
}
