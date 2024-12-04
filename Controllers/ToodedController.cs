using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veeb.Data;
using veeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace veeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToodedController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ToodedController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /tooded
        [HttpGet]
        public async Task<ActionResult<List<Toode>>> Get()
        {
            return await _dbContext.Tooted.ToListAsync();
        }

        // DELETE: /tooted/kustuta/1
        [HttpDelete("kustuta/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var toode = await _dbContext.Tooted.FindAsync(id);
            if (toode == null)
            {
                return NotFound("Toodet ei leitud.");
            }

            _dbContext.Tooted.Remove(toode);
            await _dbContext.SaveChangesAsync();
            return Ok("Toode kustutatud!");
        }

        // POST: /tooted/lisa
        [HttpPost("lisa")]
        public async Task<ActionResult<List<Toode>>> Add([FromBody] Toode toode)
        {
            // Если CartId не передан, установите его как null
            if (toode.CartId == 0)
            {
                toode.CartId = null;  // или просто оставьте null, если это допустимо
            }

            _dbContext.Tooted.Add(toode);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Tooted.ToListAsync();
        }

        // PATCH: /tooted/hind-dollaritesse/1.5
        //[HttpPatch("hind-dollaritesse/{kurss}")]
        //public async Task<ActionResult<List<Toode>>> UpdatePrices(double kurss)
        //{
        //    var tooted = await _dbContext.Tooted.ToListAsync();
        //    foreach (var toode in tooted)
        //    {
        //        toode.Price *= kurss;
        //    }

        //    _dbContext.Tooted.UpdateRange(tooted);
        //    await _dbContext.SaveChangesAsync();
        //    return tooted;
        //}

        // PUT: /tooted/muuda/1
        [HttpPut("muuda/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Toode updatedToode)
        {
            var toode = await _dbContext.Tooted.FindAsync(id);
            if (toode == null)
            {
                return NotFound("Toodet ei leitud.");
            }

            toode.Name = updatedToode.Name;
            toode.Price = updatedToode.Price;
            toode.Quantity = updatedToode.Quantity;

            _dbContext.Tooted.Update(toode);
            await _dbContext.SaveChangesAsync();
            return Ok("Toode uuendatud!");
        }

        // DELETE: /tooted/kustuta-koik
        [HttpDelete("kustuta-koik")]
        public async Task<ActionResult> DeleteAll()
        {
            _dbContext.Tooted.RemoveRange(await _dbContext.Tooted.ToListAsync());
            await _dbContext.SaveChangesAsync();
            return Ok("Kõik tooted kustutatud!");
        }

        // GET: /tooted/kalleimtoode
        [HttpGet("kalleimtoode")]
        public async Task<ActionResult<Toode>> GetKalleimToode()
        {
            var toode = await _dbContext.Tooted.OrderByDescending(t => t.Price).FirstOrDefaultAsync();
            if (toode == null)
            {
                return NotFound("Ühtegi toodet ei leitud.");
            }

            return toode;
        }
    }
}
