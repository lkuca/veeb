using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace veeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimitiividController : ControllerBase
    {
        // GET: primitiivid/hello-world
        [HttpGet("hello-world")]
        public string HelloWorld()
        {
            return "Hello world at " + DateTime.Now;
        }

        // GET: primitiivid/hello-variable/mari
        [HttpGet("hello-variable/{nimi}")]
        public string HelloVariable(string nimi)
        {
            return "Hello " + nimi;
        }

        // GET: primitiivid/add/5/6
        [HttpGet("add/{nr1}/{nr2}")]
        public int AddNumbers(int nr1, int nr2)
        {
            return nr1 + nr2;
        }

        // GET: primitiivid/multiply/5/6
        [HttpGet("multiply/{nr1}/{nr2}")]
        public int Multiply(int nr1, int nr2)
        {
            return nr1 * nr2;
        }

        // GET: primitiivid/do-logs/5
        [HttpGet("do-logs/{arv}")]
        public void DoLogs(int arv)
        {
            for (int i = 0; i < arv; i++)
            {
                Console.WriteLine("See on logi nr " + i);
            }
        }

        // GET: primitiivid/random/5/15
        [HttpGet("random/{min}/{max}")]
        public int GetRandomNumber(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max + 1); // +1, et max kaasa arvatud oleks
        }

        // GET: primitiivid/age/1990
        [HttpGet("age/{yearOfBirth}")]
        public string GetAge(int yearOfBirth)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonthDay = DateTime.Now.Month * 100 + DateTime.Now.Day;
            var birthMonthDay = new DateTime(yearOfBirth, 1, 1).Month * 100 + 1; // eeldades, et sünnipäev on 1. jaanuaril
            int age = currentYear - yearOfBirth;

            if (currentMonthDay < birthMonthDay)
            {
                age--; // kui sünnipäev pole veel olnud, lahutame 1
            }

            return $"Oled {age} aastat vana.";
        }
    }
}
