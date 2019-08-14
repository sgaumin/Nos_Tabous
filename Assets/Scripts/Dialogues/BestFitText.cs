using UnityEngine;
using UnityEngine.UI;

public class BestFitText : MonoBehaviour
{
	public static int BestFitFrontSize { get; private set; }

	private void Start() => BestFitFrontSize = GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;
}
