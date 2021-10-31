using Microsoft.AspNetCore.Mvc;
using RuneAPI.Models;
using System;
using System.Collections.Generic;

namespace RuneAPI.Controllers
{
    public struct EnumNameValue
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        [HttpGet]
        [Route("[controller]")]
        public IEnumerable<EnumNameValue> GetAll()
        {
            var result = new List<EnumNameValue>();

            foreach (var item in Enum.GetValues<ItemType>())
            {
                result.Add(new EnumNameValue { Value = (int)item, Name = Enum.GetName<ItemType>(item) });
            }

            return result;
        }
    }
}
