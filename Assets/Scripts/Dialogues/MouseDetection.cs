using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public static int WhoIsHighlighted;
	private bool MouseIsOverHere;
	private float timeCount;

	protected void Start() => MouseIsOverHere = true;

	public void OnPointerEnter(PointerEventData pointerEvent)
	{
		MouseIsOverHere = false;
		timeCount = 0;
	}

	public void OnPointerExit(PointerEventData pointerEvent) => MouseIsOverHere = true;

	void Update()
	{
		if (MouseIsOverHere)
		{
			timeCount += 5 * Time.deltaTime;
			if (timeCount > 6)
			{
				timeCount -= 6;
			}
			WhoIsHighlighted = (int)Mathf.Floor(timeCount);
		}
		else
		{
			WhoIsHighlighted = 0;
		}
	}
}
