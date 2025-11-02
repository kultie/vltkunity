using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;
using System;
using KRandom = Photon.ShareLibrary.Utils.KRandom;

namespace Photon.JXGameServer.Skills
{
    public class SkillTemplate
    {
        public int id;
        public byte lvl;

        public eSKillStyle m_eSkillStyle;
        public byte nReqLevel;

        public short m_nEquiptLimited, m_nHorseLimited;
        public bool m_bIsPhysical, m_nIsMagic;

        public bool m_bTargetSelf,m_bTargetEnemy,m_bTargetAlly, m_bTargetObj,m_bTargetOther;

        public eNpcAttrib m_nSkillCostType;
        public byte m_nCostValue;

        public short m_nAttackRadius,m_nWaitTime;
        
        public eMisslesForm m_eMisslesForm;

        public byte m_nChildSkillNum;
        public int m_nChildSkillId;

        public short m_nValue1, m_nValue2;
        public bool m_bBaseSkill;

        public bool m_bCollideEvent, m_bVanishedEvent, m_bStartEvent, m_bFlyingEvent, m_bHeelAtParent;
        public int m_nFlyEventTime;

        public eMisslesGenerateStyle m_eMisslesGenerateStyle;
        public short m_nMisslesGenerateData;

        public int m_nStateSpecialId;
        public eMissleInteruptType m_nInteruptTypeWhenMove;

        public NPCRELATION m_eRelation;
        public SkillTemplate(int id, byte lvl) 
        {
            this.id = id;
            this.lvl = lvl;

            m_eSkillStyle = (eSKillStyle)(byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.SkillStyle, id);
            nReqLevel = (byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.ReqLevel, id);

            m_nEquiptLimited = (short)SkillModule.Me.skills.Get<int>((int)Skills_Index.EqtLimit, id, -2);
            m_nHorseLimited = (short)SkillModule.Me.skills.Get<int>((int)Skills_Index.HorseLimit, id, 0);

            m_bIsPhysical = SkillModule.Me.skills.Get<bool>((int)Skills_Index.IsPhysical, id);
            m_nIsMagic = !m_bIsPhysical;

            m_bTargetSelf = SkillModule.Me.skills.Get<bool>((int)Skills_Index.TargetSelf, id);
            m_bTargetEnemy = SkillModule.Me.skills.Get<bool>((int)Skills_Index.TargetEnemy, id);
            m_bTargetAlly = SkillModule.Me.skills.Get<bool>((int)Skills_Index.TargetAlly, id);
            m_bTargetObj = SkillModule.Me.skills.Get<bool>((int)Skills_Index.TargetObj, id);
            m_bTargetOther = SkillModule.Me.skills.Get<bool>((int)Skills_Index.TargetOther, id);

            m_nSkillCostType = (eNpcAttrib)(byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.SkillCostType, id);
            m_nCostValue = (byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.CostValue, id);

            m_nAttackRadius = (short)SkillModule.Me.skills.Get<int>((int)Skills_Index.AttackRadius, id);
            m_nWaitTime = (short)SkillModule.Me.skills.Get<int>((int)Skills_Index.WaitTime, id);

            m_eMisslesForm = (eMisslesForm)(byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.MisslesForm, id);

            m_nChildSkillNum = (byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.ChildSkillNum, id);
            m_nChildSkillId = SkillModule.Me.skills.Get<int>((int)Skills_Index.ChildSkillId, id);

            m_nValue1 = (short)SkillModule.Me.skills.Get<int>((int)Skills_Index.Param1, id);
            m_nValue2 = (short)SkillModule.Me.skills.Get<int>((int)Skills_Index.Param2, id);

            m_bBaseSkill = SkillModule.Me.skills.Get<bool>((int)Skills_Index.BaseSkill, id);
            m_bCollideEvent = SkillModule.Me.skills.Get<bool>((int)Skills_Index.CollideEvent, id);
            m_bVanishedEvent = SkillModule.Me.skills.Get<bool>((int)Skills_Index.VanishedEvent, id);
            m_bStartEvent = SkillModule.Me.skills.Get<bool>((int)Skills_Index.StartEvent, id);
            m_bFlyingEvent = SkillModule.Me.skills.Get<bool>((int)Skills_Index.FlyEvent, id);
            m_bHeelAtParent = SkillModule.Me.skills.Get<bool>((int)Skills_Index.HeelAtParent, id);
            m_nFlyEventTime = SkillModule.Me.skills.Get<int>((int)Skills_Index.FlyEventTime, id);

            m_eMisslesGenerateStyle = (eMisslesGenerateStyle)(byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.MslsGenerate, id);
            m_nMisslesGenerateData = (short)SkillModule.Me.skills.Get<int>((int)Skills_Index.MslsGenerateData, id);

            m_nStateSpecialId = SkillModule.Me.skills.Get<int>((int)Skills_Index.StateSpecialId, id);
            m_nInteruptTypeWhenMove = (eMissleInteruptType)(byte)SkillModule.Me.skills.Get<int>((int)Skills_Index.StopWhenMove, id); 

            m_eRelation = NPCRELATION.relation_empty;
            
            if (m_bTargetEnemy)
                m_eRelation |= NPCRELATION.relation_enemy;

            if (m_bTargetAlly)
                m_eRelation |= NPCRELATION.relation_ally;

            if (m_bTargetSelf)
                m_eRelation |= NPCRELATION.relation_self;

            if (m_bTargetOther)
            {
                m_eRelation |= NPCRELATION.relation_dialog;
                m_eRelation |= NPCRELATION.relation_none;
            }

            var path = SkillModule.Me.skills.Get<string>((int)Skills_Index.LevelUpScript, id);
            if (!string.IsNullOrEmpty(path))
            {
                var script = ScriptModule.Me.GetScript(path);
                if (script != null)
                {
                    string szSettingNameValue, szSettingDataValue;
                    for (int i = 1; i <= 20; ++i)
                    {
                        szSettingNameValue = SkillModule.Me.skills.Get<string>((int)Skills_Index.LvlSetting1, id);
                        szSettingDataValue = SkillModule.Me.skills.Get<string>((int)Skills_Index.LvlData1, id);

                        if (string.IsNullOrEmpty(szSettingNameValue) || szSettingDataValue[0] == '0')
                            continue;

                        script.CallFunction(null, "GetSkillLevelData", 1, "ssd", szSettingNameValue, szSettingDataValue, lvl);

                        szSettingDataValue = script.GetTopString;
                        if (string.IsNullOrEmpty(szSettingDataValue))
                            break;

                        ParseString2MagicAttrib(lvl, szSettingNameValue, szSettingDataValue);
                    }
                }
                else
                { 
                }
            }
        }
        void ParseString2MagicAttrib(byte ulLevel, string szMagicAttribName, string szValue)
        {

        }
        public bool CanCastSkill(CharacterObj nLauncher,int nParam1,int nParam2)
        {
            if (m_bTargetSelf && nParam1 != -1)
                return true;
            else
            {
                if (nParam1 == -1)
                {
                    var obj = nLauncher.scene.FindObj(nParam2);
                    if (obj == null)
                        return false;

                    NPCRELATION Relation = Utils.GetRelation(nLauncher, obj);

                    if (m_bTargetSelf)
                    {
                        if ((Relation & NPCRELATION.relation_self) != 0)
                            goto relationisvalid;
                    }

                    if (m_bTargetEnemy)
                    {
                        if ((Relation & NPCRELATION.relation_enemy) != 0) 
                            goto relationisvalid;
                    }

                    if (m_bTargetAlly)
                    {
                        if ((Relation & NPCRELATION.relation_ally) != 0) 
                            goto relationisvalid;
                    }

                    if (m_bTargetOther)
                    {
                        if ((Relation & NPCRELATION.relation_none) != 0) 
                            goto relationisvalid;

                        if ((Relation & NPCRELATION.relation_dialog) != 0) 
                            goto relationisvalid;
                    }

                    return false;
                }
            }
relationisvalid:
            if (nLauncher.IsPlayer)
            {
                if (-2 != m_nEquiptLimited)
                {

                }

                //if (m_nHorseLimited)
                {

                }
            }
            return true;
        }
        public bool Cast(CharacterObj nLauncher, int nParam1, int nParam2, int nWaitTime = 0, eSkillLauncherType eLauncherType = eSkillLauncherType.SKILL_SLT_Npc)
        {
            if (nParam1 < 0 && nParam2 < 0)
                return false;

            switch (eLauncherType)
            {
                case eSkillLauncherType.SKILL_SLT_Npc:
                    if (nParam1 == -1)
                    {
                        if (nLauncher.scene.FindObj(nParam2) == null)
                            return false;
                    }
                    break;

                case eSkillLauncherType.SKILL_SLT_Missle:
                    {

                    }
                    break;

                default: 
                    return false;
            }

            if (m_eSkillStyle == eSKillStyle.SKILL_SS_PassivityNpcState)
            {
                //CastPassivitySkill(nLauncher, nParam1, nParam2, nWaitTime, nIsEuq);
            }
            else
            {
                if (nWaitTime < 0)
                {
                    nWaitTime = 0;
                }

                switch (m_eSkillStyle)
                {
                    case eSKillStyle.SKILL_SS_Melee:
                    case eSKillStyle.SKILL_SS_CreateNpc:
                    case eSKillStyle.SKILL_SS_BuildPoison:
                    case eSKillStyle.SKILL_SS_AddPoison:
                    case eSKillStyle.SKILL_SS_GetObjDirectly:
                    case eSKillStyle.SKILL_SS_StrideObstacle:
                    case eSKillStyle.SKILL_SS_BodyToObject:
                    case eSKillStyle.SKILL_SS_Mining:
                    case eSKillStyle.SKILL_SS_RepairWeapon:
                    case eSKillStyle.SKILL_SS_Capture:
                        break;

                    case eSKillStyle.SKILL_SS_InitiativeNpcState:
                        //CastInitiativeSkill(nLauncher, nParam1, nParam2, nWaitTime,nMaxShangHai)
                        break;

                    default:
                    case eSKillStyle.SKILL_SS_Missles:
                        CastMissles(nLauncher, nParam1, nParam2, nWaitTime, eLauncherType);
                        break;
                }
            }
            return false;
        }
        MissleObj CreateMissle(TOrdinSkillParam param,int nChildSkillId, int X, int Y)
        {
            var pMissle = new MissleObj(SkillModule.Me.GetMissleTemplate(nChildSkillId), param.nLauncher)
            {
                m_nLevel = lvl,
                m_bCollideEvent = m_bCollideEvent,
                m_bVanishedEvent = m_bVanishedEvent,
                m_bStartEvent = m_bStartEvent,
                m_bFlyEvent = m_bFlyingEvent,
                m_bHeelAtParent = m_bHeelAtParent,
                m_nFlyEventTime = m_nFlyEventTime,

                m_nInteruptTypeWhenMove = m_nInteruptTypeWhenMove,
                m_eRelation = m_eRelation,

                m_nRefPX = X,
                m_nRefPY = Y,
                scene = param.nLauncher.scene,
            };

            pMissle.template.m_nSkillId = id;

            return pMissle;
        }
        int GetMissleGenerateTime(int nNo)
        {
            switch (m_eMisslesGenerateStyle)
            {
                case eMisslesGenerateStyle.SKILL_MGS_NULL:
                    return m_nWaitTime;

                case eMisslesGenerateStyle.SKILL_MGS_SAMETIME:
                    return m_nWaitTime + m_nMisslesGenerateData;

                case eMisslesGenerateStyle.SKILL_MGS_ORDER:
                    return m_nWaitTime + nNo * m_nMisslesGenerateData;

                case eMisslesGenerateStyle.SKILL_MGS_RANDONORDER:
                    if (KRandom.g_Random(2) == 1)
                        return m_nWaitTime + nNo * m_nMisslesGenerateData + KRandom.g_Random(m_nMisslesGenerateData);
                    else
                        return m_nWaitTime + nNo * m_nMisslesGenerateData - KRandom.g_Random(m_nMisslesGenerateData / 2);

                case eMisslesGenerateStyle.SKILL_MGS_RANDONSAME:
                    return m_nWaitTime + KRandom.g_Random(m_nMisslesGenerateData);

                case eMisslesGenerateStyle.SKILL_MGS_CENTEREXTENDLINE:
                    {
                        if (m_nChildSkillNum <= 1)
                            return m_nWaitTime;

                        int nCenter = m_nChildSkillNum / 2;
                        return m_nWaitTime + Math.Abs(nNo - nCenter) * m_nMisslesGenerateData;
                    }
            }
            return m_nWaitTime;
        }
        void CastMissles(CharacterObj nLauncher, int nParam1, int nParam2, int nWaitTime, eSkillLauncherType eLauncherType)
        {
            switch (m_eMisslesForm)
            {
                case eMisslesForm.SKILL_MF_Wall:
                    break;
                case eMisslesForm.SKILL_MF_Line:
                    __CastLine(nLauncher, nParam1, nParam2, nWaitTime, eLauncherType);
                    break;
                case eMisslesForm.SKILL_MF_Spread:
                    break;
                case eMisslesForm.SKILL_MF_Circle:
                    break;
                case eMisslesForm.SKILL_MF_AtTarget:
                    break;
                case eMisslesForm.SKILL_MF_AtFirer:
                    break;
                case eMisslesForm.SKILL_MF_Zone:
                    break;
            }
        }
        void __CastLine(CharacterObj nLauncher, int nParam1, int nParam2, int nWaitTime, eSkillLauncherType eLauncherType)
        {
            TOrdinSkillParam SkillParam = new TOrdinSkillParam();
            SkillParam.eLauncherType = eSkillLauncherType.SKILL_SLT_Npc;
            SkillParam.nParent = 0;
            SkillParam.eParentType = (eSkillLauncherType)0;
            SkillParam.nWaitTime = nWaitTime;

            if (nParam1 == -2)//SKILL_SPT_Direction
            {

            }
            else//SKILL_SPT_TargetIndex
            {
                switch (eLauncherType)
                {
                    case eSkillLauncherType.SKILL_SLT_Npc:
                        SkillParam.nLauncher = nLauncher;
                        SkillParam.eLauncherType = eLauncherType;

                        var missle = SkillModule.Me.GetMissleTemplate(m_nChildSkillId);
                        if (missle == null)
                            return;

                        SkillParam.nTargetId = nLauncher.scene.FindObj(nParam2);

                        int nDesPX = 0, nDesPY = 0;
                        SkillParam.nTargetId.GetMpsPos(ref nDesPX, ref nDesPY);

                        int nSrcPX = 0, nSrcPY = 0;
                        nLauncher.GetMpsPos(ref nSrcPX, ref nSrcPY);

                        int nDir = JXHelper.g_DirIndex2Dir(JXHelper.g_GetDirIndex(nSrcPX, nSrcPY, nDesPX, nDesPY));
                        nLauncher.m_Dir = (byte)nDir;

                        if ((m_nChildSkillNum == 1) && (missle.m_eMoveKind == eMissleMoveKind.MISSLE_MMK_Line || missle.m_eMoveKind == eMissleMoveKind.MISSLE_MMK_Parabola))
                        {
                            if (nSrcPX == nDesPX && nSrcPY == nDesPY)
                                return;

                            var nDistance = JXHelper.g_GetDistance(nSrcPX, nSrcPY, nDesPX, nDesPY);
                            int nYLength = nDesPY - nSrcPY;
                            int nXLength = nDesPX - nSrcPX;
                            int nSin = (nYLength << 10) / nDistance;
                            int nCos = (nXLength << 10) / nDistance;

                            if (Math.Abs(nSin) > 1024)
                                return;

                            if (Math.Abs(nCos) > 1024)
                                return;

                            CastExtractiveLineMissle(SkillParam, nDir, nSrcPX, nSrcPY, nCos, nSin, nDesPX, nDesPY);
                        }
                        else
                        {
                            CastLine(SkillParam, nDir, nSrcPX, nSrcPY);
                        }
                        break;
                }
            }
        }
        void CastLine(TOrdinSkillParam pSkillParam, int nDir, int nRefPX, int nRefPY)
        {
            if (pSkillParam.eLauncherType != eSkillLauncherType.SKILL_SLT_Npc)
                return;

            int nDirIndex = JXHelper.g_Dir2DirIndex(nDir), nDesSubX, nDesSubY, nNum = 0;

            for (int i = 0; i < m_nChildSkillNum; ++i)
            {
                if (m_nValue2 != 0)
                {
                    int nCurMSDistance = -1 * m_nValue1 * m_nChildSkillNum / 2;

                    int nDir1 = nDirIndex + JXHelper.MaxMissleDir >> 2;
                    if (nDir1 > JXHelper.MaxMissleDir)
                        nDir1 -= JXHelper.MaxMissleDir;

                    int nDIndex = JXHelper.g_Dir2DirIndex(nDir1);
                    if (i % 2 == 1)
                    {
                        nCurMSDistance = -nCurMSDistance;
                        nNum++;
                    }

                    if (nDIndex < 0)
                        nDIndex = JXHelper.MaxMissleDir + nDIndex;

                    if (nDIndex >= JXHelper.MaxMissleDir)
                        nDIndex -= JXHelper.MaxMissleDir;

                    nDesSubX = nRefPX + ((nCurMSDistance * nNum * JXHelper.g_DirCos(nDIndex)) >> 10);
                    nDesSubY = nRefPY + ((nCurMSDistance * nNum * JXHelper.g_DirSin(nDIndex)) >> 10);
                }
                else
                {
                    if (nDirIndex < 0)
                        nDirIndex = JXHelper.MaxMissleDir + nDirIndex;

                    if (nDirIndex >= JXHelper.MaxMissleDir)
                        nDirIndex -= JXHelper.MaxMissleDir;

                    nDesSubX = nRefPX + ((m_nValue1 * (i + 1) * JXHelper.g_DirCos(nDirIndex)) >> 10);
                    nDesSubY = nRefPY + ((m_nValue1 * (i + 1) * JXHelper.g_DirSin(nDirIndex)) >> 10);
                }

                if (nDesSubX < 0 || nDesSubY < 0)
                    continue;

                if (m_bBaseSkill)
                {
                    var missle = CreateMissle(pSkillParam, m_nChildSkillId, nDesSubX, nDesSubY);
                    missle.m_nDir = nDir;
                    missle.m_nDirIndex = nDirIndex;

                    missle.m_nStartLifeTime = (short)(pSkillParam.nWaitTime + GetMissleGenerateTime(i));
                    missle.template.m_nLifeTime += missle.m_nStartLifeTime;
                }
                else
                {

                }
            }
        }
        void CastExtractiveLineMissle(TOrdinSkillParam pSkillParam, int nDir, int nSrcX, int nSrcY, int nXOffset, int nYOffset, int nDesX, int nDesY)
        {
            if (pSkillParam.eLauncherType != eSkillLauncherType.SKILL_SLT_Npc)
                return;

            int nDirIndex = JXHelper.g_Dir2DirIndex(nDir);

            if (m_bBaseSkill)
            {
                var missle = CreateMissle(pSkillParam, m_nChildSkillId, nDesX, nDesY);
                missle.m_nDir = nDir;
                missle.m_nDirIndex = nDirIndex;

                int nLength = JXHelper.g_GetDistance(nSrcX, nSrcY, nDesX, nDesY);

                if (missle.template.m_eMoveKind == eMissleMoveKind.MISSLE_MMK_Parabola)
                {
                    if (missle.template.m_nSpeed <= 0)
                        missle.template.m_nSpeed = 4;

                    int nTime = nLength / missle.template.m_nSpeed;

                    missle.template.m_nHeightSpeed = missle.template.m_nZAcceleration * (nTime - 1) / 2;
                }
                missle.m_nStartLifeTime = (short)(pSkillParam.nWaitTime + GetMissleGenerateTime(0));
                missle.template.m_nLifeTime += missle.m_nStartLifeTime;

                if (missle.template.m_eMoveKind == eMissleMoveKind.MISSLE_MMK_Line || missle.template.m_eMoveKind == eMissleMoveKind.MISSLE_MMK_Parabola)
                {

                    missle.m_nXFactor = nXOffset;
                    missle.m_nYFactor = nYOffset;
                }
            }
            else
            {

            }
        }
    }
}
