using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager8 : MonoBehaviour
{
	private enum LevelState
	{
		Zero,
		First,
		Second,
		Third,
		Fourth
	}

	[SerializeField] private GrenierObjects henriLetter;
	[SerializeField] private GameObject commentsBox;
	[SerializeField] private GameObject lettersBox;
	[SerializeField] private Animator fadScreen;
	[SerializeField] private Button quitButton;

	private GrenierObjects[] grenierObjects;

	private LevelState currentStep = LevelState.First;

	// TO DO: BUG si on terminer par selectionner la lettre d'André
	protected void Start()
	{
		// Hide Henri's letter
		henriLetter.gameObject.SetActive(false);

		// Reteive all objects
		grenierObjects = FindObjectsOfType<GrenierObjects>();

		// Hide UI
		commentsBox.SetActive(false);
		lettersBox.SetActive(false);
		quitButton.gameObject.SetActive(false);

		// Starting level scripting
		StartCoroutine(StartStep());

		currentStep = LevelState.Zero;
	}

	protected void Update()
	{
		// If all objects is checked
		if (currentStep == LevelState.First)
			if (!IsAllObjectsChecked())
			{
				return;
			}
			else
			{
				if (currentStep != LevelState.Second)
				{
					// Show Henri's letter
					currentStep = LevelState.Third;
					StartCoroutine(ShowHenriLetter());
					return;
				}
			}

		// If Andre's letter is checked
		if (currentStep == LevelState.Second)
		{
			if (Input.anyKeyDown)
			{
				currentStep = LevelState.First;
				StartCoroutine(HideLetter());
			}
		}
	}

	private IEnumerator StartStep()
	{
		yield return new WaitForSeconds(1f);
		commentsBox.SetActive(true);
		currentStep = LevelState.First;
	}

	private IEnumerator ShowHenriLetter()
	{
		// Stop click interactions
		yield return new WaitForSeconds(0.2f);
		SetAllClickable(false);

		// Show Henri's letter
		yield return new WaitForSeconds(3f);
		henriLetter.gameObject.SetActive(true);

		// Hide dialogueBox
		commentsBox.SetActive(false);
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

		if (isHenriLetter)
		{
			// Henri's letter step
			currentStep = LevelState.Fourth;

			// Show Quit Button
			yield return new WaitForSeconds(0.2f);
			quitButton.gameObject.SetActive(true);
		}
		else
		{
			// Andre's Letter step
			currentStep = LevelState.Second;
		}
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
	}

	private void LaunchFinalSteps() => StartCoroutine(FinalStep());

	private IEnumerator FinalStep()
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
	}

	// Check if all objects is checked
	private bool IsAllObjectsChecked() => Array.TrueForAll(grenierObjects, x => x.IsChecked);

	private void SetAllClickable(bool value) => Array.ForEach(grenierObjects, x => x.CanBeSelected = value);
}
