using System;
using System.Collections.Generic;
using game.network;
using game.resource.settings;
using game.resource.settings.item;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ListStorage;
    [SerializeField]
    private GameObject ListBag;
    [SerializeField]
    private GameObject ItemEquip;
    [SerializeField]
    private Button CloseGameObject;
    [SerializeField]
    private Text TextStorages;
    [SerializeField]
    private Text TextBags;

    private Dictionary<int, GameObject> ListBags = new();
    private Dictionary<int, GameObject> ListStorages = new();

    private void Start()
    {
        CloseGameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void InitStorage()
    {
        for (int i = 0; i < 200; i++)
        {
            GridLayoutGroup gridLayoutGroup = ListStorage.GetComponent<GridLayoutGroup>();
            GameObject StoragesChild = Instantiate(ItemEquip, Vector3.zero, Quaternion.identity);
            StoragesChild.transform.SetParent(gridLayoutGroup.transform, false);
            ListStorages.Add(i, StoragesChild);

            GridLayoutGroup gridLayoutGroup2 = ListBag.GetComponent<GridLayoutGroup>();
            GameObject BagsChild = Instantiate(ItemEquip, Vector3.zero, Quaternion.identity);
            BagsChild.transform.SetParent(gridLayoutGroup2.transform, false);
            ListBags.Add(i, BagsChild);
        }
    }

    public void SetUpPlayerItem()
    {
        List<ItemData> itemDataBags = new();
        List<ItemData> itemDataStorages = new();

        foreach (var data in PhotonManager.Instance.GetPlayerItems())
        {
            ItemData itemData = data.Value;

            if (itemData.Local != (byte)ItemPosition.pos_equip)
            {
                itemDataBags.Add(itemData);
            }
        }

        TextBags.text = itemDataBags.Count + "/" + ListBags.Count;
        TextStorages.text = itemDataStorages.Count + "/" + ListStorages.Count;

        for (int i = 0; i < itemDataBags.Count; i++)
        {
            Item item = RestoreItemFromDatabase(itemDataBags[i]);
            GameObject itemBag = ListBags[i];
            itemBag.GetComponent<EquimentItem>().SetItemEquiment(item);
        }
    }

    Item RestoreItemFromDatabase(ItemData itemdata)
    {
        game.resource.settings.item.Database itemDatabase = new()
        {
            genre = itemdata.Equipclasscode, // settings/meleeweapon.txt
            detail = itemdata.Detailtype, // settings/meleeweapon.txt
            particular = itemdata.Particulartype, // settings/meleeweapon.txt
            level = itemdata.Level, // cấp 10
            series = itemdata.Series, // hệ kim
            //databaseId = itemdata.Id,
            //type = itemdata.Goldid > 0 ? Defination.Type.goldEquip : Defination.Type.normalEquip,
            //rowIndex = itemdata.Goldid,
        };
/*
        List<MagicAttribute> magicAttributes = new(itemdata.Magics);
        magicAttributes.Sort((a, b) => a.AttributeIndex.CompareTo(b.AttributeIndex));

        for (int i = 0; i < magicAttributes.Count; i++)
        {
            if (magicAttributes[i] != null)
            {
                var fieldType = itemDatabase.GetType();
                var field = fieldType.GetField(string.Format("magic{0}Type", i));
                field.SetValue(itemDatabase, magicAttributes[i].AttributeType);
                field = fieldType.GetField(string.Format("magic{0}Value0", i));
                field.SetValue(itemDatabase, magicAttributes[i].Value0);
                field = fieldType.GetField(string.Format("magic{0}Value1", i));
                field.SetValue(itemDatabase, magicAttributes[i].Value1);
                field = fieldType.GetField(string.Format("magic{0}Value2", i));
                field.SetValue(itemDatabase, magicAttributes[i].Value2);
            }
        }
*/
        return new(itemDatabase);
    }

}
