using SQLite;

namespace App.Models
{
    [Table("preparation_mode")]
    public partial class PreparationMode
    {
        [Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
