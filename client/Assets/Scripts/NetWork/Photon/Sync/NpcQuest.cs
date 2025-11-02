using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class NpcQuest : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.NpcQuest;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            Debug.Log("NpcQuest " + Parameters[(byte)ParamterCode.Id]);
            Debug.Log("NpcQuest " + Parameters[(byte)ParamterCode.Name]);
            Debug.Log("NpcQuest " + Parameters[(byte)ParamterCode.Data]);

            int id = (int)Parameters[(byte)ParamterCode.Id];
            string name = (string)Parameters[(byte)ParamterCode.Name];
            string data = (string)Parameters[(byte)ParamterCode.Data];

            PhotonManager.Instance.NpcClientListener()?.NpcQuest(id, name, data);
        }
    }
}

