using System.Collections;
using System.Collections.Generic;
using game.network;
using UnityEngine;


namespace Photon.ShareLibrary.Handlers
{
    public class SyncPlayerDel : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncPlayerDel;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            int id = (int)Parameters[(byte)ParamterCode.Id];

            PhotonManager.Instance.CharClientListener().DelPlayer(id);
        }
    }

}
