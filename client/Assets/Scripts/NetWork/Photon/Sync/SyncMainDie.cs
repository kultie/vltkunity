using System.Collections.Generic;
using UnityEngine;
using game.ui;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncMainDie : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.DoDie;
        GameObject playerDie;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            if (playerDie == null)
            {
                playerDie = UIHelpers.BringPrefabToScene("PlayerDie");
                playerDie.GetComponent<PlayerDie>().SetMessage("Bạn đã bị trọng thương!", OnPopupClosed);
            }
        }

        private void OnPopupClosed()
        {
            playerDie = null;
        }
    }
}

