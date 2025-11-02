using game.network;
using System.Collections.Generic;

namespace Photon.ShareLibrary.Handlers
{
    public class ServerChangre : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.ServerCharge;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            PhotonManager.Instance.ServerCharge((string)Parameters[(byte)ParamterCode.Data],false);
        }
    }
}