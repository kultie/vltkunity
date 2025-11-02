using System.Collections.Generic;

using game.network;
using Photon.ShareLibrary.Entities;

namespace Photon.ShareLibrary.Handlers
{
    public class AutoEquipHandler : MessageHandlers
    {
        public override MessageType Type => MessageType.Response;
        public override OperationCode Code => OperationCode.AutoEquip;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            //byte[] data = (byte[])Parameters[(byte)ParamterCode.Data];
            //ItemData item = ItemData.Parser.ParseFrom(data);
            //PhotonManager.Instance.SetPlayerItem(item);
        }
    }
}

