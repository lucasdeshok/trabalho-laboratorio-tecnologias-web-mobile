using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;

namespace App.Models
{
    /// <summary>
    /// Class for object model manipulation to database sqlite.
    /// </summary>
    [Table("recipe")]
    public class Recipe
    {
        [PrimaryKey, Indexed]
        [Column("id")]
        public string Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Ignore]
        public List<Ingredient> Ingredient { get; set; }

        [Ignore]
        public List<PreparationMode> PreparationMode { get; set; }

        [Ignore]
        public List<OtherInfo> OtherInfo { get; set; }

        [Ignore]
        public Favorite Favorite { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("update_at")]
        public DateTime UpdateAt { get; set; }

        public Recipe()
        {
            OtherInfo = new List<OtherInfo>();
            Ingredient = new List<Ingredient>();
            PreparationMode = new List<PreparationMode>();
            Favorite = new Favorite();
        }
    }

    [Table("ingredient")]
    public partial class Ingredient
    {
        [Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }

    [Table("preparation_mode")]
    public partial class PreparationMode
    {
        [Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }

    [Table("other_info")]
    public partial class OtherInfo
    {
        [Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }

    [Table("favorite")]
    public partial class Favorite
    {
        [PrimaryKey, Indexed]
        [Column("recipe_id")]
        public string RecipeId { get; set; }

        [Column("favorite")]
        public bool IsFavorite { get; set; }
    }

    /// <summary>
    /// Class for object model manipulation received by the web request.
    /// </summary>
    public class RecipeJson
    {
        [JsonProperty("_id")]
        public ObjectId Id { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("secao")]
        public List<Session> Session { get; set; }
    }

    public partial class ObjectId
    {
        [JsonProperty("$oid")]
        public string ObjectIdValue { get; set; }
    }

    public partial class Session
    {
        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("conteudo")]
        public List<string> Content { get; set; }
    }
}
