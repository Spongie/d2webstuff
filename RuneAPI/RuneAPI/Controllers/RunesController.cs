using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuneAPI.Database;
using RuneAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuneAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return database.Runes.OrderBy(r => r.Number);
        }
    }
}
