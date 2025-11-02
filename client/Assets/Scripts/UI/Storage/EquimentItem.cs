using System.Collections;
using System.Collections.Generic;
using game.resource.settings.item;
using UnityEngine;
using UnityEngine.UI;

public class EquimentItem : MonoBehaviour
{
    [SerializeField]
    private GameObject ImageItem;
    [SerializeField]
    private GameObject ImageFrame;

    private game.resource.settings.Item item;

    private static int index = 0;

    private void Start()
    {
        ImageItem.GetComponent<Image>().preserveAspect = true;
        ImageFrame.GetComponent<Image>().preserveAspect = true;

        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            OpenDetail();
        });
    }

    private void OpenDetail()
    {
        if (item != null)
        {
            PlayerMain.instance.MoveItemToStorage(item);
        }
    }

    public void SetItemEquiment(game.resource.settings.Item item)
    {
        this.item = item;

        // Image
        ImageItem.GetComponent<Image>().sprite = this.item.GetThumbnailSprite();
        ImageItem.SetActive(this.item.GetThumbnailSprite() != null);

        /// KHung
        if (item.GetItemType() == Defination.Type.goldEquip)
        {
            ImageFrame.SetActive(false);
            //game.style.canvas.Base framedTypeGold = new game.style.canvas.Base("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.gold.js");
            //framedTypeGold.SetCurrent(framedTypeGold.template.current + ".viewport.item." + ((System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 10) + (++index)));
            //framedTypeGold.SetParent(gameObject);
        }
        else
        {
            ImageFrame.GetComponent<Image>().sprite = this.item.GetTypeSprite();
            ImageFrame.SetActive(this.item.GetTypeSprite() != null);
        }
    }
}
