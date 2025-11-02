using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;

namespace Photon.JXGameServer.Entitys
{
    public class NpcAI
    {
        byte m_AiMode;
        short m_AIMAXTime;
        short m_AiAddLifeTime;
        short[] m_AiParam = new short[10];

        int m_NextAITime;

        NpcTemplate npcTemplate;
        NpcObj npcObj;
        public NpcAI(NpcObj npc, NpcTemplate template) 
        { 
            npcObj = npc;
            npcTemplate = template;
        }
        public void LoadAI(int rowIndex)
        {
            m_AiMode = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIMode, rowIndex);
            m_AIMAXTime = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIMaxTime, rowIndex);
            m_AiAddLifeTime = 0;

            m_AiParam[0] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam1, rowIndex);
            m_AiParam[1] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam2, rowIndex);
            m_AiParam[2] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam3, rowIndex);
            m_AiParam[3] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam4, rowIndex);
            m_AiParam[4] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam5, rowIndex);
            m_AiParam[5] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam6, rowIndex);
            m_AiParam[6] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam7, rowIndex);
            m_AiParam[7] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam8, rowIndex);
            m_AiParam[8] = (short)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AIParam9, rowIndex);

            m_AiParam[9] = 100;// nMaxRadius

            m_NextAITime = 0;
        }
        public void ProcessAI() 
        {
            var nCurTime = npcObj.scene.m_dwCurrentTime;
            if (m_NextAITime <= nCurTime)
            {
                m_NextAITime = nCurTime + m_AIMAXTime;

                switch (m_AiMode)
                {
                    case 1:
                    case 4:
                        ProcessAIType01_04();
                        break;
                    case 2:
                    case 5:
                        ProcessAIType02_05();
                        break;
                    case 3:
                    case 6:
                        ProcessAIType03_06();
                        break;
                }
            }
        }
        void CommonAction()
        {
            if (npcObj.Kind == NPCKIND.kind_dialoger)
            {
                return;
            }
            int nOffX, nOffY;
            if (KRandom.g_RandPercent(80))
            {
                nOffX = 0;
                nOffY = 0;
            }
            else
            {
                nOffX = KRandom.g_Random(npcTemplate.m_ActiveRadius / 2);
                nOffY = KRandom.g_Random(npcTemplate.m_ActiveRadius / 2);
                if ((nOffX & 1) == 1)
                {
                    nOffX = -nOffX;
                }
                if ((nOffY & 1) == 1)
                {
                    nOffY = -nOffY;
                }
            }
            npcObj.SendSerCommand(NPCCMD.do_walk, npcObj.m_OriginX + nOffX, npcObj.m_OriginY + nOffY);
        }
        void Flee(CharacterObj obj)
        {
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            npcObj.GetMpsPos(ref x1, ref y1);
            obj.GetMpsPos(ref x2, ref y2);

            x1 = x1 * 2 - x2;
            y1 = y1 * 2 - y2;

            npcObj.SendSerCommand(NPCCMD.do_walk, x1, y2);
        }
        void FollowAttack(CharacterObj obj)
        {
            int distance = npcObj.GetDistance(obj);
            if (distance == 0)
                distance = 1;

            if ((distance <= npcObj.m_CurrentAttackRadius) && InEyeshot(obj))
            {
                npcObj.SendSerCommand(NPCCMD.do_skill, npcObj.m_ActiveSkillID, -1, obj.id);
                return;
            }

            int x = 0, y = 0;
            obj.GetMpsPos(ref x,ref y);

            npcObj.SendSerCommand(NPCCMD.do_walk, x, y);
        }
        bool InEyeshot(MapObj obj) => npcTemplate.m_VisionRadius >= npcObj.GetDistance(obj);
        bool KeepActiveRange()
        {
            int X = 0, Y = 0;
            npcObj.GetMpsPos(ref X, ref Y);

            var range = JXHelper.g_GetDistance(npcObj.m_OriginX,npcObj.m_OriginY,X,Y);

            if (npcTemplate.m_ActiveRadius < range)
            {
                npcObj.SendSerCommand(NPCCMD.do_walk, npcObj.m_OriginX, npcObj.m_OriginY);
                return true;
            }
            return false;
        }
        CharacterObj GetNearestNpc(NPCRELATION nRelation)
        {
            int nRangeX = npcTemplate.m_VisionRadius;
            int nRangeY = nRangeX;

            nRangeX = nRangeX / JXHelper.m_nCellWidth;
            nRangeY = nRangeY / JXHelper.m_nCellHeight;

            return npcObj.GetNearestNpc(nRangeX, nRangeY, nRelation);
        }
        CharacterObj ProcessAIType00()
        {
            if (KeepActiveRange())
                return null;

            if (npcObj.RandMove.nTime > 0)
            {
                npcObj.m_nPeopleIdx = 0;
                //if (m_AiParam[0] > 0 && JXHelper.g_RandPercent(m_AiParam[0]))
                //{
                CommonAction();
                //}
                return null;
            }
            CharacterObj nEnemyIdx = null;
            if (npcObj.m_nPeopleIdx == 0)
            {
                nEnemyIdx = GetNearestNpc(NPCRELATION.relation_enemy);
            }
            else
            {
                nEnemyIdx = npcObj.region.FindObj(npcObj.m_nPeopleIdx);
                if ((nEnemyIdx == null) || !InEyeshot(nEnemyIdx))
                {
                    nEnemyIdx = GetNearestNpc(NPCRELATION.relation_enemy);
                }
            }
            if (nEnemyIdx != null)
            {
                if ((nEnemyIdx.Doing == NPCCMD.do_death) || (nEnemyIdx.Doing == NPCCMD.do_revive) || nEnemyIdx.Hide.nTime > 0)
                {
                    nEnemyIdx = null;
                }
                else
                if (nEnemyIdx.Kind == NPCKIND.kind_player && !nEnemyIdx.FightMode)
                {
                    nEnemyIdx = null;
                }
            }
            if (nEnemyIdx == null)
            {
                npcObj.m_nPeopleIdx = 0;
                //if (m_AiParam[0] > 0 && JXHelper.g_RandPercent(m_AiParam[0]))
                //{
                CommonAction();
                //}
                return null;
            }
            return nEnemyIdx;
        }
        void ProcessAIType01_04()
        {
            CharacterObj nEnemyIdx = ProcessAIType00();
            if (nEnemyIdx == null)
                return;

            npcObj.m_nPeopleIdx = nEnemyIdx.id;
            var nRand = KRandom.g_Random(100);

            if (npcObj.GetDistanceSquare(nEnemyIdx) > m_AiParam[9])
            {
                if (nRand < m_AiParam[5])
                    return;
                if (nRand < m_AiParam[5] + m_AiParam[6])
                {
                    CommonAction();
                    return;
                }
                if (!npcObj.SetActiveSkill((byte)KRandom.GetRandomNumber(1, 4)))
                {
                    CommonAction();
                    return;
                }
                FollowAttack(nEnemyIdx);
                return;
            }
            if (nRand < m_AiParam[1])
            {
                if (!npcObj.SetActiveSkill(1))
                {
                    CommonAction();
                    return;
                }
            }
            else
            if (nRand < m_AiParam[1] + m_AiParam[2])
            {
                if (!npcObj.SetActiveSkill(2))
                {
                    CommonAction();
                    return;
                }
            }
            else
            if (nRand < m_AiParam[1] + m_AiParam[2] + m_AiParam[3])
            {
                if (!npcObj.SetActiveSkill(3))
                {
                    CommonAction();
                    return;
                }
            }
            else
            if (nRand < m_AiParam[1] + m_AiParam[2] + m_AiParam[3] + m_AiParam[4])
            {
                if (!npcObj.SetActiveSkill(4))
                {
                    CommonAction();
                    return;
                }
            }
            else
            {
                return;
            }
            FollowAttack(nEnemyIdx);
        }
        void ProcessAIType02_05()
        {
            CharacterObj nEnemyIdx = ProcessAIType00();
            if (nEnemyIdx == null)
                return;

            if (npcObj.HPMax <= 0)
                npcObj.HPMax = 100;

            if (npcObj.HPCur * 100 / npcObj.HPMax < m_AiParam[1]) // hoi ma'u
            {
                if (KRandom.g_RandPercent(m_AiParam[2]))
                {
                    if (m_AiAddLifeTime < m_AiParam[9] && KRandom.g_RandPercent(m_AiParam[3]))
                    {
                        m_AiAddLifeTime++;
                        npcObj.SetActiveSkill(1);
                        npcObj.SendSerCommand(NPCCMD.do_skill,npcObj.m_ActiveSkillID, -1, npcObj.id);
                    }
                    else
                    {
                        Flee(nEnemyIdx);
                    }
                    return;
                }
            }

            npcObj.m_nPeopleIdx = nEnemyIdx.id;
            var nRand = KRandom.g_Random(100);

            if (npcObj.GetDistanceSquare(nEnemyIdx) > m_AiParam[9])
            {
                if (nRand < m_AiParam[7])
                    return;
                if (nRand < m_AiParam[7] + m_AiParam[8])
                {
                    CommonAction();
                    return;
                }
                if (!npcObj.SetActiveSkill((byte)KRandom.GetRandomNumber(2, 4)))
                {
                    CommonAction();
                    return;
                }
                FollowAttack(nEnemyIdx);
                return;
            }
            if (nRand < m_AiParam[4])
            {
                if (!npcObj.SetActiveSkill(2))
                {
                    CommonAction();
                    return;
                }
            }
            else if (nRand < m_AiParam[4] + m_AiParam[5])
            {
                if (!npcObj.SetActiveSkill(3))
                {
                    CommonAction();
                    return;
                }
            }
            else if (nRand < m_AiParam[4] + m_AiParam[5] + m_AiParam[6])
            {
                if (!npcObj.SetActiveSkill(4))
                {
                    CommonAction();
                    return;
                }
            }
            else
            {
                return;
            }
            FollowAttack(nEnemyIdx);
        }
        void ProcessAIType03_06()
        {
            CharacterObj nEnemyIdx = ProcessAIType00();
            if (nEnemyIdx == null)
                return;

            if (npcObj.HPMax <= 0)
                npcObj.HPMax = 100;

            if (npcObj.HPCur * 100 / npcObj.HPMax < m_AiParam[1])
            {
                if (KRandom.g_RandPercent(m_AiParam[2]))
                {
                    if (KRandom.g_RandPercent(m_AiParam[3]))
                    {
                        npcObj.SetActiveSkill(1);
                        FollowAttack(nEnemyIdx);
                    }
                    else
                    {
                        Flee(nEnemyIdx);
                    }
                    return;
                }
            }

            npcObj.m_nPeopleIdx = nEnemyIdx.id;
            var nRand = KRandom.g_Random(100);

            if (npcObj.GetDistanceSquare(nEnemyIdx) > m_AiParam[9])
            {
                if (nRand < m_AiParam[7])
                    return;
                if (nRand < m_AiParam[7] + m_AiParam[8])
                {
                    CommonAction();
                    return;
                }
                if (!npcObj.SetActiveSkill((byte)KRandom.GetRandomNumber(1, 4)))
                {
                    CommonAction();
                    return;
                }
                FollowAttack(nEnemyIdx);
                return;
            }

            if (nRand < m_AiParam[4])
            {
                if (!npcObj.SetActiveSkill(2))
                {
                    CommonAction();
                    return;
                }
            }
            else if (nRand < m_AiParam[4] + m_AiParam[5])
            {
                if (!npcObj.SetActiveSkill(3))
                {
                    CommonAction();
                    return;
                }
            }
            else if (nRand < m_AiParam[4] + m_AiParam[5] + m_AiParam[6])
            {
                if (!npcObj.SetActiveSkill(4))
                {
                    CommonAction();
                    return;
                }
            }
            else
            {
                return;
            }
            FollowAttack(nEnemyIdx);
        }
    }
}
