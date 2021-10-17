using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuneAPI.Models;
using System.Collections.Generic;

namespace RuneAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunesController : ControllerBase
    {
        private static readonly Rune[] cRunes = new Rune[]
        {

        };

        [HttpGet]
        public IEnumerable<Rune> Get()
        {
            return new List<Rune>
            {
                new Rune{Name = "El"},
                new Rune{Name = "Eld"},
            };
        }
    }
}
