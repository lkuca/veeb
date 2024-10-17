using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using veeb.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ToodedController : ControllerBase
    {
        private static List<Toode> _tooted = new()
        {
            new Toode(1,"Koola", 1.5, true),
            new Toode(2,"Fanta", 1.0, false),
            new Toode(3,"Sprite", 1.7, true),
            new Toode(4,"Vichy", 2.0, true),
            new Toode(5,"Vitamin well", 2.5, true)
        };

        // GET https://localhost:4444/tooted
        [HttpGet]
        public List<Toode> Get()
        {
            return _tooted;
        }

        // DELETE https://localhost:4444/tooted/kustuta/0
        [HttpDelete("kustuta/{index}")]
        public List<Toode> Delete(int index)
        {
            _tooted.RemoveAt(index);
            return _tooted;
        }

        [HttpDelete("kustuta2/{index}")]
        public string Delete2(int index)
        {
            _tooted.RemoveAt(index);
            return "Kustutatud!";
        }

        [HttpPost("lisa")]
        public List<Toode> Add([FromBody] Toode toode)
        {
            _tooted.Add(toode);
            return _tooted;
        }

        // POST https://localhost:4444/tooted/lisa/1/Coca/1.5/true
        [HttpPost("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
        public List<Toode> Add(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        [HttpPost("lisa2")]
        public List<Toode> Add2(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        // PATCH https://localhost:4444/tooted/hind-dollaritesse/1.5
        [HttpPatch("hind-dollaritesse/{kurss}")]
        public List<Toode> UpdatePrices(double kurss)
        {
            for (int i = 0; i < _tooted.Count; i++)
            {
                _tooted[i].Price = _tooted[i].Price * kurss;
            }
            return _tooted;
        }

        // või foreachina:

        [HttpGet("hind-dollaritesse2/{kurss}")] // GET /tooted/hind-dollaritesse2/1.5
        public List<Toode> Dollaritesse2(double kurss)
        {
            foreach (var t in _tooted)
            {
                t.Price = t.Price * kurss;
            }

            return _tooted;
        }
        [HttpPut("muuda/{id}/{nimi}/{hind}/{aktiivne}")]
        public void Update(int id, string nimi, double hind, bool aktiivne)
        {
            var olemasolevToode = _tooted.FirstOrDefault(t => t.Id == id);
            
            olemasolevToode.Name = nimi;
            olemasolevToode.Price = hind;
            olemasolevToode.IsActive = aktiivne;

        }
        // GET: /tooted/kustutakoik
        [HttpGet("kustutuakoik")]
        public string kustutakõiktooted()
        {
            _tooted.Clear();

            return "kõik on kustutatud";
        }

        [HttpGet("aktiivsuseväär")]
        public List<Toode> aktiivsusväär(bool aktiivne)
        {
            for (int i = 0; i < _tooted.Count; i++)
            {
                _tooted[i].IsActive = aktiivne;
            }
            //_tooted = _tooted.Select(x =>
            //{
            //    x.IsActive = aktiivne;
            //    return x;
            //});
            //_tooted = _tooted.ForEach(x =>
            //{
            //    x.IsActive = aktiivne;
            //    return x;
            //});
            return _tooted;
        }
        [HttpGet("tootetagastamine")]
        public Toode tootetagamine(int id) => _tooted.ElementAtOrDefault(id) ?? new Toode(-1, "The object is missing", 0, false);

        [HttpGet("kalleimtoode")]
        public Toode KalleimToode()
        {
            if (_tooted == null || !_tooted.Any())
            {
                return new Toode(-1, "Tooteid pole saadaval", 0, false);
            }

            var kalleimToode = _tooted.OrderByDescending(t => t.Price).FirstOrDefault();
            return kalleimToode ?? new Toode(-1, "Toode puudub", 0, false);
        }

    }
}

