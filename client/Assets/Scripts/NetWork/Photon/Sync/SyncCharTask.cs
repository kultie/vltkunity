using System.Collections.Generic;
using UnityEngine;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncCharTask : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncCharTask;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            //byte[] data = (byte[])Parameters[(byte)ParamterCode.Data];
            //PlayerTask playerTask = new();
            //playerTask.MergeFrom(data);
            //Debug.Log(data.Length);
            //PhotonManager.Instance.SetPlayerTask(playerTask);
        }
    }
}

