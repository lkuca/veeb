using System.Text.Json.Serialization;

namespace veeb.Models
{
    public class Toode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Quantity { get; set; }

        public int? CartId { get; set; }
        [JsonIgnore]
        public Cart? Cart { get; set; }

    }

}
//asdsadasd