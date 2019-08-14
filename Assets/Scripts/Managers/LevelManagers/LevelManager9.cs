using System.Collections;
using UnityEngine;

public class LevelManager9 : LevelSequence
{
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Animator henryAnimator;
	[SerializeField] private Animator sylvieAnimator;

	[SerializeField] private Background background;

	// UI
	[SerializeField] private Animator fadOutScreen;

	private Character mathiasCharacter;
	private Character henryCharacter;
	private Character sylvieCharacter;

	private int indexCount;

	private bool isStarting = true;

	void Start()
	{
		// Deactivate fad in animation for Mathias
		mathiasAnimator.SetBool("IsFadIn", false);

		// Asign Character components
		mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
		henryCharacter = henryAnimator.gameObject.GetComponent<Character>();
		sylvieCharacter = sylvieAnimator.gameObject.GetComponent<Character>();

		// Hide
		mathiasCharacter.gameObject.SetActive(false);
		henryCharacter.gameObject.SetActive(false);
		sylvieCharacter.gameObject.SetActive(false);
		background.gameObject.SetActive(false);

		//fadOutScreen.SetActive(false);

		// Index for checking the current IndexDialogue of DialogueSystemScript script 
		indexCount = 999;

		// Hide Dialogue Box
		ShowDialogues(false);
	}

	// Update is called once per frame
	void Update()
	{
		// Check if the choice is Tabou and set the animation
		if (DialogueSystemScript.isTabou)
			StartCoroutine(TabouStepLevel());

		// Set Animation and Sound according to the Dialogue Index
		if (DialogueSystemScript.indexDialogue == indexCount)
			return;

		indexCount = DialogueSystemScript.indexDialogue;

		if ((indexCount == 10 || indexCount == 9 || indexCount == 0) && isStarting)
		{
			//DialogueSystemScript.indexDialogue = 0; // TO report
			indexCount = 0;
			StartCoroutine(StartLevel());
			isStarting = false;
		}

		// Mathias Speaking Steps
		if (indexCount == 5)
			StartCoroutine(MathiasTalking());

		// Mathias Surpris
		if (indexCount == 1)
			StartCoroutine(MathiasSurprised());

		//Mathias Triste
		if (indexCount == 4)
			StartCoroutine(MathiasSad());

		//henri et Sylvie surpris
		if (indexCount == 5)
			StartCoroutine(GrandParentsSurprised());

		// henry Speaking Steps
		if (indexCount == 1 || indexCount == 3)
			StartCoroutine(HenryTalking());

		// Reset Animations
		if (indexCount == 4)
			ResetAnimations();

		// End Level
		if (indexCount == 6)
			StartCoroutine(FinalStepLevel());
	}

	IEnumerator StartLevel()
	{
		// Waiting time
		yield return new WaitForSeconds(1f);
		mathiasCharacter.gameObject.SetActive(true);
		mathiasAnimator.SetBool("IsFadIn", true);
		yield return new WaitForSeconds(1f);

		// Show Background
		StartCoroutine(background.FadIn());

		// Show Henry & Sylvie Characters facing
		henryCharacter.gameObject.SetActive(true);
		sylvieCharacter.gameObject.SetActive(true);

		henryAnimator.SetBool("IsFadInFacing", true);
		sylvieAnimator.SetBool("IsFadInFacing", true);

		yield return new WaitForSeconds(1f);

		// Show dialogues box 
		ShowDialogues(true);
		yield return new WaitForSeconds(0.5f);

		// Show Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(true);

		// Coroutine End
		yield break;
	}

	void ResetAnimations()
	{
		// Set Mathias Animation
		mathiasAnimator.SetTrigger("Reset");
		henryAnimator.SetTrigger("Reset");
	}

	IEnumerator HenryTalking()
	{
		// Set Mathias Animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set henry  talking animation
		henryAnimator.SetTrigger("Talking");

		// Coroutine End
		yield break;
	}

	IEnumerator MathiasTalking()
	{
		// Set henry idle animation
		henryAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Mathias Idle animation
		mathiasAnimator.SetTrigger("Talking");

		// Coroutine End
		yield break;
	}

	IEnumerator MathiasSurprised()
	{
		// Set henry idle animation
		henryAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.2f);

		// Mathias Idle animation
		mathiasAnimator.SetTrigger("Surprised");

		// Coroutine End
		yield break;
	}

	IEnumerator MathiasSad()
	{
		// Set henry idle animation
		henryAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.2f);

		// Mathias Idle animation
		mathiasAnimator.SetTrigger("Sad");

		// Coroutine End
		yield break;
	}

	IEnumerator GrandParentsSurprised()
	{
		yield return new WaitForSeconds(0.2f);
		henryAnimator.SetTrigger("Surprised");
		sylvieAnimator.SetTrigger("Surprised");
	}

	IEnumerator FinalStepLevel()
	{
		// Set Henry & Mathias idle animation
		//mathiasAnimator.SetTrigger("Reset");
		//henryAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Hide Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(false);
		yield return new WaitForSeconds(0.5f);

		// Animation FadOut dialogues box
		yield return new WaitForSeconds(0.5f);

		// Background fad out animation
		StartCoroutine(background.FadOut());

		fadOutScreen.SetTrigger("FadOut");
		yield return new WaitForSeconds(2f);

		// Coroutine End
		GameSystem.Instance.LoadNextScene();
		yield break;
	}

	IEnumerator TabouStepLevel()
	{
		mathiasAnimator.SetTrigger("Reset");
		mathiasAnimator.SetTrigger("Tabou");

		yield return new WaitForSeconds(0.2f);
		mathiasAnimator.SetTrigger("Talking");
	}
}
