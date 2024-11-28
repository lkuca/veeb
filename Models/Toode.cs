namespace veeb.Models
{
    public class Toode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        // Foreign key to Kasutaja
        public int? KasutajaId { get; set; }
        public Kasutaja Kasutaja { get; set; } // Navigation property

        public Toode(int id, string name, double price, bool isActive)
        {
            Id = id;
            Name = name;
            Price = price;
            IsActive = isActive;
        }
    }
}
//asdsadasd