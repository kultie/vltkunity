using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;

using Photon.ShareLibrary.Handlers;
using System.Reflection;
using game.network.listener;
using UnityEngine.SceneManagement;
using game.ui;
using game.config;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Utils;

public class PhotonManager : MonoBehaviour, IPhotonPeerListener
{
    private static PhotonManager instance;
    public PhotonPeer client;

    public game.scene.World world;
    public game.scene.CharManager CharMgrs;
    public game.scene.NpcManager NpcMgrs;

    List<MessageHandlers> resHandlers = new List<MessageHandlers>();
    List<MessageHandlers> syncHandlers = new List<MessageHandlers>();
    private bool isConnected = false;
    private bool isDestroy = false;

    string PHOTON_SERVER_URL = "154.26.129.47:4530";

    GameObject reConnect;


    public static PhotonManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new();
                instance = singletonObject.AddComponent<PhotonManager>();
                singletonObject.name = "PhotonManager (Singleton)";
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        LoadHandles();
        RegisterTypes();

        client = new PhotonPeer(this, ConnectionProtocol.Tcp);
        client.TimePingInterval = 3000;

        if (client.Connect(PHOTON_SERVER_URL, "JXServer"))
        {
            Debug.Log("----- Photon Connecting");
        }
    }

    void LoadHandles()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var type in types)
        {
            if (type.Namespace == "Photon.ShareLibrary.Handlers")
            {
                try
                {
                    var temp = (MessageHandlers)Activator.CreateInstance(type);
                    if (temp.Type == MessageType.Response)
                        resHandlers.Add(temp);
                    else
                        syncHandlers.Add(temp);
                }
                catch (Exception ex)
                {
                    Debug.LogError((ex.InnerException ?? ex).ToString());
                }
            };
        }
    }
    public void RegisterTypes()
    {
        PhotonPeer.RegisterType(typeof(ushort), (byte)CustomTypeCode.UShort,
                                       SerializerMethods.SerializeUShort,
                                       SerializerMethods.DeserializeUShort);

        PhotonPeer.RegisterType(typeof(uint), (byte)CustomTypeCode.UInt,
                                       SerializerMethods.SerializeUInt,
                                       SerializerMethods.DeserializeUInt);

        PhotonPeer.RegisterType(typeof(ulong), (byte)CustomTypeCode.ULong,
                                       SerializerMethods.SerializeULong,
                                       SerializerMethods.DeserializeULong);

        //PhotonPeer.RegisterType(typeof(MmoGuid), (byte)CustomTypeCode.Guid,
        //                               GlobalSerializerMethods.SerializeGuid,
        //                               GlobalSerializerMethods.DeserializeGuid);
    }

    public void ReConnect() => client.Connect(PHOTON_SERVER_URL, "JXServer");

    public void ShowReConnect()
    {
        if (isDestroy)
        {
            return;
        }
        Scene currentScene = SceneManager.GetActiveScene();

        if (isConnected && reConnect == null && currentScene.name == "GameWorldScene")
        {
            reConnect = UIHelpers.BringPrefabToScene("Reconnect");
            reConnect.GetComponent<Reconnect>().SetError("Mất kết nối đến máy chủ. Thử lại hoặc thoát game.");
        }
    }

    public void OnDisableReConnect()
    {
        if (reConnect != null)
        {
            Destroy(reConnect);
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    bool ischarge = false;
    bool issync = false;

    public void ServerCharge(string ip, bool sync)
    {
        ischarge = true;
        issync = sync;

        client.Disconnect();
        client.Connect(ip, "JXServer");
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("----- Photon Connected");
                if (ischarge)
                {
                    Dictionary<byte, object> opParameters = new()
                        {
                            {(byte) ParamterCode.UserId, (uint)PlayerPrefs.GetInt(PlayerPrefsKey.USER_ID)},
                            {(byte) ParamterCode.CharacterId, (uint)PlayerPrefs.GetInt(PlayerPrefsKey.CHARACTER_ID)},
                            {(byte) ParamterCode.Data, issync},
                        };
                    client.SendOperation((byte)OperationCode.WorldJoin, opParameters, ExitGames.Client.Photon.SendOptions.SendReliable);
                    ischarge = false; return;
                }

                isConnected = true;
                OnDisableReConnect();
                break;

            case StatusCode.Disconnect:
                if (ischarge)
                    return;

                Debug.Log("----- Photon DisConnected");
                ShowReConnect();
                isConnected = false;
                break;
        }
    }

    public void OnOperationResponse(OperationResponse op)
    {

        Debug.Log((OperationCode)op.OperationCode);
        if (op.DebugMessage != null)
        {
            Debug.Log(op.DebugMessage);
        }
        foreach (var handle in resHandlers)
        {
            if (handle.Code == (OperationCode)op.OperationCode)
            {
                try
                {
                    handle.Process(op.ReturnCode, op.Parameters);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                break;
            }
        }
    }

    public void OnEvent(EventData ev)
    {
        Debug.Log((OperationCode)ev.Code);
        foreach (var handle in syncHandlers)
        {
            if (handle.Code == (OperationCode)ev.Code)
            {
                try
                {
                    handle.Process(0, ev.Parameters);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                break;
            }
        }
    }

    int msDeltaForServiceCalls = 50, msTimestampOfLastServiceCall = 0;

    // Update is called once per frame
    void Update()
    {
        if (this.client == null)
            return;

        while (this.client.DispatchIncomingCommands())
        {
        }
        if (Environment.TickCount - this.msTimestampOfLastServiceCall > this.msDeltaForServiceCalls || this.msTimestampOfLastServiceCall == 0)
        {
            this.msTimestampOfLastServiceCall = Environment.TickCount;

            while (this.client.SendOutgoingCommands())
            {
            }
        }
    }

    private void OnDestroy()
    {
        isDestroy = true;
        if (client != null)
        {
            client.Disconnect();
            client = null;
            isConnected = false;
        }

        if (reConnect != null)
        {
            Destroy(reConnect);
        }

    }

    /// <summary>
    /// Photon Listener
    /// </summary>
    private ICharClientListener iCharClientListener;
    private INpcClientListener iNpcClientListener;
    private IMainPlayerClientListener iMainPlayerClientListener;

    public void SetCharClientListener(ICharClientListener listener) => this.iCharClientListener = listener;

    public ICharClientListener CharClientListener() => iCharClientListener;

    public void SetNpcClientListener(INpcClientListener listener) => this.iNpcClientListener = listener;

    public INpcClientListener NpcClientListener() => iNpcClientListener;

    public void SetMainPlayerClientListener(IMainPlayerClientListener listener) => this.iMainPlayerClientListener = listener;

    public IMainPlayerClientListener MainPlayerClientListener() => iMainPlayerClientListener;


    /// <summary>
    /// Photon Client
    /// </summary>
    /// <returns></returns>
    public PhotonPeer Client() => this.client;
    public bool IsConnected() => this.isConnected;
    public bool IsDisConnected()
    {
        return client == null || !isConnected;
    }

    /// <summary>
    /// Photon Data
    /// </summary>

    [HideInInspector]
    public int PlayerId;
    [HideInInspector]
    public ushort MapId;
    [HideInInspector]
    public int MapX, MapY;

    public CharacterData character = null;
    public CharacterData GetChracter() => character;

    /// <summary>
    /// Chat
    /// </summary>
    private Dictionary<PlayerChat, MessageData> Messages = new();

    public void SetMessage(MessageData data, PlayerChat playerChat)
    {
        Messages[playerChat] = data;
        MainCanvas.instance.GetMiniChat().GetComponent<Chat>().AddNewMessage(data, playerChat);
        PopUpCanvas.instance.GetOpenChat().GetComponent<OpenChat>().AddNewMessage(data, playerChat);
    }

    public Dictionary<PlayerChat, MessageData> GetMessage() => Messages;

    /// <summary>
    /// PlayerItems
    /// </summary>
    private Dictionary<uint, ItemData> playerItems = new Dictionary<uint, ItemData>();

    public void SetPlayerItem(ItemData data)
    {
        bool containsKey = playerItems.ContainsKey(data.id);
        if (containsKey)
        {
            playerItems[data.id] = data;
            iMainPlayerClientListener?.SyncUpdateItem(data);
        }
        else
        {
            playerItems.Add(data.id, data);
            iMainPlayerClientListener?.SyncNewItem(data);
        }
    }

    public Dictionary<uint, ItemData> GetPlayerItems() => playerItems;

    /// <summary>
    /// PlayerSkills
    /// </summary>
    private Dictionary<ushort, PlayerSkill> playerSkills = new Dictionary<ushort, PlayerSkill>();

    public void SetPlayerSkill(PlayerSkill data)
    {
        bool containsKey = playerSkills.ContainsKey(data.id);
        if (containsKey)
        {
            playerSkills[data.id] = data;
            iMainPlayerClientListener?.SyncUpdateSkill(data);
        }
        else
        {
            playerSkills.Add(data.id, data);
            iMainPlayerClientListener?.SyncAddSkill(data);
        }
    }

    public Dictionary<ushort, PlayerSkill> GetPlayerSkill() => playerSkills;

    /// <summary>
    /// Player Task
    /// </summary>
    public List<PlayerTask> playerTasks;
    public void SetPlayerTask(PlayerTask data)
    {
        playerTasks.Add(data);
        iMainPlayerClientListener?.SyncTask();
    }
    public List<PlayerTask> GetPlayerTasks() => playerTasks;

    internal void DoTask()
    {
        client.SendOperation((byte)OperationCode.DoTask, new Dictionary<byte, object>()
        {
            [(byte)ParamterCode.TaskId] = 1,
            [(byte)ParamterCode.TaskValue] = 2,
        }, ExitGames.Client.Photon.SendOptions.SendReliable);
    }

    internal void SetMessage(PlayerChat type, MessageData messageData)
    {
        throw new NotImplementedException();
    }
}