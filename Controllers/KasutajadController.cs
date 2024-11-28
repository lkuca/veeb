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
    public class KasutajadController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KasutajadController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET /kasutajad
        [HttpGet]
        public async Task<ActionResult<List<Kasutaja>>> Get()
        {
            return await _dbContext.kasutajad.ToListAsync();
        }

        // GET /kasutajad/kustuta/1
        [HttpDelete("kustuta/{id}")]
        public async Task<ActionResult<List<Kasutaja>>> Delete(int id)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            _dbContext.kasutajad.Remove(kasutaja);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.kasutajad.ToListAsync();
        }

        // POST /kasutajad/lisa
        [HttpPost("lisa")]
        public async Task<ActionResult<List<Kasutaja>>> Add(Kasutaja newKasutaja)
        {
            _dbContext.kasutajad.Add(newKasutaja);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.kasutajad.ToListAsync();
        }

        // PUT /kasutajad/muuda-parooli/1
        [HttpPut("muuda-parooli/{id}")]
        public async Task<ActionResult<List<Kasutaja>>> MuudaParooli(int id, [FromBody] string uusParool)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            kasutaja.Parool = uusParool;
            _dbContext.kasutajad.Update(kasutaja);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.kasutajad.ToListAsync();
        }

        // DELETE /kasutajad/kustuta-koik
        [HttpDelete("kustuta-koik")]
        public async Task<ActionResult<List<Kasutaja>>> DeleteAll()
        {
            _dbContext.kasutajad.RemoveRange(_dbContext.kasutajad);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.kasutajad.ToListAsync();
        }

        // GET /kasutajad/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Kasutaja>> GetKasutajaById(int id)
        {
            var kasutaja = await _dbContext.kasutajad.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }
            return kasutaja;
        }
    }
}
