using System.Collections.Generic;
using UnityEngine;
using game.network;
using game.config;
using UnityEngine.SceneManagement;

namespace Photon.ShareLibrary.Handlers
{
    public class SyncWorld : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SyncWorld;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            //byte[] data = (byte[])Parameters[(byte)ParamterCode.Data];
            //Region region = Region.Parser.ParseFrom(data);

            Debug.Log("SyncWorld");
            //PhotonManager.Instance.SetRegion(region);
            //SceneManager.LoadScene(ConfigGame.gameWorldScreen);
        }
    }
}

