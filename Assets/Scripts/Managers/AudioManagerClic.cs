using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManagerClic : MonoBehaviour
{
	public static AudioManagerClic Instance { get; private set; }

	private AudioSource audioSource;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != null)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	protected void Start() => audioSource = GetComponent<AudioSource>();

	public void PlayClicSound() => audioSource.Play();
}
