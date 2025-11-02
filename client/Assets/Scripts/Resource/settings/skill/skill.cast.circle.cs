
using System.Collections.Generic;

namespace game.resource.settings.skill.cast
{
    public class Circle : skill.Data
    {
        protected List<settings.skill.Missile> CastCircle(skill.Params.TOrdinSkillParam pSkillParam, int nDir, int nRefPX, int nRefPY)
        {
            List<settings.skill.Missile> result = new List<settings.skill.Missile>();

            skill.Defination.eSkillLauncherType eLauncherType = pSkillParam.launcher.type;
            if (eLauncherType != Defination.eSkillLauncherType.SKILL_SLT_Npc)
            {
                return result;
            }
            int nDesSubPX = 0;
            int nDesSubPY = 0;
            int nFirstStep = this.skillSetting.m_nValue2;         //第一步的长度，子弹在刚发出去时离玩家的距离
            int nCurSubDir = 0;
            int nDirPerNum = skill.Defination.MaxMissleDir / this.skillSetting.m_nChildSkillNum;
            int nCastMissleNum = 0;

            //分别生成多个子弹
            for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
            {
                nCurSubDir = nDir + nDirPerNum * i;

                if (nCurSubDir < 0)
                    nCurSubDir = skill.Defination.MaxMissleDir + nCurSubDir;

                if (nCurSubDir >= skill.Defination.MaxMissleDir)
                    nCurSubDir -= skill.Defination.MaxMissleDir;

                int nSinAB = skill.Static.g_DirSin(nCurSubDir, skill.Defination.MaxMissleDir);
                int nCosAB = skill.Static.g_DirCos(nCurSubDir, skill.Defination.MaxMissleDir);

                nDesSubPX = nRefPX + ((nCosAB * nFirstStep) >> 10);
                nDesSubPY = nRefPY + ((nSinAB * nFirstStep) >> 10);

                if (nDesSubPX < 0 || nDesSubPY < 0)
                {
                    //UnityEngine.Debug.LogError("continue at: x: " + nDesSubPX + ", y: " + nDesSubPY);
                    continue;
                }
                else
                {
                    //UnityEngine.Debug.Log("Des: x: " + nDesSubPX + ", y: " + nDesSubPY);
                }

                if (this.skillSetting.m_bBaseSkill != 0)
                {
                    skill.Missile missile = new Missile(this.skillSetting, this.missileSetting, this.map, pSkillParam);

                    missile.m_nDir = nCurSubDir;
                    missile.m_nDirIndex = skill.Static.g_Dir2DirIndex(nCurSubDir, skill.Defination.MaxMissleDir);

                    missile.m_nSkillId = this.skillSetting.m_nId;
                    missile.m_nStartLifeTime = pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i);
                    missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
                    missile.m_nRefPX = nDesSubPX;
                    missile.m_nRefPY = nDesSubPY;

                    missile.m_nXFactor = skill.Static.g_DirCos(nCurSubDir, skill.Defination.MaxMissleDir);
                    missile.m_nYFactor = skill.Static.g_DirSin(nCurSubDir, skill.Defination.MaxMissleDir);

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

                        settings.Skill pOrdinSkill = new Skill(this.skillSetting.m_nChildSkillId, m_ChildSkillLevel, this.map);

                        if(pSkillParam.parent.HaveData() == false)
                        {
                            result.AddRange(pOrdinSkill.Cast(pSkillParam.launcher, nDesSubPX, nDesSubPY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i), eLauncherType));
                        }
                        else
                        {
                            result.AddRange(pOrdinSkill.Cast(pSkillParam.parent, nDesSubPX, nDesSubPY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i), pSkillParam.parent.type));
                        }
                    }
                }

            }

            //UnityEngine.Debug.Log("missile generated cound: " + result.Count);

            return result;
        }


        //protected List<settings.skill.Missile> CastCircle(map.Position sourceMapPosition, map.Position destinationMapPosition)
        //{
        //    List<settings.skill.Missile> result = new List<skill.Missile>();

        //    int nDesSubPX = 0;
        //    int nDesSubPY = 0;
        //    int nFirstStep = this.skillSetting.m_nValue2;
        //    int nDirPerNum = 0;

        //    int nRefPX = 0;
        //    int nRefPY = 0;

        //    if (this.skillSetting.m_nValue1 == 0)
        //    {
        //        nRefPX = sourceMapPosition.left;
        //        nRefPY = sourceMapPosition.top;
        //    }
        //    else
        //    {
        //        nRefPX = destinationMapPosition.left;
        //        nRefPY = destinationMapPosition.top;
        //    }

        //    if (this.skillSetting.m_nChildSkillNum > 0)
        //        nDirPerNum = skill.Defination.MaxMissleDir / this.skillSetting.m_nChildSkillNum;

        //    for (int i = 0; i < this.skillSetting.m_nChildSkillNum; ++i)
        //    {
        //        int nCurSubDir = skill.Static.nDir(sourceMapPosition, destinationMapPosition) + nDirPerNum * i;

        //        if (nCurSubDir < 0)
        //            nCurSubDir = skill.Defination.MaxMissleDir + nCurSubDir;

        //        if (nCurSubDir >= skill.Defination.MaxMissleDir)
        //            nCurSubDir -= skill.Defination.MaxMissleDir;

        //        int nSinAB = skill.Static.g_DirSin(nCurSubDir, skill.Defination.MaxMissleDir);
        //        int nCosAB = skill.Static.g_DirCos(nCurSubDir, skill.Defination.MaxMissleDir);

        //        nDesSubPX = nRefPX + ((nCosAB * nFirstStep) >> 10);
        //        nDesSubPY = nRefPY + ((nSinAB * nFirstStep) >> 10);


        //        if (nDesSubPX < 0 || nDesSubPY < 0) continue;

        //        if (this.skillSetting.m_bBaseSkill != 0)
        //        {
        //            skill.Missile missile = new skill.Missile(this.skillSetting, this.missileSetting, this.map);

        //            missile.sourceMapPosition = sourceMapPosition;
        //            missile.destinationMapPosition = destinationMapPosition;

        //            missile.m_nDir = nCurSubDir;
        //            missile.m_nDirIndex = skill.Static.g_Dir2DirIndex(nCurSubDir, skill.Defination.MaxMissleDir);
        //            missile.m_nChildSkillId = this.skillSetting.m_nChildSkillId;


        //            missile.m_nSkillId = this.skillSetting.m_nId;
        //            missile.m_nStartLifeTime = 0 + this.skillSetting.GetMissleGenerateTime(i);
        //            missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
        //            missile.m_nRefPX = nDesSubPX;
        //            missile.m_nRefPY = nDesSubPY;

        //            if (this.missileSetting.m_eMoveKind == skill.Defination.MissleMoveKind.MISSLE_MMK_Line 
        //                || this.missileSetting.m_eMoveKind == skill.Defination.MissleMoveKind.MISSLE_MMK_Follow
        //                || this.missileSetting.m_eMoveKind == skill.Defination.MissleMoveKind.MISSLE_MMK_RollBack)
        //            {
        //                missile.m_nXFactor = skill.Static.g_DirCos(nCurSubDir, skill.Defination.MaxMissleDir);
        //                missile.m_nYFactor = skill.Static.g_DirSin(nCurSubDir, skill.Defination.MaxMissleDir);
        //            }

        //            result.Add(missile);
        //        }
        //        else
        //        {
        //            if (this.skillSetting.m_nChildSkillId <= 0)
        //            {
        //                return result;
        //            }

        //            settings.Skill newSkill = new Skill(this.skillSetting.m_nChildSkillId, this.map);
        //            result.AddRange(newSkill.Cast(sourceMapPosition, destinationMapPosition));
        //        }

        //    }

        //    return result;
        //}
    }
}
