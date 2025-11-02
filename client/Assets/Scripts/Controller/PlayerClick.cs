using game.network;
using game.network.listener;
using game.scene;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;
using UnityEngine;

public class PlayerClick : CharacterClick
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUp()
    {
        PopUpCanvas.instance.OpenPlayerPopUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
