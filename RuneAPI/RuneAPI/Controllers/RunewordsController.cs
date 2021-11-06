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
        public IEnumerable<Runeword> Search(string runeNumbers)
        {
            var runes = runeNumbers.Split(",").Select(x => int.Parse(x)).ToHashSet();
            var matchingRunewords = new List<Runeword>();

            foreach (var runeword in database.Runewords)
            {
                //int hits = 0;

                //foreach (var rune in runeword.Runes.Select(r => r.Number).ToArray())
                //{
                //    if (!runes.Contains(rune))
                //    {
                //        break;
                //    }

                //    hits++;
                //}

                //if (hits == runeword.Runes.Count)
                //{
                //    matchingRunewords.Add(runeword);
                //}
            }

            return matchingRunewords;
        }
    }
}
