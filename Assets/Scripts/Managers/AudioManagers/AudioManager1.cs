using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPhoneSound(bool play)
    {
        if (play)
            audioSource.Play();

        if (!play)
            audioSource.Stop();
    }
}
