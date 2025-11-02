using System.Collections.Generic;
using game.network;
using game.config;
using UnityEngine.SceneManagement;

namespace Photon.ShareLibrary.Handlers
{
    public class SendWorld : MessageHandlers
    {
        public override MessageType Type => MessageType.Event;
        public override OperationCode Code => OperationCode.SendWorld;

        public override void Process(short res, ExitGames.Client.Photon.ParameterDictionary Parameters)
        {
            PhotonManager.Instance.PlayerId = (int)Parameters[(byte)ParamterCode.Id];
            PhotonManager.Instance.MapId = (ushort)Parameters[(byte)ParamterCode.MapId];
            PhotonManager.Instance.MapX = (int)Parameters[(byte)ParamterCode.MapX];
            PhotonManager.Instance.MapY = (int)Parameters[(byte)ParamterCode.MapY];

            var scene = SceneManager.GetSceneByName(ConfigGame.worldScreen);
            if (scene.isLoaded)
            {
                PhotonManager.Instance.CharClientListener().ChangeWorld();
                PhotonManager.Instance.NpcClientListener().ChangeWorld();
            }
            else
            {
                SceneManager.LoadScene(ConfigGame.worldScreen);
            }
        }
    }
}

