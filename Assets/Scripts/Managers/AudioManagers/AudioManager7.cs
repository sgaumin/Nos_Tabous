using UnityEngine;

public class AudioManager7 : MonoBehaviour
{
    public float lenghtSound;

    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        lenghtSound = audioSources[0].clip.length;
    }

    public void PlayStairsSound(bool play)
    {
        if (play)
            audioSources[0].Play();

        if (!play)
            audioSources[0].Stop();
    }

    public void PlayOpenDoorSound(bool play)
    {
        if (play)
            audioSources[1].Play();

        if (!play)
            audioSources[1].Stop();
    }

    public void PlayOpenBoxSound(bool play)
    {
        if (play)
            audioSources[2].Play();

        if (!play)
            audioSources[2].Stop();
    }
}
