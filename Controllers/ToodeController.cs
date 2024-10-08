using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private static Toode _toode = new Toode(1, "Koola", 1.5, true);

        // GET: toode
        [HttpGet]
        public Toode GetToode()
        {
            return _toode;
        }

        // GET: toode/suurenda-hinda
        [HttpGet("suurenda-hinda")]
        public Toode SuurendaHinda()
        {
            _toode.Price = _toode.Price + 1;
            return _toode;
        }

        // GET: toode/muuda-aktiivsust
        [HttpGet("muuda-aktiivsust")]
        public Toode MuudaAktiivsust()
        {
            _toode.IsActive = !_toode.IsActive; // Muudab aktiivsust vastupidiseks
            return _toode;
        }

        // GET: toode/muuda-nime/UusNimi
        [HttpGet("muuda-nime/{uusNimi}")]
        public Toode MuudaNime(string uusNimi)
        {
            _toode.Name = uusNimi; // Määrab uue nime URL-i kaudu
            return _toode;
        }

        // GET: toode/muuda-hinda/2
        [HttpGet("muuda-hinda/{kordaja}")]
        public Toode MuudaHinda(double kordaja)
        {
            _toode.Price = _toode.Price * kordaja; // Korrutab hinna antud kordajaga
            return _toode;
        }
    }
}
