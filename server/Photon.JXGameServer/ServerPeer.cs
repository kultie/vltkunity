using ExitGames.Client.Photon;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using System;
using System.Collections.Generic;

namespace Photon.JXGameServer
{
    public class ServerPeer : IPhotonPeerListener
    {
        byte id;
        string ip;

        public PhotonPeer peer;
        public ServerPeer(byte id, string ip)
        {
            this.registerTypes();
            this.peer = new PhotonPeer(this, ConnectionProtocol.Tcp);

            this.id = id;
            this.ip = ip;
        }
        void registerTypes()
        {
            PhotonPeer.RegisterType(typeof(ushort), (byte)CustomTypeCode.UShort,
                                           SerializerMethods.SerializeUShort,
                                           SerializerMethods.DeserializeUShort);

            PhotonPeer.RegisterType(typeof(uint), (byte)CustomTypeCode.UInt,
                                           SerializerMethods.SerializeUInt,
                                           SerializerMethods.DeserializeUInt);

            PhotonPeer.RegisterType(typeof(ulong), (byte)CustomTypeCode.ULong,
                                           SerializerMethods.SerializeULong,
                                           SerializerMethods.DeserializeULong);
        }
        public void Connect(string address, string app) => peer.Connect(address, app);
        public void DebugReturn(DebugLevel debugLevel, string debug)
        {

        }
        public void OnEvent(EventData ev)
        {
            switch ((OperationCode)ev.Code)
            {
                case OperationCode.ServerCharge://login server => game server
                    PlayerModule.Me.ServerCharge((uint)ev.Parameters[(byte)ParamterCode.CharacterId],
                        (int)ev.Parameters[(byte)ParamterCode.MapId],
                        (int)ev.Parameters[(byte)ParamterCode.MapX],
                        (int)ev.Parameters[(byte)ParamterCode.MapY],
                        (string)ev.Parameters[(byte)ParamterCode.Data]);
                    break;

                case OperationCode.NotifyPlayer://login server => game server
                    PlayerModule.Me.NotifyPlayer((uint)ev.Parameters[(byte)ParamterCode.CharacterId]);
                    break;

                case OperationCode.ServerTask://social server => game server
                    {
                        var name = (string)ev.Parameters[(byte)ParamterCode.Name];
                        var file = (string)ev.Parameters[(byte)ParamterCode.Data];
                        var func = (string)ev.Parameters[(byte)ParamterCode.Message];

                        PlayerModule.Me.Broadcast(name);
                        ScriptModule.Me.RunScript(file, func);
                    }
                    break;

                case OperationCode.ServerTong:
                    break;

                case OperationCode.TongCommand:
                    {
                        var cid = (string)ev.Parameters[(byte)ParamterCode.CharacterId];
                        switch ((TongCommand)ev.Parameters[(byte)ParamterCode.ActionId])
                        {
                            case TongCommand.Query:
                                break;
                        }
                    }
                    break;

                case OperationCode.DoChat:
                    {
                        var dict = new Dictionary<byte, object>();
                        foreach (var pair in ev.Parameters)
                        {
                            dict.Add(pair.Key,pair.Value);
                        }
                        PlayerModule.Me.DoChat(dict);
                    }
                    break;
            }
        }
        public void OnOperationResponse(OperationResponse response)
        {

        }
        public void OnStatusChanged(ExitGames.Client.Photon.StatusCode statusCode)
        {
            switch (statusCode)
            {
                case ExitGames.Client.Photon.StatusCode.Connect:
                    peer.SendOperation((byte)OperationCode.ServerRegister, new Dictionary<byte, object>
                    {
                        { (byte)ParamterCode.Id, this.id },
                        { (byte)ParamterCode.Data, this.ip }
                    }, ExitGames.Client.Photon.SendOptions.SendReliable);
                    break;

                case ExitGames.Client.Photon.StatusCode.Disconnect:
                    break;
            }
        }

        int msDeltaForServiceCalls = 50, msTimestampOfLastServiceCall = 0;
        public void Update()
        {
            if (this.peer == null)
                return;

            while (this.peer.DispatchIncomingCommands())
            {
            }
            if (Environment.TickCount - this.msTimestampOfLastServiceCall > this.msDeltaForServiceCalls || this.msTimestampOfLastServiceCall == 0)
            {
                this.msTimestampOfLastServiceCall = Environment.TickCount;

                while (this.peer.SendOutgoingCommands())
                {
                }
            }
        }
    }
}
