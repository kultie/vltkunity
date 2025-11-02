using System.Collections.Generic;
using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class TeamCommand : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.TeamCommand;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            var error = (ErrorCode)res;
        }
    }
}
