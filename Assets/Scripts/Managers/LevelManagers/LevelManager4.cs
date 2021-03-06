﻿using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LevelManager4 : MonoBehaviour
{
	[SerializeField] private Animator mathiasAnimator;
	[SerializeField] private Animator jadeAnimator;
	[SerializeField] private Animator car;
	[SerializeField] private Animator insideCar;

	[SerializeField] private Transform initialPosition;

	[SerializeField] private AudioPlayer4 audioManager;

	private Character mathiasCharacter;
	private Character jadeCharacter;

	private int indexCount;

	private bool isStarting = true;

	private void Start()
	{
		// Deactivate fad in animation for Mathias
		mathiasAnimator.SetBool("IsFadIn", false);

		// Asign Character components
		mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
		jadeCharacter = jadeAnimator.gameObject.GetComponent<Character>();

		// Hide
		jadeCharacter.gameObject.SetActive(false);
		car.gameObject.SetActive(false);

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
			StartCoroutine(StartLevel());
			isStarting = false;
		}

		// Mathias Speaking Steps
		if (indexCount == 2 || indexCount == 3 || indexCount == 4 || indexCount == 9 || indexCount == 11 || indexCount == 14 || indexCount == 16 || indexCount == 18 || indexCount == 19 || indexCount == 21)
		{
			StartCoroutine(MathiasTalking());
		}

		// Jade Speaking Steps
		if (indexCount == 1 || indexCount == 5 || indexCount == 7 || indexCount == 10 || indexCount == 12 || indexCount == 15 || indexCount == 17 || indexCount == 20 || indexCount == 22)
		{
			StartCoroutine(JadeTalking());
		}

		//Mathias Angry
		if (indexCount == 6)
		{
			StartCoroutine(MathiasAnger());
		}

		// Mathias Surpris
		if (indexCount == 7 || indexCount == 17)
		{
			StartCoroutine(MathiasSurprised());
		}

		//Mathias Reset
		if (indexCount == 8)
		{
			StartCoroutine(MathiasReset());
		}

		// facing right Jade's animation
		if (indexCount == 6)
		{
			StartCoroutine(JadeFacingRight());
		}

		// End Level
		if (indexCount == 22)
		{
			StartCoroutine(FinalStepLevel());
		}
	}

	private IEnumerator StartLevel()
	{
		mathiasCharacter.transform.DOMove(initialPosition.position, 1f, false).SetEase(Ease.InOutSine);
		yield return new WaitForSeconds(1f);

		// Play car starting sound
		audioManager.PlayStartDrivingSound(true);
		yield return new WaitForSeconds(5f);

		// Show jade Character
		jadeCharacter.gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);

		// Show Background
		car.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);

		car.SetTrigger("Move");

		// Show dialogues box 
		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(true));

		// Play car ambience sound
		audioManager.PlayAmbianceSound(true);
		yield return new WaitForSeconds(0.5f);

		// Add animation by parents to characters
		mathiasCharacter.transform.SetParent(insideCar.transform);
		jadeCharacter.transform.SetParent(insideCar.transform);

		// Coroutine End
		yield break;
	}

	private IEnumerator MathiasTalking()
	{
		// Set jade idle animation
		jadeAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias talking animation
		mathiasAnimator.SetTrigger("Talking");

		// Coroutine End
		yield break;
	}

	private IEnumerator MathiasAnger()
	{

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("Anger");

		// Coroutine End
		yield break;
	}

	private IEnumerator MathiasSurprised()
	{
		// Set Caroline calling idle animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);

		// Set Mathias calling anger animation
		mathiasAnimator.SetTrigger("Surprised");

		// Coroutine End
		yield break;
	}

	private IEnumerator MathiasReset()
	{
		// Set Caroline calling idle animation
		mathiasAnimator.SetTrigger("Reset");
		yield return new WaitForSeconds(0.5f);



		// Coroutine End
		yield break;
	}

	private IEnumerator JadeTalking()
	{
		// Set Mathias  idle animation
		if (indexCount != 1)
		{
			mathiasAnimator.SetTrigger("Reset");
		}

		// Set jade  talking animation
		yield return new WaitForSeconds(0.5f);
		jadeAnimator.SetTrigger("Talking");

		// Coroutine End
		yield break;
	}

	private IEnumerator JadeFacingRight()
	{



		// Set jade  talking animation
		yield return new WaitForSeconds(0.5f);
		jadeAnimator.SetTrigger("FacingRight");

		// Coroutine End
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
		// Set jade  talking animation
		yield return new WaitForSeconds(0.5f);
		jadeAnimator.SetTrigger("Talking");

		// Reset Mathias animation
		mathiasAnimator.SetTrigger("Reset");

		// Set jade idle animation
		yield return new WaitForSeconds(0.5f);
		jadeAnimator.SetTrigger("Reset");

		// Stop car animation
		car.SetTrigger("Idle");
		// Stop animation by parents to characters
		mathiasCharacter.transform.SetParent(null);
		jadeCharacter.transform.SetParent(null);

		// Stop ambiance sound
		audioManager.PlayAmbianceSound(false);
		yield return new WaitForSeconds(2f);
		audioManager.PlayClosedDoorSound(true);

		jadeAnimator.SetTrigger("FadOut");
		car.SetTrigger("FadOut");

		// Hide the dialogues box
		yield return StartCoroutine(DialogueBox.Instance.ShowDialogueBox(false));

		// Background fad out animation
		car.SetTrigger("FadOut");
		yield return new WaitForSeconds(1.5f);

		GameSystem.Instance.LoadNextScene();
	}
}
