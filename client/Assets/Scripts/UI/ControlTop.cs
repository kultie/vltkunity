using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ControlTop : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject ControlPannel;
    private GameObject Minimize;
    private bool IsOpen = true;

    [SerializeField]
    private GameObject FirstRecharge;
    [SerializeField]
    private GameObject LoginReward;
    [SerializeField]
    private GameObject PhucLoi;
    [SerializeField]
    private GameObject Shop;
    [SerializeField]
    private GameObject News;

    void Start()
    {
        Minimize = this.gameObject.transform.Find("Panel").gameObject.transform.Find("Minimize").gameObject;
        ControlPannel = this.gameObject.transform.Find("PanelControl").gameObject;

        Minimize.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChanegUI();
        });

        RotateImage();


        FirstRecharge.GetComponent<Button>().onClick.AddListener(() =>
        {
            PopUpCanvas.instance.OpenFirstRecharge();
        });

        LoginReward.GetComponent<Button>().onClick.AddListener(() =>
        {
            PopUpCanvas.instance.OpenLoginReward();
        });

        PhucLoi.GetComponent<Button>().onClick.AddListener(() =>
        {
            PopUpCanvas.instance.OpenPhucLoi();
        });

        Shop.GetComponent<Button>().onClick.AddListener(() =>
        {
            PopUpCanvas.instance.OpenShop();
        });

        News.GetComponent<Button>().onClick.AddListener(() =>
        {
            PopUpCanvas.instance.OpenNewsPannel();
        });
    }

    void RotateImage()
    {
        Minimize.GetComponent<Image>().rectTransform.rotation = Quaternion.Euler(0, 0, IsOpen ? 180 : 0);
    }

    void ChanegUI()
    {
        IsOpen = !IsOpen;
        ControlPannel.SetActive(IsOpen);
        RotateImage();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
