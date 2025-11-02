using Photon.ShareLibrary.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Photon.JXGameServer.Skills
{
    public class SkillList
    {
        public List<SkillObj> m_SkillList;

        public SkillList(List<PlayerSkill> skillList) 
        {
            m_SkillList = skillList.Select(x => x == null ? null : new SkillObj(x.id, (byte)x.level)).ToList();
        }
        public SkillObj GetSkillById(int id)
        {
            return m_SkillList.FirstOrDefault(x => x.id == id);
        }
        public SkillObj GetSkillByIndex(int idx)
        {
            if ((idx <= 0) || (idx > m_SkillList.Count))
                return null;

            return m_SkillList[idx - 1];
        }
        public void SetSkillAtIndex(byte idx,int id,byte level)
        {
            while (idx > m_SkillList.Count)
                m_SkillList.Add(null);

            m_SkillList[idx - 1] = new SkillObj(id, level);
        }
    }
}
