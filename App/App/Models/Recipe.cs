using SQLite;
using System;
using System.Collections.Generic;

namespace App.Models
{
    [Table("recipe")]
    public class Recipe
    {
        [PrimaryKey, Indexed]
        [Column("id")]
        public string Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("is_favorite")]
        public bool IsFavorite { get; set; }

        [Column("update_at")]
        public DateTime UpdateAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Ignore]
        public List<OtherInfo> OtherInfo { get; set; }

        [Ignore]
        public List<Ingredient> Ingredient { get; set; }

        [Ignore]
        public List<PreparationMode> PreparationMode { get; set; }

        public Recipe()
        {            
            OtherInfo = new List<OtherInfo>();
            Ingredient = new List<Ingredient>();
            PreparationMode = new List<PreparationMode>();            
        }
    }
}
