using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncObjDel : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncObjDel;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            int id = (int)Parameters[(byte)ParamterCode.Id];

            PhotonManager.Instance.NpcClientListener().DelObj(id);
        }
    }
}

