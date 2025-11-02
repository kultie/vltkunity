using Photon.JXSocialServer.Modulers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace Photon.JXSocialServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest initRequest)
            : base(initRequest)
        {
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            PhotonApp.log.InfoFormat("OnOperationRequest: opCode={0}", (OperationCode)operationRequest.OperationCode);

            switch ((OperationCode)operationRequest.OperationCode)
            {
                case OperationCode.ServerRegister:
                    ServerModule.Me.ConnectMe((byte)operationRequest.Parameters[(byte)ParamterCode.Id], this, sendParameters);
                    break;

                case OperationCode.TongCommand:// tong join
                    switch ((TongCommand)operationRequest.Parameters[(byte)ParamterCode.ActionId])
                    {
                        case TongCommand.Create:
                            break;

                        case TongCommand.Delete:
                            break;

                        case TongCommand.Query:
                            {
                                var cid = (uint)operationRequest.Parameters[(byte)ParamterCode.CharacterId];
                                var tid = (uint)operationRequest.Parameters[(byte)ParamterCode.FactionId];

                                TongModule.Me.QueryInfo(cid, tid, this, sendParameters);
                            }
                            break;
                    }
                    break;

                case OperationCode.DoChat:// player chat
                    ServerModule.Me.DoChat(this,operationRequest.Parameters);
                    break;
            }
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {

        }
    }
}