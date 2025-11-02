
using System.Collections.Generic;
using UnityEngine;

namespace game.scene.world.userInterface
{
    public class PanelUser : MonoBehaviour
    {
        //public style.canvas.Base closeButton;
        //public style.canvas.Base equipButton;
        //public style.canvas.Base propertiesButton;
        //public style.canvas.Base seriesButton;

        [SerializeField]
        PanelUserProperties propertiesTab;
        [SerializeField]
        PanelUserEquipment equipTab;
        [SerializeField]
        PanelUserItems itemTab;
        [SerializeField]
        PanelUserSeries seriesTab;

        void Start()
        {
/*
            new style.canvas.Base("\\user.interface\\panel.equipment\\background.color.js");
            new style.canvas.Base("\\user.interface\\panel.equipment\\background.image.js");
            this.closeButton = new style.canvas.Base("\\user.interface\\panel.equipment\\close.button.js");
            this.equipButton = new style.canvas.Base("\\user.interface\\panel.equipment\\equip.button.js");
            this.propertiesButton = new style.canvas.Base("\\user.interface\\panel.equipment\\properties.button.js");
            this.seriesButton = new style.canvas.Base("\\user.interface\\panel.equipment\\series.button.js");

            this.itemTab = new panelEquipment.ItemTab();
            this.seriesTab = new panelEquipment.SeriesTab();
            this.equipTab = new panelEquipment.PanelEquipmentTab(this.seriesTab);
            this.propertiesTab = new panelEquipment.PropertiesTab();
*/
            //this.SetupDefault();
            //this.SetupEvent();
        }
/*
        private void SetupDefault()
        {
            this.equipButton.button.SetSelectedImage();
            this.propertiesButton.button.SetNormalImage();
            this.seriesButton.button.SetNormalImage();

            this.equipTab.gameObject.SetActive(true);
            this.itemTab.current.SetActive(true);
            this.propertiesTab.current.SetActive(false);
            this.seriesTab.current.SetActive(false);
        }

        public void OpenProperties()
        {
            this.equipButton.button.SetNormalImage();
            this.equipTab.current.SetActive(false);
            this.propertiesButton.button.SetSelectedImage();
            this.propertiesTab.current.SetActive(true);
            this.seriesButton.button.SetNormalImage();
            this.seriesTab.current.SetActive(false);
            this.itemTab.ShowSeries(false);
        }

        private void SetupEvent()
        {
            this.closeButton.callbacks.button = () => this.current.SetActive(false);

            this.equipButton.callbacks.button = () =>
            {
                this.equipButton.button.SetSelectedImage();
                this.equipTab.current.SetActive(true);
                this.propertiesButton.button.SetNormalImage();
                this.propertiesTab.current.SetActive(false);
                this.seriesButton.button.SetNormalImage();
                this.seriesTab.current.SetActive(false);
                this.itemTab.ShowSeries(false);
            };

            this.propertiesButton.callbacks.button = () =>
            {
                this.equipButton.button.SetNormalImage();
                this.equipTab.current.SetActive(false);
                this.propertiesButton.button.SetSelectedImage();
                this.propertiesTab.current.SetActive(true);
                this.seriesButton.button.SetNormalImage();
                this.seriesTab.current.SetActive(false);
                this.itemTab.ShowSeries(false);
            };

            this.seriesButton.callbacks.button = () =>
            {
                this.equipButton.button.SetNormalImage();
                this.equipTab.current.SetActive(false);
                this.propertiesButton.button.SetNormalImage();
                this.propertiesTab.current.SetActive(false);
                this.seriesButton.button.SetSelectedImage();
                this.seriesTab.current.SetActive(true);
                this.itemTab.ShowSeries(true);
            };
        }
*/
        //public readonly UnityEngine.GameObject parentGameObject;
        //public readonly UnityEngine.GameObject backgroundColor;
        //public readonly UnityEngine.GameObject backgroundImage;
        //public readonly UnityEngine.GameObject closeButtonObject;
        //public readonly UnityEngine.UI.Button closeButtonComponent;

        //////////////////////////////////////////////////////////////////////////////////

        //public readonly userInterface.panelEquipment.Items items;
        //public readonly userInterface.panelEquipment.Equipped equipped;
        //public readonly userInterface.panelEquipment.Infor infor;

        //public PanelEquipment()
        //{
        //    this.parentGameObject = UnityEngine.GameObject.Find("panel.equipment");
        //    this.backgroundColor = this.parentGameObject.transform.Find("background.color").gameObject;
        //    this.backgroundImage = this.parentGameObject.transform.Find("background.image").gameObject;
        //    this.closeButtonObject = this.parentGameObject.transform.Find("close.button").gameObject;
        //    this.closeButtonComponent = this.closeButtonObject.GetComponent<UnityEngine.UI.Button>();

        //    Game.Style.Canvas.Gridable(0, 39, 0, 19).Apply(this.parentGameObject);
        //    Game.Style.Canvas.Base(this.parentGameObject).Gridable(0, 39, 0, 19).Apply(this.backgroundColor);
        //    Game.Style.Canvas.Base(this.parentGameObject).Gridable(4, 35, 1, 17).Apply(this.backgroundImage);
        //    Game.Style.Canvas.Base(this.parentGameObject).Gridable(35, 35.5f, 2.2f, 2.7f).Apply(this.closeButtonObject);

        //    ////////////////////////////////////////////////////////////////////////////////

        //    this.items = new userInterface.panelEquipment.Items(this.parentGameObject);
        //    this.equipped = new userInterface.panelEquipment.Equipped(this.parentGameObject);
        //    this.infor = new userInterface.panelEquipment.Infor(this.parentGameObject);

        //    ////////////////////////////////////////////////////////////////////////////////

        //    this.closeButtonComponent.onClick.AddListener(() => this.parentGameObject.SetActive(false));
        //    this.parentGameObject.SetActive(false);
        //}
    }
}
