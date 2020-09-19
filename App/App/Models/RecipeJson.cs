using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
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
