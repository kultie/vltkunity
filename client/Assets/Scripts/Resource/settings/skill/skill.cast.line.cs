
using System.Collections.Generic;
using static game.resource.settings.Skill;

namespace game.resource.settings.skill.cast
{
    public class Line : skill.cast.Zone
    {
        protected List<settings.skill.Missile> CastLine(skill.Params.TOrdinSkillParam pSkillParam, int nDir, int nRefPX, int nRefPY)
        {
            List<settings.skill.Missile> result = new List<settings.skill.Missile>();

            skill.Defination.eSkillLauncherType eLauncherType = pSkillParam.launcher.type;
            if (eLauncherType != Defination.eSkillLauncherType.SKILL_SLT_Npc)
            {
                return result;
            }
            int nDirIndex = skill.Static.g_Dir2DirIndex(nDir, skill.Defination.MaxMissleDir);
            int nDesSubX = 0;
            int nDesSubY = 0;

            //子弹之间的间距
            int nMSDistanceEach = this.skillSetting.m_nValue1;

            //分别生成多少子弹
            for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
            {
                nDesSubX = nRefPX + ((nMSDistanceEach * (i + 1) * skill.Static.g_DirCos(nDirIndex, skill.Defination.MaxMissleDir)) >> 10);
                nDesSubY = nRefPY + ((nMSDistanceEach * (i + 1) * skill.Static.g_DirSin(nDirIndex, skill.Defination.MaxMissleDir)) >> 10);

                if (nDesSubX < 0 || nDesSubY < 0) continue;

                if (this.skillSetting.m_bBaseSkill != 0)
                {
                    skill.Missile missile = new skill.Missile(this.skillSetting, this.missileSetting, this.map, pSkillParam);

                    missile.m_nDir = nDir;
                    missile.m_nDirIndex = nDirIndex;

                    missile.m_nSkillId = this.skillSetting.m_nId;
                    missile.m_nStartLifeTime = pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i);
                    missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
                    missile.m_nRefPX = nDesSubX;
                    missile.m_nRefPY = nDesSubY;

                    missile.m_nXFactor = skill.Static.g_DirCos(nDir, skill.Defination.MaxMissleDir);
                    missile.m_nYFactor = skill.Static.g_DirSin(nDir, skill.Defination.MaxMissleDir);

                    result.Add(missile);
                }
                else
                {
                    if(this.skillSetting.m_nChildSkillId > 0)
                    {
                        int m_ChildSkillLevel = 0;

                        if (this.skillSetting.m_nChildSkillLevel <= 0)
                            m_ChildSkillLevel = this.skillSetting.skillLevel;
                        else
                            m_ChildSkillLevel = this.skillSetting.m_nChildSkillLevel;

                        settings.Skill pOrdinSkill = new settings.Skill(this.skillSetting.m_nChildSkillId, m_ChildSkillLevel, this.map);

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

            }

            return result; 
        }

        protected List<settings.skill.Missile> CastExtractiveLineMissle(skill.Params.TOrdinSkillParam pSkillParam, int nDir, int nSrcX, int nSrcY, int nXOffset, int nYOffset, int nDesX, int nDesY)
        {
            List<settings.skill.Missile> result = new List<settings.skill.Missile>();

            settings.skill.Params.Owner nLauncher = pSkillParam.launcher;
            if (pSkillParam.launcher.type != Defination.eSkillLauncherType.SKILL_SLT_Npc)
            {
                return result;
            }
            int nDirIndex = skill.Static.g_Dir2DirIndex(nDir, skill.Defination.MaxMissleDir);
            int nDesSubX = 0;
            int nDesSubY = 0;

            {
                if (this.skillSetting.m_bBaseSkill != 0)
                {
                    skill.Missile missile = new Missile(this.skillSetting, this.missileSetting, this.map, pSkillParam);

                    missile.m_nDir = nDir;
                    missile.m_nDirIndex = nDirIndex;

                    if (this.missileSetting.m_eMoveKind == Defination.MissleMoveKind.MISSLE_MMK_Parabola)
                    {
                        int nLength = skill.Static.g_GetDistance(nSrcX, nSrcY, nDesX, nDesY);
                        int nTime = nLength / this.missileSetting.m_nSpeed;
                        missile.m_nHeightSpeed = this.missileSetting.m_nZAcceleration * (nTime - 1) / 2;

                    }

                    if (pSkillParam.parent != null && pSkillParam.parent.HaveData())
                        missile.m_nParentMissleIndex = pSkillParam.parent.missile;
                    else
                        missile.m_nParentMissleIndex = null;

                    missile.m_nSkillId = this.skillSetting.m_nId;
                    missile.m_nStartLifeTime = pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(0);
                    missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
                    missile.m_nRefPX = nSrcX;
                    missile.m_nRefPY = nSrcY;

                    missile.m_nXFactor = nXOffset;
                    missile.m_nYFactor = nYOffset;

                    result.Add(missile);
                }
                else
                {
                    int m_ChildSkillLevel = 0;
                    if (this.skillSetting.m_nChildSkillLevel <= 0)
                        m_ChildSkillLevel = this.skillSetting.skillLevel;
                    else
                        m_ChildSkillLevel = this.skillSetting.m_nChildSkillLevel;

                    settings.Skill pOrdinSkill = new Skill(this.skillSetting.m_nChildSkillId, m_ChildSkillLevel, this.map);

                    if(pSkillParam.parent.HaveData() == false)
                    {
                        result.AddRange(pOrdinSkill.Cast(nLauncher, nDesSubX, nDesSubY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(0), pSkillParam.launcher.type));
                    }
                    else
                    {
                        result.AddRange(pOrdinSkill.Cast(pSkillParam.parent, nDesSubX, nDesSubY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(0), pSkillParam.launcher.type));
                    }
                }

            }

            return result;
        }

        //protected List<settings.skill.Missile> CastLine(map.Position sourceMapPosition, map.Position destinationMapPosition)
        //{
        //    List<settings.skill.Missile> result = new List<skill.Missile>();

        //    int nDir = skill.Static.nDir(sourceMapPosition, destinationMapPosition);
        //    int nDirIndex = skill.Static.g_Dir2DirIndex(nDir, skill.Defination.MaxMissleDir);
        //    int nDesSubX = 0;
        //    int nDesSubY = 0;

        //    int nMSDistanceEach = this.skillSetting.m_nValue1;

        //    for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
        //    {
        //        nDesSubX = sourceMapPosition.left + ((nMSDistanceEach * (i + 1) * skill.Static.g_DirCos(nDirIndex, skill.Defination.MaxMissleDir)) >> 10);
        //        nDesSubY = sourceMapPosition.top + ((nMSDistanceEach * (i + 1) * skill.Static.g_DirSin(nDirIndex, skill.Defination.MaxMissleDir)) >> 10);

        //        if (nDesSubX < 0 || nDesSubY < 0) continue;

        //        if (this.skillSetting.m_bBaseSkill != 0)
        //        {
        //            skill.Missile missile = new skill.Missile(this.skillSetting, this.missileSetting, this.map);

        //            missile.m_nDir = nDir;
        //            missile.m_nDirIndex = nDirIndex;
        //            missile.m_nSkillId = this.skillSetting.m_nId;
        //            missile.m_nStartLifeTime = 0 + this.skillSetting.GetMissleGenerateTime(i);
        //            missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
        //            missile.m_nRefPX = nDesSubX;
        //            missile.m_nRefPY = nDesSubY;

        //            if (this.missileSetting.m_eMoveKind == skill.Defination.MissleMoveKind.MISSLE_MMK_Line 
        //                || this.missileSetting.m_eMoveKind == skill.Defination.MissleMoveKind.MISSLE_MMK_RollBack)
        //            {
        //                missile.m_nXFactor = skill.Static.g_DirCos(nDir, skill.Defination.MaxMissleDir);
        //                missile.m_nYFactor = skill.Static.g_DirSin(nDir, skill.Defination.MaxMissleDir);
        //            }

        //            result.Add(missile);
        //        }
        //        else
        //        {
        //            settings.Skill ordinSkill = new Skill(this.skillSetting.m_nChildSkillId, this.map);
        //            result.AddRange(ordinSkill.Cast(sourceMapPosition, destinationMapPosition));
        //        }

        //    }

        //    return result;
        //}
    }
}
