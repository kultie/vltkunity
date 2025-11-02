using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncCharItem : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncCharItem;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            Debug.Log("SyncCharItem");
            //byte[] data = (byte[])Parameters[(byte)ParamterCode.Data];
            //ItemData item = ItemData.Parser.ParseFrom(data);
            //PhotonManager.Instance.SetPlayerItem(item);
        }
    }
}

