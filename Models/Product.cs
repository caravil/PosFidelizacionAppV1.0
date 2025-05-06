using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SQLite;

namespace PosFidelizacionAppV1._0.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; } // ID local en la base de datos (SQLite)

        [JsonPropertyName("id")]
        public int ApiId { get; set; } // ID que viene de la API

        [JsonPropertyName("title")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("rating")]
        [Ignore] // Ignorar para SQLite
        public Rating Rating { get; set; } = new Rating();

        // Propiedades planas para guardar en SQLite
        public double Rate { get; set; }
        public int Count { get; set; }
    }

    public class Rating
    {
        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}