using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayHeelShoesSound(bool play)
    {
        if (play)
            audioSources[0].Play();

        if (!play)
            audioSources[0].Stop();
    }

    public void PlayClosedDoorSound(bool play)
    {
        if (play)
            audioSources[1].Play();

        if (!play)
            audioSources[1].Stop();
    }
}
