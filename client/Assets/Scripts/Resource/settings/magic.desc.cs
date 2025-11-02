
using static game.resource.settings.skill.SkillSettingData;
using System.Collections.Generic;

namespace game.resource.settings
{
    public class MagicDesc
    {
        public class Table
        {
            public int id;
            public string key;
            public string desc;
        }

        public static void Initialize()
        {
            resource.Cache.Settings.MagicDesc.id = new System.Collections.Generic.Dictionary<int, Table>();
            resource.Cache.Settings.MagicDesc.key = new System.Collections.Generic.Dictionary<string, Table>();

            resource.Table table = Game.Resource(resource.mapping.Settings.magicDescTable).Get<resource.Table>();

            for(int rowIndex = 1; rowIndex < table.RowCount; rowIndex++)
            {
                settings.MagicDesc.Table newField = new Table();
                newField.id = table.Get<int>(0, rowIndex);
                newField.key = table.Get<string>(1, rowIndex);
                newField.desc = table.Get<string>(2, rowIndex);

                resource.Cache.Settings.MagicDesc.id[newField.id] = newField;
                resource.Cache.Settings.MagicDesc.key[newField.key] = newField;
            }
        }

        public static string IdToKey(int magicId)
        {
            if(resource.Cache.Settings.MagicDesc.id.ContainsKey(magicId) == false)
            {
                return null;
            }

            return resource.Cache.Settings.MagicDesc.id[magicId].key;
        }

        public static int KeyToId(string key)
        {
            if(resource.Cache.Settings.MagicDesc.key.ContainsKey(key) == false)
            {
                return -1;
            }

            return resource.Cache.Settings.MagicDesc.key[key].id;
        }

        public static string Get(settings.skill.SkillSettingData.KMagicAttrib magicAttrib)
        {
            if(resource.Cache.Settings.MagicDesc.id.ContainsKey(magicAttrib.nAttribType) == false)
            {
                return "<không xác định: " + magicAttrib.nAttribType + ", table>";
            }

            settings.MagicDesc.Table magicDesc = resource.Cache.Settings.MagicDesc.id[magicAttrib.nAttribType];
            string keyDesc = settings.item.Getter.GetRichText(magicDesc.desc);
            string result = string.Empty;

            if(keyDesc == string.Empty || keyDesc == null)
            {
                return "<không xác định: " + magicAttrib.nAttribType + ", desc>";
            }

            for (int charIndex = 0; charIndex < keyDesc.Length;)
            {
                char charEntry = keyDesc[charIndex];

                if (charEntry != '#')
                {
                    result += charEntry;
                    charIndex++;
                    continue;
                }

                char charDataType = keyDesc[charIndex += 1];
                char charValue = keyDesc[charIndex += 1];
                char charAddType = keyDesc[charIndex += 1];

                int dataValue = 0;
                string dataString = string.Empty;

                switch (charValue)
                {
                    case '1': dataValue = magicAttrib.nValue[0]; break;
                    case '2': dataValue = magicAttrib.nValue[1]; break;
                    case '3': dataValue = magicAttrib.nValue[2]; break;
                }

                switch (charDataType)
                {
                    case 'm':
                        string[] faction = new[] { "thiếu lâm", "thiên vương", "đường môn", "ngũ độc", "nga mi", "thúy yên", "cái bang", "thiên nhẫn", "võ đang", "côn lôn" };
                        dataString += faction[dataValue];
                        break;

                    case 's':
                        string[] series = new[] { "kim", "mộc", "thủy", "hỏa", "thổ" };
                        dataString += series[dataValue];
                        break;

                    case 'k':
                        string[] type = new[] { "nội lực", "sinh lực", "thể lực", "ngân lượng" };
                        dataString += type[dataValue];
                        break;

                    case 'd':
                        if(charAddType == '+')
                        {
                            dataString += "+";
                        }
                        else if (charAddType == '~')
                        {
                            dataString += "-";
                        }

                        dataString += System.Math.Abs(dataValue);
                        break;

                    case 'x':
                        dataString += (dataValue != 0) ? "nữ" : "nam";
                        break;

                    case 'l':
                        dataString += settings.skill.SkillSetting.Get(dataValue, 1).m_szName;
                        dataString += charAddType;
                        break;
                }

                result += dataString;
                charIndex++;
            }

            return result;
        }
    }
}
