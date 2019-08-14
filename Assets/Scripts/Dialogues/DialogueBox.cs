using System;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
	private enum Languages
	{
		French,
		English,
		Japanese
	}

	public static DialogueBox Instance { get; private set; }

	[SerializeField] private Languages language = Languages.French;

	[Header("Dialogues")]
	[SerializeField] private OneDialogueElementList french;
	[SerializeField] private OneDialogueElementList english;
	[SerializeField] private OneDialogueElementList japanese;

	[Header("Texts")]
	[SerializeField] private DialogueSystemScript sentence;
	[SerializeField] private TextNameScript name;
	[SerializeField] private TextChoixScript[] choices;

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

	private void SetDialogueFile()
	{
		OneDialogueElementList currentLanguage = null;
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

		sentence.DialogueContent = currentLanguage ?? french;
		name.DialogueContent = currentLanguage ?? french;
		Array.ForEach(choices, x => x.DialogueContent = currentLanguage ?? french);
	}
}
