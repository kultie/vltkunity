
using UnityEngine;

namespace game.scene.world.userInterface
{
    public class PanelUserSeries : MonoBehaviour
    {
/*
        public class Cell : style.canvas.Base
        {
            public class Framed
            {
                public UnityEngine.GameObject goCurrent;
                public UnityEngine.RectTransform compRect;
                public UnityEngine.UI.Image compImage;
            }

            public style.canvas.Base background;
            public SeriesTab.Cell.Framed thumbnail;
            public SeriesTab.Cell.Framed type;
            public style.canvas.Base typeGold = null;
            public style.canvas.Base series;

            public Cell(string stylePath) : base(stylePath) { }

            public void Apply(resource.settings.Item item)
            {
                if(this.background == null)
                {
                    this.background = new style.canvas.Base("\\user.interface\\panel.equipment\\series.tab\\framed.background.js");
                    this.background.SetParent(this.current);
                    this.background.SetCurrent(this.background.GetCurrentObjectName() + "." + this.GetCurrentFileName());
                }

                if (this.thumbnail == null)
                {
                    this.thumbnail = new SeriesTab.Cell.Framed();
                    this.thumbnail.goCurrent = new UnityEngine.GameObject("thumbnail");
                    this.thumbnail.compRect = this.thumbnail.goCurrent.AddComponent<UnityEngine.RectTransform>();
                    this.thumbnail.compImage = this.thumbnail.goCurrent.AddComponent<UnityEngine.UI.Image>();
                    this.thumbnail.compImage.preserveAspect = true;
                    this.thumbnail.goCurrent.transform.SetParent(this.current.transform, false);
                }

                this.thumbnail.compImage.sprite = item.GetThumbnailSprite();
                resource.settings.item.Getter.GridablePos gp = item.GetThumbnailGridable();
                Game.Style.Canvas
                    .Base(this.current)
                    .Gridable(gp.cB, gp.cE, gp.rB, gp.rE)
                    .Apply(this.thumbnail.compRect);

                if (this.type != null) this.type.goCurrent.SetActive(false);
                if (this.typeGold != null) this.typeGold.current.SetActive(false);

                switch(item.GetItemType())
                {
                    case resource.settings.item.Defination.Type.normalEquip:
                        if (this.type == null)
                        {
                            this.type = new Framed();
                            this.type.goCurrent = new UnityEngine.GameObject("type");
                            this.type.compRect = this.type.goCurrent.AddComponent<UnityEngine.RectTransform>();
                            this.type.compImage = this.type.goCurrent.AddComponent<UnityEngine.UI.Image>();
                            this.type.goCurrent.transform.SetParent(this.current.transform, false);
                        }

                        this.type.goCurrent.SetActive(true);
                        this.type.compImage.sprite = item.GetTypeSprite();
                        Game.Style.Canvas
                            .Base(this.current)
                            .Gridable(0, 39, 0, 19)
                            .Apply(this.type.compRect);
                        break;

                    case resource.settings.item.Defination.Type.goldEquip:
                        if (this.typeGold == null)
                        {
                            this.typeGold = new style.canvas.Base("\\user.interface\\panel.equipment\\item.tab\\item.framed.type.gold.js");
                            this.typeGold.SetCurrent(this.typeGold.template.current + ".seriestab." + System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
                            this.typeGold.SetParent(this.current);
                        }

                        this.typeGold.current.SetActive(true);
                        break;
                }

                if(this.series == null)
                {
                    this.series = new style.canvas.Base("\\user.interface\\panel.equipment\\series.tab\\series.js");
                    this.series.SetParent(this.current);
                    this.series.SetCurrent(this.series.GetCurrentObjectName() + "." + this.GetCurrentFileName());
                }

                string[] seriesElement = this.series.template.text.content.Split("||");

                if(item.GetSeries() >= 0 && item.GetSeries() < seriesElement.Length)
                {
                    this.series.components.text.text = seriesElement[item.GetSeries()];
                }
            }

            public void Clear()
            {
                if(this.background != null)
                {
                    this.background.Destroy();
                    this.background = null;
                }

                if(this.series != null)
                {
                    this.series.Destroy();
                    this.series = null;
                }

                if(this.thumbnail != null)
                {
                    UnityEngine.GameObject.Destroy(this.thumbnail.goCurrent);
                    this.thumbnail = null;
                }

                if(this.type != null)
                {
                    UnityEngine.GameObject.Destroy(this.type.goCurrent);
                    this.type = null;
                }

                if(this.typeGold != null)
                {
                    this.typeGold.Destroy();
                    this.typeGold = null;
                }
            }
        }

        public SeriesTab.Cell amulet;
        public SeriesTab.Cell armor;
        public SeriesTab.Cell belt;
        public SeriesTab.Cell boot;
        public SeriesTab.Cell cuff;
        public SeriesTab.Cell helm;
        public SeriesTab.Cell pendant;
        public SeriesTab.Cell ringAbove;
        public SeriesTab.Cell ringBelow;
        public SeriesTab.Cell weapon;

        public SeriesTab() : base("\\user.interface\\panel.equipment\\series.tab\\series.tab.js")
        {
            new style.canvas.Base("\\user.interface\\panel.equipment\\series.tab\\background.js");
            this.amulet = new("\\user.interface\\panel.equipment\\series.tab\\framed.amulet.js");
            this.armor = new("\\user.interface\\panel.equipment\\series.tab\\framed.armor.js");
            this.belt = new("\\user.interface\\panel.equipment\\series.tab\\framed.belt.js");
            this.boot = new("\\user.interface\\panel.equipment\\series.tab\\framed.boot.js");
            this.cuff = new("\\user.interface\\panel.equipment\\series.tab\\framed.cuff.js");
            this.helm = new("\\user.interface\\panel.equipment\\series.tab\\framed.helm.js");
            this.pendant = new("\\user.interface\\panel.equipment\\series.tab\\framed.pendant.js");
            this.ringAbove = new("\\user.interface\\panel.equipment\\series.tab\\framed.ring.above.js");
            this.ringBelow = new("\\user.interface\\panel.equipment\\series.tab\\framed.ring.below.js");
            this.weapon = new("\\user.interface\\panel.equipment\\series.tab\\framed.weapon.js");
        }
*/
    }
}
