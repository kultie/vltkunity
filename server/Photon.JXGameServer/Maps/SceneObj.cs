using Photon.JXGameServer.Helpers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Photon.JXGameServer.Skills;
using System.Linq;
using Photon.JXGameServer.Modulers;
using Photon.JXGameServer.Items;
using System.Text;
using Photon.JXGameServer.Entitys;

namespace Photon.JXGameServer.Maps
{
    public class SceneObj
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public ushort MapId;
        public ushort WorldIndex;
        public byte DropItemKind;

        public IntCounter NpcIndex;
        public IntCounter ObjIndex;

        public int m_dwCurrentTime = 0;
        public uint nNormalDropRate = 0;
        public uint nGoldenDropRate = 0;

        int m_nRegionBeginX, m_nRegionBeginY, m_nRegionEndX, m_nRegionEndY;
        int m_nWorldRegionWidth, m_nWorldRegionHeight;

        public List<RegionObj> m_Region;

        public List<int> o_Removes = new List<int>();
        List<MissleObj> o_Missles = new List<MissleObj>();

        public SceneObj(ushort id, ushort index) 
        {
            MapId = id;
            WorldIndex = index;

            NpcIndex = new IntCounter(0);
            ObjIndex = new IntCounter(0);

            DropItemKind = 0;
            m_Region = new List<RegionObj>();
        }
        public bool LoadMap(string path)
        {
            PhotonApp.log.InfoFormat("LoadMap start : id {0} index {1}", MapId, WorldIndex);

            if (!JXHelper.ExtractElem($"\\maps\\{path}.wor"))
                return false;

            var rectangleLiteral = new JX_Ini(JXHelper.bytes).Get<string>("MAIN", "rect");
            if (string.IsNullOrEmpty(rectangleLiteral))
                return false;

            string[] rectangleSplited = rectangleLiteral.Split(',');

            m_nRegionBeginX = (rectangleSplited.Length > 0) ? int.Parse(rectangleSplited[0]) : 0;
            m_nRegionBeginY = (rectangleSplited.Length > 1) ? int.Parse(rectangleSplited[1]) : 0;
            m_nRegionEndX = (rectangleSplited.Length > 2) ? int.Parse(rectangleSplited[2]) : 0;
            m_nRegionEndY = (rectangleSplited.Length > 3) ? int.Parse(rectangleSplited[3]) : 0;

            m_nWorldRegionWidth = m_nRegionEndX - m_nRegionBeginX + 1;
            m_nWorldRegionHeight = m_nRegionEndY - m_nRegionBeginY + 1;
            //m_nTotalRegion = m_nWorldRegionWidth * m_nWorldRegionHeight;

            int m_NpcNum = 0;

            int nX, nY;
            for (nY = 0; nY < m_nWorldRegionHeight; nY++)
            {
                for (nX = 0; nX < m_nWorldRegionWidth; nX++)
                {
                    var region = new RegionObj(this);
                    m_Region.Add(region);

                    m_NpcNum += region.LoadObjectS($"\\maps\\{path}", nX + m_nRegionBeginX, nY + m_nRegionBeginY);
                }
            }
            for (nY = 0; nY < m_nWorldRegionHeight; ++nY)
            {
                for (nX = 0; nX < m_nWorldRegionWidth; ++nX)
                {
                    int nIdx = nY * m_nWorldRegionWidth + nX;
                    for (int i = 0; i < 8; ++i)
                    {
                        int nTmpX = JXHelper.LoWord(m_Region[nIdx].m_nConRegionID[i]) - m_nRegionBeginX;
                        int nTmpY = JXHelper.HiWord(m_Region[nIdx].m_nConRegionID[i]) - m_nRegionBeginY;
                        if (nTmpX < 0 || nTmpY < 0 || nTmpX >= m_nWorldRegionWidth || nTmpY >= m_nWorldRegionHeight)
                        {
                            m_Region[nIdx].m_nConnectRegion[i] = -1;
                            m_Region[nIdx].m_pConRegion[i] = null;
                            continue;
                        }
                        int nConIdx = nTmpY * m_nWorldRegionWidth + nTmpX;
                        m_Region[nIdx].m_nConnectRegion[i] = nConIdx;
                        m_Region[nIdx].m_pConRegion[i] = m_Region[nConIdx];
                    }
                }
            }

            PhotonApp.log.InfoFormat("LoadMap end : id {0} index {1} => NPC {2}", MapId, WorldIndex, m_NpcNum);
            return true;
        }
        public byte TestBarrier(int nMpsX, int nMpsY)
        {
            int x = nMpsX / (JXHelper.m_nRegionWidth * JXHelper.m_nCellWidth);
            int y = nMpsY / (JXHelper.m_nRegionHeight * JXHelper.m_nCellHeight);

            if (x >= m_nWorldRegionWidth + m_nRegionBeginX || y >= m_nWorldRegionHeight + m_nRegionBeginY || x < m_nRegionBeginX || y < m_nRegionBeginY)
                return 0xff;

            int nRegion = FindRegion(JXHelper.MakeLong(x, y));
            if (nRegion < 0)
                return 0xff;

            if (nRegion >= m_Region.Count)
                return 0xff;

            x = nMpsX - m_Region[nRegion].m_nRegionX;
            y = nMpsY - m_Region[nRegion].m_nRegionY;

            int nCellX = x / JXHelper.m_nCellWidth;
            int nCellY = y / JXHelper.m_nCellHeight;

            int nOffX = x - nCellX * JXHelper.m_nCellWidth;
            int nOffY = y - nCellY * JXHelper.m_nCellHeight;

            return m_Region[nRegion].GetBarrier(nCellX, nCellY, nOffX, nOffY);
        }
        public byte TestBarrierMin(int nRegion, int nMapX, int nMapY, int nDx, int nDy, int nChangeX, int nChangeY)
        {
            int nOldMapX = nMapX;
            int nOldMapY = nMapY;
            int nOldRegion = nRegion;

            nDx += nChangeX;
            nDy += nChangeY;

            if (nDx < 0)
            {
                nDx += JXHelper.CELLWIDTH;
                nMapX--;
            }
            else 
            if (nDx >= JXHelper.CELLWIDTH)
            {
                nDx -= JXHelper.CELLWIDTH;
                nMapX++;
            }

            if (nDy < 0)
            {
                nDy += JXHelper.CELLHEIGHT;
                nMapY--;
            }
            else 
            if (nDy >= JXHelper.CELLHEIGHT)
            {
                nDy -= JXHelper.CELLHEIGHT;
                nMapY++;
            }

            if (nMapX < 0)
            {
                if (m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_LEFT] == -1)
                    return 0xff;

                nRegion = m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_LEFT];
                nMapX += JXHelper.m_nRegionWidth;
            }
            else 
            if (nMapX >= JXHelper.m_nRegionWidth)
            {
                if (m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_RIGHT] == -1)
                    return 0xff;

                nRegion = m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_RIGHT];
                nMapX -= JXHelper.m_nRegionWidth;
            }

            if (nMapY < 0)
            {
                if (m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_UP] == -1)
                    return 0xff;

                nRegion = m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_UP];
                nMapY += JXHelper.m_nRegionHeight;
            }
            else 
            if (nMapY >= JXHelper.m_nRegionHeight)
            {
                if (m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_DOWN] == -1)
                    return 0xff;

                nRegion = m_Region[nRegion].m_nConnectRegion[(byte)NPCDIR.DIR_DOWN];
                nMapY -= JXHelper.m_nRegionHeight;
            }

            if (nMapX == nOldMapX && nMapY == nOldMapY && nRegion == nOldRegion)
            {
                //return m_Region[nRegion].GetBarrierMin(nMapX, nMapY, nDx, nDy, false);
                return m_Region[nRegion].GetBarrierNewMin(nMapX, nMapY, nDx, nDy, false);
            }

            //return m_Region[nRegion].GetBarrierMin(nMapX, nMapY, nDx, nDy, true);
            return m_Region[nRegion].GetBarrierNewMin(nMapX, nMapY, nDx, nDy, true);
        }
        public void HeartBeat()
        {
            ++m_dwCurrentTime;
            foreach (var mis in o_Missles)
            {
                mis.HeartBeat();
            }
            if (o_Removes.Count > 0)
            {
                o_Missles = o_Missles.Where(x => !o_Removes.Contains(x.id)).ToList();
                o_Removes.Clear();
            }
            foreach (var region in m_Region)
            {
                region.HeartBeat();
            }
        }
        public void AddMissleObj(MissleObj obj)
        {
            o_Missles.Add(obj);
            obj.id = o_Missles.Count;
        }
        public void DelMissleObj(int id)
        {
            o_Removes.Add(id);
        }
        public MissleObj GetMissleObj(int id)
        {
            return o_Missles.FirstOrDefault(o => o.id == id);
        }
        void DropObj(ObjectObj obj, int nBelongPlayer, int nX, int nY)
        {
            int region = obj.AddMap(nX, nY);
            if (region < 0)
                return;

            if (region >= m_Region.Count)
                return;

            obj.id = ObjIndex.Next;
            obj.m_Dir = SceneModule.Me.GetObjectDir(obj.nTemplateID);
            m_Region[region].AddObjectObj(obj);

            obj.SetItemBelong(nBelongPlayer);
        }
        public void DropItem(int nBelongPlayer, ItemObj nItem, int nX, int nY)
        {
            var obj = new ObjectObj(this, null);
            obj.nTemplateID = nItem.m_CommonAttrib.nObjIdx;
            obj.Kind = ObjKind.Obj_Kind_Item;
            obj.iItem = nItem;

            DropObj(obj, nBelongPlayer, nX, nY);
        }
        public void DropMoney(int nBelongPlayer, int nMoney, int nX, int nY)
        {
            var obj = new ObjectObj(this, null);
            obj.nTemplateID = SceneModule.Me.GetMoneyDataId(nMoney);
            obj.Kind = ObjKind.Obj_Kind_Money;
            obj.iMoney = nMoney;

            DropObj(obj, nBelongPlayer, nX, nY);
        }
        public bool CanPutObj(int posx, int posy,int nModle,bool nIsCheckNpc)
        {
            int nRegion = 0, nMapX = 0, nMapY = 0, nOffX = 0, nOffY = 0;
            Mps2Map(posx, posy, ref nRegion, ref nMapX, ref nMapY, ref nOffX, ref nOffY);

            if (nRegion < 0)
                return false;

            if (nRegion >= m_Region.Count) 
                return false;

            if (m_Region[nRegion].HasObj(nMapX, nMapY))
                return false;
            
            if (nModle==0)
            {
                return (m_Region[nRegion].GetNewBarrier(nMapX, nMapY, nOffX, nOffY, nIsCheckNpc) == 0);
            }
            else
            {
                return (m_Region[nRegion].GetBarrierNewMin(nMapX, nMapY, nOffX, nOffY, nIsCheckNpc) == 0);
            }
        }
        public void GetFreeObjPos(ref int posLocalX, ref int posLocalY)
        {
            byte nModel = 0; bool nIsCheckNpc = false;
            if (CanPutObj(posLocalX, posLocalY, nModel, nIsCheckNpc))
                return;

            int posTempX, posTempY;

            for (int nLayer = 1; nLayer < 10; nLayer++)
            {
                for (int i = 0; i <= nLayer; ++i)
                {
                    posTempY = posLocalY + i * 32;
                    posTempX = posLocalX + (nLayer - i) * 32;
                    if (CanPutObj(posTempX, posTempY, nModel, nIsCheckNpc))
                    {
                        posLocalX = posTempX;
                        posLocalY = posTempY;
                        return;
                    }
                    posTempY = posLocalY + i * 32;
                    posTempX = posLocalX - (nLayer - i) * 32;
                    if (CanPutObj(posTempX, posTempY, nModel, nIsCheckNpc))
                    {
                        posLocalX = posTempX;
                        posLocalY = posTempY;
                        return;
                    }
                    posTempY = posLocalY - i * 32;
                    posTempX = posLocalX + (nLayer - i) * 32;
                    if (CanPutObj(posTempX, posTempY, nModel, nIsCheckNpc))
                    {
                        posLocalX = posTempX;
                        posLocalY = posTempY;
                        return;
                    }
                    posTempY = posLocalY - i * 32;
                    posTempX = posLocalX - (nLayer - i) * 32;
                    if (CanPutObj(posTempX, posTempY, nModel, nIsCheckNpc))
                    {
                        posLocalX = posTempX;
                        posLocalY = posTempY;
                        return;
                    }
                }
            }
        }
        public void Mps2Map(int Rx, int Ry, ref int nR, ref int nX, ref int nY, ref int nDx, ref int nDy)
        {
            int x = Rx / (JXHelper.m_nRegionWidth * JXHelper.m_nCellWidth);
            int y = Ry / (JXHelper.m_nRegionHeight * JXHelper.m_nCellHeight);

            nX = 0;
            nY = 0;
            nDx = 0;
            nDy = 0;

            nR = FindRegion(JXHelper.MakeLong(x, y));
            if (nR < 0)
                return;

            if (nR >= m_Region.Count) 
                return;

            x = Rx - m_Region[nR].m_nRegionX;
            y = Ry - m_Region[nR].m_nRegionY;

            nX = x / JXHelper.m_nCellWidth;
            nY = y / JXHelper.m_nCellHeight;

            nDx = (x - nX * JXHelper.m_nCellWidth) << 10;
            nDy = (y - nY * JXHelper.m_nCellHeight) << 10;
        }
        public void NewMap2Mps(RegionObj nR, int nX, int nY, int nDx, int nDy, ref int nRx, ref int nRy)
        {
            int x, y;

            x = nR.m_nRegionX;
            y = nR.m_nRegionY;

            x += nX * JXHelper.m_nCellWidth;
            y += nY * JXHelper.m_nCellHeight;

            x += (nDx >> 10);
            y += (nDy >> 10);

            nRx = x;
            nRy = y;
        }
        public int ToIndex(RegionObj obj) => m_Region.IndexOf(obj);
        public int FindRegion(int nRegion)
        {
            for (int i = 0; i < m_Region.Count; ++i)
            {
                if (m_Region[i].m_RegionID == nRegion)
                    return i;
            }
            return -1;
        }
        public RegionObj GetRegion(int nRegion)
        {
            foreach (var region in m_Region)
            {
                if (region.m_RegionID == nRegion)
                    return region;
            }

            return null;
        }
        public RegionObj GetRegionByIndex(int nRegionIndex)
        {
            foreach (var region in m_Region)
            {
                if (region.RegionIndex == nRegionIndex)
                    return region;
            }

            return null;
        }
        public ObjectObj FindObjectObj(int id)
        {
            foreach (var region in m_Region)
            {
                var obj = region.FindObjectObj(id);
                if (obj != null) return obj;
            }
            return null;
        }
        public NpcObj FindNpc(int id)
        {
            foreach (var region in m_Region)
            {
                var npc = region.FindNpc(id);
                if (npc != null) return npc;
            }
            return null;
        }
        public PlayerObj FindPlayer(int id)
        {
            foreach (var region in m_Region)
            {
                var player = region.FindPlayer(id);
                if (player != null) return player;
            }
            return null;
        }
        public CharacterObj FindObj(int id)
        {
            CharacterObj obj = FindPlayer(id);

            if (obj == null)
                obj = FindNpc(id);

            return obj;
        }
        public void AddPlayer(PlayerObj player, int X, int Y)
        {
            int nRegion = player.AddMap(X, Y);
            if (nRegion >= 0)
            {
                player.state = PlayerState.wait;

                m_Region[nRegion].AddPlayer(player);

                player.ClientPeer.SendEvent(new EventData
                {
                    Code = (byte)OperationCode.SendWorld,
                    Parameters = new Dictionary<byte, object>()
                    {
                        [(byte)ParamterCode.Id] = player.id,
                        [(byte)ParamterCode.MapId] = MapId,
                        [(byte)ParamterCode.MapX] = X,
                        [(byte)ParamterCode.MapY] = Y,
                    }
                }, player.sendParameters);
            }
            else
            {
                PhotonApp.log.ErrorFormat("region {0} not found",nRegion);
            }
        }
        public void RemovePlayer(PlayerObj player)
        {
            if (player.region != null)
            {
                player.region.RemovePlayer(player);
            }
        }
        public void ChangeRegion(int nSrcRnidx, int nDesRnIdx, MapObj obj, int OldX, int OldY)
        {
            var player = obj as PlayerObj;
            var npc = obj as NpcObj;

            if (nSrcRnidx >= 0)
            {
                var tx = obj.m_MapX; obj.m_MapX = OldX;
                var ty = obj.m_MapY; obj.m_MapY = OldY;
                if (player != null)
                {
                    m_Region[nSrcRnidx].RemovePlayer(player);
                }
                else
                if (npc != null)
                {
                    m_Region[nSrcRnidx].RemoveNpc(npc);
                }
                obj.m_MapX = tx;
                obj.m_MapY = ty;
            }
            if (nDesRnIdx >= 0)
            {
                if (player != null)
                {
                    m_Region[nDesRnIdx].AddPlayer(player);
                    m_Region[nDesRnIdx].SendMe(player);
                }
                else
                if (npc != null)
                {
                    m_Region[nDesRnIdx].AddNpc(npc);
                }
            }
        }
    }
}
