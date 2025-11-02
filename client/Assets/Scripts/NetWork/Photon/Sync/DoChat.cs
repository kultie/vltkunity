using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary;
using ExitGames.Client.Photon;
using game.network;
using Photon.ShareLibrary.Constant;
using Unity.VisualScripting;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncChat : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.DoChat;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            var sender = (string)Parameters[(byte)ParamterCode.CharacterName];
            var message = (string)Parameters[(byte)ParamterCode.Message];
            var type = (PlayerChat)Parameters[(byte)ParamterCode.Data];

            //Save message.
            PhotonManager.Instance.SetMessage(new MessageData(sender, message), type);

            if (string.IsNullOrEmpty(sender) && type == PlayerChat.system)// system send
            {
                PopUpCanvas.instance.ShowMessage(message);
            }
            else
            {
                if (string.IsNullOrEmpty(sender))// thong bao he thong tren moi kenh chat
                {

                }
                else
                {
                    if (type == PlayerChat.hiden)// chat rieng, mat
                    {
                        UnityEngine.Debug.LogFormat("{0} goi tin {1} cho ban", sender, message);
                    }
                    else// chat tren kenh
                    {
                        UnityEngine.Debug.LogFormat("{0} goi tin {1} len kenh", sender, message);
                    }
                }
            }
        }
    }
}

