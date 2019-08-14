using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager1 : LevelSequence
{
	// TO DO: Create an abract class to avoid repetition in variables between all LevelManager
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Animator henriAnimator;
	[SerializeField] private Image disclaimer;
	[SerializeField] private GameObject nameDialogues;

	[SerializeField] private AudioPlayer1 audioManager;

	private Character mathiasCharacter;
	private Character henriCharacter;
	private int indexCount;

	void Start()
	{
		// Asign Character components
		mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
		henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

		// Hide Mathias & Henri Character at Starting
		mathiasCharacter.gameObject.SetActive(false);
		henriCharacter.gameObject.SetActive(false);

		// Hide
		disclaimer.gameObject.SetActive(false);

		// Index for checking the current IndexDialogue of DialogueSystemScript script 
		indexCount = 999;

		// Hide Dialogue Box
		ShowDialogues(false);
	}

	// TO DO: Create a Delegate function on DialogueSystemScript for isTabou & indexDialogue variables
	private void Update()
	{
		// Check if the choice is Tabou and set the animation
		if (DialogueSystemScript.isTabou)
			StartCoroutine(TabouStepLevel());

		// Set Animation and Sound according to the Dialogue Index
		if (DialogueSystemScript.indexDialogue == indexCount)
			return;

		indexCount = DialogueSystemScript.indexDialogue;

		// Start Level scripting
		if (indexCount == 0)
			StartCoroutine(StartLevel());

		// Mathias Talking
		if (indexCount == 2 || indexCount == 4 || indexCount == 5 || indexCount == 8 || indexCount == 10 || indexCount == 13)
			StartCoroutine(MathiasTalkingStep());

		// Mathias Anger
		if (indexCount == 15)
			StartCoroutine(AngerStepLevel());

		// Mathias AngryIdle
		if (indexCount == 14)
			AngerIdleStep();


		// Henri Talking
		if (indexCount == 1 || indexCount == 3 || indexCount == 6 || indexCount == 9 || indexCount == 12 || indexCount == 14)
			StartCoroutine(HenriTalkingStep());

		// Final Step
		if (indexCount == 15)
			StartCoroutine(LastStepLevel());
	}

	IEnumerator StartLevel()
	{
		// Waiting before starting
		yield return new WaitForSeconds(1f);

		//Show Disclaimer
		disclaimer.gameObject.SetActive(true);
		yield return new WaitForSeconds(13f);
		disclaimer.gameObject.SetActive(false);

		// Show Henri
		yield return new WaitForSeconds(2f);
		henriCharacter.gameObject.SetActive(true);

		// Henri phone call animation
		yield return new WaitForSeconds(2f);
		henriAnimator.SetTrigger("BackCallStarting");

		// Phone sound playing
		yield return new WaitForSeconds(1f);
		audioManager.PlayPhoneSound(true);

		// Show Mathias character
		yield return new WaitForSeconds(2f);
		mathiasCharacter.gameObject.SetActive(true);

		// Activate fad in animation for Mathias
		mathiasAnimator.SetBool("IsFadIn", true);

		// Mathias pick up the phone animation
		yield return new WaitForSeconds(2f);
		mathiasAnimator.SetTrigger("CallStarting");

		// End phone sound
		audioManager.PlayPhoneSound(false);
		yield return new WaitForSeconds(0.5f);

		// Henri turn
		henriCharacter.Flip();
		yield return new WaitForSeconds(1f);

		// Henri start talking animation
		henriAnimator.SetTrigger("BackCallTalking");
		yield return new WaitForSeconds(0.5f);

		// Show dialogues box 
		ShowDialogues(true);
		yield return new WaitForSeconds(0.5f);

		// Show Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(true);
	}

	IEnumerator MathiasTalkingStep()
	{
		// Set Henri calling idle animation
		henriAnimator.SetTrigger("BackCallIdle");
		yield return new WaitForSeconds(0.2f);

		// Set Mathias calling talking animation
		mathiasAnimator.SetTrigger("CallTalking");
	}

	IEnumerator HenriTalkingStep()
	{
		// Set Mathias calling idle animation
		if (indexCount != 1)
		{
			mathiasAnimator.SetTrigger("CallIdle");

			// Set Henri calling talking animation
			yield return new WaitForSeconds(0.2f);
			henriAnimator.SetTrigger("BackCallTalking");
		}
	}

	IEnumerator LastStepLevel()
	{
		// Set Henri calling idle animation
		henriAnimator.SetTrigger("BackCallIdle");
		yield return new WaitForSeconds(0.2f);

		// Animation hang up Mathias
		mathiasAnimator.SetTrigger("CallIdle");
		yield return new WaitForSeconds(0.2f);
		mathiasAnimator.SetTrigger("CallEnding");
		yield return new WaitForSeconds(0.2f);

		// Set Mathias Anger animation
		mathiasAnimator.SetTrigger("AngerIdle");
		yield return new WaitForSeconds(1.5f);

		// Henri turn
		henriCharacter.Flip();
		yield return new WaitForSeconds(1f);

		// Animation hang up Henri
		henriAnimator.SetTrigger("BackCallEnding");
		yield return new WaitForSeconds(1f);

		// Animation FadOut Henri
		henriAnimator.SetTrigger("FadOut");
		yield return new WaitForSeconds(2f);

		// Set Mathias Anger animation
		mathiasAnimator.SetTrigger("Reset");

		// Hide Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(false);
		yield return new WaitForSeconds(0.5f);

		// Animation FadOut dialogues box
		Animator dialoguesAnimator = DialogueBox.Instance.GetComponent<Animator>();
		dialoguesAnimator.SetTrigger("FadOut");
		Animator dialoguesNameAnimator = nameDialogues.GetComponent<Animator>();
		dialoguesNameAnimator.SetTrigger("FadOut");

		// Quit PLay Mode
		yield return new WaitForSeconds(2f);
		GameSystem.Instance.LoadNextScene();
	}

	IEnumerator TabouStepLevel()
	{
		mathiasAnimator.SetTrigger("CallIdle");
		mathiasAnimator.SetTrigger("CallTabou");

		yield return new WaitForSeconds(0.2f);
		mathiasAnimator.SetTrigger("CallTalking");
	}

	IEnumerator AngerStepLevel()
	{
		// Set Henri calling idle animation
		henriAnimator.SetTrigger("BackCallIdle");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("CallAnger");
	}

	void AngerIdleStep()
	{
		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("CallAngerIdle");
	}
}
