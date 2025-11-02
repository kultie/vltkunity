
using System.Collections.Generic;

namespace game.resource.settings.skill
{
    public class StateSetting
    {
        public class Data
        {
            public int m_nID;
            public settings.skill.Defination.StateMagicType m_nType;
            public int m_nPlayType;
            public int m_nBackStart;
            public int m_nBackEnd;
            public int m_nTotalFrame;
            public int m_nTotalDir;
            public int m_nInterVal;
            public string m_szName;
        }

        private static readonly StateSetting.Data emptyState = new StateSetting.Data();

        public static StateSetting.Data Get(int stateId)
        {
            if(Cache.Settings.Skill.stateMagicTable.ContainsKey(stateId) == false)
            {
                return StateSetting.emptyState;
            }

            return Cache.Settings.Skill.stateMagicTable[stateId];
        }

        public static Dictionary<int, StateSetting.Data> Load()
        {
            Dictionary<int, StateSetting.Data> result = new Dictionary<int, StateSetting.Data>();
            resource.Table dataTable = Game.Resource(mapping.settings.NpcRes.stateMagicTable).Get<resource.Table>();

            if(dataTable.IsEmpty())
            {
                return result;
            }

            for(int rowIndex = 1; rowIndex < dataTable.RowCount; rowIndex++)
            {
                StateSetting.Data data = new StateSetting.Data();

                string typeString = dataTable.Get<string>(2, rowIndex);

                if(typeString.CompareTo("Head") == 0)
                {
                    data.m_nType = Defination.StateMagicType.STATE_MAGIC_HEAD;
                }
                else if (typeString.CompareTo("Foot") == 0)
                {
                    data.m_nType = Defination.StateMagicType.STATE_MAGIC_FOOT;
                }
                else
                {
                    data.m_nType = Defination.StateMagicType.STATE_MAGIC_BODY;
                }

                string playTypeString = dataTable.Get<string>(3, rowIndex);

                if(playTypeString.CompareTo("Loop") == 0)
                {
                    data.m_nPlayType = 0;
                }
                else
                {
                    data.m_nPlayType = 1;
                }

                data.m_nBackStart = dataTable.Get<int>(4, rowIndex);
                data.m_nBackEnd = dataTable.Get<int>(5, rowIndex);
                data.m_nTotalFrame = dataTable.Get<int>(6, rowIndex);
                data.m_nTotalDir = dataTable.Get<int>(7, rowIndex);
                data.m_nInterVal = dataTable.Get<int>(8, rowIndex);
                data.m_szName = dataTable.Get<string>(1, rowIndex);
                data.m_nID = rowIndex;

                result[rowIndex] = data;
            }

            return result;
        }
    }
}
