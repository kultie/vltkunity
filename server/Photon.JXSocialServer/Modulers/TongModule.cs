using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.SocketServer;
using System.Collections.Generic;

namespace Photon.JXSocialServer.Modulers
{
    public class TongModule
    {
        public static TongModule Me;
        public TongModule()
        {
            Me = this;
        }
        public void QueryInfo(uint cid,uint tid,ClientPeer client,SendParameters sendParameters)
        {
            client.SendEvent(new EventData
            {
                Code = (byte)OperationCode.TongCommand,
                Parameters = new Dictionary<byte, object>
                {
                    {(byte)ParamterCode.ActionId, (byte)TongCommand.Query},
                    {(byte)ParamterCode.CharacterId, cid},
                },
            }, sendParameters);
        }
    }
}
