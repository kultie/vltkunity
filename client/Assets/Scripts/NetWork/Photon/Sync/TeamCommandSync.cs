using System.Collections.Generic;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class TeamCommandSync : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.TeamCommand;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            var cmd = (Photon.ShareLibrary.Constant.TeamCommand)Parameters[(byte)ParamterCode.ActionId];
            
            var id = (int)Parameters[(byte)ParamterCode.CharacterId];
            var str = (string)Parameters[(byte)ParamterCode.CharacterName];

            var obj = PhotonManager.Instance.CharClientListener().FindPlayer(id);
            if (obj != null)
            {
                switch (cmd)
                {
                    case Constant.TeamCommand.TeamAccept://str join team
                        break;

                    case Constant.TeamCommand.TeamLeave: //str leave team
                        break;

                    case Constant.TeamCommand.TeamCharge://str le chu team
                        break;

                    case Constant.TeamCommand.TeamInvite://str moi team
                        break;

                    default://str xin vao team
                        break;
                }
            }
        }
    }
}
