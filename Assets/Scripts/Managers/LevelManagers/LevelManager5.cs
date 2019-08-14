using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LevelManager5 : LevelSequence
{
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Animator sylvieAnimator;
	[SerializeField] private Animator henriAnimator;
	[SerializeField] private Background background;

	[SerializeField] private AudioPlayer5 audioManager;

	[SerializeField] private Transform initialPosition;

	private Character mathiasCharacter;
	private Character sylvieCharacter;
	private Character henriCharacter;

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
		henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

		// Hide
		sylvieCharacter.gameObject.SetActive(false);
		henriCharacter.gameObject.SetActive(false);

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

		// TO DO: Implement more flexible IF statement regarding previous previous scnene loading and indexCount was not reset
		if (isStarting)
		{
			//DialogueSystemScript.indexDialogue = 0; // TO report
			indexCount = 0;
			StartCoroutine(StartLevel());
			isStarting = false;
		}

		// Mathias Speaking Steps
		if (indexCount == 1 || indexCount == 3 || indexCount == 5 || indexCount == 8 || indexCount == 10 || indexCount == 11 || indexCount == 12)
		{
			StartCoroutine(MathiasTalking());
		}

		//Mathias Angry
		if (indexCount == 15)
		{
			StartCoroutine(MathiasAnger());
		}

		//Mathias Tres Angry
		if (indexCount == 16)
		{
			StartCoroutine(MathiasVeryAnger());
		}

		// Sylvie Speaking Steps
		if (indexCount == 2 || indexCount == 4 || indexCount == 9)
		{
			StartCoroutine(sylvieTalking());
		}

		// Henri Speaking Steps
		if (/*indexCount == 6 ||*/ indexCount == 14 || indexCount == 17)
		{
			StartCoroutine(HenriTalking());
		}

		// Entrance Henri
		if (indexCount == 6)
		{
			StartCoroutine(HenriEntrance());
		}

		// End Level
		if (indexCount == 19)
		{
			StartCoroutine(FinalStepLevel());
		}
	}

	private IEnumerator StartLevel()
	{
		// Play wind sound
		audioManager.PlayWindSound(true);
		yield return new WaitForSeconds(3f);
		mathiasCharacter.transform.DOMove(initialPosition.position, 1f, false).SetEase(Ease.InOutSine);

		// Play foot sound
		audioManager.PlayFootSound(true);
		yield return new WaitForSeconds(6f);

		// Play knock sound
		audioManager.PlayKnockSound(true);
		yield return new WaitForSeconds(3f);

		// Disalble all sounds
		audioManager.PlayWindSound(false);
		audioManager.PlayFootSound(false);
		audioManager.PlayKnockSound(false);

		// Show Background
		StartCoroutine(background.FadIn());

		// Show sylvie Character
		sylvieCharacter.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);

		// Show dialogues box 
		ShowDialogues(true);
		yield return new WaitForSeconds(0.5f);

		// Show Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(true);

		// Coroutine End
		yield break;
	}

	private IEnumerator MathiasTalking()
	{
		if (indexCount != 1)
		{
			// Set sylvie idle animation
			sylvieAnimator.SetTrigger("ResetBack");

			henriAnimator.SetTrigger("ResetBack");
			yield return new WaitForSeconds(0.5f);
		}

		// Set Mathias talking animation
		mathiasAnimator.SetTrigger("Talking");

		// Coroutine End
		yield break;
	}

	private IEnumerator MathiasAnger()
	{
		// Set sylvie idle animation
		sylvieAnimator.SetTrigger("ResetBack");

		henriAnimator.SetTrigger("ResetBack");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias talking animation
		mathiasAnimator.SetTrigger("Anger");

		// Coroutine End
		yield break;
	}

	private IEnumerator MathiasVeryAnger()
	{
		// Set sylvie idle animation
		sylvieAnimator.SetTrigger("ResetBack");

		henriAnimator.SetTrigger("ResetBack");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias talking animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.2f);
		mathiasAnimator.SetTrigger("VeryAnger");

		// Coroutine End
		yield break;
	}

	private IEnumerator sylvieTalking()
	{
		// Set Mathias  idle animation
		mathiasAnimator.SetTrigger("Reset");
		henriAnimator.SetTrigger("ResetBack");
		yield return new WaitForSeconds(0.5f);

		// Set sylvie  talking animation
		sylvieAnimator.SetTrigger("BackTalking");

		// Coroutine End
		yield break;
	}

	private IEnumerator HenriTalking()
	{
		mathiasAnimator.SetTrigger("Reset");
		//sylvieAnimator.SetTrigger("ResetBack");
		yield return new WaitForSeconds(0.5f);

		// Set sylvie  talking animation
		henriAnimator.SetTrigger("BackTalking");

		// Coroutine End
		yield break;
	}

	private IEnumerator HenriEntrance()
	{
		// Show Henri
		henriCharacter.gameObject.SetActive(true);

		// Henri Talking
		yield return StartCoroutine(HenriTalking());

		// End Coroutine
		yield break;
	}

	private IEnumerator TabouStepLevel()
	{
		// Retrieve the current animation state
		AnimatorClipInfo[] m_CurrentClipInfo;
		m_CurrentClipInfo = mathiasAnimator.GetCurrentAnimatorClipInfo(0);
		string m_ClipName = m_CurrentClipInfo[0].clip.name;

		// Set Mathias Tabou animation
		mathiasAnimator.SetTrigger("Reset");
		mathiasAnimator.SetTrigger("Tabou");
		yield return new WaitForSeconds(2f);

		// Set the same animation as the start of this dialogue
		if (m_ClipName == "Mathias_Talking")
		{
			mathiasAnimator.SetTrigger("Talking");
		}

		if (m_ClipName == "Mathias_Anger")
		{
			mathiasAnimator.SetTrigger("Anger");
		}

		if (m_ClipName == "Mathias_VeryAnger")
		{
			mathiasAnimator.SetTrigger("VeryAnger");
		}

		// Coroutine End
		yield break;
	}

	private IEnumerator FinalStepLevel()
	{
		// Reset Mathias animation
		mathiasAnimator.SetTrigger("Reset");
		henriAnimator.SetTrigger("ResetBack");

		// sylvie Fad Out animation
		sylvieAnimator.SetTrigger("FadOut");
		henriAnimator.SetTrigger("FadOut");

		// Hide Texts into the dialogues box
		DialogueBox.Instance.ShowTexts(false);
		yield return new WaitForSeconds(0.5f);

		// Animation FadOut dialogues box
		Animator dialoguesAnimator = DialogueBox.Instance.GetComponent<Animator>();
		dialoguesAnimator.SetTrigger("FadOut");
		Animator dialoguesNameAnimator = DialogueBox.Instance.GetComponent<Animator>();
		dialoguesNameAnimator.SetTrigger("FadOut");

		yield return new WaitForSeconds(0.5f);

		// Background fad out animation
		StartCoroutine(background.FadOut());

		GameSystem.Instance.LoadNextScene();
	}
}
