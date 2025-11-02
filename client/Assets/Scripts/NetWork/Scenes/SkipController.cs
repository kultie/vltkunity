using game.config;
using game.resource;
using game.resource.dataController;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipController : MonoBehaviour
{
    private void Start()
    {
    }
    public void dowload()
    {
        SceneManager.LoadScene(sceneName: ConfigGame.downloadScreen);
    }
}
