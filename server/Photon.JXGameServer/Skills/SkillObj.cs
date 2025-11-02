using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Modulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.JXGameServer.Skills
{
    public class SkillObj
    {
        public int id;

        byte m_level;
        public byte level
        {
            get { return m_level; }
            set 
            { 
                m_level = value;
                skillTemplate = SkillModule.Me.GetSkillTemplate(id, value);
            }
        }
            
        
        public int NextCastTime;
        public int NextHorseCastTime;

        public SkillTemplate skillTemplate;

        public SkillObj(int id, byte lvl)
        {
            this.id = id;
            this.level = lvl;

            this.NextCastTime = this.NextHorseCastTime = 0;

        }
        public bool CanCast(CharacterObj obj)
        {
            if (obj.IsPlayer)
            {
                switch (skillTemplate.m_nSkillCostType)
                {
                    case ShareLibrary.Constant.eNpcAttrib.attrib_life:
                        return obj.HPCur >= skillTemplate.m_nCostValue;
                    case ShareLibrary.Constant.eNpcAttrib.attrib_mana:
                        return obj.MPCur >= skillTemplate.m_nCostValue;
                    case ShareLibrary.Constant.eNpcAttrib.attrib_stamina:
                        return obj.SPCur >= skillTemplate.m_nCostValue;
                }
                return true;
            }
            return false;
        }
        public void SaveNextTime(CharacterObj obj,int dwTime)
        {
            this.NextCastTime = dwTime + this.skillTemplate.m_nWaitTime;
            if (obj.IsPlayer)
            {
                switch (skillTemplate.m_nSkillCostType)
                {
                    case ShareLibrary.Constant.eNpcAttrib.attrib_life:
                        obj.HPCur -= skillTemplate.m_nCostValue;
                        break;
                    case ShareLibrary.Constant.eNpcAttrib.attrib_mana:
                        obj.MPCur -= skillTemplate.m_nCostValue;
                        break;
                    case ShareLibrary.Constant.eNpcAttrib.attrib_stamina:
                        obj.SPCur -= skillTemplate.m_nCostValue;
                        break;
                }
            }
        }
    }
}
