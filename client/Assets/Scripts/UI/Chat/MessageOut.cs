using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageOut : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject NameMessage;
    [SerializeField] GameObject Message;

    void Start()
    {

    }

    public void UpdateMessage(string name, string message)
    {
        NameMessage.GetComponent<Text>().text = name;
        Message.GetComponent<Text>().text = message;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
