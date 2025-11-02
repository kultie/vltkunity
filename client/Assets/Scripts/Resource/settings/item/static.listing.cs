
using System.Collections.Generic;

namespace game.resource.settings.item
{
    class Listing
    {
        public static List<string> Equipment()
        {
            return new List<string>()
            {
                mapping.settings.Item.amulet,
                mapping.settings.Item.armor,
                mapping.settings.Item.belt,
                mapping.settings.Item.boot,
                mapping.settings.Item.cuff,
                mapping.settings.Item.helm,
                mapping.settings.Item.horse,
                mapping.settings.Item.meleeweapon,
                mapping.settings.Item.pendant,
                mapping.settings.Item.rangeweapon,
                mapping.settings.Item.ring,
            };
        }

        public static List<string> Appearance()
        {
            return new List<string>()
            {
                mapping.settings.Item.armorres,
                mapping.settings.Item.helmres,
                mapping.settings.Item.horseres,
                mapping.settings.Item.meleeres,
                mapping.settings.Item.rangeres,
            };
        }
    }
}
