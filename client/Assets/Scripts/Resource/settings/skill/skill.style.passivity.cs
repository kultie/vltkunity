
using System.Collections.Generic;

namespace game.resource.settings.skill
{
    public class CastPassivity : skill.CastInitiative
    {
        protected List<settings.skill.Missile> CastPassivitys(settings.skill.Params.Cast castParams)
        {
            if(this.skillSetting.m_nStateSpecialId > 0)
            {
                castParams.launcher.npc.SetStateSkillEffect(this.skillSetting.m_nStateSpecialId);
            }

            //if (this.skillSetting.m_nStateAttribsNum > 0)
            //{
            //    castParams.launcher.npc.SetStateSkillEffect(
            //        this.skillSetting.m_nStateSpecialId,
            //        this.skillSetting.m_StateAttribs, 
            //        this.skillSetting.m_nStateAttribsNum, 
            //        -1);
            //}

            return null;
        }
    }
}

//if (castParams.launcher.npc == null)
//{
//    return null;
//}

//int m_StateGraphics = this.skillSetting.m_nStateSpecialId;

//if (m_StateGraphics == 0)
//{
//    return null;
//}

//skill.StateSetting.Data stateData = skill.StateSetting.Get(m_StateGraphics);

//if (stateData.m_nID <= 0)
//{
//    return null;
//}

//castParams.launcher.npc.AddStateSkillEffect(stateData);

//return null;