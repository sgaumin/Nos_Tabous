using UnityEngine;
using UnityEngine.UI;

public class BestFitScript : MonoBehaviour
{
	public static int fontsize;

	void Update() => fontsize = gameObject.GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;
}
