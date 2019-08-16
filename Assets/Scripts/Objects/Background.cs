using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Background : MonoBehaviour
{
	[SerializeField] private float fadDuration = 1f;

	private SpriteRenderer sprite;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.DOFade(0f, 0f);
	}

	public IEnumerator FadIn()
	{
		Tween fad = sprite.DOFade(1f, 1f);
		yield return fad.WaitForCompletion(true);
	}

	public IEnumerator FadOut()
	{
		Tween fad = sprite.DOFade(0f, 1f);
		yield return fad.WaitForCompletion(true);
	}
}
