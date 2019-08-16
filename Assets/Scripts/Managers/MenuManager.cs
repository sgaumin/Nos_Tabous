using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private Dropdown dropdown;

	private TranslationText[] traductions;

	private void Start() => traductions = FindObjectsOfType<TranslationText>();

	private void Update()
	{
		if (Input.GetButtonDown("Grenier"))
		{
			GameSystem.Instance.LoadSceneByName("7- Grenier");
		}
	}

	public void UpdateLanguage(int value)
	{
		switch (dropdown.value)
		{
			case 0:
				LanguageData.Language = Languages.English;
				break;

			case 1:
				LanguageData.Language = Languages.French;
				break;

			case 2:
				LanguageData.Language = Languages.Japanese;
				break;
		}

		Array.ForEach(traductions, x => x.UpdateTraduction());
	}
}
