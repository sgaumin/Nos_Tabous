using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerClic : MonoBehaviour
{
    // Singleton
    public static AudioManagerClic instance;
    private AudioSource son;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        son = GetComponent<AudioSource>();
    }

    public void PlayClicSound()
    {
        son.Play();
    }
}
