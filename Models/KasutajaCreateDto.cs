namespace veeb.Models
{
    public class KasutajaCreateDto
    {
        public int Id { get; set; }
        public string Kasutajanimi { get; set; } = string.Empty;
        public string Parool { get; set; } = string.Empty;
        public string Eesnimi { get; set; } = string.Empty;
        public string Perenimi { get; set; } = string.Empty;
        public int? CartId { get; set; } // Оставляем CartId, но без полной информации о корзине
    }
}
