using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
	public static DialogueBox Instance { get; private set; }

	[Header("Debug")]
	[SerializeField] private bool useLanguageData;
	[SerializeField] private Languages language = Languages.French;

	[Header("Parameters")]
	private float durationBeforeShowText = 0.5f;
	private float fadDuration = 0.3f;

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

	protected void Start()
	{
		Array.ForEach(choices, x => x.gameObject.SetActive(false));
		gameObject.SetActive(false);
	}

	public IEnumerator ShowDialogueBox(bool show)
	{
		gameObject.SetActive(true);

		if (show)
		{
			yield return StartCoroutine(FadBackground(show));
			yield return StartCoroutine(ShowTexts(show));
		}
		else
		{
			yield return StartCoroutine(ShowTexts(show));
			yield return StartCoroutine(FadBackground(show));
		}
	}

	private IEnumerator FadBackground(bool fadIn)
	{
		Array.ForEach(backgrounds, x => x.DOFade(fadIn ? 1f : 0f, fadDuration));
		yield return new WaitForSeconds(fadDuration);
	}

	private IEnumerator ShowTexts(bool show)
	{
		sentence.gameObject.SetActive(show);
		name.gameObject.SetActive(show);
		Array.ForEach(choices, x => x.gameObject.SetActive(show));
		yield return new WaitForSeconds(fadDuration);
	}

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
