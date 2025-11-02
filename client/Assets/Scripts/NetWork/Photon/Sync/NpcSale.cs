using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class NpcSale : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.NpcSale;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            Debug.Log("NpcSale " + Parameters[(byte)ParamterCode.Id]);
            Debug.Log("NpcSale " + Parameters[(byte)ParamterCode.Data]);
            Debug.Log("NpcSale " + Parameters[(byte)ParamterCode.Name]);

            int id = (int)Parameters[(byte)ParamterCode.Id];
            string data = (string)Parameters[(byte)ParamterCode.Data];

            PhotonManager.Instance.NpcClientListener()?.NpcSale(id, data);
        }
    }
}

