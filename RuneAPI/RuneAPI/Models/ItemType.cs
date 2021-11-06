using System;

namespace RuneAPI.Models
{
    [Flags]
    public enum ItemType
    {
        Sword = 1,
        Axe = 2,
        Mace = 4,
        Staff = 8,
        MeleeWeapon = 16,
        BowsCrossbow = 32,
        Scepter = 64,
        Shield = 128,
        Club = 256,
        Hammer = 512,
        Wand = 1024,
        Polearm = 2048,
        RangedWeapon = 4096,
        Claws = 8192,
        BodyArmor = 16384,
        PaladinShield = 32768,
        Helmet = 65536
    }
}
