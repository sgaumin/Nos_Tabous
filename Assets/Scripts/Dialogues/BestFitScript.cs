using UnityEngine;
using UnityEngine.UI;

public class BestFitScript : MonoBehaviour
{
	public static int fontsize;

	private void Start() => GetComponent<Text>().fontSize = GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;

	void Update() => fontsize = gameObject.GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;

}
