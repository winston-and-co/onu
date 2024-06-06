using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region SOURCES
    public AudioSource mainSource;
    public AudioSource backgroundSource;
    #endregion

    #region BACKGROUND
    public AudioClip backgroundMap;
    #endregion

    #region SOUND_EFFECTS
    public AudioClip enterNode;
    public AudioClip enterBossNode;
    public AudioClip drawCard;
    public AudioClip shuffleDeck;
    #endregion

    static SoundManager Instance;
    public static SoundManager GetInstance() => Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
