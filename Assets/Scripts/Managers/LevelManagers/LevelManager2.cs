﻿using System.Collections;
using UnityEngine;

public class LevelManager2 : MonoBehaviour
{
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Animator carolineAnimator;
	[SerializeField] private Background background;

	[SerializeField] private AudioPlayer2 audioManager;

	private Character mathiasCharacter;
	private Character carolineCharacter;
	private int indexCount;

	private bool isStarting = true;

	private void Start()
	{
		// Deactivate fad in animation for Mathias
		mathiasAnimator.SetBool("IsFadIn", false);

		// Asign Character components
		mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
		carolineCharacter = carolineAnimator.gameObject.GetComponent<Character>();

		// Hide Caroline Character at Starting
		carolineCharacter.gameObject.SetActive(false);

		// Index for checking the current IndexDialogue of DialogueSystemScript script 
		indexCount = 999;
	}

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
			DialogueSystemScript.indexDialogue = 1; // TO report

			indexCount = 1;
			isStarting = false;
			StartCoroutine(StartLevel());
		}

		// Mathias Speaking Steps
		if (indexCount == 6 || indexCount == 10 || indexCount == 12 || indexCount == 17 || indexCount == 19 || indexCount == 20 || indexCount == 21)
		{
			StartCoroutine(MathiasTalking());
		}

		// Mathias Anger Steps
		if (indexCount == 2 || indexCount == 15)
		{
			StartCoroutine(MathiasAnger());
		}

		//// Mathias IdleAnger
		if (indexCount == 16)
		{
			StartCoroutine(MathiasIdleAnger());
		}

		// Mathias Surpris
		if (indexCount == 7 || indexCount == 18 || indexCount == 25)
		{
			StartCoroutine(MathiasSurprised());
		}

		if (indexCount == 8)
		{
			StartCoroutine(ResetCaro());
		}

		//Mathias Tristoun
		if (indexCount == 26)
		{
			StartCoroutine(MathiasSad());
		}

		// Caroline Speaking Steps
		if (indexCount == 1 || indexCount == 3 || indexCount == 5 || indexCount == 7 || indexCount == 9
			|| indexCount == 11 || indexCount == 13 || indexCount == 16 || indexCount == 18
			|| indexCount == 22 || indexCount == 27)
		{
			StartCoroutine(CarolineTalking());
		}

		if (indexCount == 28)
		{
			StartCoroutine(FinalStepLevel());
		}
	}

	private IEnumerator StartLevel()
	{
		// Show Caroline Character
		carolineCharacter.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);

		// Flip Mathias
		mathiasCharacter.Flip();
		yield return new WaitForSeconds(0.5f);

		yield return StartCoroutine(background.FadIn());

		// Show dialogues box 
		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(true));

		// Caroline start talking animation
		carolineAnimator.SetTrigger("Talking");
		yield return new WaitForSeconds(0.5f);
	}

	private IEnumerator MathiasTalking()
	{
		// Set Caroline idle animation
		carolineAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias talking animation
		mathiasAnimator.SetTrigger("Talking");
	}

	private IEnumerator CarolineTalking()
	{
		// Set Mathias  idle animation
		if (indexCount != 1)
		{
			mathiasAnimator.SetTrigger("Reset");

			// Set Caroline  talking animation
			yield return new WaitForSeconds(0.5f);
			carolineAnimator.SetTrigger("Talking");
		}
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
	}

	private IEnumerator MathiasAnger()
	{
		// Set Caroline calling idle animation
		carolineAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("Anger");
	}

	private IEnumerator MathiasIdleAnger()
	{
		// Set Caroline calling idle animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("AngerIdle");
	}

	private IEnumerator ResetCaro()
	{
		// Set Caroline calling idle animation
		carolineAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);
	}

	private IEnumerator MathiasSurprised()
	{
		// Set Caroline calling idle animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("Surprised");
	}

	private IEnumerator MathiasSad()
	{
		// Set Caroline calling idle animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("Sad");
	}

	private IEnumerator VeryAngerStepLevel()
	{
		// Set Caroline calling idle animation
		carolineAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("VeryAnger");
	}

	private IEnumerator FinalStepLevel()
	{
		// Set Caroline idle animation
		carolineAnimator.SetTrigger("Reset");

		// Hide Texts into the dialogues box
		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(false));

		// Flip Mathias
		mathiasCharacter.Flip();
		//mathiasAnimator.SetTrigger("Reset");
		mathiasAnimator.SetTrigger("AngerIdle");
		yield return new WaitForSeconds(0.5f);

		// Reset Caroline animation 
		carolineAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Caroline Fad Out animation
		carolineAnimator.SetTrigger("FadOut");

		// Play sound Shoes
		audioManager.PlayHeelShoesSound(true);
		yield return new WaitForSeconds(2f);
		audioManager.PlayHeelShoesSound(false);

		// Play sound door
		audioManager.PlayClosedDoorSound(true);
		yield return new WaitForSeconds(3f);

		// Flip Mathias
		mathiasCharacter.Flip();
		mathiasAnimator.SetTrigger("Reset");

		yield return StartCoroutine(background.FadOut());

		// Coroutine End
		GameSystem.Instance.LoadNextScene();
	}
}
