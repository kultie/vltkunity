using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class NpcSkill : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.NpcSkill;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            var id = (int)Parameters[(byte)ParamterCode.Id];
            var skill = (int)Parameters[(byte)ParamterCode.SkillId];
            var level = (byte)Parameters[(byte)ParamterCode.SkillLevel];
            var tagetId = (int)Parameters[(byte)ParamterCode.NpcType];

            Debug.Log("Id " + id);
            Debug.Log("SkillId " + skill);
            Debug.Log("SkillLevel " + level);
            Debug.Log("obj " + tagetId);

            var player = PhotonManager.Instance.CharClientListener().FindPlayer(id);
            if (player != null)
            {
                player.DoSkill(skill, level, tagetId);
            }
            else
            {
                var npc = PhotonManager.Instance.NpcClientListener().FindNpc(id);
                if (npc != null)
                {
                    npc.DoSkill(skill, level);
                }
            }
        }
    }
}

