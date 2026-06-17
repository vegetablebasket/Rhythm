using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource musicSource;

    public bool HasStarted { get; private set; }

    private void Awake()
    {
        if (musicSource == null)
        {
            musicSource = GetComponent<AudioSource>();
        }
    }

    public void PlayMusic()
    {
        if (musicSource == null)
        {
            Debug.LogWarning("AudioManager 缺少 AudioSource");
            return;
        }

        musicSource.Stop();
        musicSource.time = 0f;
        musicSource.Play();

        HasStarted = true;
    }

    public void PauseMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        HasStarted = false;
    }

    public float GetMusicTime()
    {
        if (musicSource == null)
        {
            return 0f;
        }

        return musicSource.time;
    }

    public float GetMusicLength()
    {
        if (musicSource == null || musicSource.clip == null)
        {
            return 0f;
        }

        return musicSource.clip.length;
    }

    public bool IsMusicPlaying()
    {
        return musicSource != null && musicSource.isPlaying;
    }

    public bool IsMusicFinished()
    {
        if (!HasStarted || musicSource == null || musicSource.clip == null)
        {
            return false;
        }

        return musicSource.time >= musicSource.clip.length - 0.05f;
    }
}