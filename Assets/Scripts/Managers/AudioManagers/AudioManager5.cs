using UnityEngine;

public class AudioManager5 : MonoBehaviour
{
    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayWindSound(bool play)
    {
        if (play)
            audioSources[0].Play();

        if (!play)
            audioSources[0].Stop();
    }

    public void PlayFootSound(bool play)
    {
        if (play)
            audioSources[1].Play();

        if (!play)
            audioSources[1].Stop();
    }

    public void PlayKnockSound(bool play)
    {
        if (play)
            audioSources[2].Play();

        if (!play)
            audioSources[2].Stop();
    }
}
