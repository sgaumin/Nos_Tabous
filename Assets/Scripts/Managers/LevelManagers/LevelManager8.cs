using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager8 : MonoBehaviour
{
	private enum LevelState
	{
		CheckingObjects,
		CheckingAndreLetter,
		CheckingHenryLetter,
		Finishing
	}

	[SerializeField] private AtticObject henriLetter;
	[SerializeField] private GameObject commentsBox;
	[SerializeField] private GameObject lettersBox;
	[SerializeField] private Animator fadScreen;
	[SerializeField] private Button quitButton;

	private AtticObject[] grenierObjects;

	private LevelState currentStep = LevelState.CheckingObjects;

	protected void Start()
	{
		grenierObjects = FindObjectsOfType<AtticObject>();

		henriLetter.gameObject.SetActive(false);

		lettersBox.SetActive(false);
		quitButton.gameObject.SetActive(false);

		currentStep = LevelState.CheckingObjects;
		commentsBox.SetActive(true);
	}

	protected void Update()
	{
		if (currentStep == LevelState.CheckingObjects)
		{
			if (!IsAllObjectsChecked())
			{
				return;
			}
			else
			{
				currentStep = LevelState.CheckingHenryLetter;
				StartCoroutine(FindHenriLetter());
				return;
			}
		}
		else if (currentStep == LevelState.CheckingAndreLetter)
		{
			henriLetter.CanBeSelected = false;
			if (Input.anyKeyDown)
			{
				currentStep = LevelState.CheckingObjects;
				StartCoroutine(HideLetter());
				henriLetter.CanBeSelected = true;
			}
		}
	}

	private IEnumerator FindHenriLetter()
	{
		yield return StartCoroutine(SetAllClickable(false));

		yield return new WaitForSeconds(3f);
		henriLetter.gameObject.SetActive(true);
		commentsBox.SetActive(false);
	}

	public IEnumerator OpenAndreLetter()
	{
		yield return StartCoroutine(SetAllClickable(false));

		currentStep = LevelState.CheckingAndreLetter;
		commentsBox.SetActive(false);
		lettersBox.SetActive(true);
	}

	public IEnumerator OpenHenriLetter()
	{
		yield return StartCoroutine(SetAllClickable(false));

		currentStep = LevelState.Finishing;
		henriLetter.CanBeSelected = false;

		commentsBox.SetActive(false);
		lettersBox.SetActive(true);
		quitButton.gameObject.SetActive(true);
	}

	public IEnumerator HideLetter()
	{
		yield return StartCoroutine(SetAllClickable(true));

		commentsBox.SetActive(true);
		lettersBox.SetActive(false);
	}

	private void LaunchFinalSteps() => StartCoroutine(ExecuteFinalStep());

	private IEnumerator ExecuteFinalStep()
	{
		yield return new WaitForSeconds(1f);

		quitButton.gameObject.SetActive(false);
		commentsBox.SetActive(false);
		lettersBox.SetActive(false);

		// fad Out animation
		fadScreen.SetTrigger("FadOut");
		yield return new WaitForSeconds(2f);

		GameSystem.Instance.LoadNextScene();
	}

	private bool IsAllObjectsChecked() => Array.TrueForAll(grenierObjects, x => x.HasBeenChecked);

	private IEnumerator SetAllClickable(bool value)
	{
		yield return new WaitForSeconds(0.2f);
		Array.ForEach(grenierObjects, x => x.CanBeSelected = value);
	}
}
