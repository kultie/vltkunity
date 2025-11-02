using game.network;
using Photon.ShareLibrary.Handlers;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas instance;

    public game.scene.world.userInterface.PanelUser PanelUser;
    public game.scene.world.userInterface.MiniMap MiniMap;

    public VariableJoystick variableJoystick;

    private UnityEngine.Vector2 joybase;

    private game.scene.World world;

    private GameObject PanelSafeArea;
    private GameObject PanelHotKeys;

    private GameObject ButtonCommonGroup;
    private GameObject ButtonCommonGuild;
    private GameObject ButtonCommonSkill;
    private GameObject ButtonCommonSettting;
    private GameObject ButtonCommonSwitch;
    private GameObject TopBar;
    private GameObject MiniChat;

    private bool isOpen = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PanelSafeArea = this.gameObject.transform.Find("PanelSafeArea").gameObject;

        PanelHotKeys = gameObject.transform.Find("PanelHotKeys").gameObject;
        ButtonCommonGroup = gameObject.transform.Find("ButtonCommonGroup").gameObject;
        ButtonCommonGuild = ButtonCommonGroup.transform.Find("ButtonCommonGuild").gameObject;
        ButtonCommonSkill = ButtonCommonGroup.transform.Find("ButtonCommonSkill").gameObject;
        ButtonCommonSettting = ButtonCommonGroup.transform.Find("ButtonCommonSettting").gameObject;
        MiniChat = gameObject.transform.Find("Chat").gameObject;

        TopBar = PanelSafeArea.transform.Find("TopBar").gameObject;

        world = GetComponent<game.scene.World>();
        joybase = new UnityEngine.Vector2(variableJoystick.getHandle().position.x, variableJoystick.getHandle().position.z);
    }

    void Update()
    {
        this.JoystickChange();
        this.SynInterval();

        //this.userInterface.ReStyle(this.currentFixedUpdateFps);
    }

    /// <summary>
    /// Sync data after syncTime
    /// </summary>
    bool isMove = false;
    private UnityEngine.Vector2 moveDirection;

    /// <summary>
    /// Call move to service.
    /// </summary>
    private void JoystickChange()
    {
        if (variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0)
        {
            var joycurrent = new UnityEngine.Vector2(variableJoystick.getHandle().position.x, variableJoystick.getHandle().position.z);
            var distance = UnityEngine.Vector2.Distance(joybase, joycurrent);
            if (distance > 0.25)
            {
                moveDirection = variableJoystick.Direction * distance;
            }
        }
        else
        {
            /// Call stop move.
            moveDirection = Vector2.zero;
        }
    }
    private void SynInterval()
    {
        if (moveDirection != Vector2.zero)
        {
            isMove = true;
            var playerPosition = world.GetMainPlayer().GetMapPosition();
            playerPosition.top += (int)moveDirection.x;
            playerPosition.left += (int)moveDirection.y;

            PlayerMain.instance.SynCharMove(playerPosition.left, playerPosition.top);
        }
        else
        {
            if (isMove)
            {
                isMove = false;
                PhotonManager.Instance.Client().SendOperation((byte)OperationCode.StopMove, new Dictionary<byte, object>(), ExitGames.Client.Photon.SendOptions.SendReliable);
            }
            else
            {
                //Debug.Log("KO SYNC DI CHUYEN");
            }
        }
    }

    /// <summary>
    /// Open Store
    /// </summary>
    public void OpenBag()
    {
        //world.GetUserInterface().panelEquipment.current.SetActive(true);
    }


    /// <summary>
    /// Open Store
    /// </summary>
    public void OpenProfileDetail()
    {
        //world.GetUserInterface().panelEquipment.OpenProperties();
        //world.GetUserInterface().panelEquipment.current.SetActive(true);
    }

    /// <summary>
    /// Open setting
    /// </summary>
    public void OpenSetting()
    {
        //world.GetUserInterface().panelSettings.current.SetActive(true);
    }

    /// <summary>
    /// Skill
    /// </summary>
    public void OpenSkill()
    {
        if (PanelHotKeys != null)
        {
            isOpen = !isOpen;
            PanelHotKeys.GetComponent<SkillAction>().UpdateSkill();
            PanelHotKeys.SetActive(isOpen);
            ButtonCommonGuild.SetActive(!isOpen);
            ButtonCommonSkill.SetActive(!isOpen);
            ButtonCommonSettting.SetActive(!isOpen);
        }
    }

    public GameObject CommonSwitch() => this.ButtonCommonSwitch;
    public GameObject GetMiniChat() => this.MiniChat;
}

