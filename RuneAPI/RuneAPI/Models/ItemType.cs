using System;
using System.ComponentModel;

namespace RuneAPI.Models
{
    [Flags]
    public enum ItemType
    {
        [Description("Swords")]
        Sword = 1,
        [Description("Axes")]
        Axe = 2,
        [Description("Maces")]
        Mace = 4,
        [Description("Staves")]
        Staff = 8,
        [Description("All Melee Weapons")]
        MeleeWeapon = 16,
        [Description("Bows & Crossbows")]
        BowsCrossbow = 32,
        [Description("Scepters")]
        Scepter = 64,
        [Description("All Shields")]
        Shield = 128,
        [Description("Clubs")]
        Club = 256,
        [Description("Hammers")]
        Hammer = 512,
        [Description("Wands")]
        Wand = 1024,
        [Description("Polearms")]
        Polearm = 2048,
        [Description("All Ranged Weapons")]
        RangedWeapon = 4096,
        [Description("Claws")]
        Claws = 8192,
        [Description("Body Armor")]
        BodyArmor = 16384,
        [Description("Paladin Shields")]
        PaladinShield = 32768,
        [Description("Helmets")]
        Helmet = 65536,
        [Description("Daggers")]
        Dagger = 131072
    }
}
