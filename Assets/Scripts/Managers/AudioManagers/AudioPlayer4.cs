using UnityEngine;

public class AudioPlayer4 : MonoBehaviour
{
	private AudioSource[] audioSources;

	private void Start() => audioSources = GetComponents<AudioSource>();

	public void PlayStartDrivingSound(bool play)
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

	public void PlayAmbianceSound(bool play)
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

	public void PlayClosedDoorSound(bool play)
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
