using System.Collections;
using System.Collections.Generic;
using game.network;
using UnityEngine;
using UnityEngine.UI;


namespace game.ui
{
    public class Reconnect : MonoBehaviour
    {
        [SerializeField]
        public Button btnReconnect;
        [SerializeField]
        public Button closeGame;
        [SerializeField]
        public Text txtError;
        [SerializeField]
        public GameObject Loading;

        // Start is called before the first frame update
        void Start()
        {
            btnReconnect.onClick.AddListener(() =>
            {
                Loading.SetActive(true);
                PhotonManager.Instance.ReConnect();
            });

            closeGame.onClick.AddListener(() =>
            {
                QuitGame();
            });
        }

        // Phương thức thoát game
        public void QuitGame()
        {
            Destroy(gameObject);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void SetError(string text)
        {
            txtError.text = text;
        }
    }
}

