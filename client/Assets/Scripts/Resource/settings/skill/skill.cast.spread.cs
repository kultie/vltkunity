
using System;
using System.Collections.Generic;

namespace game.resource.settings.skill.cast
{
    public class Spread : skill.cast.Wall
    {
        protected List<settings.skill.Missile> CastSpread(skill.Params.TOrdinSkillParam pSkillParam, int nDir, int nRefPX, int nRefPY)
        {
            List<settings.skill.Missile> result = new List<settings.skill.Missile>();

            skill.Defination.eSkillLauncherType eLauncherType = pSkillParam.launcher.type;
            if (eLauncherType != skill.Defination.eSkillLauncherType.SKILL_SLT_Npc)
            {
                return result;
            }
            int nFirstStep = this.skillSetting.m_nValue2;         //第一步的长度，子弹在刚发出去时离玩家的距离
            int nCurMSRadius = this.skillSetting.m_nChildSkillNum / 2;
            int nCurSubDir = 0;

            // Sin A+B = SinA*CosB + CosA*SinB
            // Cos A+B = CosA*CosB - SinA*SinB
            // Sin A = nYFactor
            // Cos A = nXFactor

            int nDesSubX = 0;
            int nDesSubY = 0;
            int nXFactor = 0;
            int nYFactor = 0;

            if (pSkillParam.target.HaveData() == true)
            {
                int nDistance = 0;
                int nDesX, nDesY;

                resource.map.Position targetPosition = pSkillParam.target.GetMapPosition();
                nDesX = targetPosition.left;
                nDesY = targetPosition.top;

                nDistance = (int)Math.Sqrt((nDesX - nRefPX) * (nDesX - nRefPX) + (nDesY - nRefPY) * (nDesY - nRefPY));

                if(nDistance <= 0)
                {
                    nDistance = 1;
                }

                nXFactor = ((nDesX - nRefPX) << 10) / nDistance;
                nYFactor = ((nDesY - nRefPY) << 10) / nDistance;

                nDesSubX = nRefPX + ((nXFactor * nFirstStep) >> 10);
                nDesSubY = nRefPY + ((nYFactor * nFirstStep) >> 10);

                if (nDesSubX < 0 || nDesSubY < 0)
                {
                    return result;
                }
            }

            //分别生成多个子弹
            for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
            {
                int nDSubDir = this.skillSetting.m_nValue1 * nCurMSRadius;
                nCurSubDir = nDir - this.skillSetting.m_nValue1 * nCurMSRadius;


                if (nCurSubDir < 0)
                    nCurSubDir = skill.Defination.MaxMissleDir + nCurSubDir;

                if (nCurSubDir >= skill.Defination.MaxMissleDir)
                    nCurSubDir -= skill.Defination.MaxMissleDir;

                int nSinAB;
                int nCosAB;

                if (pSkillParam.target.HaveData() == true)
                {
                    nDSubDir += 48;
                    if (nDSubDir >= skill.Defination.MaxMissleDir)
                        nDSubDir -= skill.Defination.MaxMissleDir;
                    //sin(a - b) = sinacosb - cosa*sinb
                    //cos(a - b) = cosacoab + sinasinb
                    nSinAB = (nYFactor * skill.Static.g_DirCos(nDSubDir, skill.Defination.MaxMissleDir) - nXFactor * skill.Static.g_DirSin(nDSubDir, skill.Defination.MaxMissleDir)) >> 10;
                    nCosAB = (nXFactor * skill.Static.g_DirCos(nDSubDir, skill.Defination.MaxMissleDir) + nYFactor * skill.Static.g_DirSin(nDSubDir, skill.Defination.MaxMissleDir)) >> 10;
                }
                else
                {
                    nSinAB = skill.Static.g_DirSin(nCurSubDir, skill.Defination.MaxMissleDir);
                    nCosAB = skill.Static.g_DirCos(nCurSubDir, skill.Defination.MaxMissleDir);
                }

                nDesSubX = nRefPX + ((nCosAB * nFirstStep) >> 10);
                nDesSubY = nRefPY + ((nSinAB * nFirstStep) >> 10);

                if (nDesSubX < 0 || nDesSubY < 0) continue;

                if (this.skillSetting.m_bBaseSkill != 0)
                {
                    skill.Missile missile = new skill.Missile(this.skillSetting, this.missileSetting, this.map, pSkillParam);

                    missile.m_nDir = nCurSubDir;
                    missile.m_nDirIndex = skill.Static.g_Dir2DirIndex(nCurSubDir, skill.Defination.MaxMissleDir);

                    missile.m_nSkillId = this.skillSetting.m_nId;
                    missile.m_nStartLifeTime = pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i);
                    missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
                    missile.m_nXFactor = nCosAB;
                    missile.m_nYFactor = nSinAB;
                    missile.m_nRefPX = nDesSubX;
                    missile.m_nRefPY = nDesSubY;

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

                        if (pSkillParam.parent.HaveData() == false)
                        {
                            result.AddRange(pOrdinSkill.Cast(pSkillParam.launcher, nRefPX, nRefPY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i), eLauncherType));
                        }
                        else
                        {
                            result.AddRange(pOrdinSkill.Cast(pSkillParam.parent, nRefPX, nRefPY, pSkillParam.nWaitTime + this.skillSetting.GetMissleGenerateTime(i), pSkillParam.parent.type));
                        }
                    }
                }

                nCurMSRadius--;
            }

            return result;
        }



        //protected List<settings.skill.Missile> CastSpread(map.Position sourceMapPosition, map.Position destinationMapPosition)
        //{
        //    List<settings.skill.Missile> result = new List<skill.Missile>();

        //    int nDesSubMapX = 0;
        //    int nDesSubMapY = 0;
        //    int nFirstStep = this.skillSetting.m_nValue2;
        //    int nCurMSRadius = this.skillSetting.m_nChildSkillNum / 2;
        //    int nCurSubDir = 0;
        //    int nCastMissleNum = 0;

        //    // Sin A+B = SinA*CosB + CosA*SinB
        //    // Cos A+B = CosA*CosB - SinA*SinB
        //    // Sin A = nYFactor
        //    // Cos A = nXFactor

        //    int nDesSubX = 0;
        //    int nDesSubY = 0;
        //    int nXFactor = 0;
        //    int nYFactor = 0;

        //    for (int i = 0; i < this.skillSetting.m_nChildSkillNum; i++)
        //    {
        //        int nDSubDir = this.skillSetting.m_nValue1 * nCurMSRadius;
        //        nCurSubDir = skill.Static.nDir(sourceMapPosition, destinationMapPosition) - this.skillSetting.m_nValue1 * nCurMSRadius;


        //        if (nCurSubDir < 0)
        //            nCurSubDir = skill.Defination.MaxMissleDir + nCurSubDir;

        //        if (nCurSubDir >= skill.Defination.MaxMissleDir)
        //            nCurSubDir -= skill.Defination.MaxMissleDir;

        //        int nSinAB;
        //        int nCosAB;

        //        nSinAB = skill.Static.g_DirSin(nCurSubDir, skill.Defination.MaxMissleDir);
        //        nCosAB = skill.Static.g_DirCos(nCurSubDir, skill.Defination.MaxMissleDir);

        //        nDesSubX = sourceMapPosition.left + ((nCosAB * nFirstStep) >> 10);
        //        nDesSubY = sourceMapPosition.top + ((nSinAB * nFirstStep) >> 10);

        //        if (nDesSubX < 0 || nDesSubY < 0) continue;

        //        if (this.skillSetting.m_bBaseSkill != 0)
        //        {
        //            skill.Missile missile = new skill.Missile(this.skillSetting, this.missileSetting, this.map);

        //            missile.m_nDir = nCurSubDir;
        //            missile.m_nDirIndex = skill.Static.g_Dir2DirIndex(nCurSubDir, skill.Defination.MaxMissleDir);
        //            missile.m_nSkillId = this.skillSetting.m_nId;
        //            missile.m_nStartLifeTime = 0 + this.skillSetting.GetMissleGenerateTime(i);
        //            missile.m_nLifeTime = this.missileSetting.m_nLifeTime + missile.m_nStartLifeTime;
        //            missile.m_nXFactor = nCosAB;
        //            missile.m_nYFactor = nSinAB;
        //            missile.m_nRefPX = nDesSubX;
        //            missile.m_nRefPY = nDesSubY;

        //            result.Add(missile);
        //        }
        //        else
        //        {
        //            if(this.skillSetting.m_nChildSkillId > 0)
        //            {
        //                settings.Skill skill = new Skill(this.skillSetting.m_nChildSkillId, this.map);
        //                result.AddRange(skill.Cast(sourceMapPosition, destinationMapPosition));
        //            }
        //        }

        //        nCurMSRadius--;
        //    }

        //    return result;
        //}
    }
}
