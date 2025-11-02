using System.Collections.Generic;
using System.Security.Cryptography;
using game.network;
using Photon.ShareLibrary.Constant;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncNpc : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncNpc;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            int id = (int)Parameters[(byte)ParamterCode.Id];

            var client = PhotonManager.Instance.NpcClientListener();
            var npc = client.FindNpc(id);

            if (npc != null)
            {
                if (Parameters.ContainsKey((byte)ParamterCode.ActionId))
                {
                    NpcAction.DoAction(npc.GetController(), (NPCCMD)(byte)Parameters[(byte)ParamterCode.ActionId]);
                }
                if (Parameters.ContainsKey((byte)ParamterCode.Data))
                {
                    npc.SetGold((byte)Parameters[(byte)ParamterCode.Data]);
                }
                if (Parameters.ContainsKey((byte)ParamterCode.HPMax))
                {
                    npc.CurrentHPMax = (int)Parameters[(byte)ParamterCode.HPMax];
                }
                if (Parameters.ContainsKey((byte)ParamterCode.HPCur))
                {
                    npc.CurrentHPCur = (int)Parameters[(byte)ParamterCode.HPCur];
                }
                if (Parameters.ContainsKey((byte)ParamterCode.Dir))
                {
                    npc.GetController().SyncDirection((byte)Parameters[(byte)ParamterCode.Dir]);
                }
                if (Parameters.ContainsKey((byte)ParamterCode.MapX) && Parameters.ContainsKey((byte)ParamterCode.MapY))
                {
                    client.UpdateNpc(npc.GetController(), (int)Parameters[(byte)ParamterCode.MapY] / 2, (int)Parameters[(byte)ParamterCode.MapX]);
                }
            }
            else
            {
                UnityEngine.Debug.LogWarningFormat("Npc {0} not found", id);
            }
        }
    }
}

