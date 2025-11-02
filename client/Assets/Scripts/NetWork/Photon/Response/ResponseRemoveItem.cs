using System.Collections;
using System.Collections.Generic;
using game.network;
using UnityEngine;

namespace Photon.ShareLibrary.Handlers
{
    public class ResponseRemoveItem : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.RemoveItem;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            Debug.Log("Sddasdad" + Parameters[(byte)ParamterCode.Data]);

            if ((bool)Parameters[(byte)ParamterCode.Data])
            {
                PhotonManager.Instance.MainPlayerClientListener().SyncRemoveItem();
            }
        }
    }
}

