using Photon.JXLoginServer.Modulers;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace Photon.JXLoginServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public bool isServer = false;
        public DateTime timePeer = DateTime.Now;

        public ClientPeer(InitRequest initRequest)
            : base(initRequest)
        {
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            PhotonApp.log.InfoFormat("OnOperationRequest: opCode={0}", (OperationCode)operationRequest.OperationCode);

            switch ((OperationCode)operationRequest.OperationCode)
            {
                case OperationCode.ServerRegister:// game server => login server
                    isServer = true;
                    ServerModule.Me.ConnectMe(this, (byte)operationRequest.Parameters[(byte)ParamterCode.Id], (string)operationRequest.Parameters[(byte)ParamterCode.Data]);
                    break;

                case OperationCode.NotifyPlayer:// game server => login server
                    {
                        var cid = (uint)operationRequest.Parameters[(byte)ParamterCode.CharacterId];
                        var play = (bool)operationRequest.Parameters[(byte)ParamterCode.Data];

                        AccountModule.Me.NotifyPlayer(cid, play, this, sendParameters);
                    }
                    break;

                case OperationCode.ServerCharge:// game server => login server
                    AccountModule.Me.ChangreServer(operationRequest.Parameters, this, sendParameters);
                    break;

                case OperationCode.Register:// client => login
                    {
                        string account = (string)operationRequest.Parameters[(byte)ParamterCode.Account];
                        string pass = (string)operationRequest.Parameters[(byte)ParamterCode.Password];
                        string pass2 = (string)operationRequest.Parameters[(byte)ParamterCode.Password2];

                        AccountModule.Me.RegisterAccount(account, pass, pass2, this, sendParameters);
                    }
                    break;

                case OperationCode.Login:// client => login
                    {
                        string account = (string)operationRequest.Parameters[(byte)ParamterCode.Account];
                        string password = (string)operationRequest.Parameters[(byte)ParamterCode.Password];

                        AccountModule.Me.Login(account, password, this, sendParameters);
                    }
                    break;

                case OperationCode.CreateCharacter:// client => login
                    {
                        var uid = (uint)operationRequest.Parameters[(byte)ParamterCode.UserId];
                        var series = (byte)operationRequest.Parameters[(byte)ParamterCode.Series];
                        string name = (string)operationRequest.Parameters[(byte)ParamterCode.CharacterName];
                        var sex = (bool)operationRequest.Parameters[(byte)ParamterCode.Sex];

                        AccountModule.Me.CreateCharacter(uid, series, name, sex, this, sendParameters);
                    }
                    break;

                case OperationCode.SelectCharacter:// client => login
                    {
                        var uid = (uint)operationRequest.Parameters[(byte)ParamterCode.UserId];
                        var cid = (uint)operationRequest.Parameters[(byte)ParamterCode.CharacterId];

                        AccountModule.Me.SelectCharacter(uid, cid, this, sendParameters);
                    }
                    break;
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {

        }
    }
}