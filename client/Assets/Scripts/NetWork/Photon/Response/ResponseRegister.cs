using System.Collections.Generic;

using game.network;

namespace Photon.ShareLibrary.Handlers
{
    public class ResponseRegister : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.Register;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            if (res == (short)ErrorCode.Ok)
            {
                LoginManger.instance.RegisterResponse();
            }
            else
            {
                LoginManger.instance.Fails("Không tạo đươc tài khoản, Vui lòng thử lại !");
            }
        }
    }
}

