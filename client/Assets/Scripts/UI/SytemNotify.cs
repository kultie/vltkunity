using UnityEngine;
using UnityEngine.UI;

public class SytemNotify : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text txtMessage;

    public void SetMessage(string message)
    {
        txtMessage.text = message;
        gameObject.SetActive(true);
        Invoke(nameof(AutoDestroy), 3f);
    }

    void AutoDestroy()
    {
        gameObject.SetActive(false);
    }
}
