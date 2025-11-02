using game.config;
using game.network;
using game.ui;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace game.basemono
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        GameObject loadingBox;

        public void ShowMessageBox(string mes, string title)
        {
            var messageBox = UIHelpers.BringMessageBox();
            messageBox.SetMessage(mes);
            messageBox.SetTitle(LocalizationSettings.StringDatabase.GetLocalizedString(ConfigGame.tableLanguage, title));
        }

        public void ShowLoading()
        {
            if (loadingBox == null)
            {
                loadingBox = UIHelpers.BringPrefabToScene("LoadingBox");
            }
        }

        public void HideLoading()
        {
            Destroy(loadingBox);
        }
    }
}