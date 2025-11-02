
using UnityEngine;

namespace game.scene.world.userInterface
{
    public class PanelUserProperties : MonoBehaviour
    {
/*
        private readonly style.canvas.Base highlight;
        private readonly style.canvas.Base name;
        private readonly style.canvas.Base id;
        private readonly style.canvas.Base verificationNo;
        private readonly style.canvas.Base verificationYes;
        private readonly style.canvas.Base title;
        private readonly style.canvas.Base exp;
        private readonly style.canvas.Base level;
        private readonly style.canvas.Base faction;
        private readonly style.canvas.Base factionIcon;
        private readonly style.canvas.Base rank;
        private readonly style.canvas.Base phucDuyen;
        private readonly style.canvas.Base pk;
        private readonly style.canvas.Base danhVong;
        private readonly style.canvas.Base health;
        private readonly style.canvas.Base mana;
        private readonly style.canvas.Base stamina;
        private readonly style.canvas.Base series;
        private readonly style.canvas.Base sucmanh;
        private readonly style.canvas.Base noicong;
        private readonly style.canvas.Base sinhkhi;
        private readonly style.canvas.Base thanphap;
        private readonly style.canvas.Base tiemnang;
        public readonly style.canvas.Base tiemnangAdd;


        public void SetHighLight(int value)
        {
            this.highlight.components.text.text = string.Format(this.highlight.template.text.content, value);
        }

        public void SetName(string value)
        {
            this.name.components.text.text = string.Format(this.name.template.text.content, value);
        }

        public void SetId(string value)
        {
            this.id.components.text.text = string.Format(this.id.template.text.content, value);
        }

        public void SetVerificationState(bool value)
        {
            if (value == true)
            {
                this.verificationNo.current.SetActive(false);
                this.verificationYes.current.SetActive(true);
                return;
            }

            this.verificationNo.current.SetActive(true);
            this.verificationYes.current.SetActive(false);
        }

        public void SetTitle(string value)
        {
            this.title.components.text.text = value != null
                ? value
                : this.title.template.text.content;
        }

        public void SetExp(string value)
        {
            this.exp.components.text.text = value;
        }

        public void SetFactionIcon(int factionId)
        {
            string[] iconArray = this.factionIcon.template.text.content.Split('|');

            if (factionId < 0
                || factionId >= iconArray.Length)
            {
                this.factionIcon.components.image.sprite = this.factionIcon.GetSprite(this.factionIcon.template.image.file);
                return;
            }

            this.factionIcon.components.image.sprite = this.factionIcon.GetSprite(iconArray[factionId]);
        }

        public void SetFaction(int factionId)
        {
            string[] factionSplited = this.faction.template.text.content.Split('|');

            this.faction.components.text.text = factionSplited[factionId + 1];
            this.SetFactionIcon(factionId);
        }

        public void SetLevel(int level)
        {
            this.level.components.text.text = level.ToString();
        }

        public void SetRank(int value)
        {
            this.rank.components.text.text = (value != 0) ? value.ToString() : this.rank.template.text.content;
        }

        public void SetPhucDuyen(int value)
        {
            this.phucDuyen.components.text.text = value.ToString();
        }

        public void SetPk(int value)
        {
            this.pk.components.text.text = value.ToString();
        }

        public void SetDanhVong(int value)
        {
            this.danhVong.components.text.text = value.ToString();
        }

        public void SetHealth(int value)
        {
            this.health.components.text.text = value.ToString();
        }

        public void SetMana(int value)
        {
            this.mana.components.text.text = value.ToString();
        }

        public void SetStamina(int value)
        {
            this.stamina.components.text.text = value.ToString();
        }

        public void SetSeries(int value)
        {
            string[] seriesSplited = this.series.template.text.content.Split('|');
            this.series.components.text.text = seriesSplited[value + 1];
        }

        public void SetTiemNang(int value)
        {
            this.tiemnang.components.text.text = value.ToString();
        }

        public void SetSucManh(int value)
        {
            this.sucmanh.components.text.text = value.ToString();
        }

        public void SetNoiCong(int value)
        {
            this.noicong.components.text.text = value.ToString();
        }

        public void SetSinhKhi(int value)
        {
            this.sinhkhi.components.text.text = value.ToString();
        }

        public void SetThanPhap(int value)
        {
            this.thanphap.components.text.text = value.ToString();
        }

        public PanelUserProperties() : base("\\user.interface\\panel.equipment\\properties.tab\\properties.tab.js")
        {
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\avata.framed.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\avata.image.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\background.child.tab.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\exp.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\exp.background.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\exp.label.js");
            this.exp = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\exp.value.js");
            this.factionIcon = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\faction.icon.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\faction.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\faction.label.js");
            this.level = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\level.js");
            this.faction = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\faction.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\health.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\health.label.js");
            this.health = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\health.value.js");
            this.highlight = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\highlight.js");
            this.id = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\id.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\phucduyen.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\phucduyen.label.js");
            this.phucDuyen = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\phucduyen.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\danhvong.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\danhvong.label.js");
            this.danhVong = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\danhvong.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\mana.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\mana.label.js");
            this.mana = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\mana.value.js");
            this.name = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\name.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\noicong.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\noicong.label.js");
            this.noicong = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\noicong.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\pk.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\pk.background.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\pk.label.js");
            this.pk = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\pk.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\rank.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\rank.background.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\rank.label.js");
            this.rank = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\rank.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\series.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\series.label.js");
            this.series = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\series.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\sinhkhi.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\sinhkhi.label.js");
            this.sinhkhi = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\sinhkhi.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\stamina.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\stamina.label.js");
            this.stamina = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\stamina.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\sucmanh.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\sucmanh.label.js");
            this.sucmanh = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\sucmanh.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\thanphap.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\thanphap.label.js");
            this.thanphap = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\thanphap.value.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\thuoctinhkhac.label.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\tiemnang.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\tiemnang.label.js");
            this.tiemnang = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\tiemnang.value.js");
            this.tiemnangAdd = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\tiemnang.add.button.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\title.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\title.background.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\title.label.js");
            this.title = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\title.value.js");
            this.verificationNo = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\verification.no.js");
            this.verificationYes = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.tab\\verification.yes.js");


            string[] factionSplited = this.faction.template.text.content.Split('|');
            this.faction.components.text.text = factionSplited[0];

            string[] seriesSplited = this.series.template.text.content.Split('|');
            this.series.components.text.text = seriesSplited[0];
        }
*/
    }
}
