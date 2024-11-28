using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veeb.Data;
using veeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace veeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KasutajaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KasutajaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: kasutaja
        [HttpGet]
        public async Task<ActionResult<Kasutaja>> GetKasutaja()
        {
            var kasutaja = await _dbContext.kasutajad.FirstOrDefaultAsync();
            if (kasutaja == null)
            {
                return NotFound("Kasutaja ei leitud.");
            }
            return kasutaja;
        }

        // PATCH: kasutaja/muuda-nime
        [HttpPatch("muuda-nime")]
        public async Task<ActionResult<Kasutaja>> MuudaNime([FromQuery] int id, [FromQuery] string uusNimi)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound("Kasutaja ei leitud.");
            }

            kasutaja.Kasutajanimi = uusNimi;
            await _dbContext.SaveChangesAsync();
            return kasutaja;
        }

        // PATCH: kasutaja/muuda-parooli
        [HttpPatch("muuda-parooli")]
        public async Task<ActionResult<Kasutaja>> MuudaParooli([FromQuery] int id, [FromQuery] string uusParool)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound("Kasutaja ei leitud.");
            }

            kasutaja.Parool = uusParool;
            await _dbContext.SaveChangesAsync();
            return kasutaja;
        }

        // PATCH: kasutaja/muuda-eesnime
        [HttpPatch("muuda-eesnime")]
        public async Task<ActionResult<Kasutaja>> MuudaEesnime([FromQuery] int id, [FromQuery] string uusEesnimi)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound("Kasutaja ei leitud.");
            }

            kasutaja.Eesnimi = uusEesnimi;
            await _dbContext.SaveChangesAsync();
            return kasutaja;
        }

        // PATCH: kasutaja/muuda-perenime
        [HttpPatch("muuda-perenime")]
        public async Task<ActionResult<Kasutaja>> MuudaPerenime([FromQuery] int id, [FromQuery] string uusPerenimi)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound("Kasutaja ei leitud.");
            }

            kasutaja.Perenimi = uusPerenimi;
            await _dbContext.SaveChangesAsync();
            return kasutaja;
        }
    }
}
