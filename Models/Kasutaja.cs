using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace veeb.Models
{
    public class Kasutaja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Это гарантирует, что поле будет автоинкрементируемым
        public int KasutajaId { get; set; }

        public string Kasutajanimi { get; set; }
        public string Parool { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }

        // Не забудьте пометить внешний ключ CartId, если хотите привязать пользователя к корзине
        public int? CartId { get; set; }
        [JsonIgnore]
        public Cart? Cart { get; set; }

        public List<Toode> PurchasedProducts { get; set; } = new List<Toode>();
    }

}