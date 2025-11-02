using System.Collections.Generic;
using game.network;


namespace Photon.ShareLibrary.Handlers
{
    public class CreateCharcter : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.CreateCharacter;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            if (res == (short)ErrorCode.Ok)
            {
                LoginManger.instance.CreateCharcterSuccess((uint)Parameters[(byte)ParamterCode.CharacterId]);
            }
            else
            {
                LoginManger.instance.Fails("Tạo nhân vật không thành công, Vui lòng thử lại !");
            }
        }
    }
}