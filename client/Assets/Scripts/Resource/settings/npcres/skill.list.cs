
namespace game.resource.settings.npcres
{
    public class SkillList
    {
        public class NPCSKILL
        {
            public int SkillId;
            public int SkillLevel;
            public int MaxTimes;
            public int RemainTimes;
            public int NextCastTime;
            public int CurrentSkillLevel;
            public int AddPoint;
            public int EnChance;
        }

        public const int MAX_NPCSKILL = 80;

        /// skill.id => <...>
        public SkillList.NPCSKILL[] m_Skills;

        public SkillList()
        {
            this.m_Skills = new SkillList.NPCSKILL[SkillList.MAX_NPCSKILL];
        }

        public int FindSame(int nSkillID)
        {
            if (nSkillID == 0)
                return 0;

            for (int i = 1; i < MAX_NPCSKILL; i++)
            {
                if (m_Skills[i] != null 
                    && m_Skills[i].SkillId == nSkillID)
                {
                    return i;
                }
            }
            return 0;
        }

        public int GetCurrentLevel(int nSkillID)
        {
            int i;

            if (nSkillID == 0)
                return 0;

            i = FindSame(nSkillID);

            if (i != 0)
            {
                return m_Skills[i].CurrentSkillLevel;
            }

            return 0;
        }

        public void QeuipAddPoint(int nListIndex, int add)
        {
            m_Skills[nListIndex].AddPoint += add;
        }

        public bool IncreaseLevel(int nIdx, int nLvl, bool Qeuip)
        {
            if (nIdx <= 0 || nIdx >= MAX_NPCSKILL)
                return false;

            m_Skills[nIdx].SkillLevel += nLvl;
            m_Skills[nIdx].CurrentSkillLevel += nLvl;

            return true;
        }

        public int GetSkillId(int nListIndex)
        {
            if (nListIndex < MAX_NPCSKILL && nListIndex > 0)
                return m_Skills[nListIndex].SkillId;
            else
                return -1;
        }

        public void AddEnChance(int nListIndex, int add)
        {
            m_Skills[nListIndex].EnChance += add;
        }

        public int GetCurrentLevelByIdx(int nListIdx)
        {
            if (nListIdx > 0 && nListIdx < MAX_NPCSKILL)
                return m_Skills[nListIdx].CurrentSkillLevel;

            return 0;
        }

    }
}
