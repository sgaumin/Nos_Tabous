using UnityEngine;

public class AudioManager4 : MonoBehaviour
{
    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayStartDrivingSound(bool play)
    {
        if (play)
            audioSources[0].Play();

        if (!play)
            audioSources[0].Stop();
    }

    public void PlayAmbianceSound(bool play)
    {
        if (play)
            audioSources[1].Play();

        if (!play)
            audioSources[1].Stop();
    }

    public void PlayClosedDoorSound(bool play)
    {
        if (play)
            audioSources[2].Play();

        if (!play)
            audioSources[2].Stop();
    }
}
