
using System.Collections.Generic;

namespace game.resource.settings.skill.cast
{
    public class Zone : skill.cast.Circle
    {
        protected List<settings.skill.Missile> CastZone(skill.Params.TOrdinSkillParam pSkillParam, int nDir, int nRefPX, int nRefPY)
        {
            List<settings.skill.Missile> result = new List<settings.skill.Missile>();

            skill.Defination.eSkillLauncherType eLauncherType = pSkillParam.launcher.type;

            if (eLauncherType != Defination.eSkillLauncherType.SKILL_SLT_Npc)
            {
                return result;
            }

            int nBeginPX;
            int nBeginPY;
            if (this.skillSetting.m_nChildSkillNum == 1)
            {
                nBeginPX = nRefPX;
                nBeginPY = nRefPY;
            }
            else
            {
                nBeginPX = nRefPX - this.skillSetting.m_nChildSkillNum * 32 / 2;
                nBeginPY = nRefPY - this.skillSetting.m_nChildSkillNum * 32 / 2;
            }

            for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
                for (int j = 0; j < this.skillSetting.m_nChildSkillNum; j++)
                {
                    if (this.skillSetting.m_bBaseSkill != 0)
                    {

                        if (this.skillSetting.m_nValue1 == 1)
                            if (((i - this.skillSetting.m_nChildSkillNum / 2) * (i - this.skillSetting.m_nChildSkillNum / 2) + (j - this.skillSetting.m_nChildSkillNum / 2) * (j - this.skillSetting.m_nChildSkillNum / 2)) > (this.skillSetting.m_nChildSkillNum * this.skillSetting.m_nChildSkillNum / 4)) continue;


                        int nDesSubX = nBeginPX + j * 32;
                        int nDesSubY = nBeginPY + i * 32;

                        skill.Missile missile = new Missile(this.skillSetting, this.missileSetting, this.map, pSkillParam);

                        missile.m_nDir = nDir;
                        missile.m_nDirIndex = skill.Static.g_Dir2DirIndex(nDir, skill.Defination.MaxMissleDir);

                        missile.m_nSkillId = this.skillSetting.m_nId;
                        missile.m_nStartLifeTime = pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i * this.skillSetting.m_nChildSkillNum + j);
                        missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
                        missile.m_nRefPX = nDesSubX;
                        missile.m_nRefPY = nDesSubY;

                        missile.m_nXFactor = skill.Static.g_DirCos(nDir, skill.Defination.MaxMissleDir);
                        missile.m_nYFactor = skill.Static.g_DirSin(nDir, skill.Defination.MaxMissleDir);

                        result.Add(missile);
                    }
                    else
                    {
                        if (this.skillSetting.m_nChildSkillId > 0)
                        {
                            int m_ChildSkillLevel = 0;
                            if (this.skillSetting.m_nChildSkillLevel <= 0)
                                m_ChildSkillLevel = this.skillSetting.skillLevel;
                            else
                                m_ChildSkillLevel = this.skillSetting.m_nChildSkillLevel;

                            settings.Skill pOrdinSkill = new Skill(this.skillSetting.m_nChildSkillId, m_ChildSkillLevel, this.map);

                            if (pSkillParam.parent.HaveData() == false)
                                result.AddRange(pOrdinSkill.Cast(pSkillParam.launcher, nBeginPX + j * 32, nBeginPY + i * 32, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i * this.skillSetting.m_nChildSkillNum + j), eLauncherType));
                            else
                                result.AddRange(pOrdinSkill.Cast(pSkillParam.launcher, nBeginPX + j * 32, nBeginPY + i * 32, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i * this.skillSetting.m_nChildSkillNum + j), pSkillParam.launcher.type));
                        }
                    }

                }

            return result;
        }


        //protected List<settings.skill.Missile> CastZone(map.Position sourceMapPosition, map.Position destinationMapPosition)
        //{
        //    List<settings.skill.Missile> result = new List<skill.Missile>();

        //    int nBeginPX;
        //    int nBeginPY;
        //    if (this.skillSetting.m_nChildSkillNum == 1)
        //    {
        //        nBeginPX = destinationMapPosition.left;
        //        nBeginPY = destinationMapPosition.top;
        //    }
        //    else
        //    {
        //        nBeginPX = destinationMapPosition.left - this.skillSetting.m_nChildSkillNum * 32 / 2;
        //        nBeginPY = destinationMapPosition.top - this.skillSetting.m_nChildSkillNum * 32 / 2;
        //    }

        //    for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
        //    {
        //        for (int j = 0; j < this.skillSetting.m_nChildSkillNum; j++)
        //        {
        //            if (this.skillSetting.m_bBaseSkill != 0)
        //            {
        //                if (this.skillSetting.m_nValue1 == 1)
        //                    if (((i - this.skillSetting.m_nChildSkillNum / 2) * (i - this.skillSetting.m_nChildSkillNum / 2) + (j - this.skillSetting.m_nChildSkillNum / 2) * (j - this.skillSetting.m_nChildSkillNum / 2)) > (this.skillSetting.m_nChildSkillNum * this.skillSetting.m_nChildSkillNum / 4)) continue;

        //                int nDesSubX = nBeginPX + j * 32;
        //                int nDesSubY = nBeginPY + i * 32;

        //                int nDir = skill.Static.nDir(sourceMapPosition, destinationMapPosition);
        //                skill.Missile missile = new skill.Missile(this.skillSetting, this.missileSetting, this.map);

        //                missile.m_nDir = nDir;
        //                missile.m_nDirIndex = skill.Static.g_Dir2DirIndex(nDir, skill.Defination.MaxMissleDir);

        //                missile.m_nSkillId = this.skillSetting.m_nId;
        //                missile.m_nStartLifeTime = 0 + this.skillSetting.GetMissleGenerateTime(i * this.skillSetting.m_nChildSkillNum + j);
        //                missile.m_nLifeTime += this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
        //                missile.m_nRefPX = nDesSubX;
        //                missile.m_nRefPY = nDesSubY;

        //                if (this.missileSetting.m_eMoveKind == skill.Defination.MissleMoveKind.MISSLE_MMK_Line
        //                    || this.missileSetting.m_eMoveKind == skill.Defination.MissleMoveKind.MISSLE_MMK_RollBack)
        //                {
        //                    missile.m_nXFactor = skill.Static.g_DirCos(nDir, skill.Defination.MaxMissleDir);
        //                    missile.m_nYFactor = skill.Static.g_DirSin(nDir, skill.Defination.MaxMissleDir);
        //                }

        //                result.Add(missile);
        //            }
        //            else
        //            {
        //                if (this.skillSetting.m_nChildSkillId <= 0) return result;

        //                settings.Skill newSkill = new Skill(this.skillSetting.m_nChildSkillId, this.map);
        //                result.AddRange(newSkill.Cast(sourceMapPosition, destinationMapPosition));
        //            }
        //        }
        //    }

        //    return result;
        //}
    }
}
