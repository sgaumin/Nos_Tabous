﻿using UnityEngine;

public class AudioPlayer3 : MonoBehaviour
{
	private AudioSource audioSource;

	private void Start() => audioSource = GetComponent<AudioSource>();

	public void PlayPhoneSound(bool play)
	{
		if (play)
		{
			audioSource.Play();
		}
		else
		{
			audioSource.Stop();
		}
	}
}
