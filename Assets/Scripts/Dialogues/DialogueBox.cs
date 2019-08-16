using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
	public static DialogueBox Instance { get; private set; }

	[Header("Debug")]
	[SerializeField] private bool useLanguageData;
	[SerializeField] private Languages language = Languages.French;

	[Header("Dialogues")]
	[SerializeField] private OneDialogueElementList french;
	[SerializeField] private OneDialogueElementList english;
	[SerializeField] private OneDialogueElementList japanese;

	[Header("Texts")]
	[SerializeField] private DialogueSystemScript sentence;
	[SerializeField] private TextNameScript name;
	[SerializeField] private TextChoixScript[] choices;

	[Header("Backgrounds")]
	[SerializeField] private Image[] backgrounds;

	private void Awake()
	{
		Instance = this;
		SetDialogueFile();
	}

	public void ShowTexts(bool show)
	{
		sentence.gameObject.SetActive(show);
		name.gameObject.SetActive(show);
		Array.ForEach(choices, x => x.gameObject.SetActive(show));
	}

	public void FadBackground(bool fadIn) => Array.ForEach(backgrounds, x => x.DOFade(fadIn ? 1f : 0f, 1f));

	private void SetDialogueFile()
	{
		OneDialogueElementList currentLanguage = null;
		if (useLanguageData)
		{
			switch (LanguageData.Language)
			{
				case Languages.English:
					currentLanguage = english;
					break;
				case Languages.French:
					currentLanguage = french;
					break;
				case Languages.Japanese:
					currentLanguage = japanese;
					break;
			}
		}
		else
		{
			switch (language)
			{
				case Languages.English:
					currentLanguage = english;
					break;
				case Languages.French:
					currentLanguage = french;
					break;
				case Languages.Japanese:
					currentLanguage = japanese;
					break;
			}
		}

		sentence.DialogueContent = currentLanguage ?? french;
		name.DialogueContent = currentLanguage ?? french;
		Array.ForEach(choices, x => x.DialogueContent = currentLanguage ?? french);
	}
}
