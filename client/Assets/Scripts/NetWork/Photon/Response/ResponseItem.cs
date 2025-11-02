using System.Collections;
using System.Collections.Generic;
using game.network;
using UnityEngine;

namespace Photon.ShareLibrary.Handlers
{
    public class ResponseItem : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.AddItem;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
        }
    }
}

