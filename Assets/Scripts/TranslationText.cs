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
	private TMP_Text textPro;

	private void Start()
	{
		text = GetComponent<Text>();
		textPro = GetComponent<TMP_Text>();

		UpdateTraduction();
	}

	public void UpdateTraduction()
	{
		switch (LanguageData.Language)
		{
			case Languages.French: SetText(frenchContent); break;
			case Languages.English: SetText(englishContent); break;
			case Languages.Japanese: SetText(japaneseContent); break;
		}
	}

	private void SetText(string content)
	{
		if (text != null)
		{
			text.text = content;
		}
		else if (textPro != null)
		{
			textPro.text = content;
		}
	}
}
