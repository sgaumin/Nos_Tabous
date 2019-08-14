using System.Collections;
using UnityEngine;

public class LevelManager3 : LevelSequence
{
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Animator henriAnimator;

	// Objects
	[SerializeField] private GameObject clock;
	[SerializeField] private GameObject callSignals;

	[SerializeField] private AudioPlayer3 audioManager;

	private Character mathiasCharacter;
	private Character henriCharacter;

	private int indexCount;

	private bool isStarting = true;

	private void Start()
	{
		// Asign Character components
		mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
		henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

		// Hide 
		henriCharacter.gameObject.SetActive(false);
		clock.SetActive(false);
		callSignals.SetActive(false);

		// Index for checking the current IndexDialogue of DialogueSystemScript script 
		indexCount = 999;

		// Hide Dialogue Box
		ShowDialogues(false);
	}

	// Update is called once per frame
	private void Update()
	{
		// Check if the choice is Tabou and set the animation
		if (DialogueSystemScript.isTabou)
		{
			StartCoroutine(TabouStepLevel());
		}

		// Set Animation and Sound according to the Dialogue Index
		if (DialogueSystemScript.indexDialogue == indexCount)
		{
			return;
		}

		indexCount = DialogueSystemScript.indexDialogue;

		if (isStarting)
		{
			DialogueSystemScript.indexDialogue = 0; // TO report
			indexCount = 0;
			StartCoroutine(StartLevel());
			isStarting = false;
		}

		// Mathias Speaking Steps
		if (/*indexCount == 1 ||*/ indexCount == 4 || indexCount == 9 || indexCount == 12 || indexCount == 15 || indexCount == 17)
		{
			StartCoroutine(SecondStepLevel());
		}

		// Henri Speaking Steps
		if (indexCount == 2 || indexCount == 11 || indexCount == 14 || indexCount == 16)
		{
			StartCoroutine(ThirdStepLevel());
		}

		// Final Step
		if (indexCount == 18)
		{
			StartCoroutine(LastStepLevel());
		}
	}

	private IEnumerator StartLevel()
	{
		// Waiting before starting
		yield return new WaitForSeconds(1f);

		// Clock Animation
		clock.SetActive(true);
		yield return new WaitForSeconds(3f);
		clock.SetActive(false);

		// Mathias pick up the phone animation
		mathiasAnimator.SetTrigger("CallStarting");
		yield return new WaitForSeconds(1f);

		// Phone sound playing
		audioManager.PlayPhoneSound(true);
		yield return new WaitForSeconds(1f);
		callSignals.SetActive(true);
		yield return new WaitForSeconds(7f);

		// Fad In Animation Henri
		henriCharacter.gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);

		// Henri phone call animation
		henriAnimator.SetTrigger("BackCallStarting");
		yield return new WaitForSeconds(2f);

		// End phone sound
		callSignals.SetActive(false);
		audioManager.PlayPhoneSound(false);
		yield return new WaitForSeconds(2f);

		// Mathias start talking animation
		mathiasAnimator.SetTrigger("CallTalking");
		yield return new WaitForSeconds(1f);

		// Henri turn
		henriCharacter.Flip();
		yield return new WaitForSeconds(1f);

		// Show dialogues box 
		ShowDialogues(true);
		yield return new WaitForSeconds(0.5f);

		// Show Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(true);
	}

	private IEnumerator SecondStepLevel()
	{
		// Set Henri calling idle animation
		henriAnimator.SetTrigger("BackCallIdle");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling talking animation
		mathiasAnimator.SetTrigger("CallTalking");
	}

	private IEnumerator ThirdStepLevel()
	{
		// Set Mathias calling idle animation
		mathiasAnimator.SetTrigger("CallIdle");

		// Set Henri calling talking animation
		yield return new WaitForSeconds(0.5f);
		henriAnimator.SetTrigger("BackCallTalking");
	}

	private IEnumerator TabouStepLevel()
	{
		mathiasAnimator.SetTrigger("CallIdle");
		mathiasAnimator.SetTrigger("CallTabou");

		yield return new WaitForSeconds(0.2f);
		mathiasAnimator.SetTrigger("CallTalking");
	}

	private IEnumerator LastStepLevel()
	{
		// Set Henri calling idle animation
		henriAnimator.SetTrigger("BackCallIdle");
		yield return new WaitForSeconds(0.5f);

		// Mathias talking animation for last dialogue
		mathiasAnimator.SetTrigger("CallTalking");
		yield return new WaitForSeconds(1f);

		// Mathias stop talking animation
		mathiasAnimator.SetTrigger("CallIdle");
		yield return new WaitForSeconds(0.5f);

		// Henri stop talking animation
		henriAnimator.SetTrigger("BackCallIdle");
		yield return new WaitForSeconds(0.5f);

		// Animation hang up Henri
		henriAnimator.SetTrigger("BackCallEnding");
		yield return new WaitForSeconds(.05f);

		// Animation FadOut Henri
		henriAnimator.SetTrigger("FadOut");
		yield return new WaitForSeconds(1f);

		// Mathias hang up animation
		mathiasAnimator.SetTrigger("CallEnding");
		yield return new WaitForSeconds(0.5f);

		// Hide Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(false);
		yield return new WaitForSeconds(0.5f);

		// Animation FadOut dialogues box
		Animator dialoguesAnimator = DialogueBox.Instance.GetComponent<Animator>();
		dialoguesAnimator.SetTrigger("FadOut");

		// Quit PLay Mode
		yield return new WaitForSeconds(2f);
		GameSystem.Instance.LoadNextScene();

		// Coroutine End
		yield break;
	}
}
