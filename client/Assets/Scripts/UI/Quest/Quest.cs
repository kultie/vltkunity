using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    private GameObject Minimize;

    [SerializeField]
    private GameObject ButtonAll;
    [SerializeField]
    private GameObject ButtonGroup;
    [SerializeField]
    private GameObject ListQuest;
    [SerializeField]
    public GameObject childPrefab;

    private bool IsOpen = false;

    void Start()
    {
        Minimize = this.gameObject.transform.Find("Minimize").gameObject;


        ButtonAll.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeTabUI(0);
        });

        ButtonGroup.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChangeTabUI(1);
        });


        Minimize.GetComponent<Button>().onClick.AddListener(() =>
        {
            ChanegUI();
        });

        RotateImage();
        SetItemQuest();
    }

    void SetItemQuest()
    {
        string message1 = "<color=green>[Phụ]Luyện công(1/1)</color>\n Tham gia <color=yellow>Thi Đấu Môn Phái</color> 1 lần";
        string message2 = "<color=green>[Phụ]Luyện công(1/1)</color>\n Tham gia <color=yellow>Thi Đấu Môn Phái</color> 1 lần";
        string message3 = "<color=green>[Phụ]Luyện công(1/1)</color>\n Tham gia <color=yellow>Thi Đấu Môn Phái</color> 1 lần";

        string[] lists = { message1, message2, message3 };

        VerticalLayoutGroup verticalLayoutGroup = ListQuest.GetComponent<VerticalLayoutGroup>();

        foreach (string textString in lists)
        {
            GameObject gameObject = Instantiate(childPrefab, Vector3.zero, Quaternion.identity);
            gameObject.GetComponentInChildren<Text>().text = textString;
            gameObject.transform.SetParent(verticalLayoutGroup.transform, false);
        }
    }

    void ChangeTabUI(int positionTab)
    {
        ButtonAll.GetComponentInChildren<Text>().color = Color.white;
        ButtonGroup.GetComponentInChildren<Text>().color = Color.white;

        Color newColor = new(246f / 255f, 1f, 105f / 255f);

        if (positionTab == 0)
        {
            ButtonAll.GetComponentInChildren<Text>().color = newColor;
        }
        else
        {
            ButtonGroup.GetComponentInChildren<Text>().color = newColor;
        }
    }


    void RotateImage()
    {
        Minimize.GetComponent<Image>().rectTransform.rotation = Quaternion.Euler(0, 0, IsOpen ? 0 : 180);
    }

    void ChanegUI()
    {
        IsOpen = !IsOpen;
        gameObject.GetComponent<RectTransform>().anchoredPosition = IsOpen ? new Vector2(0, -150) : new Vector2(-350, -150);
        RotateImage();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
