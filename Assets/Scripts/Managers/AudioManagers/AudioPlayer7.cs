using UnityEngine;

public class AudioPlayer7 : MonoBehaviour
{
	private AudioSource[] audioSources;

	private void Start() => audioSources = GetComponents<AudioSource>();

	public float StairsSoundLenght => audioSources[0].clip.length;

	public void PlayStairsSound(bool play)
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

	public void PlayOpenDoorSound(bool play)
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

	public void PlayOpenBoxSound(bool play)
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
