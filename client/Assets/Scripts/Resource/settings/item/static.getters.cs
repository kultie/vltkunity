
using System.Collections.Generic;

namespace game.resource.settings.item
{
    class Getters
    {
        public class Appearance
        {
            public static int Line(string _partName, int _declareLine)
            {
                if (Cache.Settings.Item.appearanceParsedMapping.ContainsKey(_partName) == false) return 0;
                if (Cache.Settings.Item.appearanceParsedMapping[_partName].ContainsKey(_declareLine) == false) return 0;

                return Cache.Settings.Item.appearanceParsedMapping[_partName][_declareLine];
            }

            public static int ArmorLine(int _declareLine) => Appearance.Line(mapping.settings.Item.armorres, _declareLine);

            public static int HelmLine(int _declareLine) => Appearance.Line(mapping.settings.Item.helmres, _declareLine);

            public static int HorseLine(int _declareLine) => Appearance.Line(mapping.settings.Item.horseres, _declareLine);

            public static int MeleWeaponLine(int _declareLine) => Appearance.Line(mapping.settings.Item.meleeres, _declareLine);

            public static int RangeWeaponLine(int _declareLine) => Appearance.Line(mapping.settings.Item.rangeres, _declareLine);
        }

        public static settings.item.EquipmentBase GetEquipmentBase(int g, int d, int p, int l)
        {
            string key = string.Empty + g + ", " + d + ", " + p + ", " + l;

            if (Cache.Settings.Item.equipmentBaseMapping.ContainsKey(key) == false)
            {
                return null;
            }

            return Cache.Settings.Item.equipmentBaseMapping[key];
        }

        public static settings.item.EquipmentBase GetMaskBase(int g, int d, int p)
        {
            string key = string.Empty + g + ", " + d + ", " + p;

            if(Cache.Settings.Item.maskEquipBase.ContainsKey(key) == false)
            {
                return null;
            }

            return Cache.Settings.Item.maskEquipBase[key];
        }

        public static settings.item.MagicScriptBase GetMagicScriptBase(int g, int d, int p)
        {
            string key = string.Empty + g + ", " + d + ", " + p;

            if(Cache.Settings.Item.magicScriptBase.ContainsKey(key) == false)
            {
                return null;
            }

            return Cache.Settings.Item.magicScriptBase[key];
        }

        /// <summary>
        /// position: 1: show, 0: hide
        /// return nPropKind mapping
        /// </summary>
        public static Dictionary<int, Dictionary<int, settings.item.MagicattribBase>> GetMagicAttribBase(int detail, int series, int position)
        {
            // ["detail, series, position"]
            string key = string.Empty + detail + ", " + series + ", " + position;

            if (Cache.Settings.Item.magicAttribBaseMapping.ContainsKey(key) == false)
            {
                return null;
            }

            return Cache.Settings.Item.magicAttribBaseMapping[key];
        }

        public static settings.item.GoldEquipBase GetGoldEquipBase(int index)
        {
            if(Cache.Settings.Item.goldEquipBase.ContainsKey(index) == false)
            {
                return null;
            }

            return Cache.Settings.Item.goldEquipBase[index];
        }

        public static settings.item.GoldMagicBase GetGoldMagicBase(int index)
        {
            if(Cache.Settings.Item.goldMagicBase.ContainsKey(index) == false)
            {
                return null;
            }

            return Cache.Settings.Item.goldMagicBase[index];
        }

        public static Dictionary<int, string> GetGoldItemSet(int idSet)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            if(Cache.Settings.Item.goldEquipSet.ContainsKey(idSet) == false)
            {
                return result;
            }

            List<int> setList = Cache.Settings.Item.goldEquipSet[idSet];

            for(int i = 0; i < setList.Count; i++)
            {
                int setElementIndex = (setList[i] - (setList[i] % 100)) / 100;
                result[setElementIndex] = item.Getters.GetGoldEquipBase(setElementIndex).name;
            }

            return result;
        }

        public static int GetGoldEquipRes(int goldEquipRowIndex)
        {
            if(Cache.Settings.Item.goldEquipRes.ContainsKey(goldEquipRowIndex) == false)
            {
                return 0;
            }

            return Cache.Settings.Item.goldEquipRes[goldEquipRowIndex].resId;
        }
    }
}
