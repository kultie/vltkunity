using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncCharSkill : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncCharSkill;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            Debug.Log("SyncCharSkill");
            //PlayerSkill skill = new();
            //skill.id = (ushort)Parameters[(byte)ParamterCode.Id];
            //skill.level = (byte)Parameters[(byte)ParamterCode.Level];
            //skill.exp = (uint)Parameters[(byte)ParamterCode.Exp];

            //PhotonManager.Instance.SetPlayerSkill(skill);
        }
    }
}

