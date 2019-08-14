using DG.Tweening;
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

	public void FadIn() => sprite.DOFade(1f, 1f);

	public void FadOut() => sprite.DOFade(0f, 1f);
}
