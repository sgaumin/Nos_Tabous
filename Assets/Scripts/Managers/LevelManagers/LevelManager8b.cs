using System.Collections;
using UnityEngine;

public class LevelManager8b : MonoBehaviour
{
	void Start() => StartCoroutine(StartStep());

	private IEnumerator StartStep()
	{
		yield return new WaitForSeconds(20f);
		GameSystem.Instance.LoadNextScene();
	}
}
