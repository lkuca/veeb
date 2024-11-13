using Microsoft.AspNetCore.Mvc;
using veeb.Models;

namespace veeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KasutajaToodeController : ControllerBase
    {
        private static List<Kasutaja> _kasutajad = new List<Kasutaja>{
            new Kasutaja(1, "kasutaja1", "1234", "Eesnimi1", "Perenimi1"),
            new Kasutaja(2, "kasutaja2", "5678", "Eesnimi2", "Perenimi2")
        };

        private static List<Toode> _tooted = new List<Toode>{
            new Toode(1, "Koola", 1.5, true),
            new Toode(2, "Fanta", 1.0, false),
            new Toode(3, "Sprite", 1.7, true)
        };

        // list for users
        private static Dictionary<int, List<Toode>> _kasutajaTooted = new Dictionary<int, List<Toode>>{
            { 1, new List<Toode>{ _tooted[0], _tooted[1] } },  // kasutaja1 - Koola and Fanta
            { 2, new List<Toode>{ _tooted[2] } }               // kasutaja2 - Sprite
        };

        // GET: kasutaja-toode/{userId}
        // Gives users and their list of products
        [HttpGet("{userId}")]
        public ActionResult<Kasutaja> GetKasutajaWithTooded(int userId)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == userId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            var kasutajaTooted = _kasutajaTooted.ContainsKey(userId) ? _kasutajaTooted[userId] : new List<Toode>();
            return Ok(new { kasutaja, tooted = kasutajaTooted });
        }

        // POST: kasutaja-toode/lisa-toode/{userId}/{toodeId}
        // Adds product to the user list
        [HttpPost("lisa-toode/{userId}/{toodeId}")]
        public ActionResult AddToodeToKasutaja(int userId, int toodeId)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == userId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            var toode = _tooted.FirstOrDefault(t => t.Id == toodeId);
            if (toode == null)
            {
                return NotFound("Toodet ei leitud.");
            }

            if (!_kasutajaTooted.ContainsKey(userId))
            {
                _kasutajaTooted[userId] = new List<Toode>();
            }

            _kasutajaTooted[userId].Add(toode);
            return Ok(new { message = "Toode lisatud!", kasutaja, tooded = _kasutajaTooted[userId] });
        }

        // DELETE: kasutaja-toode/eemalda-toode/{userId}/{toodeId}
        // Deletes user's product
        [HttpDelete("eemalda-toode/{userId}/{toodeId}")]
        public ActionResult RemoveToodeFromKasutaja(int userId, int toodeId)
        {
            if (!_kasutajaTooted.ContainsKey(userId) || !_kasutajaTooted[userId].Any(t => t.Id == toodeId))
            {
                return NotFound("Toodet või kasutajat ei leitud.");
            }

            var toode = _kasutajaTooted[userId].FirstOrDefault(t => t.Id == toodeId);
            _kasutajaTooted[userId].Remove(toode);
            return Ok(new { message = "Toode eemaldatud!", tooded = _kasutajaTooted[userId] });
        }

        // PUT: kasutaja-toode/muuda-hinda/{userId}/{toodeId}/{uusHind}
        // Changes the price of a product for the user
        [HttpPut("muuda-hinda/{userId}/{toodeId}/{uusHind}")]
        public ActionResult UpdateToodePriceForKasutaja(int userId, int toodeId, double uusHind)
        {
            if (!_kasutajaTooted.ContainsKey(userId) || !_kasutajaTooted[userId].Any(t => t.Id == toodeId))
            {
                return NotFound("Toodet või kasutajat ei leitud.");
            }

            var toode = _kasutajaTooted[userId].FirstOrDefault(t => t.Id == toodeId);
            toode.Price = uusHind;
            return Ok(new { message = "Toote hind muudetud!", tooded = _kasutajaTooted[userId] });
        }

        // GET: kasutaja-toode/koik-tooted
        // Returns a list of all users and their products
        [HttpGet("koik-tooted")]
        public ActionResult GetAllKasutajadWithTooded()
        {
            var result = _kasutajad.Select(k => new
            {
                kasutaja = k,
                tooted = _kasutajaTooted.ContainsKey(k.Id) ? _kasutajaTooted[k.Id] : new List<Toode>()
            }).ToList();

            return Ok(result);
        }
    }
}
