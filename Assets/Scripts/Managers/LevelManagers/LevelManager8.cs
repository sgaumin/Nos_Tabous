using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager8 : MonoBehaviour
{
	[SerializeField] private GrenierObjects henriLetter;
	[SerializeField] private GameObject commentsBox;
	[SerializeField] private GameObject lettersBox;
	[SerializeField] private Animator fadScreen;
	[SerializeField] private Button quitButton;

	private GrenierObjects[] grenierObjects;

	private int countObject;
	private int currentStep;


	// TO DO: BUG si on terminer par selectionner la lettre d'André
	void Start()
	{
		// Hide Henri's letter
		henriLetter.gameObject.SetActive(false);

		// Reteive all objects
		grenierObjects = FindObjectsOfType<GrenierObjects>();
		countObject = grenierObjects.Length;

		// Hide UI
		commentsBox.SetActive(false);
		lettersBox.SetActive(false);
		quitButton.gameObject.SetActive(false);

		// Starting level scripting
		StartCoroutine(StartStep());

		currentStep = 0;
	}

	void Update()
	{
		// If all objects is checked
		if (currentStep == 1)
			if (!isAllChecked())
			{
				return;
			}
			else
			{
				if (currentStep != 2)
				{
					// Show Henri's letter
					currentStep = 3;
					StartCoroutine(ShowHenriLetter());
					return;
				}
			}

		// If Andre's letter is checked
		if (currentStep == 2)
		{
			if (Input.anyKeyDown)
			{
				currentStep = 1;
				StartCoroutine(HideLetter());
			}
		}
	}

	IEnumerator StartStep()
	{
		yield return new WaitForSeconds(1f);
		commentsBox.SetActive(true);

		// Increase step
		currentStep++;

		// End of coroutine
		yield break;
	}

	IEnumerator ShowHenriLetter()
	{
		// Stop click interactions
		yield return new WaitForSeconds(0.2f);
		SetAllClickable(false);

		// Show Henri's letter
		yield return new WaitForSeconds(3f);
		henriLetter.gameObject.SetActive(true);

		// Hide dialogueBox
		commentsBox.SetActive(false);

		// End of coroutine
		yield break;
	}

	public IEnumerator ShowLetter(bool isHenriLetter)
	{
		// Hide dialogueBox
		commentsBox.SetActive(false);

		// Show lettersBox
		lettersBox.SetActive(true);

		// Stop click interactions
		yield return new WaitForSeconds(0.2f);
		henriLetter.GetComponent<GrenierObjects>().CanBeSelected = false;
		SetAllClickable(false);

		// Andre's Letter step
		if (!isHenriLetter)
			currentStep = 2;

		if (isHenriLetter)
		{
			// Henri's letter step
			currentStep = 4;

			// Show Quit Button
			yield return new WaitForSeconds(0.2f);
			quitButton.gameObject.SetActive(true);
		}

		// End of coroutine
		yield break;
	}

	public IEnumerator HideLetter()
	{
		// Hide dialogueBox
		commentsBox.SetActive(true);

		// Hide lettersBox
		lettersBox.SetActive(false);

		// Allow click interaction
		yield return new WaitForSeconds(0.2f);
		SetAllClickable(true);

		// End of coroutine
		yield break;
	}

	public void LaunchFinalSteps()
	{
		StartCoroutine(FinalStep());
	}

	IEnumerator FinalStep()
	{
		// Waiting time
		yield return new WaitForSeconds(1f);

		// Hide quit button
		quitButton.gameObject.SetActive(false);

		// Hide dialogue box
		commentsBox.SetActive(false);

		// Hide lettersBox
		lettersBox.SetActive(false);

		// fad Out animation
		fadScreen.SetTrigger("FadOut");
		yield return new WaitForSeconds(2f);

		// Load Next scene
		GameSystem.Instance.LoadNextScene();

		// End of coroutine
		yield break;
	}

	// Check if all objects is checked
	bool isAllChecked()
	{
		int compteur = 0;
		foreach (GrenierObjects grenierObject in grenierObjects)
		{
			if (grenierObject.IsChecked)
				compteur++;
		}

		if (compteur == countObject)
			return true;

		return false;
	}

	void SetAllClickable(bool value) => Array.ForEach(grenierObjects, x => x.CanBeSelected = value);
}
