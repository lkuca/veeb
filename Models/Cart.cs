using System.Text.Json.Serialization;

namespace veeb.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int KasutajaId { get; set; }

        // Игнорируем это свойство, чтобы не передавать его в ответах
        [JsonIgnore]
        public Kasutaja? Kasutaja { get; set; }

        // Добавим список продуктов (не обязательно, если не нужно)
        public List<Toode> Tooted { get; set; } = new List<Toode>();
    }
}
