
using System.Collections.Generic;

namespace game.resource.settings.skill.cast
{
    public class Wall : skill.cast.Line
    {
        protected List<settings.skill.Missile> CastWall(settings.skill.Params.TOrdinSkillParam pSkillParam, int nDir, int nRefPX, int nRefPY)
        {
            List<settings.skill.Missile> result = new List<settings.skill.Missile>();
            
            skill.Defination.eSkillLauncherType eLauncherType = pSkillParam.launcher.type;

            if (eLauncherType != skill.Defination.eSkillLauncherType.SKILL_SLT_Npc)
            {
                return result;
            }

            int nDirIndex = skill.Static.g_Dir2DirIndex(nDir, skill.Defination.MaxMissleDir);
            int nDesSubX = 0;
            int nDesSubY = 0;

            //子弹之间的间距
            int nMSDistanceEach = this.skillSetting.m_nValue1;
            int nCurMSDistance = -1 * nMSDistanceEach * this.skillSetting.m_nChildSkillNum / 2;

            //分别生成多少子弹
            for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
            {
                nDesSubX = nRefPX + ((nCurMSDistance * skill.Static.g_DirCos(nDirIndex, skill.Defination.MaxMissleDir)) >> 10);
                nDesSubY = nRefPY + ((nCurMSDistance * skill.Static.g_DirSin(nDirIndex, skill.Defination.MaxMissleDir)) >> 10);

                if (nDesSubX < 0 || nDesSubY < 0) continue;

                if (this.skillSetting.m_bBaseSkill != 0)
                {
                    skill.Missile missile = new skill.Missile(this.skillSetting, this.missileSetting, this.map, pSkillParam);

                    if (this.skillSetting.m_nValue2 != 0)
                    {
                        int nDirTemp = nDir - skill.Defination.MaxMissleDir / 4;
                        if (nDirTemp < 0) nDirTemp += skill.Defination.MaxMissleDir;
                        missile.m_nDir = nDirTemp;
                        missile.m_nDirIndex = skill.Static.g_Dir2DirIndex(nDirTemp, 64);

                    }
                    else
                    {
                        missile.m_nDir = nDir;
                        missile.m_nDirIndex = nDirIndex;
                    }

                    missile.m_nSkillId = this.skillSetting.m_nId;
                    missile.m_nStartLifeTime = pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i);
                    missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
                    missile.m_nRefPX = nDesSubX;
                    missile.m_nRefPY = nDesSubY;

                    if (missile.m_nDir < 0)
                        missile.m_nDir = skill.Defination.MaxMissleDir + missile.m_nDir;

                    if (missile.m_nDir >= skill.Defination.MaxMissleDir)
                        missile.m_nDir -= skill.Defination.MaxMissleDir;

                    missile.m_nXFactor = skill.Static.g_DirCos(missile.m_nDir, skill.Defination.MaxMissleDir);
                    missile.m_nYFactor = skill.Static.g_DirSin(missile.m_nDir, skill.Defination.MaxMissleDir);

                    result.Add(missile);
                }
                else
                {
                    if (this.skillSetting.m_nChildSkillId > 0)
                    {
                        int childLevel = 0;
                        if (this.skillSetting.m_nChildSkillLevel <= 0)
                            childLevel = this.skillSetting.skillLevel;
                        else
                            childLevel = this.skillSetting.m_nChildSkillLevel;

                        settings.Skill pOrdinSkill = new settings.Skill(this.skillSetting.m_nChildSkillId, childLevel, this.map);

                        if (pSkillParam.parent.HaveData() == false)
                        {
                            result.AddRange(pOrdinSkill.Cast(pSkillParam.launcher, nDesSubX, nDesSubY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i), eLauncherType));
                        }
                        else
                        {
                            result.AddRange(pOrdinSkill.Cast(pSkillParam.parent, nDesSubX, nDesSubY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i), pSkillParam.parent.type));

                        }
                    }
                }

                nCurMSDistance += nMSDistanceEach;
            }

            return result;
        }

    }
}
