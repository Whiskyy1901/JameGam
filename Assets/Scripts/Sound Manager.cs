using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource source;
    public AudioClip[] clickClips;
    public AudioClip upgradeClip;
    public AudioClip errorClip;
    public AudioClip endingClip;

    [Header("Music (optional)")]
    public AudioSource musicSource;
    public AudioClip musicClip;
    [Range(0f, 1f)] public float musicVolume = 0.4f;

    const string MuteKey = "jalg_muted";
    public bool Muted { get; private set; }

    void Start()
    {
        Muted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        if (musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
        ApplyMute();
    }

    public void PlayClick()
    {
        if (clickClips == null || clickClips.Length == 0) return;
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(clickClips[Random.Range(0, clickClips.Length)], 0.6f);
    }

    public void PlayUpgrade(int level)
    {
        source.pitch = Mathf.Min(1f + level * 0.05f, 2f);
        source.PlayOneShot(upgradeClip);
    }

    public void PlayError()
    {
        source.pitch = 1f;
        source.PlayOneShot(errorClip, 0.8f);
    }

    public void PlayEnding()
    {
        source.pitch = 1f;
        if (musicSource != null) musicSource.volume *= 0.3f;
        source.PlayOneShot(endingClip);
    }

    public void ToggleMute()
    {
        Muted = !Muted;
        PlayerPrefs.SetInt(MuteKey, Muted ? 1 : 0);
        ApplyMute();
    }

    void ApplyMute()
    {
        source.mute = Muted;
        if (musicSource != null)
        {
            musicSource.mute = Muted;
            musicSource.volume = musicVolume;
        }
    }
}