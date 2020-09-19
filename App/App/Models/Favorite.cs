using SQLite;

namespace App.Models
{
    [Table("favorite")]
    public partial class Favorite
    {
        [PrimaryKey, Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("favorite")]
        public bool IsFavorite { get; set; }
    }
}
