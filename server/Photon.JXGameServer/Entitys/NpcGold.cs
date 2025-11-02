using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using System;

namespace Photon.JXGameServer.Entitys
{
    public class NpcGold
    {
        public byte m_nGoldType;
        NpcObj npcObj;
        public NpcGold(NpcObj npc) 
        {
            m_nGoldType = 0;
            npcObj = npc;
        }
        public void ChangeGold(short bossType)
        {
            m_nGoldType = 0;
            npcObj.szDropFile = 0;

            if (bossType < 0)
            {
            }
            else
            if (bossType > 0)
            {
                m_nGoldType = (byte)(bossType + 1);
            }
            else//
            {
                var npMapGoldRate = SceneModule.Me.g_NpcMapDropRate.Get<int>("List", $"{npcObj.scene.MapId}_AutoGoldenNpc");
                if ((0 <= npMapGoldRate)  && KRandom.g_Random(npMapGoldRate) <= KRandom.g_Random(PhotonApp.MapAutoGoldNpcRank))
                {
                    npcObj.szDropFile = npcObj.scene.nNormalDropRate;
                    return;
                }
                m_nGoldType = (byte)KRandom.g_Random(6);
            }

            if (m_nGoldType == 0)
                return;

            npcObj.szDropFile = npcObj.scene.nGoldenDropRate;

            npcObj.m_CurrentExperience *= SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.Exp, m_nGoldType);
            
            npcObj.HPMax *= SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.Life, m_nGoldType);
            npcObj.HPCur = npcObj.HPMax;

            npcObj.m_CurrentLifeReplenish *= (ushort)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.LifeReplenish, m_nGoldType);
            npcObj.m_CurrentAttackRating *= (ushort)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.AttackRating, m_nGoldType);
            npcObj.m_CurrentDefend *= (ushort)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.Defense, m_nGoldType);

            npcObj.m_PhysicsDamage.nValue[0] *= (ushort)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.MinDamage, m_nGoldType);
            npcObj.m_PhysicsDamage.nValue[2] *= (ushort)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.MaxDamage, m_nGoldType);

            npcObj.m_CurrentTreasure += (byte)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.Treasure, m_nGoldType);
            npcObj.m_CurrentWalkSpeed += (byte)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.WalkSpeed, m_nGoldType);
            npcObj.m_CurrentRunSpeed += (byte)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.RunSpeed, m_nGoldType);
            npcObj.m_CurrentAttackSpeed += (byte)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.AttackSpeed, m_nGoldType);
            npcObj.m_CurrentCastSpeed += (byte)SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.CastSpeed, m_nGoldType);

            var m_dwSkill5ID = SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.SkillName, m_nGoldType);
            var m_szSkill5Level = SceneModule.Me.NpcTableGold.Get<int>((byte)NpcGolds_Index.SkillLevel, m_nGoldType);

            if (m_dwSkill5ID > 0 && m_szSkill5Level > 0)
            {
                npcObj.SetSkillLevel(5, m_dwSkill5ID, (byte)m_szSkill5Level);
            }
            var aura = npcObj.GetSkillLevel(5);
            if (aura != null)
            {
                if (aura.level <= 0)
                    aura.level = 1;

                npcObj.SetAuraSkill(5);
            }

            var skill = npcObj.GetSkillLevel(6);
            if (skill != null)
            {
                if (skill.level <= 0)
                    skill.level = 1;

                npcObj.SetActiveSkill(6);
            }

            npcObj.szDropFile = npcObj.scene.nGoldenDropRate;
        }
    }
}
