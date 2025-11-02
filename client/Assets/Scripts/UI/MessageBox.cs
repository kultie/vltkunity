using UnityEngine;
using UnityEngine.UI;


namespace game.ui
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField]
        public Text Message;
        [SerializeField]
        public Text Title;

        public void OnClose()
        {
            Destroy(gameObject);
        }

        public void SetMessage(string mes)
        {
            Message.text = mes;
        }

        public void SetTitle(string title)
        {
        }
    }

}

