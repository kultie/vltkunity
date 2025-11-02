
using System;

namespace game.resource.settings.skill
{
    public class SkillSettingBase : SkillSettingData
    {
        protected void LoadBase(int skillId)
        {
            if (Cache.Settings.Skill.skillsIdToRowIndexMapping.ContainsKey(skillId) == false)
            {
                return;
            }

            this.InitSettingData();

            int rowIndex = Cache.Settings.Skill.skillsIdToRowIndexMapping[skillId];

            this.m_nId = skillId;
            this.m_szName = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.SkillName, rowIndex);
            this.m_property = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.Property, rowIndex);
            this.m_nAttrib = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.Attrib, rowIndex);
            this.m_usReqLevel = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ReqLevel, rowIndex);
            this.m_maxLevel = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.MaxLevel, rowIndex);
            this.m_nEquiptLimited = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.EqtLimit, rowIndex);
            this.m_nHorseLimited = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.HorseLimit, rowIndex);
            this.m_bDoHurt = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.DoHurt, rowIndex);
            this.m_nChildSkillNum = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ChildSkillNum, rowIndex);
            this.m_eMisslesForm = (skill.Defination.MisslesForm)Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.MisslesForm, rowIndex);
            this.m_nCharClass = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.CharClass, rowIndex);
            this.m_eSkillStyle = (skill.Defination.SKillStyle)Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.SkillStyle, rowIndex);
            this.m_nCharActionId = (skill.Defination.CLIENTACTION)Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.CharAnimId, rowIndex);
            this.m_bIsAura = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.IsAura, rowIndex);
            this.m_bUseAttackRate = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.IsUseAR, rowIndex);
            this.m_bTargetOnly = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TargetOnly, rowIndex);
            this.m_bTargetEnemy = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TargetEnemy, rowIndex);
            this.m_bTargetAlly = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TargetAlly, rowIndex);
            this.m_bTargetObj = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TargetObj, rowIndex);
            this.m_bTargetOther = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TargetOther, rowIndex);
            this.m_bTargetNoNpc = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TargetNoNpc, rowIndex);
            this.m_bBaseSkill = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.BaseSkill, rowIndex);
            this.m_bByMissle = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ByMissle, rowIndex);
            this.m_nChildSkillId = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ChildSkillId, rowIndex);
            this.m_bFlyingEvent = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.FlyEvent, rowIndex);
            this.m_bStartEvent = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.StartEvent, rowIndex);
            this.m_bCollideEvent = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.CollideEvent, rowIndex);
            this.m_bVanishedEvent = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.VanishedEvent, rowIndex);
            this.m_nFlySkillId = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.FlySkillId, rowIndex);
            this.m_nStartSkillId = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.StartSkillId, rowIndex);
            this.m_nVanishedSkillId = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.VanishedSkillId, rowIndex);
            this.m_nCollideSkillId = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.CollidSkillId, rowIndex);
            this.m_nSkillCostType = (skill.Defination.NPCATTRIB)Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.SkillCostType, rowIndex);
            this.m_nCost = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.CostValue, rowIndex);
            this.m_nMinTimePerCast = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TimePerCast, rowIndex);
            this.m_nMinTimePerCastOnHorse = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TimePerCastOnHorse, rowIndex);
            this.m_nValue1 = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.Param1, rowIndex);
            this.m_nValue2 = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.Param2, rowIndex);
            this.m_nChildSkillLevel = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ChildSkillLevel, rowIndex);
            this.m_nEventSkillLevel = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.EventSkillLevel, rowIndex);
            this.m_bIsMelee = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.IsMelee, rowIndex);
            this.m_nFlyEventTime = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.FlyEventTime, rowIndex);
            this.m_nShowEvent = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ShowEvent, rowIndex);
            this.m_eMisslesGenerateStyle = (skill.Defination.MisslesGenerateStyle)Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.MslsGenerate, rowIndex);
            this.m_nMisslesGenerateData = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.MslsGenerateData, rowIndex);
            this.m_nMaxShadowNum = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.MaxShadowNum, rowIndex);
            this.m_nAttackRadius = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.AttackRadius, rowIndex);
            this.m_nWaitTime = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.WaitTime, rowIndex);
            this.m_bClientSend = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ClientSend, rowIndex);
            this.m_bTargetSelf = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.TargetSelf, rowIndex);
            this.m_nInteruptTypeWhenMove = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.StopWhenMove, rowIndex);
            this.m_bHeelAtParent = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.HeelAtParent, rowIndex);
            this.m_nIsExpSkill = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.IsExpSkill, rowIndex);
            this.m_nSeries = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.Series, rowIndex);
            this.m_nShowAddition = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.ShowAddition, rowIndex);
            this.m_bIsPhysical = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.IsPhysical, rowIndex);
            this.m_nStateSpecialId = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.StateSpecialId, rowIndex);
            this.m_maxLevel = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.MaxLevel, rowIndex);
            this.m_property = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.Property, rowIndex);

            m_eRelation = 0;
            if (this.m_bTargetEnemy != 0)
                m_eRelation |= (int)skill.Defination.NPC_RELATION.relation_enemy;

            if (this.m_bTargetAlly != 0)
                m_eRelation |= (int)skill.Defination.NPC_RELATION.relation_ally;

            if (this.m_bTargetSelf != 0)
                m_eRelation |= (int)skill.Defination.NPC_RELATION.relation_self;

            if (this.m_bTargetOther != 0)
            {
                m_eRelation |= (int)skill.Defination.NPC_RELATION.relation_dialog;
                m_eRelation |= (int)skill.Defination.NPC_RELATION.relation_none;
            }

            this.m_szSkillDesc = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.SkillDesc, rowIndex);
            this.m_bNeedShadow = Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.NeedShadow, rowIndex);
            this.m_szSkillIcon = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.SkillIcon, rowIndex);
            this.m_eLRSkillInfo = (skill.Defination.SkillLRInfo)Cache.Settings.Skill.skillsTable.Get<int>((int)mapping.settings.Skill.HeaderIndexer.LRSkill, rowIndex);
            this.m_szPreCastEffectFile = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.PreCastSpr, rowIndex);
            this.m_szManPreCastSoundFile = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.ManCastSnd, rowIndex);
            this.m_szFMPreCastSoundFile = Cache.Settings.Skill.skillsTable.Get<string>((int)mapping.settings.Skill.HeaderIndexer.FMCastSnd, rowIndex);

            if (this.m_szSkillIcon == string.Empty)
            {
                this.m_szSkillIcon = "\\spr\\skill\\ͼ��\\ͨ��.spr";
            }

            if (Cache.Settings.Skill.skillsTable.GetEncoding().byteOrderMarks == 0)
            {
                this.m_szName = formater.TCVN3.UTF8(this.m_szName);
                this.m_property = formater.TCVN3.UTF8(this.m_property);
                this.m_szSkillDesc = formater.TCVN3.UTF8(this.m_szSkillDesc);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        public int GetMissleGenerateTime(int nNo)
        {
            switch (this.m_eMisslesGenerateStyle)
            {
                case skill.Defination.MisslesGenerateStyle.SKILL_MGS_NULL:
                    {
                        return this.m_nWaitTime;
                    }

                case skill.Defination.MisslesGenerateStyle.SKILL_MGS_SAMETIME:
                    {
                        return this.m_nWaitTime + this.m_nMisslesGenerateData;
                    }

                case skill.Defination.MisslesGenerateStyle.SKILL_MGS_ORDER:
                    {
                        return this.m_nWaitTime + nNo * this.m_nMisslesGenerateData;
                    }

                case skill.Defination.MisslesGenerateStyle.SKILL_MGS_RANDONORDER:
                    {
                        if (skill.Static.g_Random(2) == 1)
                            return this.m_nWaitTime + nNo * this.m_nMisslesGenerateData + skill.Static.g_Random(this.m_nMisslesGenerateData);
                        else
                            return this.m_nWaitTime + nNo * this.m_nMisslesGenerateData - skill.Static.g_Random(this.m_nMisslesGenerateData / 2);
                    }

                case skill.Defination.MisslesGenerateStyle.SKILL_MGS_RANDONSAME:
                    {
                        return this.m_nWaitTime + skill.Static.g_Random(this.m_nMisslesGenerateData);
                    }

                case skill.Defination.MisslesGenerateStyle.SKILL_MGS_CENTEREXTENDLINE:
                    {
                        if (this.m_nChildSkillNum <= 1) return this.m_nWaitTime;
                        int nCenter = this.m_nChildSkillNum / 2;
                        return this.m_nWaitTime + Math.Abs(nNo - nCenter) * this.m_nMisslesGenerateData;
                    }
            }
            return this.m_nWaitTime;
        }
    }
}
