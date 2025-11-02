
using System.Collections.Generic;
using UnityEngine;

namespace game.scene.world.userInterface
{
    public class PanelUserEquipment : MonoBehaviour
    {
        public class Cell : MonoBehaviour
        {
/*
            public class Framed
            {
                public UnityEngine.GameObject goCurrent;
                public UnityEngine.RectTransform compRect;
                public UnityEngine.UI.Image compImage;
                public UnityEngine.UI.Button compButton;
            }

            public style.canvas.Base background;
            public Cell.Framed thumbnail;
            public Cell.Framed type;
            public style.canvas.Base typeGold;

            public resource.settings.Item item = null;
            public readonly panelEquipment.SeriesTab.Cell seriesRef;
            public readonly bool isRingBelow;

            public Cell(string stylePath, panelEquipment.SeriesTab.Cell seriesRef, bool isRingBelow = false) : base(stylePath)
            {
                this.seriesRef = seriesRef;
                this.isRingBelow = isRingBelow;
            }

            public void Clear()
            {
                if (this.background != null)
                {
                    this.background.Destroy();
                    this.background = null;
                }

                if (this.thumbnail != null)
                {
                    UnityEngine.GameObject.Destroy(this.thumbnail.goCurrent);
                    this.thumbnail = null;
                }

                if (this.type != null)
                {
                    UnityEngine.GameObject.Destroy(this.type.goCurrent);
                    this.type = null;
                }

                if (this.typeGold != null)
                {
                    this.typeGold.Destroy();
                    this.typeGold = null;
                }

                this.item = null;

                if (this.seriesRef != null)
                {
                    this.seriesRef.Clear();
                }
            }

            public void Apply(resource.settings.Item item)
            {
                this.item = item;

                if (this.background == null)
                {
                    this.background = new style.canvas.Base("\\user.interface\\panel.equipment\\equip.tab\\framed.background.js");
                    this.background.SetParent(this.current);
                    this.background.SetCurrent(this.background.GetCurrentObjectName() + "." + this.GetCurrentFileName());
                }

                if (this.thumbnail == null)
                {
                    this.thumbnail = new Cell.Framed();
                    this.thumbnail.goCurrent = new UnityEngine.GameObject("thumbnail");
                    this.thumbnail.compRect = this.GetComponentFromParent<UnityEngine.RectTransform>(this.thumbnail.goCurrent);
                    this.thumbnail.compImage = this.GetComponentFromParent<UnityEngine.UI.Image>(this.thumbnail.goCurrent);

                    this.thumbnail.goCurrent.transform.SetParent(this.current.transform, false);
                    this.thumbnail.compImage.preserveAspect = true;
                }

                resource.settings.item.Getter.GridablePos thumbnailGp = item.GetThumbnailGridable();

                Game.Style.Canvas
                        .Base(this.current)
                        .Gridable(thumbnailGp.cB, thumbnailGp.cE, thumbnailGp.rB, thumbnailGp.rE)
                        .Apply(this.thumbnail.goCurrent);

                this.thumbnail.compImage.sprite = item.GetThumbnailSprite();

                if (this.type != null) this.type.goCurrent.SetActive(false);
                if (this.typeGold != null) this.typeGold.current.SetActive(false);

                switch (item.GetItemType())
                {
                    case resource.settings.item.Defination.Type.normalEquip:
                        if (this.type == null)
                        {
                            this.type = new Cell.Framed();
                            this.type.goCurrent = new UnityEngine.GameObject("type");
                            this.type.compRect = this.GetComponentFromParent<UnityEngine.RectTransform>(this.type.goCurrent);
                            this.type.compImage = this.GetComponentFromParent<UnityEngine.UI.Image>(this.type.goCurrent);

                            this.type.goCurrent.transform.SetParent(this.current.transform, false);
                        }

                        this.type.goCurrent.SetActive(true);
                        resource.settings.item.Getter.GridablePos typeGp = item.GetTypeGridable();

                        Game.Style.Canvas
                            .Base(this.current)
                            .Gridable(typeGp.cB, typeGp.cE, typeGp.rB, typeGp.rE)
                            .Apply(this.type.goCurrent);

                        this.type.compImage.sprite = item.GetTypeSprite();
                        break;

                    case resource.settings.item.Defination.Type.goldEquip:
                        if (this.typeGold == null)
                        {
                            this.typeGold = new style.canvas.Base("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.gold.js");
                            this.typeGold.SetCurrent(this.typeGold.template.current + ".equiptab." + (item.GetEquipmentBase().detail * 10 + (this.isRingBelow ? 1 : 0)));
                            this.typeGold.SetParent(this.current);
                        }

                        this.typeGold.current.SetActive(true);
                        break;
                }

                if (this.seriesRef != null)
                {
                    this.seriesRef.Apply(item);
                }
            }
*/
        }
        public class Callback
        {
            public System.Action<Cell> requestUnequip;
            public System.Action<Cell, int, string> onCellClick;
        }


/*
        private readonly EquipTab.Cell mask;
        private readonly EquipTab.Cell amulet;
        private readonly EquipTab.Cell ringAbove;
        private readonly EquipTab.Cell ringBelow;
        private readonly EquipTab.Cell pendant;

        private readonly EquipTab.Cell helm;
        private readonly EquipTab.Cell armor;
        private readonly EquipTab.Cell belt;
        private readonly EquipTab.Cell horse;

        private readonly EquipTab.Cell cuff;
        private readonly EquipTab.Cell weapon;
        private readonly EquipTab.Cell boot;

        private readonly style.canvas.Base magicRequiredSeries;

        private Dictionary<int, EquipTab.Cell> itemDetailMapping;
        private Dictionary<int, EquipTab.Cell[]> itemDetailMagicRequired; // key: detail * 10 + this.isRingBelow

        private readonly userInterface.panelEquipment.SeriesTab seriesTab;
        public EquipTab.Callback callback;

        private int hiddenMagicActiveByGoldItemSet = 0;
        private List<Dictionary<int, string>> goldItemSetCompleted;

        public EquipTab(userInterface.panelEquipment.SeriesTab seriesTab) : base("\\user.interface\\panel.equipment\\equip.tab\\equip.tab.js")
        {
            this.seriesTab = seriesTab;

            this.mask = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.mask.js", null);
            this.amulet = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.amulet.js", this.seriesTab.amulet);
            this.ringAbove = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.ring.above.js", this.seriesTab.ringAbove);
            this.ringBelow = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.ring.below.js", this.seriesTab.ringBelow, isRingBelow: true);
            this.pendant = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.pendant.js", this.seriesTab.pendant);

            this.helm = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.helm.js", this.seriesTab.helm);
            this.armor = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.armor.js", this.seriesTab.armor);
            this.belt = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.belt.js", this.seriesTab.belt);
            this.horse = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.horse.js", null);

            this.cuff = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.cuff.js", this.seriesTab.cuff);
            this.weapon = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.weapon.js", this.seriesTab.weapon);
            this.boot = new EquipTab.Cell("\\user.interface\\panel.equipment\\equip.tab\\framed.boot.js", this.seriesTab.boot);

            this.magicRequiredSeries = new style.canvas.Base("\\user.interface\\panel.equipment\\equip.tab\\magic.required.series.js");

            this.itemDetailMapping = new Dictionary<int, EquipTab.Cell>
            {
                {(int)resource.settings.item.Defination.Detail.equip_meleeweapon, this.weapon},
                {(int)resource.settings.item.Defination.Detail.equip_rangeweapon, this.weapon},
                {(int)resource.settings.item.Defination.Detail.equip_armor, this.armor},
                {(int)resource.settings.item.Defination.Detail.equip_ring, this.ringAbove},
                {(int)resource.settings.item.Defination.Detail.equip_amulet, this.amulet},
                {(int)resource.settings.item.Defination.Detail.equip_boots, this.boot},
                {(int)resource.settings.item.Defination.Detail.equip_belt, this.belt},
                {(int)resource.settings.item.Defination.Detail.equip_helm, this.helm},
                {(int)resource.settings.item.Defination.Detail.equip_cuff, this.cuff},
                {(int)resource.settings.item.Defination.Detail.equip_pendant, this.pendant},
                {(int)resource.settings.item.Defination.Detail.equip_horse, this.horse},
                {11, this.mask},
            };

            this.itemDetailMagicRequired = new Dictionary<int, Cell[]>()
            {
                {(int)resource.settings.item.Defination.Detail.equip_meleeweapon * 10, new Cell[]{ this.amulet, this.armor } }, // vũ khí <= dây chuyền và trang phục
                {(int)resource.settings.item.Defination.Detail.equip_rangeweapon * 10, new Cell[]{ this.amulet, this.armor } }, // vũ khí <= dây chuyền và trang phục
                {(int)resource.settings.item.Defination.Detail.equip_ring * 10, new Cell[]{ this.weapon, this.helm } }, // nhẫn trên <= vũ khí và nón
                {(int)resource.settings.item.Defination.Detail.equip_cuff * 10, new Cell[]{ this.boot, this.ringAbove } }, // bao tay <= giày và nhẫn trên
                {(int)resource.settings.item.Defination.Detail.equip_ring * 10 + 1, new Cell[]{ this.cuff, this.pendant } }, // nhẫn dưới <= bao tay và ngọc bội
                {(int)resource.settings.item.Defination.Detail.equip_amulet * 10, new Cell[]{ this.ringBelow, this.belt } }, // dây chuyền <= nhẫn dưới và thắt lưng
                {(int)resource.settings.item.Defination.Detail.equip_armor * 10, new Cell[]{ this.ringBelow, this.belt } }, // áo <= nhẫn dưới và thắt lưng
                {(int)resource.settings.item.Defination.Detail.equip_helm * 10, new Cell[]{ this.armor, this.amulet } }, // nón <= trang phục và dây chuyền
                {(int)resource.settings.item.Defination.Detail.equip_boots * 10, new Cell[]{ this.helm, this.weapon } }, // giày <= nón và vũ khí
                {(int)resource.settings.item.Defination.Detail.equip_pendant * 10, new Cell[]{ this.boot, this.ringAbove } }, // ngọc bội <= giày và nhẫn trên
                {(int)resource.settings.item.Defination.Detail.equip_belt * 10, new Cell[]{ this.pendant, this.cuff } }, // thắt lưng <= ngọc bội và bao tay
            };

            this.callback = new EquipTab.Callback();

            foreach (KeyValuePair<int, EquipTab.Cell> cellEntry in this.itemDetailMapping)
            {
                Cell cell = cellEntry.Value;
                cell.callbacks.button = () =>
                {
                    this.OnCellClick(cell);
                };

                if (cell.seriesRef != null)
                {
                    cell.seriesRef.callbacks.button = () =>
                    {
                        this.OnCellClick(cell);
                    };
                }
            }

            this.ringBelow.callbacks.button = () => this.OnCellClick(this.ringBelow);
            this.ringBelow.seriesRef.callbacks.button = () => this.OnCellClick(this.ringBelow);

            this.goldItemSetCompleted = new List<Dictionary<int, string>>();
        }

        private void OnCellClick(EquipTab.Cell cell)
        {
            if (cell == null
                || cell.item == null)
            {
                return;
            }

            Dictionary<int, int> seriesRequired = new Dictionary<int, int>()
            {
                {0, 4 }, // kim <= thổ
                {4, 3 }, // thổ <= hỏa
                {3, 1 }, // hỏa <= mộc
                {1, 2 }, // mộc <= thủy
                {2, 0 }, // thủy <= kim
            };

            int detailKey = cell.item.GetEquipmentBase().detail * 10 + (cell.isRingBelow ? 1 : 0);
            int magicHiddenActiveCount = 0;
            string magicRequiredToActive = null;
            string[] magicRequiredName = new string[2];

            if (this.itemDetailMagicRequired.ContainsKey(detailKey))
            {
                int index = 0;

                foreach (EquipTab.Cell requiredItem in this.itemDetailMagicRequired[detailKey])
                {
                    if (requiredItem.item != null &&
                        requiredItem.item.GetSeries() == seriesRequired[cell.item.GetSeries()])
                    {
                        magicHiddenActiveCount++;
                    }

                    if (index < magicRequiredName.Length)
                    {
                        magicRequiredName[index] = requiredItem.template.text.content;
                    }

                    index++;
                }

                string[] jsLevel1 = this.magicRequiredSeries.template.text.content.Split("|||");
                string[] jsLevel2 = jsLevel1[1].Split("||");

                magicRequiredToActive = string.Format(jsLevel1[0], jsLevel2[seriesRequired[cell.item.GetSeries()]], magicRequiredName[0], magicRequiredName[1]);
            }

            if(this.hiddenMagicActiveByGoldItemSet > 0)
            {
                magicHiddenActiveCount = this.hiddenMagicActiveByGoldItemSet;
            }

            if (this.callback.onCellClick != null)
            {
                this.callback.onCellClick(cell, magicHiddenActiveCount, magicRequiredToActive);
            }
        }

        /// <summary>
        /// chuyển vật phẩm vào ô trang bị tương ứng
        /// return: nếu ô trang bị tương ứng đã có trang bị trước đó
        /// trả về item của ô đó.
        /// nếu không có trang bị trong ô, trả về null
        /// <summary>
        public resource.settings.Item EquipItem(resource.settings.Item item)
        {
            resource.settings.item.EquipmentBase equipmentBase = item.GetEquipmentBase();

            if (equipmentBase == null
                || equipmentBase.genre != (int)resource.settings.item.Defination.Genre.item_equip)
            {
                return null;
            }

            if (this.itemDetailMapping.ContainsKey(equipmentBase.detail) == false)
            {
                return null;
            }

            EquipTab.Cell cellEquip;
            resource.settings.Item result;

            // xử lý nhẫn
            // mặc định thêm vào nhẫn trên
            // nếu nhẫn trên đã có trang bị, đẩy nhẫn trên xuống nhẫn dưới
            // nếu nhẫn dưới đã có trang bị, trả về handle nhẫn dưới
            // nếu nhẫn dưới chưa có trang bị, trả về handle nhẫn trên
            if (equipmentBase.detail == (int)resource.settings.item.Defination.Detail.equip_ring)
            {
                if (this.ringAbove.item != null)
                {
                    if (this.ringBelow.item != null)
                    {
                        result = this.ringBelow.item;
                        cellEquip = this.ringAbove;

                        this.ringBelow.Apply(this.ringAbove.item);
                        this.ringAbove.Clear();
                    }
                    else
                    {
                        this.ringBelow.Apply(this.ringAbove.item);
                        this.ringAbove.Clear();

                        cellEquip = this.ringAbove;
                        result = null;
                    }
                }
                else
                {
                    cellEquip = this.ringAbove;
                    result = null;
                }
            }
            else
            {
                cellEquip = this.itemDetailMapping[equipmentBase.detail];
                result = cellEquip.item;
            }

            cellEquip.Apply(item);

            Dictionary<int, string> setItemList = item.GetSetItemList();

            if (item.GetItemType() == resource.settings.item.Defination.Type.goldEquip
                && setItemList != null)
            {   // xử lý bộ trang bị
                // nếu mặc đủ bộ trang bị bất kỳ
                // kích hoạt tất cả các thuộc tính ẩn của trang bị khác

                bool setIsCompleted = true;
                
                foreach(KeyValuePair<int, string> setEntry in setItemList)
                {
                    if(this.GoldItemHasEquipped(setEntry.Key) == false)
                    {
                        setIsCompleted = false;
                        break;
                    }
                }

                if (setIsCompleted == true)
                {
                    this.hiddenMagicActiveByGoldItemSet = 3;
                    this.goldItemSetCompleted.Add(setItemList);
                }
            }

            return result;
        }

        public void UnEquipCell(EquipTab.Cell cell)
        {
            if(cell.item.GetItemType() == resource.settings.item.Defination.Type.goldEquip
                && this.hiddenMagicActiveByGoldItemSet > 0)
            {
                int unequipRowIndex = cell.item.GetEquipmentBase().rowIndex;
                int unequipSetCompleteIndex = -1;

                for(int setIndex = 0; setIndex < this.goldItemSetCompleted.Count; setIndex++)
                {
                    foreach(KeyValuePair<int, string> itemEntry in this.goldItemSetCompleted[setIndex])
                    {
                        if(itemEntry.Key == unequipRowIndex)
                        {
                            unequipSetCompleteIndex = setIndex;
                            break;
                        }
                    }

                    if(unequipSetCompleteIndex >= 0)
                    {
                        break;
                    }
                }

                if(unequipSetCompleteIndex >= 0)
                {
                    this.goldItemSetCompleted.RemoveAt(unequipSetCompleteIndex);

                    if(this.goldItemSetCompleted.Count <= 0)
                    {
                        this.hiddenMagicActiveByGoldItemSet = 0;
                    }
                }
            }

            if (this.callback.requestUnequip != null)
            {
                this.callback.requestUnequip(cell);
            }
        }

        /// <summary>
        /// lấy thông số vật phẩm tương tự như vậy
        /// nếu đang được trang bị trên nhân vật
        /// nếu không có, trả về null
        /// </summary>
        public resource.settings.Item GetSimilarly(resource.settings.Item item)
        {
            resource.settings.item.EquipmentBase equipmentBase = item.GetEquipmentBase();

            if (equipmentBase == null)
            {
                return null;
            }

            if (this.itemDetailMapping.ContainsKey(equipmentBase.detail) == false)
            {
                return null;
            }

            return this.itemDetailMapping[equipmentBase.detail].item;
        }

        public bool GoldItemHasEquipped(int goldRowIndex)
        {
            foreach (KeyValuePair<int, EquipTab.Cell> cellEntry in this.itemDetailMapping)
            {
                if(cellEntry.Value != null && cellEntry.Value.item != null)
                {
                    if(cellEntry.Value.item.GetItemType() == resource.settings.item.Defination.Type.goldEquip
                        && cellEntry.Value.item.GetEquipmentBase().rowIndex == goldRowIndex)
                    {
                        return true;
                    }
                }
            }

            if (this.ringBelow != null && this.ringBelow.item != null)
            {
                if (this.ringBelow.item.GetItemType() == resource.settings.item.Defination.Type.goldEquip
                    && this.ringBelow.item.GetEquipmentBase().rowIndex == goldRowIndex)
                {
                    return true;
                }
            }

            return false;
        }
*/
    }
}




