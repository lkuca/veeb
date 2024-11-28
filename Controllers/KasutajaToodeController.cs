using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veeb.Data;
using veeb.Models;

namespace veeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KasutajaToodeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KasutajaToodeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetKasutajaWithTooded(int userId)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(userId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            var kasutajaTooted = await _dbContext.Tooded
                .Where(t => t.KasutajaId == userId) // Assuming `Toode` has a `KasutajaId` foreign key
                .ToListAsync();

            return Ok(new { kasutaja, tooted = kasutajaTooted });
        }

        // POST: kasutaja-toode/lisa-toode/{userId}/{toodeId}
        // Adds product to the user list
        [HttpPost("lisa-toode/{userId}/{toodeId}")]
        public async Task<ActionResult> AddToodeToKasutaja(int userId, int toodeId)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(userId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            var toode = await _dbContext.Tooded.FindAsync(toodeId);
            if (toode == null)
            {
                return NotFound("Toodet ei leitud.");
            }

            // Assuming `Toode` has a `KasutajaId` foreign key
            toode.KasutajaId = userId;
            _dbContext.Tooded.Update(toode);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Toode lisatud!", toode });
        }

        // DELETE: kasutaja-toode/eemalda-toode/{userId}/{toodeId}
        // Removes user's product
        [HttpDelete("eemalda-toode/{userId}/{toodeId}")]
        public async Task<ActionResult> RemoveToodeFromKasutaja(int userId, int toodeId)
        {
            var toode = await _dbContext.Tooded
                .FirstOrDefaultAsync(t => t.Id == toodeId && t.KasutajaId == userId);

            if (toode == null)
            {
                return NotFound("Toodet või kasutajat ei leitud.");
            }

            toode.KasutajaId = null; // Detach the product from the user
            _dbContext.Tooded.Update(toode);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Toode eemaldatud!" });
        }

        // PUT: kasutaja-toode/muuda-hinda/{userId}/{toodeId}/{uusHind}
        // Changes the price of a product for the user
        [HttpPut("muuda-hinda/{userId}/{toodeId}/{uusHind}")]
        public async Task<ActionResult> UpdateToodePriceForKasutaja(int userId, int toodeId, double uusHind)
        {
            var toode = await _dbContext.Tooded
                .FirstOrDefaultAsync(t => t.Id == toodeId && t.KasutajaId == userId);

            if (toode == null)
            {
                return NotFound("Toodet või kasutajat ei leitud.");
            }

            toode.Price = uusHind;
            _dbContext.Tooded.Update(toode);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Toote hind muudetud!", toode });
        }

        // GET: kasutaja-toode/koik-tooted
        // Returns a list of all users and their products
        [HttpGet("koik-tooted")]
        public async Task<ActionResult> GetAllKasutajadWithTooded()
        {
            var kasutajad = await _dbContext.kasutajad.ToListAsync();
            var result = await Task.WhenAll(kasutajad.Select(async k => new
            {
                kasutaja = k,
                tooted = await _dbContext.Tooded
                    .Where(t => t.KasutajaId == k.Id)
                    .ToListAsync()
            }));

            return Ok(result);
        }
        [HttpPost("maksa/{userId}")]
        public async Task<ActionResult> PayForTooted(int userId, [FromBody] double summa)
        {
            
            var kasutaja = await _dbContext.kasutajad
                .Include(k => k.Tooded)
                .FirstOrDefaultAsync(k => k.Id == userId);

            if (kasutaja == null)
            {
                return NotFound("Користувача не знайдено.");
            }

            
            var totalPrice = kasutaja.Tooded.Sum(t => t.Price);

            // Проверить, хватает ли суммы
            if (summa < totalPrice)
            {
                return BadRequest(new { message = "Недостаточно средств для оплаты." });
            }

            
            foreach (var toode in kasutaja.Tooded)
            {
                toode.KasutajaId = null; 
                _dbContext.Tooded.Update(toode);
            }

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Makstud", totalPaid = totalPrice });
        }
    }
}
