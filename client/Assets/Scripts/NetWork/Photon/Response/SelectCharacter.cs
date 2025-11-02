using System.Collections.Generic;
using game.network;


namespace Photon.ShareLibrary.Handlers
{
    public class SelectCharacter : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.SelectCharacter;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            if (res == (short)ErrorCode.Ok)
            {
                PhotonManager.Instance.ServerCharge((string)Parameters[(byte)ParamterCode.Data], true);
            }
            else
            {
                LoginManger.instance.Fails("Tạo nhân vật không thành công, Vui lòng thử lại !");
            }
        }
    }
}