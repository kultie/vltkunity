using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class NpcExp : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.NpcExp;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            var exp = (int)Parameters[(byte)ParamterCode.Data];

            PlayerMain.instance.controller.AddStateTextEXP(exp);
        }
    }
}

