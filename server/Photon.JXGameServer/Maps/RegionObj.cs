using System.Collections.Generic;
using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Helpers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;

namespace Photon.JXGameServer.Maps
{
    public class RegionObj
    {
        public int RegionIndex
        {
            get
            {
                return scene.ToIndex(this);
            }
        }
        public int m_RegionID;
        public int m_nRegionX;
        public int m_nRegionY;

        public int[] m_nConRegionID;
        public int[] m_nConnectRegion;
        public RegionObj[] m_pConRegion;

        _KGridData m_cObstacle;
        _KGridData m_cTrap;
        _KGridData m_cNpcRef;
        _KGridData m_cObjRef;

        public bool HasNpc(int nMapX, int nMapY) => m_cNpcRef.HasData(nMapX, nMapY);
        public uint GetNpcRef(int nMapX, int nMapY) => m_cNpcRef.GetData(nMapX, nMapY);
        public void AddNpcRef(int nMapX, int nMapY) => m_cNpcRef.IncData(nMapX, nMapY);
        public void DecNpcRef(int nMapX, int nMapY) => m_cNpcRef.DecData(nMapX, nMapY);

        public bool HasObj(int nMapX, int nMapY) => m_cObjRef.HasData(nMapX, nMapY);
        public uint GetObjRef(int nMapX, int nMapY) => m_cObjRef.GetData(nMapX, nMapY);
        public void AddObjRef(int nMapX, int nMapY) => m_cObjRef.IncData(nMapX, nMapY);
        public void DecObjRef(int nMapX, int nMapY) => m_cObjRef.DecData(nMapX, nMapY);

        SceneObj scene;

        List<NpcObj> m_npcs;
        List<ObjectObj> m_objs;
        List<PlayerObj> m_players;

        public RegionObj(SceneObj scene)
        {
            this.scene = scene;

            m_cObstacle = new _KGridData();
            m_cTrap = new _KGridData();
            m_cNpcRef = new _KGridData();
            m_cObjRef = new _KGridData();

            m_npcs = new List<NpcObj>();
            m_objs = new List<ObjectObj>();
            m_players = new List<PlayerObj>();
        }
        void Load(int nX, int nY)
        {
            m_RegionID = JXHelper.MakeLong(nX, nY);

            m_nRegionX = nX * 512;
            m_nRegionY = nY * 1024;

            m_nConRegionID = new int[8];
            m_nConnectRegion = new int[8];
            m_pConRegion = new RegionObj[8];

            m_nConRegionID[0] = JXHelper.MakeLong(nX, nY + 1);
            m_nConRegionID[1] = JXHelper.MakeLong(nX - 1, nY + 1);
            m_nConRegionID[2] = JXHelper.MakeLong(nX - 1, nY);
            m_nConRegionID[3] = JXHelper.MakeLong(nX - 1, nY - 1);
            m_nConRegionID[4] = JXHelper.MakeLong(nX, nY - 1);
            m_nConRegionID[5] = JXHelper.MakeLong(nX + 1, nY - 1);
            m_nConRegionID[6] = JXHelper.MakeLong(nX + 1, nY);
            m_nConRegionID[7] = JXHelper.MakeLong(nX + 1, nY + 1);
        }
        public int LoadObjectS(string szCurPath, int nX, int nY)
        {
            Load(nX, nY);

            var szFilePath = $"{szCurPath}\\v_{nY.ToString("000")}";
            var szFile = $"{szFilePath}\\{nX.ToString("000")}_region_s.dat";

            int npc = 0;
            if (JXHelper.ExtractFile(szFile))
            {
                JXHelper.LoadObstacle(m_cObstacle);
                JXHelper.LoadTrap(m_cTrap);
                npc = JXHelper.LoadNpc(this);
                JXHelper.LoadObj(this);
            }
            return npc;
        }
        public uint CheckTrap(int nMapX, int nMapY)
        {
            if (nMapX < 0 || nMapY < 0 || nMapX >= JXHelper.m_nRegionWidth || nMapY >= JXHelper.m_nRegionHeight)
                return 0;

            return m_cTrap.GetData(nMapX, nMapY);
        }
        public void LoadNpc(KSPNpc kSPNpc)
        {
            NpcObj npc = new NpcObj(scene, this);
            if (npc.LoadNPC(kSPNpc))
            {
                npc.id = scene.NpcIndex.Next;
                m_npcs.Add(npc);
                npc.Init();
            }
        }
        public void LoadObj(KSPObj kSPObj)
        {
            ObjectObj obj = new ObjectObj(scene, this);
            if (obj.LoadData(kSPObj))
            {
                obj.id = scene.ObjIndex.Next;
                m_objs.Add(obj);
            }
        }
        public byte GetBarrier(int nMapX, int nMapY, int nDx, int nDy)
        {
            if (nMapX < 0 || nMapX >= JXHelper.m_nRegionWidth || nMapY < 0 || nMapY >= JXHelper.m_nRegionHeight)
                return (byte)Obstacle_Kind.Obstacle_NULL;

            var lInfo = m_cObstacle.GetData(nMapX, nMapY);

            var lRet = (byte)(lInfo & 0x0000000f);
            var lType = (byte)((lInfo >> 4) & 0x0000000f);

            switch (lType)
            {
                case (byte)Obstacle_Type.Obstacle_LT:
                    if (nDx + nDy > 32)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_RT:
                    if (nDx < nDy)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_LB:
                    if (nDx > nDy)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_RB:
                    if (nDx + nDy < 32)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
            }
            if (lRet != (byte)Obstacle_Kind.Obstacle_NULL)
                return lRet;

            if (HasNpc(nMapX, nMapY))
                return (byte)Obstacle_Kind.Obstacle_JumpFly;

            return (byte)Obstacle_Kind.Obstacle_NULL;
        }
        public byte GetNewBarrier(int nMapX, int nMapY, int nDx, int nDy, bool nIsCheckNpc)
        {
            if (nMapX < 0 || nMapX >= JXHelper.m_nRegionWidth || nMapY < 0 || nMapY >= JXHelper.m_nRegionHeight)
                return (byte)Obstacle_Kind.Obstacle_NULL;

            var lInfo = m_cObstacle.GetData(nMapX, nMapY);

            var lRet = (byte)(lInfo & 0x0000000f);
            var lType = (byte)((lInfo >> 4) & 0x0000000f);

            switch (lType)
            {
                case (byte)Obstacle_Type.Obstacle_LT:
                    if (nDx + nDy > 32)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_RT:
                    if (nDx < nDy)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_LB:
                    if (nDx > nDy)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_RB:
                    if (nDx + nDy < 32)
                        lRet = (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
            }

            if (lRet != (byte)Obstacle_Kind.Obstacle_NULL)
                return lRet;

            if (nIsCheckNpc)
            {
                if (HasNpc(nMapX, nMapY))
                    return (byte)Obstacle_Kind.Obstacle_JumpFly;
            }

            return (byte)Obstacle_Kind.Obstacle_NULL;
        }
        public byte GetBarrierNewMin(int nGridX, int nGridY, int nOffX, int nOffY, bool bCheckNpc)
        {
            if (nGridX < 0 || nGridX >= JXHelper.m_nRegionWidth || nGridY < 0 || nGridY >= JXHelper.m_nRegionHeight)
                return (byte)Obstacle_Kind.Obstacle_NULL;

            var lInfo = m_cObstacle.GetData(nGridX, nGridY);

            var lRet = (byte)(lInfo & 0x0000000f);
            var lType = (byte)((lInfo >> 4) & 0x0000000f);

            if (lRet == (byte)Obstacle_Kind.Obstacle_NULL)
            {
                if (bCheckNpc)
                {
                    if (HasNpc(nGridX, nGridY))
                        return (byte)Obstacle_Kind.Obstacle_JumpFly;
                }
                return (byte)Obstacle_Kind.Obstacle_NULL;
            }

            switch (lType)
            {
                case (byte)Obstacle_Type.Obstacle_LT:
                    if (nOffX + nOffY > 32 * 1024)
                        return (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_RT:
                    if (nOffX < nOffY)
                        return (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_LB:
                    if (nOffX > nOffY)
                        return (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
                case (byte)Obstacle_Type.Obstacle_RB:
                    if (nOffX + nOffY < 32 * 1024)
                        return (byte)Obstacle_Kind.Obstacle_NULL;
                    break;
            }

            return lRet;
        }
        public CharacterObj FindObj(int id)
        {
            CharacterObj obj = FindPlayer(id);

            if (obj == null)
                obj = FindNpc(id);

            return obj;
        }
        public CharacterObj FindObj(int nMapX, int nMapY, CharacterObj obj, NPCRELATION nRelation)
        {
            //if (GetNpcRef(nMapX, nMapY) <= 0)
            //    return null;

            foreach (var player in m_players)
            {
                if ((player.m_MapX == nMapX) && (player.m_MapY == nMapY))
                {
                    if (Utils.GetRelation(obj, player) == nRelation)
                        return player;
                }
            }
            foreach (var npc in m_npcs)
            {
                if ((npc.m_MapX == nMapX) && (npc.m_MapY == nMapY))
                {
                    if (Utils.GetRelation(obj, npc) == nRelation)
                        return npc;
                }
            }

            return null;
        }
        public void SendMessage(PlayerObj me,string message)
        {
            var send = new EventData
            {
                Code = (byte)OperationCode.DoChat,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.CharacterName, me.Name },
                    { (byte)ParamterCode.Message, message },
                    { (byte)ParamterCode.Data, (byte)PlayerChat.near },
                },
            };

            foreach (var player in m_players)
            {
                if (player != me)
                {
                    player.ClientPeer.SendEvent(send, player.sendParameters);
                }
            }
        }
        public void SendMe(PlayerObj me)
        {
            SendObj(me);
            SendNpc(me);
            SendPlayer(me);
        }
        void SendObj(PlayerObj me)
        {
            foreach (var obj in m_objs)
            {
                obj.SyncNormal(me);
            }
        }
        void SendNpc(PlayerObj me)
        {
            foreach (var npc in m_npcs)
            {
                npc.SyncNormal(me);
            }
        }
        void SendPlayer(PlayerObj me)
        {
            foreach (var player in m_players)
            {
                if (player != me)
                {
                    player.SyncNormal(me);
                }
            }
        }
        public void HeartBeat()
        {
            var t_players = new List<PlayerObj>(m_players);
            foreach (var me in t_players)
            {
                if (me.isPlaying)
                {
                    me.HeartBeat();
                }
            }
            var t_npcs = new List<NpcObj>(m_npcs);
            foreach (var npc in t_npcs)
            {
                npc.HeartBeat();
            }
            var t_objs = new List<ObjectObj>(m_objs);
            foreach (var obj in t_objs)
            {
                obj.HeartBeat();
            }
        }
        public ObjectObj FindObjectObj(int id)
        {
            foreach (var obj in m_objs)
            {
                if (obj.id == id)
                    return obj;
            }
            return null;
        }
        public void AddObjectObj(ObjectObj obj)
        {
            obj.region = this;
            m_objs.Add(obj);

            foreach (var me in m_players)//all
            {
                if (me.isPlaying)
                {
                    obj.SyncNormal(me);
                }
            }
        }
        public void RemoveObjectObj(ObjectObj obj)
        {
            m_objs.Remove(obj);

            var send = new EventData
            {
                Code = (byte)OperationCode.SyncObjDel,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.Id, obj.id }
                }
            };

            BroadCast(send);
        }
        public NpcObj FindNpc(int id)
        {
            foreach (var npc in m_npcs)
            {
                if (npc.id == id)
                    return npc;
            }
            return null;
        }
        public NpcObj AddNpc(int id)
        {
            return null;
        }
        public void AddNpc(NpcObj npc)
        {
            AddNpcRef(npc.m_MapX, npc.m_MapY);
            npc.region = this;
            m_npcs.Add(npc);

            foreach (var me in m_players)//all
            {
                if (me.isPlaying)
                {
                    npc.SyncNormal(me);
                }
            }
        }
        public void RemoveNpc(NpcObj npc)
        {
            DecNpcRef(npc.m_MapX, npc.m_MapY);
            npc.region = null;
            m_npcs.Remove(npc);

            var send = new EventData
            {
                Code = (byte)OperationCode.SyncNpcDel,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.Id, npc.id }
                }
            };

            BroadCast(send);
        }
        public void SyncNpc(NpcObj npc)
        {
            var param = npc.SyncUpdate();
            if (param == null || param.Count == 0)
                return;

            var send = new EventData
            {
                Code = (byte)OperationCode.SyncNpc,
                Parameters = param,
            };

            BroadCast(send);
        }
        public PlayerObj FindPlayer(int id)
        {
            foreach (var player in m_players)
            {
                if (player.id == id)
                    return player;
            }
            return null;
        }
        public void AddPlayer(PlayerObj me)
        {
            AddNpcRef(me.m_MapX, me.m_MapY);
            me.region = this;

            foreach (var player in m_players)//all
            {
                if (player.isPlaying)
                {
                    me.SyncNormal(player);
                }
            }
            m_players.Add(me);
        }
        public void RemovePlayer(PlayerObj me)
        {
            DecNpcRef(me.m_MapX, me.m_MapY);
            me.region = null;
            m_players.Remove(me);

            var send = new EventData
            {
                Code = (byte)OperationCode.SyncPlayerDel,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.Id, me.id }
                }
            };

            BroadCast(send);
        }
        public void SyncPlayer(PlayerObj me)
        {
            var param = me.SyncUpdate();
            if (param == null || param.Count == 0)
                return;

            var send = new EventData
            {
                Code = (byte)OperationCode.SyncPlayer,
                Parameters = param,
            };

            BroadCast(send);
        }
        public void BroadCast(EventData data, bool include = true)
        {
            foreach (var me in m_players)//all
            {
                if (me.isPlaying)
                {
                    me.ClientPeer.SendEvent(data, me.sendParameters);
                }
            }
            /*
                        if (include)
                        {
                            for (var i = 0; i < 8; ++i)
                            {
                                if (m_nConnectRegion[i] == -1)
                                    continue;

                                m_pConRegion[i].BroadCast(data, false);
                            }
                        }
            */
        }
    }
}
