using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPopUp : MonoBehaviour
{
    [SerializeField]
    public GameObject ButtonPrefab;
    [SerializeField]
    private GameObject ListActions;


    void Start()
    {
        GridLayoutGroup verticalLayout = ListActions.GetComponent<GridLayoutGroup>();
        ResetChildren(verticalLayout);

        // Add Button.

        CreateButton("Mật", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Mật");
        });

        CreateButton("Thông tin", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Thông tin");
        });

        CreateButton("Tổ đội", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Tổ đội");
        });

        CreateButton("Vào đội", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Vào đội");
        });

        CreateButton("Hảo hữu", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Hảo hữu");
        });

        CreateButton("Vào bang", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Vào bang");
        });

        CreateButton("Mời bang", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Mời bang");
        });

        CreateButton("Sổ đen", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Sổ đen");
        });

        CreateButton("Bái Sư", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Bái Sư");
        });

        CreateButton("Giao dịch", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            PopUpCanvas.instance.OpenTrade();
            gameObject.SetActive(false);
        });

        CreateButton("Cừu Sát", verticalLayout).GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Cừu Sát");
        });
    }

    GameObject CreateButton(string name, GridLayoutGroup verticalLayout)
    {
        GameObject newChild = Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity);
        newChild.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(100, 60);
        newChild.GetComponentInChildren<Text>().text = name;
        newChild.transform.SetParent(verticalLayout.transform, false);
        return newChild;
    }

    void ResetChildren(GridLayoutGroup verticalLayout)
    {
        foreach (Transform child in verticalLayout.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
