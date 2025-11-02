
using System.Collections.Generic;

namespace game.resource.settings.npcres.special
{
    class Part
    {
        public static List<string> AllPartList()
        {
            return new List<string>
            {
                mapping.settings.NpcRes.Kind.Header.head,
                mapping.settings.NpcRes.Kind.Header.hair,
                mapping.settings.NpcRes.Kind.Header.shoulder,
                mapping.settings.NpcRes.Kind.Header.body,
                mapping.settings.NpcRes.Kind.Header.leftHand,
                mapping.settings.NpcRes.Kind.Header.rightHand,
                mapping.settings.NpcRes.Kind.Header.leftWeapon,
                mapping.settings.NpcRes.Kind.Header.rightWeapon,
                mapping.settings.NpcRes.Kind.Header.horseFront,
                mapping.settings.NpcRes.Kind.Header.horseMiddle,
                mapping.settings.NpcRes.Kind.Header.horseBack,
                mapping.settings.NpcRes.Kind.Header.mantle,
            };
        }

        public static List<string> AllTabFileList()
        {
            List<string> result = new()
            {
                mapping.settings.NpcRes.Kind.Header.partFileName,
                mapping.settings.NpcRes.Kind.Header.weaponActionTab1,
                mapping.settings.NpcRes.Kind.Header.weaponActionTab2,
            };

            result.AddRange(special.Part.AllPartList());
            return result;
        }

        public static List<string> AllIniFileList()
        {
            return new List<string>
            {
                mapping.settings.NpcRes.Kind.Header.actionRenderOrderTab,
            };
        }

        public static Dictionary<int, string> AllPartId()
        {
            return new Dictionary<int, string>
            {
                {-1, mapping.settings.NpcRes.Shadow.partName},
                {0, mapping.settings.NpcRes.Kind.Header.head},
                {1, mapping.settings.NpcRes.Kind.Header.hair},
                {4, mapping.settings.NpcRes.Kind.Header.shoulder},
                {5, mapping.settings.NpcRes.Kind.Header.body},
                {6, mapping.settings.NpcRes.Kind.Header.leftHand},
                {7, mapping.settings.NpcRes.Kind.Header.rightHand},
                {8, mapping.settings.NpcRes.Kind.Header.leftWeapon},
                {9, mapping.settings.NpcRes.Kind.Header.rightWeapon},
                {12, mapping.settings.NpcRes.Kind.Header.horseFront},
                {13, mapping.settings.NpcRes.Kind.Header.horseMiddle},
                {14, mapping.settings.NpcRes.Kind.Header.horseBack},
                {16, mapping.settings.NpcRes.Kind.Header.mantle},
            };
        }
    }
}
