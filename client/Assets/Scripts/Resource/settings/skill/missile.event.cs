
namespace game.resource.settings.skill.missile
{
    public class Event : skill.missile.Initialization
    {
        protected void FlyEvent()
        {
            OnMissleEvent(Defination.MissileEvent.Missle_FlyEvent);
        }

        protected bool OnMissleEvent(skill.Defination.MissileEvent usEvent)
        {
            if(this.skillParam.launcher.HaveData() == false)
            {
                return false;
            }

            int nEventSkillId = 0;
            int nEventSkillLevel = 0;
            switch (usEvent)
            {
                case skill.Defination.MissileEvent.Missle_FlyEvent: 
                    if (this.skillSetting.m_bFlyingEvent == 0 || this.skillSetting.m_nFlySkillId <= 0)
                        return false;

                    if (this.skillSetting.m_nEventSkillLevel == 0)
                        return false;

                    nEventSkillId = this.skillSetting.m_nFlySkillId;
                    if (this.skillSetting.m_nEventSkillLevel <= -1)
                        nEventSkillLevel = this.skillSetting.skillLevel;
                    else
                        nEventSkillLevel = this.skillSetting.m_nEventSkillLevel;

                    break;

                case skill.Defination.MissileEvent.Missle_StartEvent: 
                    if (this.skillSetting.m_bStartEvent == 0 || this.skillSetting.m_nStartSkillId <= 0)
                        return false;

                    if (this.skillSetting.m_nEventSkillLevel == 0)
                        return false;

                    nEventSkillId = this.skillSetting.m_nStartSkillId;

                    if (this.skillSetting.m_nEventSkillLevel <= -1)
                        nEventSkillLevel = this.skillSetting.skillLevel;
                    else
                        nEventSkillLevel = this.skillSetting.m_nEventSkillLevel;

                    break;

                case skill.Defination.MissileEvent.Missle_VanishEvent: 
                    if (this.skillSetting.m_bVanishedEvent == 0 || this.skillSetting.m_nVanishedSkillId <= 0)
                        return false;
                    if (this.skillSetting.m_nEventSkillLevel == 0)
                        return false;

                    nEventSkillId = this.skillSetting.m_nVanishedSkillId;

                    if (this.skillSetting.m_nEventSkillLevel <= -1)
                        nEventSkillLevel = this.skillSetting.skillLevel;
                    else
                        nEventSkillLevel = this.skillSetting.m_nEventSkillLevel;

                    break;

                case skill.Defination.MissileEvent.Missle_CollideEvent: 
                    if (this.skillSetting.m_bCollideEvent == 0 || this.skillSetting.m_nCollideSkillId <= 0)
                        return false;

                    if (this.skillSetting.m_nEventSkillLevel == 0)
                        return false;

                    nEventSkillId = this.skillSetting.m_nCollideSkillId;


                    if (this.skillSetting.m_nEventSkillLevel <= -1)
                        nEventSkillLevel = this.skillSetting.skillLevel;
                    else
                        nEventSkillLevel = this.skillSetting.m_nEventSkillLevel;

                    break;
                default:
                    return false;
            }

            int nDesPX = 0, nDesPY = 0;

            if (this.skillSetting.m_bByMissle != 0)
            {
                resource.map.Position missilePosition = this.texture.GetMapPosition();
                nDesPX = missilePosition.left;
                nDesPY = missilePosition.top;
            }
            else
            {
                if(this.skillParam.launcher.npc == null)
                {
                    return false;
                }

                resource.map.Position npcPosition = this.skillParam.launcher.npc.GetMapPosition();
                nDesPX = npcPosition.left;
                nDesPY = npcPosition.top;
            }

            if(this.skillSetting.m_bByMissle != 0)
            {
                this.map.CastSkill(nEventSkillId, nEventSkillLevel, this.self, nDesPX, nDesPY, 0, Defination.eSkillLauncherType.SKILL_SLT_Missle);
            }
            else
            {
                this.map.CastSkill(nEventSkillId, nEventSkillLevel, this.skillParam.launcher.npc, nDesPX, nDesPY, 0, Defination.eSkillLauncherType.SKILL_SLT_Npc);
            }

            return true;
        }
    }
}
