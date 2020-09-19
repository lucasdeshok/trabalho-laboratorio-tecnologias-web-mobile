using SQLite;
using System;

namespace App.Models
{
    [Table("user")]
    public class User
    {        
        [PrimaryKey, AutoIncrement]        
        [Column("id")]
        public uint Id { get; set; }                
        
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(75)]
        [Column("last_name")]
        public string LastName { get; set; }

        [MaxLength(75)]
        [Column("email")]
        public string Email { get; set; }

        [MaxLength(75)]
        [Column("password")]
        public string Password { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [Column("remember_me")]
        public bool RememberMe { get; set; } = false;
    }
}
