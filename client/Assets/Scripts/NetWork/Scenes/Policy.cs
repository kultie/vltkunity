using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy : MonoBehaviour
{
    public GameObject pannelButton;
    public GameObject panelUILogin;
    public GameObject panelUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        PlayerPrefs.DeleteAll();
        panelUILogin.SetActive(false);
        panelUI.SetActive(true);
        pannelButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Appect()
    {
        gameObject.SetActive(false);
    }
}
