using SQLite;

namespace App.Models
{
    [Table("ingredient")]
    public partial class Ingredient
    {
        [Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
