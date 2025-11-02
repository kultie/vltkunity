using System;
using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Handlers;
using Photon.SocketServer;
using System.Collections.Generic;

using PhotonHostRuntimeInterfaces;
using Photon.JXGameServer.Items;
using Photon.ShareLibrary.Constant;

namespace Photon.JXGameServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        #region Constants and Fields

        protected int NumberAuthRequests;
        protected int MaxNumberAuthRequests = 3;

        #endregion

        #region .ctor

        public ClientPeer(InitRequest initRequest)
            : base(initRequest)
        {
        }

        #endregion

        #region Methods

        PlayerObj me = null;
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            try
            {
                if ((OperationCode)operationRequest.OperationCode != OperationCode.DoMove)
                {
                    PhotonApp.log.InfoFormat("OnOperationRequest: opCode={0}", (OperationCode)operationRequest.OperationCode);
                }

                OperationResponse operationResponse = null;

                switch ((OperationCode)operationRequest.OperationCode)
                {
                    case OperationCode.WorldJoin:
                        {
                            var uid = (uint)operationRequest.Parameters[(byte)ParamterCode.UserId];
                            var cid = (uint)operationRequest.Parameters[(byte)ParamterCode.CharacterId];
                            var sync = (bool)operationRequest.Parameters[(byte)ParamterCode.Data];

                            me = PlayerModule.Me.Load(this, uid, cid, sendParameters);
                            if (me == null)
                                this.Disconnect();
                            else
                            {
                                me.isSync = sync;

                                PhotonApp.LoginPeer.peer.SendOperation((byte)OperationCode.NotifyPlayer,
                                    new Dictionary<byte, object>
                                    {
                                        {(byte)ParamterCode.CharacterId, cid},
                                        {(byte)ParamterCode.Data, true},
                                    }, ExitGames.Client.Photon.SendOptions.SendReliable);

                            }
                        }
                        break;

                    case OperationCode.WorldLoaded:
                        me.EnterWorld();
                        break;

                    case OperationCode.NpcSkill:
                        {
                            var npc = (int)operationRequest.Parameters[(byte)ParamterCode.Id];
                            var skill = (int)operationRequest.Parameters[(byte)ParamterCode.SkillId];
                            me.MeSkill(skill, npc);
                        }
                        break;
                    case OperationCode.NpcQuery:
                        {
                            var id = (int)operationRequest.Parameters[(byte)ParamterCode.Id];
                            me.NpcQuery(id);
                        }
                        break;
                    case OperationCode.NpcSelect:
                        {
                            var id = (int)operationRequest.Parameters[(byte)ParamterCode.Id];
                            var data = (int)operationRequest.Parameters[(byte)ParamterCode.Data];
                            me.NpcSelect(id, data);
                        }
                        break;
                    case OperationCode.NpcCallback:
                        {
                            var id = (int)operationRequest.Parameters[(byte)ParamterCode.Id];
                            var func = (string)operationRequest.Parameters[(byte)ParamterCode.Name];
                            me.NpcCallback(id, func);
                        }
                        break;

                    case OperationCode.PickItem:
                        {
                            var id = (int)operationRequest.Parameters[(byte)ParamterCode.Id];
                            me.PickItem(id);
                        }
                        break;

                    case OperationCode.DoDie:
                        me.MeRevive();
                        break;

                    case OperationCode.DoMove:
                        {
                            int mapId = (int)operationRequest.Parameters[(byte)ParamterCode.MapId];
                            int mapX = (int)operationRequest.Parameters[(byte)ParamterCode.MapX];
                            int mapY = (int)operationRequest.Parameters[(byte)ParamterCode.MapY];
                            me.MeMove(mapId, mapX, mapY);
                        }
                        break;

                    case OperationCode.StopMove:
                        me.StopMove();
                        break;

                    case OperationCode.DoSit:
                        me.DoSit();
                        break;

                    case OperationCode.DoRun:
                        me.DoRun();
                        break;

                    case OperationCode.DoChat:
                        {
                            operationRequest.Parameters.Add((byte)ParamterCode.CharacterId, me.Name);
                            if (operationRequest.Parameters.ContainsKey((byte)ParamterCode.CharacterName))//chat voi ai do
                            {
                                PhotonApp.SocialPeer.peer.SendOperation((byte)OperationCode.DoChat, operationRequest.Parameters, ExitGames.Client.Photon.SendOptions.SendReliable);
                            }
                            else
                            {
                                switch ((PlayerChat)operationRequest.Parameters[(byte)ParamterCode.ActionId])
                                {
                                    case PlayerChat.near://neer
                                        me.DoChat((string)operationRequest.Parameters[(byte)ParamterCode.Message], false);
                                        return;

                                    case PlayerChat.team://team
                                        me.DoChat((string)operationRequest.Parameters[(byte)ParamterCode.Message], true);
                                        return;

                                    case PlayerChat.tong:
                                        if (me.Tong <= 0)// chua vao bang hoi
                                            return;

                                        if (me.MPCur < PlayerModule.Me.TongCost)//khong du noi luc
                                            return;
                                        me.MPCur -= PlayerModule.Me.TongCost;

                                        operationRequest.Parameters.Add((byte)ParamterCode.Data, me.Tong);
                                        break;

                                    case PlayerChat.menpai:
                                        if (me.Faction < 0)// chua gia nhap mon phai
                                            return;

                                        if (me.MPCur < PlayerModule.Me.MenpaiCost)//khong du noi luc
                                            return;
                                        me.MPCur -= PlayerModule.Me.MenpaiCost;

                                        operationRequest.Parameters.Add((byte)ParamterCode.Data, me.Faction);
                                        break;

                                    case PlayerChat.city://city
                                        if (me.Level < PlayerModule.Me.CityLevel)//khong du cap do
                                            return;

                                        if (me.MPCur < PlayerModule.Me.CityCost)//khong du noi luc
                                            return;
                                        me.MPCur -= PlayerModule.Me.CityCost;

                                        PlayerModule.Me.DoChat(operationRequest.Parameters);
                                        return;

                                     default:
                                        if (me.Level < PlayerModule.Me.WorldLevel)//khong du cap do
                                            return;

                                        if (me.MPCur < PlayerModule.Me.WorldCost)//khong du noi luc
                                            return;
                                        me.MPCur -= PlayerModule.Me.WorldCost;
                                        break;
                                }

                                PhotonApp.SocialPeer.peer.SendOperation((byte)OperationCode.DoChat, operationRequest.Parameters, ExitGames.Client.Photon.SendOptions.SendReliable);
                            }
                            PlayerModule.Me.DoChat(operationRequest.Parameters);
                        }
                        break;

                    case OperationCode.TeamCommand:
                        var action = (TeamCommand)operationRequest.Parameters[(byte)ParamterCode.ActionId];
                        if (action == TeamCommand.TeamCreate)
                            me.TeamCreate();
                        else
                        if (action == TeamCommand.TeamLeave)
                            me.TeamLeave();
                        else
                        {
                            var id = (int)operationRequest.Parameters[(byte)ParamterCode.CharacterId];
                            switch (action)
                            {
                                case TeamCommand.TeamInvite:
                                    me.TeamInvite(id);
                                    break;

                                case TeamCommand.TeamJoin: 
                                    me.TeamJoin(id); 
                                    break;

                                case TeamCommand.TeamAccept:
                                    {
                                        var act = (bool)operationRequest.Parameters[(byte)ParamterCode.Data];
                                        me.TeamAccept(id, act);
                                    }
                                    break;

                                case TeamCommand.TeamCharge: 
                                    me.TeamCharge(id);
                                    break;
                            }
                        }
                        break;

                    case OperationCode.AutoEquip:
                        {
                            string itemId = (string)operationRequest.Parameters[(byte)ParamterCode.ItemId];
                            bool isEquip = (bool)operationRequest.Parameters[(byte)ParamterCode.IsEquip];
                            me.AutoEquip(itemId, isEquip);
                        }
                        break;
                    case OperationCode.AddItem:
                        {
                            /*
                                                        byte[] data = (byte[])operationRequest.Parameters[(byte)ParamterCode.Data];
                                                        AddItemParam param = AddItemParam.Parser.ParseFrom(data);
                                                        ItemObj item = ItemModule.Me.AddRandomItem(me.character, param.ItemGen,
                                                            param.DetailType,
                                                            param.ParticulatType,
                                                            0, param.ItemLevel,
                                                            param.ItemLevel,
                                                            param.ItemSeries,
                                                            1,
                                                            null,
                                                            6,
                                                            param.MagicLevel,
                                                            0,
                                                            0,
                                                            0);

                                                        item.SaveTo(item.ItemData);
                                                        GameDataRepository.Me.AddNewItem(me.character.Cid, item.ItemData);
                                                        SendOperationResponse(new OperationResponse()
                                                        {
                                                            OperationCode = (byte)OperationCode.AddItem,
                                                            Parameters = new Dictionary<byte, object>()
                                                            {
                                                                [(byte)ParamterCode.Data] = item.ItemData.ToByteArray()
                                                            }
                                                        }, sendParameters);
                            */
                        }
                        break;
                    case OperationCode.RemoveItem:
                        {
                            string ItemId = (string)operationRequest.Parameters[(byte)ParamterCode.ItemId];
                            if (ItemId == null)
                            {
                                return;
                            }
                            int idx = me.playerItems.FindIndex((obj) => obj.m_CommonAttrib.ItemDataId == ItemId);
                            if (idx < 0)
                            {
                                return;
                            }
                            me.playerItems.RemoveAt(idx);
                            bool result = ItemModule.Me.RemoveItemFromDB(ItemId);
                            SendOperationResponse(new OperationResponse()
                            {
                                OperationCode = (byte)OperationCode.RemoveItem,
                                Parameters = new Dictionary<byte, object>()
                                {
                                    [(byte)ParamterCode.Data] = result
                                }
                            }, sendParameters);
                        }
                        break;
                    case OperationCode.DoTask:
                        {
                            int taskId = (int)operationRequest.Parameters[(byte)ParamterCode.TaskId];
                            int taskValue = (int)operationRequest.Parameters[(byte)ParamterCode.TaskValue];
                            me.DoTask(taskId, taskValue);
                        }
                        break;

                    default:
                        {
                            PhotonApp.log.WarnFormat("Unknown operation {0}. Client info: appId={1},protocol={2},remoteEndpoint={3}:{4}", (OperationCode)operationRequest.OperationCode, "authenticatedApplicationId", this.NetworkProtocol, this.RemoteIP, this.RemotePort);
                            //this.HandleUnknownOperationCode(operationRequest, sendParameters);
                        }
                        break;
                }

                if (operationResponse != null)
                {
                    this.SendOperationResponse(operationResponse, sendParameters);
                }
            }
            catch (Exception ex)
            {
                PhotonApp.log.Error(ex);
                //this.SendInternalErrorResponse(operationRequest, sendParameters, ex.Message);
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            if (me != null)
            {
                PlayerModule.Me.ClientDisconnected(me);
            }
            if (PhotonApp.log.IsDebugEnabled && reasonCode != (int)DisconnectReason.ClientDisconnect)
            {
                PhotonApp.log.DebugFormat("OnDisconnect with {0}:'{5}' appId={1},protocol={2},remoteEndpoint={3}:{4}",
                    reasonCode, "authenticatedApplicationId", this.NetworkProtocol, this.RemoteIP, this.RemotePort, reasonDetail);
            }
        }
        #endregion
    }
}