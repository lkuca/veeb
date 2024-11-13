using Microsoft.AspNetCore.Mvc;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KasutajaController : ControllerBase
    {
        private static Kasutaja _kasutaja = new Kasutaja(1, "kasutajanimi", "1234", "Eesnimi", "Perenimi");

        // GET: kasutaja
        [HttpGet]
        public Kasutaja GetKasutaja()
        {
            return _kasutaja;
        }

        // GET: kasutaja/muuda-nime/UusNimi
        [HttpGet("muuda-nime/{uusNimi}")]
        public Kasutaja MuudaNime(string uusNimi)
        {
            _kasutaja.Kasutajanimi = uusNimi;
            return _kasutaja;
        }

        // GET: kasutaja/muuda-parooli/5678
        [HttpGet("muuda-parooli/{uusParool}")]
        public Kasutaja MuudaParooli(string uusParool)
        {
            _kasutaja.Parool = uusParool;
            return _kasutaja;
        }

        // GET: kasutaja/muuda-eesnime/Eesnimi
        [HttpGet("muuda-eesnime/{uusEesnimi}")]
        public Kasutaja MuudaEesnime(string uusEesnimi)
        {
            _kasutaja.Eesnimi = uusEesnimi;
            return _kasutaja;
        }

        // GET: kasutaja/muuda-perenime/Perenimi
        [HttpGet("muuda-perenime/{uusPerenimi}")]
        public Kasutaja MuudaPerenime(string uusPerenimi)
        {
            _kasutaja.Perenimi = uusPerenimi;
            return _kasutaja;
        }
    }
}
