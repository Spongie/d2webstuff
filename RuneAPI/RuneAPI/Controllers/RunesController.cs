using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuneAPI.Database;
using RuneAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuneAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunesController : ControllerBase
    {
        private readonly RuneDbContext database;

        public RunesController(RuneDbContext database)
        {
            this.database = database;    
        }

        [HttpGet]
        public IEnumerable<Rune> Get()
        {
            return database.Runes;
        }
    }
}
