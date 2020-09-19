using SQLite;

namespace App.Models
{
    [Table("other_info")]
    public partial class OtherInfo
    {
        [Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
