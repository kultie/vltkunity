using System.Collections.Generic;
using UnityEngine;
using game.network;
using game.config;

namespace Photon.ShareLibrary.Handlers
{
    public class ResponseLogin : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.Login;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            if (res == (short)ErrorCode.Ok)
            {
                var data = Utils.Utils.DeserializeBson<Entities.CharacterResponse>((string)Parameters[(byte)ParamterCode.Data]);
                LoginManger.instance.characterReply = data.chars;
                LoginManger.instance.LoginResponse(data.id);
            }
            else
            {
                LoginManger.instance.Fails("Đang nhâp không thành công, Vui lòng thử lại !");
            }
        }
    }
}

