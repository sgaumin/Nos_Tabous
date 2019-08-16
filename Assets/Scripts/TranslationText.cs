using UnityEngine;
using UnityEngine.UI;

public class TranslationText : MonoBehaviour
{
	[TextArea(3, 10)]
	public string frenchContent;

	[TextArea(3, 10)]
	public string englishContent;

	[TextArea(3, 10)]
	public string japaneseContent;

	private Text text;

	private void Start()
	{
		text = GetComponent<Text>();
		UpdateTraduction();
	}

	public void UpdateTraduction()
	{
		switch (LanguageData.Language)
		{
			case Languages.French:
				text.text = frenchContent;
				break;
			case Languages.English:
				text.text = englishContent;
				break;
			case Languages.Japanese:
				text.text = japaneseContent;
				break;
		}
	}
}
