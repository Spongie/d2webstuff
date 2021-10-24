using Microsoft.AspNetCore.Mvc;
using RuneAPI.Database;
using RuneAPI.Models;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<Runeword> GetAll()
        {
            var runes = database.Runes;

            //database.Runewords.Add(new Runeword
            //{
            //    Name = "Steel",
            //    Modifiers = new Modifier[]
            //    {
            //        "+25% Increased Attack Speed",
            //        "+20% Enhanced Damage",
            //        "+3 to Minimum Damage",
            //        "+3 to Maximum Damage",
            //        "+50 to Attack Rating",
            //        "50% Chance of Open Wounds",
            //        "+2 to Mana after each Kill",
            //        "+1 to Light Radius"
            //    },
            //    Runes = new Rune[] {runes.FirstOrDefault(r => r.Name == "Tir"), runes.FirstOrDefault(r => r.Name == "El") },
            //    TargetTypes = ItemType.Sword | ItemType.Axe | ItemType.Mace
            //});

            //database.Runewords.Add(new Runeword
            //{
            //    Name = "Malice",
            //    Modifiers = new Modifier[]
            //    {
            //        "+33% Enhanced Damage",
            //        "+9 to Maximum Damage",
            //        "-25% Target Defense",
            //        "+50 to Attack Rating",
            //        "100% Chance of Open wounds",
            //        "Prevent Monster Heal",
            //        "-100 to Monster Defense Per Hit",
            //        "Drain Life -5"
            //    },
            //    Runes = new Rune[] { runes.FirstOrDefault(r => r.Name == "Ith"), runes.FirstOrDefault(r => r.Name == "El"), runes.FirstOrDefault(r => r.Name == "Eth") },
            //    TargetTypes = ItemType.Sword | ItemType.Axe | ItemType.Mace
            //});

            return database.Runewords;
        }

        [HttpGet]
        [Route("[controller]/search")]
        public IEnumerable<Runeword> Search(string runeNumbers)
        {
            var runes = runeNumbers.Split(",").Select(x => int.Parse(x)).ToHashSet();
            var matchingRunewords = new List<Runeword>();

            foreach (var runeword in database.Runewords)
            {
                int hits = 0;

                foreach (var rune in runeword.Runes.Select(r => r.Number).ToArray())
                {
                    if (!runes.Contains(rune))
                    {
                        break;
                    }

                    hits++;
                }

                if (hits == runeword.Runes.Count)
                {
                    matchingRunewords.Add(runeword);
                }
            }

            return matchingRunewords;
        }
    }
}
