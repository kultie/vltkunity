using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using game.basemono;
using game.config;
using game.network;
using game.network.listener;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Handlers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static game.resource.mapping.settings.NpcRes.Kind;

public class LoginManger : BaseMonoBehaviour
{
    [SerializeField]
    GameObject panelUI;
    [SerializeField]
    GameObject pannelButton;
    [SerializeField]
    GameObject pannelLogin;
    [SerializeField]
    GameObject pannelRegister;

    [SerializeField]
    GameObject pannelPlay;
    [SerializeField]
    GameObject pannelCharacter;
    [SerializeField]
    GameObject pannelCreate;

    [SerializeField]
    private Toggle togglePolicy;

    class Faction
    {
        public byte Id;
        public string Name;
        public NPCSERIES Series;
    };
        private List<Faction> factions = new List<Faction>
        {
            new Faction
            {
                Id = 0,
                Name = "shaolin",
                Series = NPCSERIES.series_metal,
            },
            new Faction
            {
                Id = 1,
                Name = "tianwang",
                Series = NPCSERIES.series_metal,
            },
            new Faction
            {
                Id = 2,
                Name = "tangmen",
                Series = NPCSERIES.series_wood,
            },
            new Faction
            {
                Id = 3,
                Name = "wudu",
                Series = NPCSERIES.series_wood,
            },
            new Faction
            {
                Id = 4,
                Name = "emei",
                Series = NPCSERIES.series_water,
            },
            new Faction
            {
                Id = 5,
                Name = "cuiyan",
                Series = NPCSERIES.series_water,
            },
            new Faction
            {
                Id = 6,
                Name = "gaibang",
                Series = NPCSERIES.series_fire,
            },
            new Faction
            {
                Id = 7,
                Name = "tianren",
                Series = NPCSERIES.series_fire,
            },
            new Faction
            {
                Id = 8,
                Name = "wudang",
                Series = NPCSERIES.series_earth,
            },
            new Faction
            {
                Id = 9,
                Name = "kunlun",
                Series = NPCSERIES.series_earth,
            },
        };

    public static LoginManger instance;
    void Start()
    {
        instance = this;

        if (PlayerPrefs.GetInt(PlayerPrefsKey.USER_ID , 0) == 0)
        {
            pannelPlay.SetActive(false);
            panelUI.SetActive(true);
        }
        else
        {
            panelUI.SetActive(false);
            pannelPlay.SetActive(true);
            LoginImp();
        }
    }

    [SerializeField]
    InputField userNameInput;
    [SerializeField]
    InputField passWordInput;

    public void LoginBtn()
    {
        var userName = userNameInput.text;
        var passWord = passWordInput.text;

        if (userName == "")
        {
            ShowMessageBox(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, "username_is_empty"), "error");
            return;
        }
        if (passWord == "")
        {
            ShowMessageBox(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, "password_is_empty"), "error");
            return;
        }
        PlayerPrefs.SetString(PlayerPrefsKey.USER_NAME, userName);
        PlayerPrefs.SetString(PlayerPrefsKey.USER_PASSWORD, passWord);

        LoginImp();
    }
    void LoginImp()
    {

        try
        {
            string userName = PlayerPrefs.GetString(PlayerPrefsKey.USER_NAME);
            string userpassword = PlayerPrefs.GetString(PlayerPrefsKey.USER_PASSWORD);

            Dictionary<byte, object> opParameters = new()
            {
                 {(byte) ParamterCode.Account,userName},
                 {(byte) ParamterCode.Password,userpassword},
            };

            PhotonManager.Instance.Client().SendOperation((byte)OperationCode.Login, opParameters, ExitGames.Client.Photon.SendOptions.SendReliable);
        }
        catch (Exception)
        {
            pannelPlay.SetActive(false);
            panelUI.SetActive(true);
        }
    }

    [SerializeField]
    InputField usernameInput;
    [SerializeField]
    InputField passWordInput1;
    [SerializeField]
    InputField passWordInput2;

    public void RegisterImp()
    {
        var userName = usernameInput.text;
        var passWord1 = passWordInput1.text;
        var passWord2 = passWordInput2.text;

        if (userName == "")
        {
            ShowMessageBox(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, "username_is_empty"), "error");
            return;
        }
        if (passWord1 == "")
        {
            ShowMessageBox(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, "password_is_empty"), "error");
            return;
        }
        if (passWord1.Length < 8)
        {
            ShowMessageBox("Mật khẩu yêu cầu 8 kí tự trở lên", "error");
            return;
        }


        if (passWord2 == "")
        {
            ShowMessageBox(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, "password_is_empty"), "error");
            return;
        }
        if (passWord2.Length < 8)
        {
            ShowMessageBox("Mật khẩu yêu cầu 8 kí tự trở lên", "error");
            return;
        }

        try
        {
            ShowLoading();
            Dictionary<byte, object> opParameters = new()
            {
                 {(byte) ParamterCode.Account,userName},
                 {(byte) ParamterCode.Password,passWord1},
                 {(byte) ParamterCode.Password2,passWord2},
            };
            PhotonManager.Instance.Client().SendOperation((byte)OperationCode.Register, opParameters, ExitGames.Client.Photon.SendOptions.SendReliable);
        }
        catch (Exception)
        {
            HideLoading();
            ShowMessageBox("Vui lòng thử lại", "error");
        }
    }

    public void RegisterResponse()
    {
        //throw new NotImplementedException();
    }

    public void PlayNowScreen()
    {
        pannelPlay.SetActive(true);
        panelUI.SetActive(false);
    }
    [SerializeField]
    GameObject btnPlay;
    [SerializeField]
    GameObject btnGender;

    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        panelUI.SetActive(true);
        pannelButton.SetActive(true);
        pannelPlay.SetActive(false);
        pannelCharacter.SetActive(false);
        pannelCreate.SetActive(false);
    }

    public void Fails(string message)
    {
        HideLoading();
        pannelPlay.SetActive(false);
        panelUI.SetActive(true);
        ShowMessageBox(message, "error");
    }

    [HideInInspector]
    public List<CharacterLogin> characterReply;

    [SerializeField]
    CharacterInf[] info;
    public void LoginResponse(uint userId)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.USER_ID, (int)userId);

        panelUI.SetActive(false);
        pannelPlay.SetActive(true);
        pannelCharacter.SetActive(true);

        for (var i = 0; i < characterReply.Count; i++)
        {
            info[i].CharacterName = characterReply[i].Name;
            info[i].CharacterLevel = characterReply[i].Level.ToString();
        }
    }

    [SerializeField]
    SelectSeries man;
    [SerializeField]
    SelectSeries girl;

    public void OnSelectRole(int idx)
    {
        if (idx < characterReply.Count)
        {
            SelectSeries temp;
            if (characterReply[idx].Sex)
            {
                man.gameObject.SetActive(false);
                temp = girl;
            }
            else
            {
                girl.gameObject.SetActive(false);
                temp = man;
            }
            temp.gameObject.SetActive(true);
            temp.ChangeAttributeType((NPCSERIES)characterReply[idx].Series);
        }
        else
        {
            pannelCharacter.SetActive(false);
            pannelCreate.SetActive(true);
            btnPlay.SetActive(false);
            btnGender.SetActive(true);

            ShowClassByAttribute();
        }
    }
    [SerializeField]
    private GameObject btnMetal;
    [SerializeField]
    private GameObject btnWood;
    [SerializeField]
    private GameObject btnWater;
    [SerializeField]
    private GameObject btnFire;
    [SerializeField]
    private GameObject btnEarth;

    [SerializeField]
    GameObject buttonPrefab;
    [SerializeField]
    GameObject pannelListClass;

    [SerializeField]
    Image imageLG;
    [SerializeField]
    Image imageBG;

    [SerializeField]
    Text TextInfor;

    private NPCSERIES Attribute = NPCSERIES.series_metal;

    public void ChangeAttributeType(string attributeTypeName)
    {
        Attribute = ConfigGame.parserCategoryToAttribute(attributeTypeName);

        UpdateUICategory(attibuteType: Attribute);

        man.ChangeAttributeType(Attribute);

        girl.ChangeAttributeType(Attribute);

        ShowClassByAttribute();

        TextInfor.text = LocalizationSettings.StringDatabase.GetLocalizedString("LanguageTable", attributeTypeName);
    }
    private void ShowClassByAttribute()
    {
        List<Faction> listFactionByAttribute = new();
        if (Attribute == NPCSERIES.series_metal)
        {
            listFactionByAttribute = factions.Where(p => p.Name == "tianwang" || p.Name == "shaolin").ToList();
        }
        else
        {
            listFactionByAttribute = factions.Where(p => p.Series == Attribute).ToList();
        }

        foreach (Transform child in pannelListClass.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < listFactionByAttribute.Count; i++)
        {
            ClassUI(listFactionByAttribute[i]);
        }

        // Show defauft
        Faction factionDefauft = listFactionByAttribute.FirstOrDefault();

        ShowCharacterClass(factionDefauft.Name, factionDefauft.Id);

        UpdateUIButtonsGender();
    }
    private void ClassUI(Faction faction)
    {
        string factionClass = faction.Name;
        int factionId = faction.Id;

        string attribute = ConfigGame.parserAttributeTypeName(Attribute);

        GameObject pannelClass = (GameObject)Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), pannelListClass.transform);
        pannelClass.name = factionClass;
        pannelClass.GetComponentInChildren<Button>().onClick.AddListener(() => ShowCharacterClass(factionClass, factionId));
        pannelClass.GetComponentInChildren<Button>().image.sprite = Resources.Load<Sprite>("characters/" + attribute + "/" + "BT_" + factionClass);
        pannelClass.GetComponentInChildren<Button>().transition = Selectable.Transition.None;
        
        var anim = pannelClass.GetComponentInChildren<Animator>();
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("characters/" + attribute + "/" + "anim_" + attribute);
        anim.Rebind();
        
        pannelClass.GetComponentInChildren<Text>().text = faction.Series.ToString();
    }
    private void ShowCharacterClass(string factionClass, int factionID)
    {
        ChangeBackgroundClass(factionClass);
        man.ReShow();
        girl.ReShow();
        GenderSelect(man.isActive ? "man" : "girl");
        UpdateUIClass(factionClass);
    }
    private void ChangeBackgroundClass(string nameClass)
    {
        string attribute = ConfigGame.parserAttributeTypeName(Attribute);

        Sprite spriteLG = Resources.Load<Sprite>("characters/" + attribute + "/" + "LG_" + nameClass);
        imageLG.sprite = spriteLG;
        Sprite spriteBG = Resources.Load<Sprite>("characters/" + attribute + "/" + "BG_" + nameClass);
        imageBG.sprite = spriteBG;
    }
    [SerializeField]
    GameObject btnMan;
    [SerializeField]
    GameObject btnGirl;
    public void GenderSelect(string gender)
    {
        man.ChangeActive(gender == "man" ? true : false);
        btnMan.GetComponent<Image>().sprite = Resources.Load<Sprite>(gender == "man" ? ResourcesManager.genderManButtonSelected : ResourcesManager.genderManButtonDisabled);

        girl.ChangeActive(gender == "man" ? false : true);
        btnGirl.GetComponent<Image>().sprite = Resources.Load<Sprite>(gender == "girl" ? ResourcesManager.genderGirlButtonSelected : ResourcesManager.genderGirlButtonDisabled);
    }
    private void UpdateUIButtonsGender()
    {
        btnMan.GetComponent<Button>().interactable = true;
        btnGirl.GetComponent<Button>().interactable = true;

        if (Attribute == NPCSERIES.series_metal)
            btnGirl.GetComponent<Button>().interactable = false;
        else
        if (Attribute == NPCSERIES.series_water)
            btnMan.GetComponent<Button>().interactable = false;
    }
    private void UpdateUIClass(string nameClass)
    {
        foreach (Transform child in pannelListClass.transform)
        {
            if (child.gameObject.name == nameClass)
                child.GetComponent<ClassUI>().ShowAnim();
            else
                child.GetComponent<ClassUI>().HideAnim();
        }
    }
    private void UpdateUICategory(NPCSERIES attibuteType)
    {
        btnMetal.SetActive(false);
        btnWood.SetActive(false);
        btnWater.SetActive(false);
        btnFire.SetActive(false);
        btnEarth.SetActive(false);

        switch (attibuteType)
        {
            case NPCSERIES.series_metal:
                btnMetal.SetActive(true);
                break;
            case NPCSERIES.series_wood:
                btnWood.SetActive(true);
                break;
            case NPCSERIES.series_water:
                btnWater.SetActive(true);
                break;
            case NPCSERIES.series_fire:
                btnFire.SetActive(true);
                break;
            case NPCSERIES.series_earth:
                btnEarth.SetActive(true);
                break;
        }
    }

    public void ResetInput()
    {

    }
    [SerializeField]
    InputField inputName;
    public void CreateCharacter()
    {
        var charaterName = inputName.text.Trim();
        if (string.IsNullOrEmpty(charaterName))
        {
            ShowMessageBox(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, "name_is_empty"), "error");
            return;
        }
        try
        {
            ShowLoading();
            Dictionary<byte, object> opParameters = new()
            {
                 //{(byte) ParamterCode.UserId, userId},
                 //{(byte) ParamterCode.FactionId, factionId},
                 //{(byte) ParamterCode.Sex, gender == "man" ? 1 : 0 },
                 {(byte) ParamterCode.CharacterName, charaterName},
            };
            PhotonManager.Instance.Client().SendOperation((byte)OperationCode.CreateCharacter, opParameters, ExitGames.Client.Photon.SendOptions.SendReliable);
        }
        catch (Exception)
        {
            HideLoading();
            ShowMessageBox("Vui lòng thử lại", "error");
        }
    }
    public void CreateCharcterSuccess(uint cid)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey.CHARACTER_ID, (int)cid);
        try
        {
            Dictionary<byte, object> opParameters = new()
            {
                {(byte) ParamterCode.UserId, (uint)PlayerPrefs.GetInt(PlayerPrefsKey.USER_ID)},
                {(byte) ParamterCode.CharacterId, cid},
            };
            PhotonManager.Instance.Client().SendOperation((byte)OperationCode.SelectCharacter, opParameters, ExitGames.Client.Photon.SendOptions.SendReliable);
        }
        catch (Exception)
        {
            HideLoading();
            ShowMessageBox("Vui lòng thử lại", "error");
        }
    }
    public void PlayGame()
    {
        if (togglePolicy.isOn)
        {
            CharacterLogin loginCharacter = characterReply[0];
            PlayerPrefs.SetInt(PlayerPrefsKey.CHARACTER_ID, (int)loginCharacter.Id);
            try
            {
                ShowLoading();
                Dictionary<byte, object> opParameters = new()
                    {
                        {(byte) ParamterCode.UserId, (uint)PlayerPrefs.GetInt(PlayerPrefsKey.USER_ID)},
                        {(byte) ParamterCode.CharacterId, loginCharacter.Id},
                    };

                PhotonManager.Instance.Client().SendOperation((byte)OperationCode.SelectCharacter, opParameters, ExitGames.Client.Photon.SendOptions.SendReliable);
            }
            catch (Exception)
            {
                HideLoading();
                ShowMessageBox("Vui lòng thử lại", "error");
            }
        }
        else
        {
            ShowMessageBox(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, "please_agree_policy"), "error");
        }
    }
}
