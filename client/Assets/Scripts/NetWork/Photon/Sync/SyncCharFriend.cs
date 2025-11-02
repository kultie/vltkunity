using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncCharFriend : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncCharFriend;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            Debug.Log("SyncCharFriend");
            //System.Collections.Hashtable hashtable = (System.Collections.Hashtable)Parameters[(byte)ParamterCode.Data];
        }
    }
}

