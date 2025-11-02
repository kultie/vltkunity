using System.Collections.Generic;
using System.Diagnostics;
using game.network;
using Photon.ShareLibrary.Constant;

namespace Photon.ShareLibrary.Handlers
{
    public class SendPlayer : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SendPlayer;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            var id = (int)Parameters[(byte)ParamterCode.Id];

            var name = (string)Parameters[(byte)ParamterCode.Name];

            var series = (byte)Parameters[(byte)ParamterCode.Series];
            var sex = (bool)Parameters[(byte)ParamterCode.Sex];

            var faction = (byte)Parameters[(byte)ParamterCode.FactionId];

            var level = (byte)Parameters[(byte)ParamterCode.Level];

            var cam = (byte)Parameters[(byte)ParamterCode.Cam];
            var fight = (bool)Parameters[(byte)ParamterCode.Fight];

            var HPMax = (int)Parameters[(byte)ParamterCode.HPMax];
            var HPCur = (int)Parameters[(byte)ParamterCode.HPCur];
            var MPMax = (int)Parameters[(byte)ParamterCode.MPMax];
            var MPCur = (int)Parameters[(byte)ParamterCode.MPCur];
            var SPMax = (int)Parameters[(byte)ParamterCode.SPMax];
            var SPCur = (int)Parameters[(byte)ParamterCode.SPCur];

            if (PhotonManager.Instance.character == null)// main
            {
                PhotonManager.Instance.character = new Entities.CharacterData();
                PhotonManager.Instance.character.Name = name;

                PhotonManager.Instance.character.Fiveprop = series;
                PhotonManager.Instance.character.Sex = sex;

                PhotonManager.Instance.character.Sect = faction;

                PhotonManager.Instance.character.FightLevel = level;

                PhotonManager.Instance.character.Camp = cam;

                PhotonManager.Instance.character.FightMode = fight;

                PhotonManager.Instance.character.MaxLife = HPMax;
                PhotonManager.Instance.character.CurLife = HPCur;
                PhotonManager.Instance.character.MaxInner = MPMax;
                PhotonManager.Instance.character.CurInner = MPCur;
                PhotonManager.Instance.character.MaxStamina = SPMax;
                PhotonManager.Instance.character.CurStamina = SPCur;
            }
            else
            {
                var mapX = (int)Parameters[(byte)ParamterCode.MapX];
                var mapY = (int)Parameters[(byte)ParamterCode.MapY];
                var dir = (byte)Parameters[(byte)ParamterCode.Dir];

                var player = PhotonManager.Instance.CharClientListener().SpwanPlayer(id, name, sex, dir, mapX, mapY / 2);
                if (player != null)
                {


                    //int HPMax = (int)Parameters[(byte)ParamterCode.HPMax];
                    //int HPCur = (int)Parameters[(byte)ParamterCode.HPCur];
                }
            }
        }
    }
}

