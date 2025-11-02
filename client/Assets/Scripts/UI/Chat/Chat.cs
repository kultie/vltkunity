using System.Collections.Generic;
using Photon.ShareLibrary.Constant;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    private GameObject TabChat;
    private GameObject ButtonAll;
    private GameObject ButtonGuild;
    private GameObject ButtonGroup;

    private GameObject ChatMessage;
    private GameObject ChatResize;

    [SerializeField]
    public GameObject ContentMessage;
    [SerializeField]
    public GameObject childPrefab;

    bool isChatFull = false;

    private string UpIcon = "WorldGameUI/Buttons/btn_narrow_up";
    private string DownIcon = "WorldGameUI/Buttons/btn_narrow_down";

    void Start()
    {
        TabChat = gameObject.transform.Find("Tab").gameObject;
        ButtonAll = TabChat.transform.Find("ButtonAll").gameObject;
        ButtonGuild = TabChat.transform.Find("ButtonGuild").gameObject;
        ButtonGroup = TabChat.transform.Find("ButtonGroup").gameObject;

        ChatMessage = gameObject.transform.Find("ChatMessage").gameObject;
        ChatResize = ChatMessage.transform.Find("ChatResize").gameObject;

        ChatResize.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChanegSizeChat();
        });

        ButtonAll.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeTabUI(0);
        });

        ButtonGuild.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeTabUI(1);
        });

        ButtonGroup.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeTabUI(2);
        });

        //InitMessage();
    }

    public void OpenChatFull()
    {
        PopUpCanvas.instance.OpenChatPannel();
    }

    public void AddNewMessage(MessageData data, PlayerChat playerChat)
    {
        if (playerChat == PlayerChat.system)
        {
            InitMessage(data.Message);
        }
    }

    void ChanegSizeChat()
    {
        isChatFull = !isChatFull;

        if (isChatFull)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 400);
            ChatResize.GetComponent<Image>().sprite = Resources.Load<Sprite>(DownIcon);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 150);
            ChatResize.GetComponent<Image>().sprite = Resources.Load<Sprite>(UpIcon);
        }
    }

    void ChangeTabUI(int positionTab)
    {
        ButtonAll.GetComponentInChildren<Text>().color = Color.white;
        ButtonGuild.GetComponentInChildren<Text>().color = Color.white;
        ButtonGroup.GetComponentInChildren<Text>().color = Color.white;

        Color newColor = new(246f / 255f, 1f, 105f / 255f);

        if (positionTab == 0)
        {
            ButtonAll.GetComponentInChildren<Text>().color = newColor;
        }
        else if (positionTab == 1)
        {
            ButtonGuild.GetComponentInChildren<Text>().color = newColor;
        }
        else
        {
            ButtonGroup.GetComponentInChildren<Text>().color = newColor;
        }
    }

    void InitMessage(string message)
    {
        string newMessage = "<color=yellow>Hệ thống</color>: " + message;

        VerticalLayoutGroup verticalLayoutGroup = ContentMessage.GetComponent<VerticalLayoutGroup>();
        GameObject gameObject = Instantiate(childPrefab, Vector3.zero, Quaternion.identity);
        gameObject.GetComponentInChildren<Text>().text = newMessage;
        gameObject.transform.SetParent(verticalLayoutGroup.transform, false);
    }
}
