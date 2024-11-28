using System.Text.Json.Serialization;

namespace veeb.Models
{
    public class Kasutaja
    {
        public int Id { get; set; }
        public string Kasutajanimi { get; set; }
        public string Parool { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }

        [JsonIgnore] // Исключает это поле из сериализации в JSON
        public ICollection<Toode> Tooded { get; set; } = new List<Toode>();

        public Kasutaja(int id, string kasutajanimi, string parool, string eesnimi, string perenimi)
        {
            Id = id;
            Kasutajanimi = kasutajanimi;
            Parool = parool;
            Eesnimi = eesnimi;
            Perenimi = perenimi;
        }
    }
}