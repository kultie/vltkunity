using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;
using static game.resource.map.Textures.Command;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncObj : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncObj;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            var id = (int)Parameters[(byte)ParamterCode.Id];
            var data = (short)Parameters[(byte)ParamterCode.Data];
            PhotonManager.Instance.NpcClientListener().ActiveObj(id, data);
        }
    }
}

