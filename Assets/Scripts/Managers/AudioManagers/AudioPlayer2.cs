using UnityEngine;

public class AudioPlayer2 : MonoBehaviour
{
	private AudioSource[] audioSources;

	private void Start() => audioSources = GetComponents<AudioSource>();

	public void PlayHeelShoesSound(bool play)
	{
		if (play)
		{
			audioSources[0].Play();
		}
		else
		{
			audioSources[0].Stop();
		}
	}

	public void PlayClosedDoorSound(bool play)
	{
		if (play)
		{
			audioSources[1].Play();
		}
		else
		{
			audioSources[1].Stop();
		}
	}
}
