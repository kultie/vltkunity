using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageIN : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject MessageName;
    [SerializeField] GameObject GameMessage;


    void Start()
    {

    }

    public void UpdateMessage(string name, string message)
    {
        MessageName.GetComponent<Text>().text = name;
        GameMessage.GetComponent<Text>().text = message;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
