
using System.Collections.Generic;
using game.network;
using game.resource.settings.skill;
using Photon.ShareLibrary.Handlers;
using Unity.VisualScripting;
using UnityEngine;

namespace game.scene
{
    public class World : UnityEngine.MonoBehaviour
    {
        private scene.world.Camera mainCamera;
        private resource.settings.NpcRes.Special mainPlayer;
        private resource.Map map;

        ////////////////////////////////////////////////////////////////////////////////
        private int currentFixedUpdateFps;
        private bool isRuningMainPlayerAction;
        ////////////////////////////////////////////////////////////////////////////////


        private void Start()
        {
            this.mainCamera = new world.Camera(UnityEngine.Camera.main);
            this.mainPlayer = new resource.settings.NpcRes.Special();
            this.map = new resource.Map();
            this.map.EnableCache(groundNode: true, groundObject: true, tree: true);

            this.SetCameraSize(3f);
            this.SetFPS(60);

            MainCanvas.instance.MiniMap.SetHandle(this.map.GetMiniMap());

            PhotonManager.Instance.world = this;
            PhotonManager.Instance.CharMgrs = this.gameObject.AddComponent<game.scene.CharManager>();
            PhotonManager.Instance.NpcMgrs = this.gameObject.AddComponent<game.scene.NpcManager>();

            this.OpenWold();
        }
        [SerializeField]
        GameObject PlayerChat;
        [SerializeField]
        GameObject PlayerCallNpc;
        public GameObject NPCSellect;

        public void OpenWold()
        {
            EnterMap(PhotonManager.Instance.MapId, PhotonManager.Instance.MapY / 2, PhotonManager.Instance.MapX);

            // Add Main Player
            BoxCollider2D boxCollider2D = this.mainPlayer.GetAppearance().parent.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            boxCollider2D.offset = new Vector2(0, 0.4f);
            Rigidbody2D rigidbody2D = mainPlayer.GetAppearance().parent.AddComponent<Rigidbody2D>();
            rigidbody2D.isKinematic = true;

            GameObject PlayerChatHandler = (GameObject)Instantiate(PlayerChat, new Vector3(0, 1.5f, 0), Quaternion.Euler(0, 0, 0), mainPlayer.GetAppearance().parent.transform);
            GameObject PlayerCallNpcHandler = (GameObject)Instantiate(PlayerCallNpc, new Vector3(0.8f, 0.5f, 0), Quaternion.Euler(0, 0, 0), mainPlayer.GetAppearance().parent.transform);

            PlayerChatHandler.SetActive(false);
            PlayerCallNpcHandler.SetActive(false);

            PlayerMain playerMain = mainPlayer.GetAppearance().parent.AddComponent<PlayerMain>();
            //            playerMain.SetUIWord(_wordGame.GetUserInterface().panelEquipment.equipTab,
            //                _wordGame.GetUserInterface().panelEquipment.propertiesTab,
            //                _wordGame.GetUserInterface().panelEquipment.itemTab,
            //                _wordGame.GetUserInterface().viewportItem);

            playerMain.InitCharacter(PhotonManager.Instance.PlayerId, mainPlayer, PlayerChatHandler, PlayerCallNpcHandler);

            NotificationOpenMap();
        }
        public void ChangeWorld()
        {
            EnterMap(PhotonManager.Instance.MapId, PhotonManager.Instance.MapY / 2, PhotonManager.Instance.MapX);
            PlayerMain.instance.id = PhotonManager.Instance.PlayerId;

            mainPlayer.SetAction(game.resource.settings.NpcRes.Action.normalStand1);

            NotificationOpenMap();
        }
        void NotificationOpenMap()
        {
            PhotonManager.Instance.Client().SendOperation((byte)OperationCode.WorldLoaded, new Dictionary<byte, object>(),
                ExitGames.Client.Photon.SendOptions.SendReliable);
        }

        private void Update()
        {
            this.map.Update();
        }


        private void OnDestroy()
        {
            CancelInvoke();
            this.map.Release();
        }

        public void CastSkill(int id, int level, game.resource.settings.NpcRes.Special TargetController, game.resource.settings.NpcRes.Special controller)
        {
            Params.Cast castParams = new(TargetController, controller);
            this.map.CastSkill(id, level, castParams);
        }

        public void CastSkill(int id, int level, game.resource.settings.NpcRes.Normal TargetController, game.resource.settings.NpcRes.Special controller)
        {
            Params.Cast castParams = new(controller, TargetController);
            this.map.CastSkill(id, level, castParams);
        }

        ////////////////////////////////////////////////////////////////////////////////

        public void SetCameraSize(float size)
        {
            const float defaultSize = 5.0f;

            float ratio = (size * 100) / defaultSize;
            float cameraWidth = (UnityEngine.Screen.width * ratio) / 100;
            float cameraHeight = (UnityEngine.Screen.height * ratio) / 100;

            resource.map.Config.Textures mapConfig = this.map.GetTextureConfig();
            mapConfig.radiusHorizontalVisibility = (int)(cameraWidth / 2 + cameraWidth / 2);
            mapConfig.radiusVerticalVisibility = (int)(cameraHeight / 2 + cameraHeight / 2);

            this.mainCamera.SetOrthographicSize(size);
            this.map.SetTextureConfig(mapConfig);
            this.map.SetPosition(this.mainPlayer.GetMapPosition());
        }

        public void SetFPS(int fps)
        {
            if (fps <= 60)
            {
                this.currentFixedUpdateFps = fps;
                UnityEngine.Time.fixedDeltaTime = 1f / fps;
            }

            UnityEngine.Application.targetFrameRate = fps;
            this.map.SetFPS(fps);
        }

        public void SetMapGroundNodeEnabled(bool enabled)
        {
            resource.map.Config.Textures mapConfig = this.map.GetTextureConfig();
            mapConfig.drawGroundNode = enabled ? 1 : 0;

            this.map.SetTextureConfig(mapConfig);
            this.map.SetPosition(this.mainPlayer.GetMapPosition());
        }

        public void SetMapGroundObjectEnabled(bool enabled)
        {
            resource.map.Config.Textures mapConfig = this.map.GetTextureConfig();
            mapConfig.drawGroundObject = enabled ? 1 : 0;

            this.map.SetTextureConfig(mapConfig);
            this.map.SetPosition(this.mainPlayer.GetMapPosition());
        }

        public void SetMapTreeEnabled(bool enabled)
        {
            resource.map.Config.Textures mapConfig = this.map.GetTextureConfig();
            mapConfig.drawTree = enabled ? 1 : 0;

            this.map.SetTextureConfig(mapConfig);
            this.map.SetPosition(this.mainPlayer.GetMapPosition());
        }

        public void SetMapBuildingEnabled(bool enabled)
        {
            resource.map.Config.Textures mapConfig = this.map.GetTextureConfig();
            mapConfig.drawBuilding = enabled ? 1 : 0;

            this.map.SetTextureConfig(mapConfig);
            this.map.SetPosition(this.mainPlayer.GetMapPosition());
        }

        public void SetMapObstacleGridEnabled(bool enabled)
        {
            resource.map.Config.Textures mapConfig = this.map.GetTextureConfig();
            mapConfig.drawObstacleGrid = enabled ? 1 : 0;

            this.map.SetTextureConfig(mapConfig);
            this.map.SetPosition(this.mainPlayer.GetMapPosition());
        }

        public void SetIdentifyNpcTitleEnabled(bool enabled)
        {
            resource.map.Config.Identification config = this.map.GetIdentifyConfig();
            config.npcTitle = enabled;
            this.map.SetIdentifyConfig(config);
        }

        public void SetIdentifyNpcTongEnabled(bool enabled)
        {
            resource.map.Config.Identification config = this.map.GetIdentifyConfig();
            config.npcTong = enabled;
            this.map.SetIdentifyConfig(config);
        }

        public void SetIdentifyNpcNameEnabled(bool enabled)
        {
            resource.map.Config.Identification config = this.map.GetIdentifyConfig();
            config.npcName = enabled;
            this.map.SetIdentifyConfig(config);
        }

        public void SetIdentifyNpcLifeEnabled(bool enabled)
        {
            resource.map.Config.Identification config = this.map.GetIdentifyConfig();
            config.npcHealth = enabled;
            this.map.SetIdentifyConfig(config);
        }

        public void SetIdentifyNpcMapPos(bool enabled)
        {
            resource.map.Config.Identification config = this.map.GetIdentifyConfig();
            config.npcMapPos = enabled;
            this.map.SetIdentifyConfig(config);
        }

        public void OnLogoutButtonClick()
        {
            this.map.HideNpc(mainPlayer);
            UnityEngine.GameObject.Destroy(mainPlayer.GetAppearance());
        }

        public void RequestEquipItemFromBag(resource.settings.Item item, int bagCellIndex)
        {
            if (PlayerMain.instance != null)
            {
                PlayerMain.instance.RequestEquipItemFromBag(item, bagCellIndex);
            }
        }

        public void RequestUseItemFromBag(resource.settings.Item item, int bagCellIndex)
        {
            if (PlayerMain.instance != null)
            {

            }
        }

        public void RequestUnequipItem(world.userInterface.PanelUserEquipment.Cell cell)
        {
            if (PlayerMain.instance != null)
            {
                PlayerMain.instance.RequestUnequipItem(cell);
            }
        }

        /// <summary>
        /// yêu cầu bán trang bị từ túi hành trang
        /// </summary>
        /// <param name="bagCellIndex">ô trong túi hành trang</param>
        public void RequestSellItemFromBag(resource.settings.Item item, int bagCellIndex)
        {
            if (PlayerMain.instance != null)
            {
                PlayerMain.instance.RequestSellItemFromBag(item, bagCellIndex);
            }
        }


        public void SetMainPlayer(resource.settings.NpcRes.Special specialNpc)
        {
            this.mainPlayer = specialNpc;
        }

        public resource.settings.NpcRes.Special GetMainPlayer() => this.mainPlayer;

        public void EnterMap(int mapId, resource.map.Position position)
        {
            this.mainPlayer.SetMapPosition(position);
            this.map.HideNpc(this.mainPlayer);
            this.map.SetMapId(mapId);
            this.map.AddDynamicNpc(this.mainPlayer);
            this.map.SetPosition(position);
            this.mainCamera.SetPosition(this.mainPlayer.GetCameraPosition());

            MainCanvas.instance.MiniMap.SetMapPosition(position);

            MusicManagerGame.Instance.PlayMucsicMap(mapId);
        }

        public void EnterMap(int mapId, int top, int left) => this.EnterMap(mapId, new resource.map.Position(top, left));

        public void Teleport(game.resource.map.Position position)
        {
            this.map.SetPosition(position);
            this.mainPlayer.SetMapPosition(position);
            this.mainCamera.SetPosition(this.mainPlayer.GetCameraPosition());
            
            MainCanvas.instance.MiniMap.SetMapPosition(position);
        }

        public void AddStaticNpc(resource.settings.NpcRes.Special specialNpc) => this.map.AddStaticNpc(specialNpc);

        public void AddStaticNpc(resource.settings.NpcRes.Normal normalNpc) => this.map.AddStaticNpc(normalNpc);

        public void AddDynamicNpc(resource.settings.NpcRes.Special specialNpc) => this.map.AddDynamicNpc(specialNpc);

        public void AddDynamicNpc(resource.settings.NpcRes.Normal normalNpc) => this.map.AddDynamicNpc(normalNpc);

        public void AddObj(resource.settings.objres.Controller obj) => this.map.AddObj(obj);
        public void RemoveObj(resource.settings.objres.Controller obj) => this.map.HideObj(obj);

        public void RemoveNpc(game.resource.settings.npcres.Controller npc) => this.map.HideNpc(npc);
        public void UpdateNpc(game.resource.settings.npcres.Controller npc, int top, int left) => this.map.UpdateNpc(npc, top, left);

        public resource.Map GetMap() => this.map;
    }
}
