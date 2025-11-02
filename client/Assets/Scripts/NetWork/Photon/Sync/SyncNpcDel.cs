using System.Collections.Generic;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncNpcDel : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncNpcDel;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            int id = (int)Parameters[(byte)ParamterCode.Id];
            PhotonManager.Instance.NpcClientListener().DelNpc(id);
        }
    }
}

