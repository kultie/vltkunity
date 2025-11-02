using IniParser;
using IniParser.Model;

namespace Photon.ShareLibrary.Utils
{
    /*
    #define SECTION_ROLE		"ROLE"	
    #define SECTION_ITEMLIST	"ITEMS"	
    #define SECTION_ITEM		"ITEM" // + id
    #define KEY_COUNT			"COUNT"
    #define KEY_BASELIST		"BLISTS"
    #define KEY_EFFICLIST		"ELISTS"
    #define KEY_REQUIRELIST		"RLISTS"

    #define SECTION_BASEINFO	"IB" //IB1_2
    #define SECTION_EFFICEINFO	"IE"
    #define SECTION_REQUIREINFO	"IR"

    #define SECTION_FRIENDLIST	"FRIENDS"
    #define KEY_FRIEND			"F" // + id

    #define SECTION_TASKLIST	"TASKS"
    #define KEY_TASKID			"T" // + id
    #define KEY_TASKVALUE		"V" // + id

    #define SECTION_FIGHTSKILLLIST		"FSKILLS"
    #define KEY_FIGHTSKILL		"S"
    #define KEY_FIGHTSKILLLEVEL	"L"

    #define SECTION_LIFESKILLLIST		"LSKILLS"
    #define KEY_LIFESKILL				"S" // + id
    #define	KEY_LIFESKILLLEVEL			"L" // + id
    */
    public struct IniSection
    {
        public static string SECTION_ROLE = "ROLE";
        public static string SECTION_ITEMLIST = "ITEMS";
        public static string SECTION_ITEM = "ITEM"; // +id
        public static string KEY_COUNT = "COUNT";
        public static string KEY_BASELIST = "BLISTS";
        public static string KEY_EFFICLIST = "ELISTS";
        public static string KEY_REQUIRELIST = "RLISTS";
        public static string SECTION_BASEINFO = "IB";
        public static string SECTION_EFFICEINFO = "IE";
        public static string SECTION_REQUIREINFO = "IR";
        public static string SECTION_FRIENDLIST = "FRIENDS";
        public static string KEY_FRIEND = "F"; // +id
        public static string SECTION_TASKLIST = "TASKS";
        public static string KEY_TASKID = "T"; // + id
        public static string KEY_TASKVALUE = "V"; // + id
        public static string SECTION_FIGHTSKILLLIST = "FSKILLS";
        public static string KEY_FIGHTSKILL = "S";
        public static string KEY_FIGHTSKILLLEVEL = "L";
        public static string SECTION_LIFESKILLLIST = "LSKILLS";
        public static string KEY_LIFESKILL = "S"; // + id
        public static string KEY_LIFESKILLLEVEL = "L";// + id
    }
    public class IniFile
    {
        IniData data = null;
        public IniFile(string path)
        {
            var parser = new FileIniDataParser();
            data = parser.ReadFile(path);
        }

        public SectionDataCollection Sections { get => data.Sections; }

        public int ReadInteger(string section, string key, int defaultValue = 0)
        {
            string val = data[section][key];
            if (val == null) return defaultValue;
            try
            {
                return int.Parse(val);
            }
            catch
            {
                return defaultValue;
            }
        }

        public string ReadString(string section, string key, string defaultValue = "")
        {
            return data[section][key] ?? defaultValue;
        }
        public void GetRevivalPosFromId(int dwSubWorldId, int nRevivalId, ref int x, ref int y)
        {
            var str = ReadString($"{dwSubWorldId}", $"{nRevivalId}");

            var strs = str.Split(',');

            x = int.Parse(strs[0]);
            y = int.Parse(strs[1]);
        }
        public void GetRevivalPosRandIdx(int dwSubWorldId, ref int r,ref int x,ref int y)
        {
            var str = ReadString($"{dwSubWorldId}", $"region");

            var strs = str.Split(',');

            r = KRandom.GetRandomNumber(int.Parse(strs[0]), int.Parse(strs[1]));
            str = ReadString($"{dwSubWorldId}", $"{r}");

            strs = str.Split(',');

            x = int.Parse(strs[0]);
            y = int.Parse(strs[1]);
        }
    }
}