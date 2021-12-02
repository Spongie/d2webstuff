using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RuneAPI.Database;
using RuneAPI.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RuneAPI.Controllers
{
    [ApiController]
    public class RunewordsController : ControllerBase
    {
        private readonly RuneDbContext _database;
        private readonly IConnectionMultiplexer _redisService;

        public RunewordsController(RuneDbContext database, IConnectionMultiplexer redisService)
        {
            this._database = database;
            this._redisService = redisService;
        }

        [HttpGet]
        [Route("[controller]")]
        public IEnumerable<RunewordDTO> GetAll()
        {
            return _database.Runewords
                .Include(r => r.RunewordRunes).ThenInclude(r => r.Rune)
                .Include(r => r.Modifiers)
                .Select(runeword => new RunewordDTO(runeword)).ToList();
        }

        [HttpPost]
        [Route("[controller]")]
        [Authorize]
        public async Task<IActionResult> Create(RunewordDTO createData)
        {
            var runeword = new Runeword
            {
                Name = createData.Name,
                TargetTypes = createData.TargetTypes,
                RequiredLevel = createData.RequiredLevel
            };
                
            _database.Runewords.Add(runeword);

            foreach (var rune in createData.Runes)
            {
                runeword.RunewordRunes.Add(new RunewordRune
                {
                    Rune = _database.Runes.First(r => r.Name.ToLower() == rune.Name.Trim().ToLower()),
                    Runeword = runeword
                });
            }

            foreach (var modifier in createData.Modifiers)
            {
                runeword.Modifiers.Add(modifier);
            }

            await _database.SaveChangesAsync();

            return Ok(new RunewordDTO(runeword));
        }

        [HttpGet]
        [Route("[controller]/search")]
        public async Task<IEnumerable<RunewordDTO>> Search(string runeNumbers)
        {
            if (string.IsNullOrEmpty(runeNumbers))
            {
                return Array.Empty<RunewordDTO>();
            }

            var redisDB = _redisService.GetDatabase();

            var redisCache = await redisDB.StringGetAsync(new RedisKey(runeNumbers));

            if (redisCache.HasValue)
            {
                return JsonSerializer.Deserialize<List<RunewordDTO>>(redisCache.ToString());
            }

            var runes = runeNumbers.Split(",").Select(x => long.Parse(x)).ToHashSet();
            var matchingRunewords = new List<RunewordDTO>();

            foreach (var runeword in _database.Runewords.Include(r => r.RunewordRunes).ThenInclude(r => r.Rune).Include(r => r.Modifiers))
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

            await redisDB.StringSetAsync(new RedisKey(runeNumbers), new RedisValue(JsonSerializer.Serialize(matchingRunewords)));

            return matchingRunewords;
        }
    }
}
