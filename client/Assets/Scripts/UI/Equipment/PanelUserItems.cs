
using System.Collections.Generic;
using UnityEngine;

namespace game.scene.world.userInterface
{
    public class PanelUserItems : MonoBehaviour
    {
        public class Cell : MonoBehaviour
        {
            public class Framed
            {
                public UnityEngine.GameObject goCurrent;
                public UnityEngine.RectTransform compRect;
                public UnityEngine.UI.Image compImage;
                public UnityEngine.UI.Button compButton;
            }

            public Cell.Framed framed;
            public Cell.Framed thumbnail = null;
            public Cell.Framed typeNormalEquip = null;
            //public style.canvas.Base typeGoldEquip = null;
            //public style.canvas.Base series = null;
            //public style.canvas.Base stack = null;

            public readonly int cellIndex;
            public bool hasUsed = false;
            public resource.settings.Item item = null;

            public Cell(int cellIndex)
            {
                this.cellIndex = cellIndex;
                this.framed = new Cell.Framed();
            }
        }

        public class Callback
        {
            public System.Action<Cell> onCellClick;
        }
/*


        ////////////////////////////////////////////////////////////////////////////////

        private readonly UnityEngine.Sprite itemFramedSprite;

        private readonly float framedDimension;
        private readonly float framedMargin;
        private readonly int itemsInLine = 5;

        private bool showSeries = false;
        private List<panelEquipment.PanelUserItems.Cell> cells;
        public panelEquipment.PanelUserItems.Callback itemTabCallback;

        ////////////////////////////////////////////////////////////////////////////////

        public PanelUserItems() : base("\\user.interface\\panel.equipment\\item.tab\\item.tab.js")
        {
            this.itemFramedSprite = this.GetSprite("item.framed.png");

            this.framedDimension = this.components.rectTransform.sizeDelta.x / this.itemsInLine;
            this.framedMargin = this.framedDimension / 2;

            this.cells = new List<PanelUserItems.Cell>();
            this.itemTabCallback = new PanelUserItems.Callback();

            this.InitializeEmptyFramed(this.components.rectTransform.sizeDelta);
        }

        private void InitializeEmptyFramed(UnityEngine.Vector2 parentSize)
        {
            int itemInColumn = (int)(parentSize.y / this.framedDimension);
            itemInColumn += 1;

            for (int index = 0; index < itemInColumn; index++)
            {
                this.CreateNewEmptyLine();
            }
        }

        private void CreateNewEmptyCell(int cellInLineIndex, int rowIndex, int cellIndex)
        {
            PanelUserItems.Cell item = new PanelUserItems.Cell(cellIndex);
            item.framed.goCurrent = new UnityEngine.GameObject(this.GetCurrentObjectName() + ".cell." + cellIndex);
            item.framed.compRect = item.framed.goCurrent.AddComponent<UnityEngine.RectTransform>();
            item.framed.compRect.sizeDelta = new UnityEngine.Vector2(this.framedDimension, this.framedDimension);
            item.framed.compImage = item.framed.goCurrent.AddComponent<UnityEngine.UI.Image>();
            item.framed.compImage.sprite = this.itemFramedSprite;
            item.framed.compButton = item.framed.goCurrent.AddComponent<UnityEngine.UI.Button>();
            item.framed.compButton.onClick.AddListener(() => this.OnCellClick(cellIndex));
            item.framed.goCurrent.transform.SetParent(this.scrollView.goContent.transform, false);

            style.Canvas.ApplyAnchor(item.framed.compRect);

            item.framed.compRect.anchoredPosition = new UnityEngine.Vector2(
                cellInLineIndex * this.framedDimension + this.framedMargin,
                rowIndex * -this.framedDimension - this.framedMargin);

            this.cells.Add(item);
        }

        private void CreateNewEmptyLine()
        {
            int rowIndex = this.cells.Count / itemsInLine;
            int cellIndex = this.cells.Count;

            for (int lineIndex = 0; lineIndex < this.itemsInLine; lineIndex++)
            {
                this.CreateNewEmptyCell(lineIndex, rowIndex, cellIndex);
                cellIndex++;
            }

            UnityEngine.RectTransform compRectScroll = this.scrollView.goScroll.GetComponent<UnityEngine.RectTransform>();
            compRectScroll.sizeDelta = new UnityEngine.Vector2(compRectScroll.sizeDelta.x, (rowIndex + 1) * this.framedDimension);
        }

        private void OnCellClick(int cellIndex)
        {
            if (this.cells[cellIndex].hasUsed == false)
            {
                return;
            }

            if (this.itemTabCallback.onCellClick == null)
            {
                return;
            }

            this.itemTabCallback.onCellClick(this.cells[cellIndex]);
        }

        private void ProcessSeries(PanelUserItems.Cell cell, bool show)
        {
            if (show == false)
            {
                if (cell.series != null)
                {
                    cell.series.Destroy();
                    cell.series = null;
                }

                return;
            }

            if (cell.series != null)
            {
                if (show)
                {
                    cell.series.current.SetActive(true);
                }
                else
                {
                    cell.series.current.SetActive(false);
                }

                return;
            }

            cell.series = new style.canvas.Base("\\user.interface\\panel.equipment\\item.tab\\series.js");
            cell.series.SetCurrent(cell.series.template.current + "." + cell.cellIndex);
            cell.series.SetParent(cell.framed.goCurrent);

            string[] seriesArray = cell.series.template.text.content.Split("||");

            if (cell.item.GetEquipmentBase() == null
                || cell.item.GetSeries() < 0
                || cell.item.GetSeries() >= seriesArray.Length)
            {
                cell.series.components.text.text = null;
            }
            else
            {
                cell.series.components.text.text = seriesArray[cell.item.GetSeries()];
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        public PanelUserItems.Cell GetCell(int cellIndex)
        {
            if (cellIndex >= this.cells.Count)
            {
                return null;
            }
            return this.cells[cellIndex];
        }

        public PanelUserItems.Cell FindUnusedCell()
        {
            foreach (panelEquipment.PanelUserItems.Cell itemEntry in this.cells)
            {
                if (itemEntry.hasUsed == false)
                {
                    return itemEntry;
                }
            }
            return null;
        }

        public PanelUserItems.Cell FindItemByDatabaseId(uint itemDatabaseId)
        {
            foreach (panelEquipment.PanelUserItems.Cell itemEntry in this.cells)
            {
                if (itemEntry.hasUsed == false)
                {
                    continue;
                }

                if (itemEntry.item == null)
                {
                    continue;
                }

                if (itemEntry.item.GetDatabaseId() == itemDatabaseId)
                {
                    return itemEntry;
                }
            }
            return null;
        }

        private void AddItem_Thumbnail(resource.settings.Item item, PanelUserItems.Cell cell)
        {
            if (cell.thumbnail == null)
            {
                cell.thumbnail = new Cell.Framed();
                cell.thumbnail.goCurrent = new UnityEngine.GameObject("thumbnail");
                cell.thumbnail.compRect = this.GetComponentFromParent<UnityEngine.RectTransform>(cell.thumbnail.goCurrent);
                cell.thumbnail.compImage = this.GetComponentFromParent<UnityEngine.UI.Image>(cell.thumbnail.goCurrent);

                cell.thumbnail.goCurrent.transform.SetParent(cell.framed.goCurrent.transform, false);
                cell.thumbnail.compImage.preserveAspect = true;
            }

            resource.settings.item.Getter.GridablePos thumbnailGridable = cell.item.GetThumbnailGridable();

            Game.Style.Canvas
                    .Base(cell.framed.goCurrent)
                    .Gridable(thumbnailGridable.cB, thumbnailGridable.cE, thumbnailGridable.rB, thumbnailGridable.rE)
                    .Apply(cell.thumbnail.goCurrent);

            cell.thumbnail.compImage.sprite = cell.item.GetThumbnailSprite();
        }

        private void AddItem_TypeFramed(resource.settings.Item item, PanelUserItems.Cell cell)
        {
            if (cell.typeNormalEquip != null) cell.typeNormalEquip.goCurrent.SetActive(false);
            if (cell.typeGoldEquip != null) cell.typeGoldEquip.current.SetActive(false);

            resource.settings.item.Getter.GridablePos typeGp = cell.item.GetTypeGridable();

            int framedType = 0; // 0: normal, 1: gold

            if (item.GetItemType() == resource.settings.item.Defination.Type.goldEquip)
            {
                framedType = 1;
            }

            switch (framedType)
            {
                case 0:
                    UnityEngine.Sprite typeFramedSprite = cell.item.GetTypeSprite();

                    if (typeFramedSprite != null)
                    {
                        if (cell.typeNormalEquip == null)
                        {
                            cell.typeNormalEquip = new Cell.Framed();
                            cell.typeNormalEquip.goCurrent = new UnityEngine.GameObject("typeNormalEquip");
                            cell.typeNormalEquip.compRect = this.GetComponentFromParent<UnityEngine.RectTransform>(cell.typeNormalEquip.goCurrent);
                            cell.typeNormalEquip.compImage = this.GetComponentFromParent<UnityEngine.UI.Image>(cell.typeNormalEquip.goCurrent);
                            cell.typeNormalEquip.goCurrent.transform.SetParent(cell.framed.goCurrent.transform, false);
                        }

                        cell.typeNormalEquip.goCurrent.SetActive(true);

                        Game.Style.Canvas
                            .Base(cell.framed.goCurrent)
                            .Gridable(typeGp.cB, typeGp.cE, typeGp.rB, typeGp.rE)
                            .Apply(cell.typeNormalEquip.goCurrent);

                        cell.typeNormalEquip.compImage.sprite = typeFramedSprite;
                    }
                    break;

                case 1:
                    if (cell.typeGoldEquip == null)
                    {
                        cell.typeGoldEquip = new style.canvas.Base("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.gold.js");
                        cell.typeGoldEquip.SetCurrent(cell.typeGoldEquip.template.current + "." + cell.cellIndex);
                        cell.typeGoldEquip.SetParent(cell.framed.goCurrent);
                    }

                    cell.typeGoldEquip.current.SetActive(true);
                    break;
            }
        }

        public void AddItem_Series(resource.settings.Item item, PanelUserItems.Cell cell)
        {
            if (this.showSeries == true)
            {
                this.ProcessSeries(cell, true);
            }
        }

        public void AddItem_Stack(resource.settings.Item item, PanelUserItems.Cell cell)
        {
            if (item.GetGenre() != 6
                || item.GetMagicScriptBase().stackValue <= 0)
            {
                if (cell.stack != null
                    && cell.stack.current.activeInHierarchy == true)
                {
                    cell.stack.current.SetActive(false);
                }

                return;
            }

            if (cell.stack == null)
            {
                cell.stack = new style.canvas.Base("\\user.interface\\panel.equipment\\item.tab\\stack.js");
                cell.stack.SetCurrent(cell.stack.template.current + "." + cell.cellIndex);
                cell.stack.SetParent(cell.framed.goCurrent);
            }

            cell.stack.components.text.text = item.GetStackCurrently().ToString();
        }

        /// <summary>
        /// thêm vật phẩm vào ô chỉ định trong hành trang
        /// </summary>
        public void AddItem(resource.settings.Item item, PanelUserItems.Cell cell)
        {
            if (item == null || cell == null)
            {
                return;
            }

            cell.hasUsed = true;
            cell.item = item;

            this.AddItem_Thumbnail(item, cell);
            this.AddItem_TypeFramed(item, cell);
            this.AddItem_Series(item, cell);
            this.AddItem_Stack(item, cell);
        }

        /// <summary>
        /// thêm vật phẩm vào ô chỉ định trong hành trang bởi ô index
        /// </summary>
        public void AddItem(resource.settings.Item item, int cellIndex)
        {
            this.AddItem(item, this.GetCell(cellIndex));
        }

        /// <summary>
        /// thêm vật phẩm vào ô trống bất kỳ trong rương hành trang
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(resource.settings.Item item)
        {
            PanelUserItems.Cell cell = this.FindItemByDatabaseId(item.GetDatabaseId());

            if(cell != null)
            {
                cell.item = item;

                this.ClearCell(cell.cellIndex);
                this.AddItem(item, cell);

                return;
            }

            while ((cell = this.FindUnusedCell()) == null)
            {
                this.CreateNewEmptyLine();
            }

            this.AddItem(item, cell);
        }

        public resource.settings.Item ClearCell(int cellIndex)
        {
            PanelUserItems.Cell cell = this.GetCell(cellIndex);

            if (cell == null || cell.hasUsed == false)
            {
                return null;
            }

            resource.settings.Item result = cell.item;

            if (cell.thumbnail != null)
            {
                UnityEngine.GameObject.Destroy(cell.thumbnail.goCurrent);
                cell.thumbnail = null;
            }

            if (cell.typeNormalEquip != null)
            {
                UnityEngine.GameObject.Destroy(cell.typeNormalEquip.goCurrent);
                cell.typeNormalEquip = null;
            }

            if (cell.typeGoldEquip != null)
            {
                cell.typeGoldEquip.Destroy();
                cell.typeGoldEquip = null;
            }

            if (cell.series != null)
            {
                cell.series.Destroy();
                cell.series = null;
            }

            if (cell.stack != null)
            {
                cell.stack.Destroy();
                cell.stack = null;
            }

            cell.item = null;
            cell.hasUsed = false;

            return result;
        }

        public void ShowSeries(bool showoff)
        {
            if (this.showSeries == showoff)
            {
                return;
            }

            this.showSeries = showoff;

            foreach (panelEquipment.PanelUserItems.Cell cellEntry in this.cells)
            {
                if (cellEntry.item != null
                    && cellEntry.item.IsEquipment())
                {
                    this.ProcessSeries(cellEntry, showoff);
                }
            }
        }

        public bool HaveGoldItem(int rowIndex)
        {
            foreach (PanelUserItems.Cell cellEntry in this.cells)
            {
                if (cellEntry != null
                    && cellEntry.item != null)
                {
                    if (cellEntry.item.GetItemType() == resource.settings.item.Defination.Type.goldEquip
                        && cellEntry.item.GetEquipmentBase().rowIndex == rowIndex)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
*/
    }
}
