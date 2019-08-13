using UnityEngine;

public class AudioPlayer5 : MonoBehaviour
{
	private AudioSource[] audioSources;

	private void Start() => audioSources = GetComponents<AudioSource>();

	public void PlayWindSound(bool play)
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

	public void PlayFootSound(bool play)
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

	public void PlayKnockSound(bool play)
	{
		if (play)
		{
			audioSources[2].Play();
		}
		else
		{
			audioSources[2].Stop();
		}
	}
}
