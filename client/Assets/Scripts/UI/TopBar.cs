using game.network;
using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Slider SliderHp;
    [SerializeField]
    public Text TextHp;

    [SerializeField]
    public Slider SliderMana;
    [SerializeField]
    public Text TextMana;

    [SerializeField]
    public Slider SliderSatamina;
    [SerializeField]
    public Text TextSatamina;

    [SerializeField]
    public GameObject BtnOpenProfile;
    [SerializeField]
    public GameObject BtnScreenShot;

    void Start()
    {
        BtnOpenProfile.GetComponent<Button>().onClick.AddListener(() =>
        {
            OpenProfile();
        });

        BtnScreenShot.GetComponent<Button>().onClick.AddListener(() =>
        {
            ScreenShot();
        });

    }

    float CalculateHPPercentage(int current, int max)
    {
        if (current <= 0 || max <= 0)
            return 0;

        return (float)current / max;
    }

    int HPCurrent => PhotonManager.Instance.character.CurLife;
    int HPMax => PhotonManager.Instance.character.MaxLife;

    int MPCurrent => PhotonManager.Instance.character.CurInner;
    int MPMax => PhotonManager.Instance.character.MaxInner;

    int SPCurrent => PhotonManager.Instance.character.CurStamina;
    int SPMax => PhotonManager.Instance.character.MaxStamina;
    public void UdpateUIHP()
    {
        string HPPecentData = HPCurrent + "/" + HPMax;
        float HPPecent = CalculateHPPercentage(HPCurrent, HPMax) * 100;
        SetUpHp(HPPecent, HPPecentData);
        //controller.SetHealthPercent((int)HPPecent);
    }
    public void UdpateUIMP()
    {
        string ManaPecentData = MPCurrent + "/" + MPMax;
        float MPPecent = CalculateHPPercentage(MPCurrent, HPMax) * 100;
        SetUpMana(MPPecent, ManaPecentData);
    }
    public void UdpateUISP()
    {
        float SpPecent = CalculateHPPercentage(SPCurrent, SPMax) * 100;
        string StaminaPecentData = SPCurrent + "/" + SPMax;
        SetUpSatamina(SpPecent, StaminaPecentData);
    }
    // Update is called once per frame
    void Update()
    {
        UdpateUIHP();
        UdpateUIMP();
        UdpateUISP();
    }

    public void OpenProfile()
    {
        MainCanvas.instance.OpenProfileDetail();
    }

    public void ScreenShot()
    {
        string folderPath = "Assets/Screenshots/"; // the path of your project folder

        if (!System.IO.Directory.Exists(folderPath)) // if this path does not exist yet
            System.IO.Directory.CreateDirectory(folderPath);  // it will get created

        var screenshotName =
                                "Screenshot_" +
                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + // puts the current time right into the screenshot name
                                ".png"; // put youre favorite data format here
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), 2); // takes the sceenshot, the "2" is for the scaled resolution, you can put this to 600 but it will take really long to scale the image up
    }

    public void SetUpHp(float pecent, string title)
    {
        TextHp.text = title;
        SliderHp.value = pecent;
    }

    public void SetUpMana(float pecent, string title)
    {
        TextMana.text = title;
        SliderMana.value = pecent;
    }

    public void SetUpSatamina(float pecent, string title)
    {
        TextSatamina.text = title;
        SliderSatamina.value = pecent;
    }
}
