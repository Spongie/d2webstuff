using Microsoft.AspNetCore.Mvc;
using RuneAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

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
        [Route("api/[controller]")]
        public IEnumerable<EnumNameValue> GetAll()
        {
            var result = new List<EnumNameValue>();

            foreach (var item in Enum.GetValues<ItemType>())
            {
                result.Add(new EnumNameValue { Value = (int)item, Name = EnumUtils.GetDescription(item) });
            }

            return result;
        }
    }

    public static class EnumUtils
    {
        public static string GetDescription<T>(T enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return enumValue.ToString();
        }
    }
}
