using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clickClip;
    public AudioClip upgradeClip;
    public AudioClip errorClip;
    public AudioClip endingClip;

    public void PlayClick()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(clickClip, 0.6f);
    }

    public void PlayUpgrade()
    {
        source.pitch = 1f;
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
        source.PlayOneShot(endingClip);
    }
}