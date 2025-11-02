using JX.Database;
using Photon.JXLoginServer.Entitys;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Photon.JXLoginServer.Modulers
{
    public class AccountModule
    {
        public static AccountModule Me;

        IniFile revivepos;
        public List<AccountEntity> players = new List<AccountEntity>();
        public AccountModule()
        {
            Me = this;
            revivepos = new IniFile(Path.Combine(PhotonApp.Instance.ApplicationPath, "settings/revivepos.ini"));

            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
        }

        public static void ThreadProc()
        {
            var clean = new List<AccountEntity>();
            while (true)
            {
                lock (Me.players)
                {
                    Me.players.ForEach(p =>
                    {
                        if ((p.server & 128) == 128)// not work
                        {
                            if (++p.count >= 60 * 2)// 2p
                            {
                                clean.Add(p);
                            }
                        }
                    });

                    foreach (var p in clean)
                        Me.players.Remove(p);
                    clean.Clear();
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
        public void NotifyPlayer(uint cid, bool play, ClientPeer client, SendParameters sendParameters)
        {
            var entity = players.FirstOrDefault(x => x.cid == cid);
            if (entity != null)// exists
            {
                if (play)// in gameserver
                {
                    entity.server = (byte)(entity.server & 127);
                }
                else
                {
                    players.Remove(entity);
                }
            }
            else// not exists
            {
                if (play)// in gameserver => kick
                {
                    client.SendEvent(new EventData
                    {
                        Code = (byte)OperationCode.NotifyPlayer,
                        Parameters = new Dictionary<byte, object>
                        {
                            { (byte)ParamterCode.CharacterId, cid }
                        },
                    }, sendParameters);
                }
            }
        }
        public void ChangreServer(Dictionary<byte, object> parameters, ClientPeer client, SendParameters sendParameters)
        {
            var entity = players.FirstOrDefault(x => x.cid == (int)parameters[(byte)ParamterCode.CharacterId]);
            if (entity != null)
            {
                var server = ServerModule.Me.FindServer((int)parameters[(byte)ParamterCode.MapId]);
                if (server != null)
                {
                    if (players.Select(x => x.server == server.id).Count() >= 999999)
                    {
                        //full
                    }
                    else
                    {
                        entity.server = (byte)(server.id | 128);
                        parameters.Add((byte)ParamterCode.Data, server.ip);

                        client.SendEvent(new EventData
                        {
                            Code = (byte)OperationCode.ServerCharge,
                            Parameters = parameters,
                        }, sendParameters);
                    }
                }
            }
        }

        public void RegisterAccount(string account, string pass1, string pass2, ClientPeer client, SendParameters sendParameters)
        {
            if (pass1 != pass2)
            {
                client.SendOperationResponse(new OperationResponse()
                {
                    DebugMessage = "register fail",
                    OperationCode = (byte)OperationCode.Register,
                    ReturnCode = (short)ErrorCode.RegisterPassword
                }, sendParameters);
                return;
            }
            int uid = 0;
            if (DatabaseRepository.Me.RegisterAccount(account, pass1, ref uid))
            {
                client.SendOperationResponse(new OperationResponse()
                {
                    DebugMessage = "register success",
                    OperationCode = (byte)OperationCode.Register,
                    Parameters = new Dictionary<byte, object>()
                    {
                        [(byte)ParamterCode.UserId] = uid
                    },
                    ReturnCode = (short)0
                }, sendParameters);
            } 
            else client.SendOperationResponse(new OperationResponse()
            {
                DebugMessage = "register fail",
                OperationCode = (byte)OperationCode.Register,
                ReturnCode = (short)ErrorCode.RegisterAccount
            }, sendParameters);
        }
        public void Login(string account, string password, ClientPeer client, SendParameters sendParameters)
        {
            if (players.Exists(x => x.account == account))
            {
                client.SendOperationResponse(new OperationResponse()
                {
                    DebugMessage = "account use",
                    OperationCode = (byte)OperationCode.Login,
                    ReturnCode = (byte)ErrorCode.LoginAlready
                }, sendParameters);
                return;
            }

            uint uid = 0;
            var chars = DatabaseRepository.Me.Login(account,password,ref uid);
            if (uid > 0)
            {
                var res = new CharacterResponse
                {
                    id = uid,
                    chars = chars,
                };
                var dict = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.Data, Utils.SerializeBson(res) }
                };
                client.SendOperationResponse(new OperationResponse()
                {
                    OperationCode = (byte)OperationCode.Login,
                    Parameters = dict,
                    ReturnCode = (byte)ErrorCode.Ok
                }, sendParameters);
            }
            else
            {
                client.SendOperationResponse(new OperationResponse()
                {
                    DebugMessage = "wrong login information",
                    OperationCode = (byte)OperationCode.Login,
                    ReturnCode = (byte)ErrorCode.LoginWrong
                }, sendParameters);
            }
        }

        Dictionary<byte,RoleTemplate> roles = new Dictionary<byte,RoleTemplate>();

        public void CreateCharacter(uint uid, byte factionId, string name, bool sex, ClientPeer client, SendParameters sendParameters)
        {
            if (!roles.ContainsKey(factionId))
            {
                var role = new RoleTemplate(factionId);
                roles.Add(factionId, role);
            }

            var cid = DatabaseRepository.Me.CreateCharacter(uid,factionId,name,sex);
            if (cid != 0)
            {
                var role = roles[factionId];
                
                var character = new CharacterData();

                character.Name = name;

                role.Load(character);

                int RegionId =0 , MapX = 0, MapY = 0, MapId = ServerModule.Me.FindMap();
                revivepos.GetRevivalPosRandIdx(MapId, ref RegionId, ref MapX, ref MapY);

                character.MapId = (ushort)MapId;
                character.MapX = MapX;
                character.MapY = MapY;

                DatabaseRepository.Me.SaveCharacter(cid, character);

                role.SaveItemSkill(cid);

                client.SendOperationResponse(new OperationResponse()
                {
                    OperationCode = (byte)OperationCode.CreateCharacter,
                    Parameters = new Dictionary<byte, object>
                    {
                        { (byte)ParamterCode.CharacterId,  cid },
                    },
                    ReturnCode = 0
                }, sendParameters);
            }
            else
            {
                client.SendOperationResponse(new OperationResponse()
                {
                    OperationCode = (byte)OperationCode.CreateCharacter,
                    Parameters = new Dictionary<byte, object>(),
                    ReturnCode = (short)ErrorCode.CharacterExist
                }, sendParameters);
            }
        }
        public void SelectCharacter(uint uid, uint cid, ClientPeer client, SendParameters sendParameters)
        {
            var result = DatabaseRepository.Me.getPlayerPosition(uid, cid);
            if (result != null)
            {
                var server = ServerModule.Me.FindServer(result.MapId);
                if (server != null)
                {
                    if (players.Select(x => x.server == server.id).Count() >= 999999)
                    {
                        //full
                    }
                    else
                    {
                        var entity = new AccountEntity();
                        entity.account = result.Account;
                        entity.cid = cid;
                        entity.server = (byte)(server.id | 128);
                        entity.count = 0;

                        players.Add(entity);

                        client.SendOperationResponse(new OperationResponse()
                        {
                            OperationCode = (byte)OperationCode.SelectCharacter,
                            Parameters = new Dictionary<byte, object>
                            {
                                { (byte)ParamterCode.Data,  server.ip },
                            },
                            ReturnCode = 0
                        }, sendParameters);
                    }
                }
                else
                {
                    client.SendOperationResponse(new OperationResponse()
                    {
                        OperationCode = (byte)OperationCode.SelectCharacter,
                        Parameters = new Dictionary<byte, object>(),
                        ReturnCode = -2
                    }, sendParameters);
                }
            }
            else
            {
                client.SendOperationResponse(new OperationResponse()
                {
                    OperationCode = (byte)OperationCode.SelectCharacter,
                    Parameters = new Dictionary<byte, object>(),
                    ReturnCode = -1
                }, sendParameters);
            }
        }
    }
}

