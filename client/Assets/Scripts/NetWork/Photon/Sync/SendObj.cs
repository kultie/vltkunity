using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;
using Photon.ShareLibrary.Constant;

namespace Photon.ShareLibrary.Handlers
{
    public class SendObj : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SendObj;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            int id = (int)Parameters[(byte)ParamterCode.Id];
            ObjKind kind = (ObjKind)Parameters[(byte)ParamterCode.Kind];
            int npcType = (int)Parameters[(byte)ParamterCode.NpcType];
            int mapX = (int)Parameters[(byte)ParamterCode.MapX];
            int mapY = (int)Parameters[(byte)ParamterCode.MapY];
            byte dir = (byte)Parameters[(byte)ParamterCode.Dir];

            //string name = "obj " + (int)Parameters[(byte)ParamterCode.NpcType];
            var obj = PhotonManager.Instance.NpcClientListener().SpwanObj(id, dir, kind, npcType, mapX, mapY);

            if (Parameters.ContainsKey((byte)ParamterCode.Name))
            {
                obj.SetName((string)Parameters[(byte)ParamterCode.Name]);
            }

            if ((kind == ObjKind.Obj_Kind_Money)|| (kind == ObjKind.Obj_Kind_Item))
            {
                obj.SetNameColor((int)Parameters[(byte)ParamterCode.Data]);
            }

            if (kind == ObjKind.Obj_Kind_Prop)
            {
                var data = (short)Parameters[(byte)ParamterCode.Data];
                PhotonManager.Instance.NpcClientListener().ActiveObj(id, data);
            }
        }
    }
}

