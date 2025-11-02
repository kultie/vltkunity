using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Helpers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Photon.JXGameServer.Modulers
{
    public class PlayerModule
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);

        public static PlayerModule Me;


        //for player
        IntCounter teamCounter,playerCounter;
        
        JX_Table LevelAdd;
        JX_Table LevelExp;

        Dictionary<uint, PlayerObj> m_objs;
        Dictionary<int, TeamObj> m_teams;
        public int Count
        {
            get
            {
                return m_objs.Count;
            }
        }

        //for chat
        public byte WorldLevel, WorldCost;
        public byte CityLevel, CityCost;
        public byte MenpaiCost, TongCost;

        public PlayerModule()
        {
            Me = this;

            teamCounter = new IntCounter(0);
            playerCounter = new IntCounter(10000);

            m_teams = new Dictionary<int,TeamObj>();
            m_objs = new Dictionary<uint, PlayerObj>();
        }
        public void LoadConfig(string root)
        {
            LevelAdd = new JX_Table(Path.Combine(root, "settings/npc/player/level_add.txt"));
            LevelExp = new JX_Table(Path.Combine(root, "settings/npc/player/level_exp.txt"));

            var path = Path.Combine(root, "settings/chatcfg.ini");

            WorldLevel = (byte)GetPrivateProfileInt("Config", "WorldLevel", 0, path);
            WorldCost = (byte)GetPrivateProfileInt("Config", "WorldCost", 0, path);
            CityLevel = (byte)GetPrivateProfileInt("Config", "CityLevel", 0, path);
            CityCost = (byte)GetPrivateProfileInt("Config", "CityCost", 0, path);
            MenpaiCost = (byte)GetPrivateProfileInt("Config", "MenpaiCost", 0, path);
            TongCost = (byte)GetPrivateProfileInt("Config", "TongCost", 0, path);
        }
        public int NextLevelExp(byte level)
        {
            return LevelExp.Get<int>(1, level);
        }
        public int GetFireResist(NPCSERIES series, byte lvl)
        {
            if (series < 0 || series >= NPCSERIES.series_num)
                return 0;
            if (lvl <= 0 || lvl >= JXHelper.MAX_LEVEL)
                return 0;
            return LevelAdd.Get<int>((byte)Level_Add_Index.FireResistPerLevel, (byte)series) * lvl / 100;
        }
        public void HeartBeat()
        {
            foreach (var obj in m_objs)
            {
                var player = obj.Value;
                if (player.isLoading)
                {
                    player.DoLoad();
                }
            }
        }
        public PlayerObj Load(ClientPeer client, uint uid, uint cid, SendParameters sendParameters)
        {
            if (m_objs.ContainsKey(cid))
            {
                PlayerObj player = m_objs[cid];
                player.ClientPeer.Disconnect();
                m_objs.Remove(cid);
            }
            PlayerObj playerObj = new PlayerObj(client, sendParameters, uid, cid);
            playerObj.id = playerCounter.Next;

            m_objs.Add(cid, playerObj);
            return playerObj;
        }

        #region Team
        public void TeamCreate(PlayerObj player)
        {
            var res = ErrorCode.Ok;

            if (player.TeamId != 0)
                res = ErrorCode.PlayerInTeam;
            else
            {
                var team = new TeamObj(player.id);
                player.TeamId = teamCounter.Next;
                m_teams.Add(player.TeamId, team);
            }

            player.ClientPeer.SendOperationResponse(new OperationResponse
            {
                OperationCode = (byte)OperationCode.TeamCommand,
                ReturnCode = (short)res,
            }, player.sendParameters);
        }
        public void TeamLeave(PlayerObj player)
        {
            var team = GetTeamObj(player.TeamId);
            if (team != null)
            {
                team.Leave(player);
                player.TeamId = 0;
            }
        }
        public void TeamInvite(PlayerObj player,int cid)
        {
            var res = ErrorCode.Ok;

            var team = GetTeamObj(player.TeamId);
            if (team == null)
                res = ErrorCode.PlayerNotTeam;
            else
            if (team.IsFull())
                res = ErrorCode.TeamIsFull;
            else
            if (team.Captain != player.id)// la leader
                res = ErrorCode.PlayerNotCaption;
            else
            {
                var obj = GetPlayerObj(cid);
                if (obj == null)
                    res = ErrorCode.PlayerNotExist;
                else
                if (obj.TeamId != 0)
                    res = ErrorCode.PlayerInTeam;
                else
                {
                    obj.ClientPeer.SendEvent(new EventData
                    {
                        Code = (byte)OperationCode.TeamCommand,
                        Parameters = new Dictionary<byte, object>
                            {
                                { (byte)ParamterCode.ActionId, (byte)TeamCommand.TeamInvite },
                                { (byte)ParamterCode.CharacterId, player.id },
                                { (byte)ParamterCode.CharacterName, player.Name },
                            },
                    }, obj.sendParameters);
                }
            }

            player.ClientPeer.SendOperationResponse(new OperationResponse
            {
                OperationCode = (byte)OperationCode.TeamCommand,
                ReturnCode = (short)res,
            }, player.sendParameters);
        }
        public void TeamJoin(PlayerObj player, int cid)
        {
            var res = ErrorCode.Ok;

            if (player.TeamId != 0)
                res = ErrorCode.PlayerInTeam;
            else
            {
                var obj = GetPlayerObj(cid);
                if (obj == null)
                    res = ErrorCode.PlayerNotExist;
                else
                {
                    var team = GetTeamObj(obj.TeamId);
                    if (team == null)
                        res = ErrorCode.PlayerNotTeam;
                    else
                    if (team.IsFull())
                        res = ErrorCode.TeamIsFull;
                    else
                    if (team.Captain != obj.id)
                        res = ErrorCode.PlayerNotCaption;
                    else
                    {
                        obj.ClientPeer.SendEvent(new EventData
                        {
                            Code = (byte)OperationCode.TeamCommand,
                            Parameters = new Dictionary<byte, object>
                            {
                                { (byte)ParamterCode.ActionId, (byte)TeamCommand.TeamJoin },
                                { (byte)ParamterCode.CharacterId, player.id },
                                { (byte)ParamterCode.CharacterName, player.Name },
                            },
                        }, obj.sendParameters);
                    }
                }
            }

            player.ClientPeer.SendOperationResponse(new OperationResponse
            {
                OperationCode = (byte)OperationCode.TeamCommand,
                ReturnCode = (short)res,
            }, player.sendParameters);
        }
        public void TeamAccept(PlayerObj player,int cid,bool act)
        {
            var res = ErrorCode.Ok;

            if (act)// chap nhan loi moi
            {
                if (player.TeamId != 0)
                    res = ErrorCode.PlayerInTeam;
                else
                {
                    var obj = GetPlayerObj(cid);
                    if (obj == null)
                        res = ErrorCode.PlayerNotExist;
                    else
                    {
                        var team = GetTeamObj(obj.TeamId);
                        if (team == null)
                            res = ErrorCode.PlayerNotTeam;
                        else
                        if (team.IsFull())
                            res = ErrorCode.TeamIsFull;
                        else
                        if (team.Captain != obj.id)
                            res = ErrorCode.PlayerNotCaption;
                        else
                        {
                            team.Add(player);
                            player.TeamId = obj.TeamId;
                        }
                    }
                }
            }
            else// chap nhan vao doi
            {
                if (player.TeamId == 0)
                    res = ErrorCode.PlayerNotTeam;
                else
                {
                    var team = GetTeamObj(player.TeamId);
                    if (team == null)
                        res = ErrorCode.PlayerNotTeam;
                    else
                    if (team.IsFull())
                        res = ErrorCode.TeamIsFull;
                    else
                    if (team.Captain != player.id)
                        res = ErrorCode.PlayerNotCaption;
                    else
                    {
                        var obj = GetPlayerObj(cid);
                        if (obj == null)
                            res = ErrorCode.PlayerNotExist;
                        else
                        if (obj.TeamId != 0)
                            res = ErrorCode.PlayerInTeam;
                        else
                        {
                            team.Add(obj);
                            obj.TeamId = player.TeamId;
                        }
                    }
                }
            }

            player.ClientPeer.SendOperationResponse(new OperationResponse
            {
                OperationCode = (byte)OperationCode.TeamCommand,
                ReturnCode = (short)res,
            }, player.sendParameters);
        }
        public void TeamCharge(PlayerObj player, int cid)
        {
            var res = ErrorCode.Ok;
            if (player.TeamId == 0)
                res = ErrorCode.PlayerNotTeam;
            else
            {
                var team = GetTeamObj(player.TeamId);
                if (team == null)
                    res = ErrorCode.PlayerNotTeam;
                else
                if (team.Captain != player.id)
                    res = ErrorCode.PlayerNotCaption;
                else
                {
                    var obj = GetPlayerObj(cid);
                    if (obj == null)
                        res = ErrorCode.PlayerNotExist;
                    else
                        team.Charge(player, obj);
                }
            }

            player.ClientPeer.SendOperationResponse(new OperationResponse
            {
                OperationCode = (byte)OperationCode.TeamCommand,
                ReturnCode = (short)res,
            }, player.sendParameters);
        }
        public TeamObj GetTeamObj(int id)
        {
            if (m_teams.ContainsKey(id))
                return m_teams[id];
            else
                return null;
        }
        public void DelTeamObj(int id)
        {
            if (m_teams.ContainsKey(id))
            {
                m_teams.Remove(id);
            }
        }
        #endregion

        public void NotifyPlayer(uint cid)
        {
            if (m_objs.ContainsKey(cid))
            {
                PlayerObj player = m_objs[cid];
                player.ClientPeer.Disconnect();
            }
        }
        public void ServerCharge(uint cid,int MapId, int MapX, int MapY, string ip)
        {
            if (m_objs.ContainsKey(cid))
            {
                PlayerObj player = m_objs[cid];
                player.TeamLeave();

                player.character.MapId = (ushort)MapId;
                player.character.MapX = MapX;
                player.character.MapY = MapY;

                player.SavePlayerToDB();
                player.state = PlayerState.exit;

                player.ClientPeer.SendEvent(new EventData 
                { 
                    Code = (byte)OperationCode.ServerCharge,
                    Parameters = new Dictionary<byte, object>
                    {
                        { (byte)ParamterCode.Data, ip },
                    },
                }, player.sendParameters);
            }
        }
        public void Broadcast(string message)
        {
            foreach (var obj in m_objs.Values)
                obj.SendMessage(string.Empty, message, PlayerChat.system);
        }
        public void DoChat(Dictionary<byte,object> Parameters)
        {
            var str = (string)Parameters[(byte)ParamterCode.CharacterId];
            var message = (string)Parameters[(byte)ParamterCode.Message];

            if (Parameters.ContainsKey((byte)ParamterCode.CharacterName))//chat voi ai do
            {
                var me = (string)Parameters[(byte)ParamterCode.CharacterName];
                foreach (var obj in m_objs.Values)
                    if (obj.Name == me)// tim ra
                    {
                        obj.SendMessage(str, message, PlayerChat.hiden);
                        break;
                    }
            }
            else
            {
                switch ((PlayerChat)Parameters[(byte)ParamterCode.ActionId])
                {
                    case PlayerChat.system:
                        Broadcast(message);
                        break;

                    case PlayerChat.tong:
                        {
                            var id = (int)Parameters[(byte)ParamterCode.Data];
                            foreach (var obj in m_objs.Values)
                                if (obj.Tong == id)
                                {
                                    obj.SendMessage(str, message, PlayerChat.tong);
                                }
                        }
                        break;

                    case PlayerChat.menpai:
                        {
                            var id = (int)Parameters[(byte)ParamterCode.Data];
                            foreach (var obj in m_objs.Values)
                                if (obj.Faction == id)
                                {
                                    obj.SendMessage(str, message, PlayerChat.tong);
                                }
                        }
                        break;

                    default:
                        foreach (var obj in m_objs.Values)
                            obj.SendMessage(str, message, (PlayerChat)Parameters[(byte)ParamterCode.ActionId]);
                        break;
                }
            }
        }
        public PlayerObj GetPlayerObj(int id)
        {
            foreach (var obj in m_objs.Values)
                if (obj.id == id)
                    return obj;
            return null;
        }
        public void ClientDisconnected(PlayerObj me)
        {
            try
            {
                if (m_objs.ContainsKey(me.cid))
                {
                    me.ClientDisconnected();
                    m_objs.Remove(me.uid);
                }
            }
            catch (Exception ex)
            {
                PhotonApp.log.Error(ex);
            }
        }
    }
}
