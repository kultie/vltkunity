using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Message;

    void Start()
    {

    }

    public void UpdateMessage(string message)
    {
        Message.GetComponent<Text>().text = message;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
