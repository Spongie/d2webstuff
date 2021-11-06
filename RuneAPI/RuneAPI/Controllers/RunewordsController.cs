using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuneAPI.Database;
using RuneAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuneAPI.Controllers
{
    [ApiController]
    public class RunewordsController : ControllerBase
    {
        private readonly RuneDbContext database;

        public RunewordsController(RuneDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        [Route("[controller]")]
        public IEnumerable<RunewordDTO> GetAll()
        {
            return database.Runewords
                .Include(r => r.RunewordRunes).ThenInclude(r => r.Rune)
                .Include(r => r.Modifiers)
                .Select(runeword => new RunewordDTO(runeword)).ToList();
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Create(RunewordDTO createData)
        {
            var runeword = new Runeword
            {
                Name = createData.Name,
                TargetTypes = createData.TargetTypes,
                RequiredLevel = createData.RequiredLevel
            };
                
            database.Runewords.Add(runeword);

            foreach (var rune in createData.Runes)
            {
                runeword.RunewordRunes.Add(new RunewordRune
                {
                    Rune = database.Runes.First(r => r.Name.ToLower() == rune.Name.Trim().ToLower()),
                    Runeword = runeword
                });
            }

            foreach (var modifier in createData.Modifiers)
            {
                runeword.Modifiers.Add(modifier);
            }

            await database.SaveChangesAsync();

            return Ok(new RunewordDTO(runeword));
        }

        [HttpGet]
        [Route("[controller]/search")]
        public IEnumerable<RunewordDTO> Search(string runeNumbers)
        {
            if (string.IsNullOrEmpty(runeNumbers))
            {
                return Array.Empty<RunewordDTO>();
            }

            var runes = runeNumbers.Split(",").Select(x => long.Parse(x)).ToHashSet();
            var matchingRunewords = new List<RunewordDTO>();

            foreach (var runeword in database.Runewords.Include(r => r.RunewordRunes).ThenInclude(r => r.Rune).Include(r => r.Modifiers))
            {
                int hits = 0;

                foreach (var rune in runeword.RunewordRunes.Select(r => r.Rune.Number).ToArray())
                {
                    if (!runes.Contains(rune))
                    {
                        break;
                    }

                    hits++;
                }

                if (hits == runeword.RunewordRunes.Count)
                {
                    matchingRunewords.Add(new RunewordDTO(runeword));
                }
            }

            return matchingRunewords;
        }
    }
}
