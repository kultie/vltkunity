using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.JXGameServer.Skills
{
    public class NpcDeathCalcExp
    {
        public Dictionary<int, int> m_sCalcInfo;
        public NpcDeathCalcExp() 
        {
            m_sCalcInfo = new Dictionary<int, int>();
        }
        public void Clear()
        {
            m_sCalcInfo.Clear();
        }
        public int CalcExp(NpcObj npcObj)
        {
            int nDamage = 0, nMaxPlayer = 0, nMinDistance, nDistance, nPlayer;

            foreach (var kvp in m_sCalcInfo)
            {
                var player = npcObj.scene.FindPlayer(kvp.Key);
                if (player == null) continue;

                nMinDistance = JXHelper.PLAYER_SHARE_EXP_DISTANCE * JXHelper.PLAYER_SHARE_EXP_DISTANCE;

                if (player.TeamId > 0)// co' team
                {
                    var team = PlayerModule.Me.GetTeamObj(player.TeamId);
                    if (team != null)
                    {
                        nPlayer = 0;

                        nDistance = npcObj.GetPlayerSquare(team.Captain);
                        if (0 <= nDistance && nDistance < nMinDistance)
                        {
                            nMinDistance = nDistance;
                            nPlayer = team.Captain;
                        }
                        foreach (var id in team.Members)
                        {
                            nDistance = npcObj.GetPlayerSquare(id);
                            if (0 <= nDistance && nDistance < nMinDistance)
                            {
                                nMinDistance = nDistance;
                                nPlayer = id;
                            }
                        }
                        if (nPlayer > 0)
                        {
                            if (kvp.Value > nDamage)
                            {
                                nMaxPlayer = nPlayer;
                                nDamage = kvp.Value;
                            }

                            if ((npcObj.scene.DropItemKind & (byte)MAP_ITEMKIND_NODROP.NO_DROP_EXP) == 0)
                            {
                                player.AddExp(npcObj.npcTemplate.m_Experience*PhotonApp.ExpSale, npcObj.Level);
                            }
                        }
                        continue;
                    }

                }
                // khong team
                nDistance = npcObj.GetDistanceSquare(player);
                if (nDistance >= nMinDistance)
                    continue;

                if (kvp.Value > nDamage)
                {
                    nMaxPlayer = kvp.Key;
                    nDamage = kvp.Value;
                }

                if ((npcObj.scene.DropItemKind & (byte)MAP_ITEMKIND_NODROP.NO_DROP_EXP) == 0)
                {
                    player.AddExp(npcObj.npcTemplate.m_Experience* PhotonApp.ExpSale, npcObj.Level);
                }
            }
            return nMaxPlayer;
        }
        public void AddDamage(int nPlayerIdx, int nDamage)
        {
            if (m_sCalcInfo.ContainsKey(nPlayerIdx))
                m_sCalcInfo[nPlayerIdx] += nDamage;
            else
                m_sCalcInfo.Add(nPlayerIdx, nDamage);
        }
    }
}
