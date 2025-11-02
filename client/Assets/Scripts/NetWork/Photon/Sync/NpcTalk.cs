using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class NpcTalk : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.NpcTalk;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            Debug.Log("NpcTalk " + Parameters[(byte)ParamterCode.Id]);
            Debug.Log("NpcTalk " + Parameters[(byte)ParamterCode.Name]);
            Debug.Log("NpcTalk " + Parameters[(byte)ParamterCode.Data]);

            int id = (int)Parameters[(byte)ParamterCode.Id];
            string name = (string)Parameters[(byte)ParamterCode.Name];
            string data = (string)Parameters[(byte)ParamterCode.Data];

            PhotonManager.Instance.NpcClientListener()?.NpcTalk(id, name, data);
        }
    }
}

