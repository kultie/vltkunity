
using System.Collections.Generic;

namespace game.resource.settings.skill
{
    public class CastInitiative : skill.CastMissile
    {
        protected List<settings.skill.Missile> CastInitiatives(settings.skill.Params.Cast castParams)
        {
            settings.npcres.Controller launcher = castParams.launcher.npc;
            settings.npcres.Controller target = castParams.target.npc;

            if (castParams.nParam1 != -1 && this.skillSetting.m_bTargetSelf != 0)
            {
                target = launcher;
            }
            else
            {
                if(target == null)
                {
                    return null;
                }

                //skill.Defination.NPC_RELATION Relation = NpcSet.GetRelation(nLauncher, nParam2);
                skill.Defination.NPC_RELATION Relation = Defination.NPC_RELATION.relation_all;

                if (this.skillSetting.m_bTargetEnemy != 0)
                {
                    if ((Relation & skill.Defination.NPC_RELATION.relation_enemy) != 0)
                        goto lab_processdamage;
                }

                if (this.skillSetting.m_bTargetAlly != 0)
                {
                    if ((Relation & skill.Defination.NPC_RELATION.relation_ally) != 0)
                        goto lab_processdamage;
                }

                if (this.skillSetting.m_bTargetSelf != 0)
                {
                    if ((Relation & skill.Defination.NPC_RELATION.relation_self) != 0)
                        goto lab_processdamage;
                }
                return null;
            }

        lab_processdamage: 
            {
                if(this.skillSetting.m_nStateSpecialId > 0)
                {
                    target.SetStateSkillEffect(this.skillSetting.m_nStateSpecialId);
                }

                //if (target.ReceiveDamage(
                //    launcher, 
                //    this.skillSetting.m_bIsMelee != 0, 
                //    this.skillSetting.m_DamageAttribs, 
                //    this.skillSetting.m_bUseAttackRate != 0, 
                //    this.skillSetting.m_bDoHurt != 0, 
                //    0))
                //{
                //    if (this.skillSetting.m_nStateAttribsNum > 0)
                //        target.SetStateSkillEffect(
                //            this.skillSetting.m_nStateSpecialId,
                //            this.skillSetting.m_StateAttribs, 
                //            this.skillSetting.m_nStateAttribsNum, 
                //            this.skillSetting.m_StateAttribs[0].nValue[1]);

                //    if (this.skillSetting.m_nImmediateAttribsNum > 0)
                //        target.SetImmediatelySkillEffect(
                //            this.skillSetting.m_ImmediateAttribs,
                //            this.skillSetting.m_nImmediateAttribsNum);
                //}

                return null;
            }
        }
    }
}

//if(castParams.launcher.npc == null)
//{
//    return null;
//}

//int m_StateGraphics = this.skillSetting.m_nStateSpecialId;

//if(m_StateGraphics == 0)
//{
//    return null;
//}

//skill.StateSetting.Data stateData = skill.StateSetting.Get(m_StateGraphics);

//if(stateData.m_nID <= 0)
//{
//    return null;
//}

//castParams.launcher.npc.AddStateSkillEffect(stateData);

//return null;
