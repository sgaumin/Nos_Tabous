using System.Collections;
using UnityEngine;

public class LevelManager6 : MonoBehaviour
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
	private void Start()
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
	}

	// Update is called once per frame
	private void Update()
	{
		// Set Animation and Sound according to the Dialogue Index
		if (DialogueSystemScript.indexDialogue == indexCount)
		{
			return;
		}

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
		{
			StartCoroutine(sylvieTalking());
		}

		// End Level
		if (indexCount == 2)
		{
			StartCoroutine(FinalStepLevel());
		}
	}

	private IEnumerator sylvieTalking()
	{
		// Set sylvie  talking animation
		sylvieAnimator.SetTrigger("BackTalking");

		// Coroutine End
		yield break;
	}

	private IEnumerator FinalStepLevel()
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

		// Hide the dialogues box
		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(false));

		// Background fad out animation
		StartCoroutine(background.FadOut());

		// Coroutine End
		GameSystem.Instance.LoadNextScene();
		yield break;
	}

	private IEnumerator StartLevel()
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
		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(true));
	}
}
