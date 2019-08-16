using TMPro;
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
	private TextMeshPro textPro;
	private TextMeshProUGUI textProUGUI;

	private void Start()
	{
		text = GetComponent<Text>();
		textPro = GetComponent<TextMeshPro>();
		textProUGUI = GetComponent<TextMeshProUGUI>();

		UpdateTraduction();
	}

	public void UpdateTraduction()
	{
		switch (LanguageData.Language)
		{
			case Languages.French:
				if (text != null)
				{
					text.text = frenchContent;
				}
				else if (textPro != null)
				{
					textPro.text = frenchContent;
				}
				else if (textProUGUI != null)
				{
					textProUGUI.text = frenchContent;
				}
				break;
			case Languages.English:
				if (text != null)
				{
					text.text = englishContent;
				}
				else if (textPro != null)
				{
					textPro.text = englishContent;
				}
				else if (textProUGUI != null)
				{
					textProUGUI.text = englishContent;
				}
				break;
			case Languages.Japanese:
				if (text != null)
				{
					text.text = japaneseContent;
				}
				else if (textPro != null)
				{
					textPro.text = japaneseContent;
				}
				else if (textProUGUI != null)
				{
					textProUGUI.text = japaneseContent;
				}
				break;
		}
	}
}
