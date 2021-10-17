using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuneAPI.Models;
using System.Collections.Generic;

namespace RuneAPI.Controllers
{
    [ApiController]
    public class RunewordsController : ControllerBase
    {
        [HttpGet]
        [Route("[controller]/search")]
        public IEnumerable<Runeword> search(string runeNumbers)
        {
            return new List<Runeword>();
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class RunesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Rune> Get()
        {
            return new List<Rune>
            {
                new Rune { Name = "El", Number = 1 },
                new Rune { Name = "Eld", Number = 2 },
                new Rune { Name = "Tir", Number = 3 },
                new Rune { Name = "Nef", Number = 4 },
                new Rune { Name = "Eth", Number = 5 },
                new Rune { Name = "Ith", Number = 6 },
                new Rune { Name = "Tal", Number = 7 },
                new Rune { Name = "Ral", Number = 8 },
                new Rune { Name = "Ort", Number = 9 },
                new Rune { Name = "Thul", Number = 10 },
                new Rune { Name = "Amn", Number = 11 },
                new Rune { Name = "Sol", Number = 12 },
                new Rune { Name = "Shael", Number = 13 },
                new Rune { Name = "Dol", Number = 14 },
                new Rune { Name = "Hel", Number = 15 },
                new Rune { Name = "Io", Number = 16 },
                new Rune { Name = "Lum", Number = 17 },
                new Rune { Name = "Ko", Number = 18 },
                new Rune { Name = "Fal", Number = 19 },
                new Rune { Name = "Lem", Number = 20 },
                new Rune { Name = "Pul", Number = 21 },
                new Rune { Name = "Um", Number = 22 },
                new Rune { Name = "Mal", Number = 23 },
                new Rune { Name = "Ist", Number = 24 },
                new Rune { Name = "Gul", Number = 25 },
                new Rune { Name = "Vex", Number = 26 },
                new Rune { Name = "Ohm", Number = 27 },
                new Rune { Name = "Lo", Number = 28 },
                new Rune { Name = "Sur", Number = 29 },
                new Rune { Name = "Ber", Number = 30 },
                new Rune { Name = "Jah", Number = 31 },
                new Rune { Name = "Cham", Number = 32 },
                new Rune { Name = "Zod", Number = 33 }
            };
        }
    }
}
