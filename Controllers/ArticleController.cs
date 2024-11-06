using Microsoft.AspNetCore.Mvc;
using veeb.Data;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Article> GetArticles()
        {
            var articles = _context.Articles.ToList();
            return articles;
        }

        [HttpPost]
        public List<Article> PostArtikkel([FromBody] Article artikkel)
        {
            _context.Articles.Add(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }

        [HttpDelete("{id}")]
        public List<Article> DeleteArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return _context.Articles.ToList();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }

        [HttpDelete("/kustuta2/{id}")]
        public IActionResult DeleteArtikkel2(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Article> GetArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            return artikkel;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Article>> PutArtikkel(int id, [FromBody] Article updatedArtikkel)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            artikkel.Header = updatedArtikkel.Header;
            artikkel.Content = updatedArtikkel.Content;

            _context.Articles.Update(artikkel);
            _context.SaveChanges();

            return Ok(_context.Articles);
        }
    }
}
