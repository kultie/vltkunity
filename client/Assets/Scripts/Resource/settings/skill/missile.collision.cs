
using System.Collections.Generic;
using Unity.VisualScripting;

namespace game.resource.settings.skill.missile
{
    public class Collision : skill.missile.Event
    {
        private Dictionary<int, int> colidePlayerTriger;

        private bool CanColideAndCalcDamagePlayer(int playerIndex)
        {
            int nextCalcDamageTime = this.m_nCurrentLife + 18 + this.missileSetting.m_ulDamageInterval;

            if (this.colidePlayerTriger == null)
            {
                this.colidePlayerTriger = new Dictionary<int, int>();
                this.colidePlayerTriger[playerIndex] = nextCalcDamageTime;
                return true;
            }

            if (this.colidePlayerTriger.ContainsKey(playerIndex) == false)
            {
                this.colidePlayerTriger[playerIndex] = nextCalcDamageTime;
                return true;
            }

            if(this.colidePlayerTriger[playerIndex] > this.m_nCurrentLife)
            {
                return false;
            }

            this.colidePlayerTriger[playerIndex] = nextCalcDamageTime;
            return true;
        }

        protected void ProcessDamage(int nNpcId)
        {
            settings.npcres.Controller targetNpc = this.map.GetNpc(nNpcId);

            if(this.skillSetting.m_nStateSpecialId > 0)
            {
                targetNpc.SetStateSkillEffect(this.skillSetting.m_nStateSpecialId);
            }

            //if (targetNpc.ReceiveDamage(
            //    this.skillParam.launcher.npc, 
            //    this.skillSetting.m_bIsMelee != 0, 
            //    this.skillSetting.m_DamageAttribs,
            //    this.skillSetting.m_bUseAttackRate != 0, 
            //    this.skillSetting.m_bDoHurt != 0, 
            //    0))
            //{
            //    if (this.skillSetting.m_nStateAttribsNum > 0)
            //    {
            //        targetNpc.SetStateSkillEffect(
            //            this.skillSetting.m_nStateSpecialId, 
            //            this.skillSetting.m_StateAttribs, 
            //            this.skillSetting.m_nStateAttribsNum, 
            //            this.skillSetting.m_StateAttribs[0].nValue[1]
            //        );
            //    }

            //    if (this.skillSetting.m_nImmediateAttribsNum > 0)
            //    {
            //        targetNpc.SetImmediatelySkillEffect(
            //            this.skillSetting.m_ImmediateAttribs, 
            //            this.skillSetting.m_nImmediateAttribsNum
            //        );
            //    }
            //}
        }

        protected void DoCollision()
        {
            if (m_eMissleStatus == Defination.MissleStatus.MS_DoCollision) return;

            if (this.skillSetting.m_bCollideEvent != 0)
            {
                this.OnMissleEvent(Defination.MissileEvent.Missle_CollideEvent);
            }

            if (this.missileSetting.m_bCollideVanish != 0)
            {
                this.self.DoVanish();
            }
            else
            {
                m_eMissleStatus = Defination.MissleStatus.MS_DoFly;
            }
        }

        protected void ProcessCollision(Dictionary<int, bool> npcList)
        {
            foreach (KeyValuePair<int, bool> pairIndex in npcList)
            {
                //if(this.skillSetting.m_nStateSpecialId > 0)
                //{
                //    this.map.GetNpc(pairIndex.Key).SetStateSkillEffect(this.skillSetting.m_nStateSpecialId);
                //}

                this.ProcessDamage(pairIndex.Key);
            }
        }

        protected void ProcessCollision()
        {
            int nRangeX = this.missileSetting.m_nDamageRange / 2;
            int nRangeY = nRangeX;

            resource.map.Position.Grid gridCenter = this.texture.GetMapPosition().GetGrid();
            Dictionary<int, Dictionary<int, bool>> gridList = new Dictionary<int, Dictionary<int, bool>>();
            Dictionary<int, bool> npcList = new Dictionary<int, bool>();

            for (int i = -nRangeX; i <= nRangeX; i++)
            {
                for (int j = -nRangeY; j <= nRangeY; j++)
                {

                    resource.map.Position position = new map.Position(gridCenter.gridTop + j, gridCenter.gridLeft + i);
                    resource.map.Position.Grid grid = position.GetGrid();

                    if (gridList.ContainsKey(grid.gridTop) == false)
                    {
                        gridList[grid.gridTop] = new Dictionary<int, bool>();
                    }

                    gridList[grid.gridTop][grid.gridLeft] = true;
                }
            }

            foreach (KeyValuePair<int, Dictionary<int, bool>> topPair in gridList)
            {
                foreach (KeyValuePair<int, bool> leftPair in topPair.Value)
                {
                    Dictionary<int, bool> listInPoint = this.map.FindListNpc(new resource.map.Position.Grid(topPair.Key, leftPair.Key));
                    npcList.AddRange(listInPoint);
                }
            }

            if (npcList.ContainsKey(this.skillParam.launcher.npc.map.npcIndex) == true)
            {
                npcList.Remove(this.skillParam.launcher.npc.map.npcIndex);
            }

            if (npcList.Count > 0)
            {
                ProcessCollision(npcList);
            }
        }

        protected int CheckCollision()
        {
            if (m_nCurrentMapZ <= skill.Defination.MISSLE_MIN_COLLISION_ZHEIGHT)
            {
                return -1;
            }

            if (m_nCurrentMapZ > skill.Defination.MISSLE_MAX_COLLISION_ZHEIGHT)
            {
                return 0;
            }

            int launcherPlayerMapIndex = this.skillParam.launcher.npc.map.npcIndex;

            if (this.missileSetting.m_nCollideRange == 1)
            {
                Dictionary<int, bool> npcList = this.map.FindListNpc(this.texture.GetMapPosition().GetGrid());

                foreach (KeyValuePair<int, bool> pairIndex in npcList)
                {
                    if(pairIndex.Key == launcherPlayerMapIndex)
                    {
                        continue;
                    }

                    if(this.CanColideAndCalcDamagePlayer(pairIndex.Key) == false)
                    {
                        continue;
                    }

                    ProcessCollision(new Dictionary<int, bool> { { pairIndex.Key, pairIndex.Value } });
                    DoCollision();
                    return 1;
                }
            }
            else
            {
                resource.map.Position.Grid gridCenter = this.texture.GetMapPosition().GetGrid();
                Dictionary<int, Dictionary<int, bool>> gridList = new Dictionary<int, Dictionary<int, bool>>();
                Dictionary<int, bool> npcList = new Dictionary<int, bool>();

                for (int i = -this.missileSetting.m_nCollideRange; i <= this.missileSetting.m_nCollideRange; i++)
                {
                    for (int j = -this.missileSetting.m_nCollideRange; j <= this.missileSetting.m_nCollideRange; j++)
                    {
                        resource.map.Position position = new map.Position(gridCenter.gridTop + j, gridCenter.gridLeft + i);
                        resource.map.Position.Grid grid = position.GetGrid();
                        
                        if(gridList.ContainsKey(grid.gridTop) ==  false)
                        {
                            gridList[grid.gridTop] = new Dictionary<int, bool>();
                        }

                        gridList[grid.gridTop][grid.gridLeft] = true;
                    }
                }

                foreach(KeyValuePair<int, Dictionary<int, bool>> topPair in gridList)
                {
                    foreach(KeyValuePair<int, bool> leftPair in topPair.Value)
                    {
                        Dictionary<int, bool> listInPoint = this.map.FindListNpc(new resource.map.Position.Grid(topPair.Key, leftPair.Key));

                        foreach(KeyValuePair<int, bool> playerIndexPair in listInPoint)
                        {
                            if(this.CanColideAndCalcDamagePlayer(playerIndexPair.Key) == false)
                            {
                                continue;
                            }

                            npcList[playerIndexPair.Key] = true;
                        }
                    }
                }

                if (npcList.ContainsKey(launcherPlayerMapIndex) == true)
                {
                    npcList.Remove(launcherPlayerMapIndex);
                }

                if (npcList.Count > 0)
                {
                    ProcessCollision(npcList);
                    DoCollision();
                    return 1;
                }
            }

            return 0;
        }

        protected void OnCollision()
        {
            return;
        }
    }
}
