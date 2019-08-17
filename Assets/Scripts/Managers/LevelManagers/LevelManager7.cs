using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LevelManager7 : MonoBehaviour
{
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Background background;

	[SerializeField] private Transform initialPosition;

	[SerializeField] private AudioPlayer7 audioManager;

	private Character mathiasCharacter;
	private int indexCount;

	private bool isStarting = true;

	// Start is called before the first frame update
	private void Start()
	{
		// Asign Character components
		mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();

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
			DialogueSystemScript.indexDialogue = 0; // TO report
			indexCount = 0;
			StartCoroutine(StartLevel());
			isStarting = false;
		}

		// Mathias Speaking Steps
		if (indexCount == 1 || indexCount == 2 || indexCount == 3 || indexCount == 4 || indexCount == 5 || indexCount == 6)
		{
			StartCoroutine(MathiasTalkingStep());
		}

		//// Open Box
		if (indexCount == 9)
		{
			StartCoroutine(LastStepLevel(false));
		}

		// Quit
		if (indexCount == 7)
		{
			StartCoroutine(LastStepLevel(true));
		}
	}

	private IEnumerator StartLevel()
	{
		mathiasCharacter.transform.DOMove(initialPosition.position, 1f, false).SetEase(Ease.InOutSine);
		yield return new WaitForSeconds(1f);

		// Flip Mathias and play Back animation
		mathiasAnimator.SetTrigger("Back");
		mathiasCharacter.Flip();

		// Stairs sound playing
		audioManager.PlayStairsSound(true);
		yield return new WaitForSeconds(audioManager.StairsSoundLenght - 1f);

		// Door sound playing
		audioManager.PlayOpenDoorSound(true);
		yield return new WaitForSeconds(2.5f);

		background.GetComponent<SpriteRenderer>().color = new Color(110f / 255f, 110f / 255f, 110f / 255f, 0f);

		yield return StartCoroutine(background.FadIn());
		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(true));
	}

	private IEnumerator MathiasTalkingStep()
	{
		if (indexCount == 4)
		{
			background.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
		}

		// Set Mathias calling talking animation
		mathiasAnimator.SetTrigger("BackTalking");
		yield return new WaitForSeconds(1f);

		// Set Mathias calling talking animation
		mathiasAnimator.SetTrigger("Back");

		// Coroutine End
		yield break;
	}

	private IEnumerator LastStepLevel(bool isEnd)
	{
		// Box sound playing
		if (!isEnd)
		{
			audioManager.PlayOpenBoxSound(true);
			yield return new WaitForSeconds(2f);
		}

		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(false));
		yield return StartCoroutine(background.FadOut());

		// Load end Game Screen is End
		if (isEnd)
		{
			// Hide Mathias 
			mathiasAnimator.SetTrigger("BackFadOut");
			yield return new WaitForSeconds(2f);

			GameSystem.Instance.LoadSceneByName("10- Fin");
		}
		else
		{
			GameSystem.Instance.LoadNextScene();
		}
	}
}
