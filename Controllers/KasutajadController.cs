using Microsoft.AspNetCore.Mvc;
using veeb.Models;

namespace veeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KasutajadController : ControllerBase
    {
        private static List<Kasutaja> _kasutajad = new List<Kasutaja>{
            new Kasutaja(1, "kasutaja1", "1234", "Eesnimi1", "Perenimi1"),
            new Kasutaja(2, "kasutaja2", "5678", "Eesnimi2", "Perenimi2"),
            new Kasutaja(3, "kasutaja3", "9101", "Eesnimi3", "Perenimi3")
        };

        [HttpGet]
        // GET /kasutajad
        public List<Kasutaja> Get()
        {
            return _kasutajad;
        }

        // GET /kasutajad/kustuta/1
        [HttpGet("kustuta/{id}")]
        public List<Kasutaja> Delete(int id)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == id);
            if (kasutaja != null)
            {
                _kasutajad.Remove(kasutaja);
            }
            return _kasutajad;
        }

        // GET /kasutajad/lisa/4/uusKasutaja/9876/Eesnimi4/Perenimi4
        [HttpGet("lisa/{id}/{nimi}/{parool}/{eesnimi}/{perenimi}")]
        public List<Kasutaja> Add(int id, string nimi, string parool, string eesnimi, string perenimi)
        {
            Kasutaja kasutaja = new Kasutaja(id, nimi, parool, eesnimi, perenimi);
            _kasutajad.Add(kasutaja);
            return _kasutajad;
        }

        // GET /kasutajad/muuda-parooli/1/5678
        [HttpGet("muuda-parooli/{id}/{uusParool}")]
        public List<Kasutaja> MuudaParooli(int id, string uusParool)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == id);
            if (kasutaja != null)
            {
                kasutaja.Parool = uusParool;
            }
            return _kasutajad;
        }

        // GET /kasutajad/kustuta-koik
        [HttpGet("kustuta-koik")]
        public List<Kasutaja> DeleteAll()
        {
            _kasutajad.Clear();
            return _kasutajad;
        }

        // GET /kasutajad/1
        [HttpGet("{id}")]
        public ActionResult<Kasutaja> GetKasutajaById(int id)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == id);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }
            return kasutaja;
        }
    }
}
