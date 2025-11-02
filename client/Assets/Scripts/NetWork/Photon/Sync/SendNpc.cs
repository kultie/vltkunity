using System.Collections.Generic;
using System.Diagnostics;
using game.network;
using Photon.ShareLibrary.Constant;

namespace Photon.ShareLibrary.Handlers
{
    public class SendNpc : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SendNpc;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            int id = (int)Parameters[(byte)ParamterCode.Id];

            var npc = PhotonManager.Instance.NpcClientListener().SpwanNpc(id);
            if (npc != null)
            {
                NPCSERIES series = (NPCSERIES)(byte)Parameters[(byte)ParamterCode.Series];
                NPCKIND kind = (NPCKIND)(byte)Parameters[(byte)ParamterCode.Kind];
                NPCCAMP cam = (NPCCAMP)(byte)Parameters[(byte)ParamterCode.Cam];
                int HPMax = (int)Parameters[(byte)ParamterCode.HPMax];
                int HPCur = (int)Parameters[(byte)ParamterCode.HPCur];
                string name = (string)Parameters[(byte)ParamterCode.Name];
                int npcType = (int)Parameters[(byte)ParamterCode.NpcType];
                byte level = (byte)Parameters[(byte)ParamterCode.Level];
                int mapX = (int)Parameters[(byte)ParamterCode.MapX];
                int mapY = (int)Parameters[(byte)ParamterCode.MapY];
                byte dir = (byte)Parameters[(byte)ParamterCode.Dir];

                npc.InitNpcDetail(kind, series, cam, HPMax, HPCur, level, npcType, mapX, mapY, name, dir);
                
                if (Parameters.ContainsKey((byte)ParamterCode.Data))
                    npc.SetGold((byte)Parameters[(byte)ParamterCode.Data]);

                NpcAction.DoAction(npc.GetController(), (NPCCMD)(byte)Parameters[(byte)ParamterCode.ActionId]);
            }
        }
    }
}

