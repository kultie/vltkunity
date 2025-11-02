using System.Collections.Generic;
using UnityEngine;

public class MusicManagerGame : MonoBehaviour
{
    private static MusicManagerGame instance;

    [SerializeField]
    private AudioSource _MusicAudioSource;

    [SerializeField]
    public Dictionary<string, AudioSource> audioDictionary = new Dictionary<string, AudioSource>();

    public static MusicManagerGame Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new();
                instance = singletonObject.AddComponent<MusicManagerGame>();
                singletonObject.name = "MusicManagerGame (Singleton)";
                DontDestroyOnLoad(singletonObject);

            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        _MusicAudioSource.clip = ((AudioClip)Resources.Load("audio/WorldMapMusic/music/music001"));
        _MusicAudioSource.loop = true;
        _MusicAudioSource.Play();

        CreateAudioByName("Move");
        CreateAudioByName("Run");
        CreateAudioByName("PlayerAction");
    }

    void CreateAudioByName(string action)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 1f;
        audioSource.name = action;
        audioDictionary.Add(action, audioSource);
    }

    public void PlayMucsicMap(int mapID)
    {
        _MusicAudioSource.Stop();
        _MusicAudioSource.clip = UnityEngine.Resources.Load<UnityEngine.AudioClip>(game.resource.settings.Music.GetMapMusicResourceFile(mapID));
        _MusicAudioSource.loop = true;
        _MusicAudioSource.volume = 0.5f;
        _MusicAudioSource.Play();
    }

    public void PlayMusic(string sourceName)
    {
        _MusicAudioSource.Stop();
        _MusicAudioSource.clip = ((AudioClip)Resources.Load("audio/WorldMapMusic/music/" + sourceName));
        _MusicAudioSource.volume = 1f;
        _MusicAudioSource.Play();
    }

    public void StartFootStep(bool isHorse)
    {
        if (audioDictionary.TryGetValue("Move", out AudioSource audioSource))
        {
            if (!audioSource.isPlaying)
            {
                string name = isHorse ? "FootStep_Horse" : "FootStep";
                audioSource.loop = true;
                audioSource.clip = ((AudioClip)Resources.Load("audio/" + name)); ;

                audioSource.Play();
            }

        }
    }

    public void StartRun(bool isHorse)
    {
        if (audioDictionary.TryGetValue("Run", out AudioSource audioSource))
        {
            if (!audioSource.isPlaying)
            {
                string name = isHorse ? "Run" : "Run_Horse";
                audioSource.loop = true;
                audioSource.clip = ((AudioClip)Resources.Load("audio/" + name)); ;

                audioSource.Play();
            }

        }
    }

    public void StopFootStep()
    {
        if (audioDictionary.TryGetValue("Move", out AudioSource audioSource))
        {
            audioSource.Stop();
        }
    }

    public void Hit(bool isMale)
    {
        if (audioDictionary.TryGetValue("PlayerAction", out AudioSource audioSource))
        {


            string name = isMale ? "Vo_Male_L_a" : "Vo_FeMale_M_a";
            AudioClip clip = ((AudioClip)Resources.Load("audio/" + name)); ;
            audioSource.PlayOneShot(clip);
        }
    }

    public void Hurt()
    {
        if (audioDictionary.TryGetValue("PlayerAction", out AudioSource audioSource))
        {
            AudioClip clip = ((AudioClip)Resources.Load("audio/Hit")); ;
            audioSource.PlayOneShot(clip);
        }
    }

    public void Die(bool isMale)
    {
        if (audioDictionary.TryGetValue("PlayerAction", out AudioSource audioSource))
        {
            string name = isMale ? "Vo_Male_L_a" : "Vo_FeMale_M_a";
            AudioClip clip = ((AudioClip)Resources.Load("audio/" + name)); ;
            audioSource.PlayOneShot(clip);
        }
    }

}
