using System.Collections.Generic;
using System.Diagnostics;
using game.network;
using Photon.ShareLibrary.Constant;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncPlayer : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncPlayer;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            int id = (int)Parameters[(byte)ParamterCode.Id];
            var client = PhotonManager.Instance.CharClientListener();

            var player = client.FindPlayer(id);
            if (player != null)
            {
                if (Parameters.ContainsKey((byte)ParamterCode.ActionId))
                {
                    NpcAction.DoAction(player.controller, (NPCCMD)(byte)Parameters[(byte)ParamterCode.ActionId]);
                    //if (player.IsMaster)
                    //{
                    //    player.DoAudio((NPCCMD)(byte)Parameters[(byte)ParamterCode.ActionId]);
                    //}
                }

                if (Parameters.ContainsKey((byte)ParamterCode.Fight))
                {
                    player.FightMode = (bool)Parameters[(byte)ParamterCode.Fight];
                }

                if (Parameters.ContainsKey((byte)ParamterCode.Level))
                {
                    PlayerMain.instance.Level = (byte)Parameters[(byte)ParamterCode.Level];
                }
                if (Parameters.ContainsKey((byte)ParamterCode.Exp))
                {
                    PlayerMain.instance.Exp = (int)Parameters[(byte)ParamterCode.Exp];
                }

                if (Parameters.ContainsKey((byte)ParamterCode.Dir))
                {
                    player.controller.SyncDirection((byte)Parameters[(byte)ParamterCode.Dir]);
                }

                if (Parameters.ContainsKey((byte)ParamterCode.MapX) && Parameters.ContainsKey((byte)ParamterCode.MapY))
                {
                    client.UpdateNpc(player.controller, (int)Parameters[(byte)ParamterCode.MapY] / 2, (int)Parameters[(byte)ParamterCode.MapX], player.IsMaster);
                }

                if (Parameters.ContainsKey((byte)ParamterCode.HPMax))
                {
                    player.HPMax = (int)Parameters[(byte)ParamterCode.HPMax];
                }

                if (Parameters.ContainsKey((byte)ParamterCode.HPCur))
                {
                    player.HPCur = (int)Parameters[(byte)ParamterCode.HPCur];
                }

                if (Parameters.ContainsKey((byte)ParamterCode.MPMax))
                {
                    player.MPMax = (int)Parameters[(byte)ParamterCode.MPMax];
                }

                if (Parameters.ContainsKey((byte)ParamterCode.MPCur))
                {
                    player.MPCur = (int)Parameters[(byte)ParamterCode.MPCur];
                }

                if (Parameters.ContainsKey((byte)ParamterCode.SPMax))
                {
                    player.SPMax = (int)Parameters[(byte)ParamterCode.SPMax];
                }

                if (Parameters.ContainsKey((byte)ParamterCode.SPCur))
                {
                    player.SPCur = (int)Parameters[(byte)ParamterCode.SPCur];
                }
            }
        }
    }
}

