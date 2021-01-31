using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectsSource;
    public AudioSource musicSource;
    public AudioSource musicSourceSecond;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    public static SoundManager Instance = null;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);   
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip)
    {
        effectsSource.clip = clip;
        effectsSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayMusicSecond(AudioClip clip)
    {
        musicSourceSecond.clip = clip;
        musicSourceSecond.Play();
    }
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        effectsSource.pitch = randomPitch;
        effectsSource.clip = clips[randomIndex];
        effectsSource.Play();
    }
}
