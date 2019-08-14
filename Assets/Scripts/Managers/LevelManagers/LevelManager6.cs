using System.Collections;
using UnityEngine;

public class LevelManager6 : LevelSequence
{
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Animator sylvieAnimator;

	[SerializeField] private Background background;

	[SerializeField] private GameObject clock;

	private Character mathiasCharacter;
	private Character sylvieCharacter;

	private int indexCount;

	private bool isStarting = true;

	// Start is called before the first frame update
	void Start()
	{
		// Deactivate fad in animation for Mathias
		mathiasAnimator.SetBool("IsFadIn", false);

		// Asign Character components
		mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
		sylvieCharacter = sylvieAnimator.gameObject.GetComponent<Character>();

		// Hide
		sylvieCharacter.gameObject.SetActive(false);
		clock.SetActive(false);

		// Index for checking the current IndexDialogue of DialogueSystemScript script 
		indexCount = 999;

		// Hide Dialogue Box
		ShowDialogueBoxBackground(false);
	}

	// Update is called once per frame
	void Update()
	{
		// Set Animation and Sound according to the Dialogue Index
		if (DialogueSystemScript.indexDialogue == indexCount)
			return;

		indexCount = DialogueSystemScript.indexDialogue;

		if (isStarting)
		{
			//DialogueSystemScript.indexDialogue = 0; // TO report
			indexCount = 0;
			StartCoroutine(StartLevel());
			isStarting = false;
		}

		// Sylvie Speaking Steps
		if (indexCount == 1)
			StartCoroutine(sylvieTalking());

		// End Level
		if (indexCount == 2)
			StartCoroutine(FinalStepLevel());
	}

	IEnumerator sylvieTalking()
	{
		// Set sylvie  talking animation
		sylvieAnimator.SetTrigger("BackTalking");

		// Coroutine End
		yield break;
	}

	IEnumerator FinalStepLevel()
	{
		// Set sylvie idle animation
		sylvieAnimator.SetTrigger("ResetBack");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias talking animation
		mathiasAnimator.SetTrigger("Talking");
		yield return new WaitForSeconds(1f);

		// Reset Mathias animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(1f);

		// sylvie Fad Out animation
		sylvieAnimator.SetTrigger("FadOut");

		// Hide Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(false);
		yield return new WaitForSeconds(0.5f);

		// Animation FadOut dialogues box
		Animator dialoguesAnimator = DialogueBox.Instance.GetComponent<Animator>();
		dialoguesAnimator.SetTrigger("FadOut");

		yield return new WaitForSeconds(0.5f);

		// Background fad out animation
		StartCoroutine(background.FadOut());

		// Coroutine End
		GameSystem.Instance.LoadNextScene();
		yield break;
	}

	IEnumerator StartLevel()
	{
		yield return new WaitForSeconds(1f);

		// Clock Animation
		clock.SetActive(true);
		yield return new WaitForSeconds(3f);
		clock.SetActive(false);
		yield return new WaitForSeconds(0.5f);

		// Show Background
		StartCoroutine(background.FadIn());

		// Show sylvie Character
		sylvieCharacter.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);

		// Show dialogues box 
		ShowDialogueBoxBackground(true);
		yield return new WaitForSeconds(0.5f);

		// Show Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(true);
	}
}
