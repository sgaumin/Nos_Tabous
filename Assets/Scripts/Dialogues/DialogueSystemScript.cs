﻿using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemScript : MonoBehaviour
{
	public static int indexDialogue;
	public static bool isTabou;

	private int indexDialogueNew;
	private int indexChoix;
	public static int clickedChoice;
	private string text;
	private bool updateText;
	private bool updateIsTotal;
	public static int numberedChoiceMode = 2; //0 means : no number. 1 means : 1. 3. 4. if 2 has disappear. 2 means : 3. becomes 2., and 4. becomes 3., if 2 has disappear.

	private bool FadeInNotOut;
	private bool IsReady;
	public static float opacity;
	public static int choiceToFadeOut;

	public OneDialogueElementList DialogueContent { get; set; }

	private void Start()
	{
		indexDialogue = DialogueContent.startingIndex;
		indexDialogueNew = DialogueContent.startingIndex;
		IsReady = false;
		FadeInNotOut = true;
		indexChoix = 0;
		clickedChoice = -1;
		choiceToFadeOut = 0;

		for (int i = 0; i < DialogueContent.ElementList.Count; i++)
		{
			for (int j = 0; j < DialogueContent.ElementList[i].Branching.ChoiceList.Count; j++)
			{
				DialogueContent.ElementList[i].Branching.ChoiceList[j].IsThere = true;
			}
		}

		//DialogueContent.ElementList[0].FollowUpDialogueElement = DialogueContent.startingIndex;
		opacity = 0;
		UpdateText();
	}

	private void Update()
	{
		gameObject.GetComponent<Text>().fontSize = BestFitText.BestFitFrontSize;

		if (IsReady)
		{
			updateText = false;
			updateIsTotal = false;

			if (DialogueContent.ElementList[indexDialogue].IsThereChoices)
			{
				indexChoix = 1;
				isTabou = false;

				for (int i = 0; i < DialogueContent.ElementList[indexDialogue].Branching.ChoiceList.Count; i++)
				{
					if (DialogueContent.ElementList[indexDialogue].Branching.ChoiceList[i].IsThere || DialogueContent.ElementList[indexDialogue].Branching.ChoiceList[i].LessTabouContent != "")
					{

						if ((numberedChoiceMode != 1 && (Input.GetKeyDown(indexChoix.ToString()) || Input.GetKeyDown(string.Concat("[", indexChoix.ToString(), "]")))) || (Input.GetKeyDown((i + 1).ToString()) || Input.GetKeyDown(string.Concat("[", (i + 1).ToString(), "]"))) || (clickedChoice == indexChoix))
						{
							clickedChoice = -1;
							if (DialogueContent.ElementList[indexDialogue].Branching.ChoiceList[i].IsTabou && DialogueContent.ElementList[indexDialogue].Branching.ChoiceList[i].IsThere)
							{
								choiceToFadeOut = indexChoix;
								updateText = true;
								updateIsTotal = false;
								isTabou = true;
							}
							else
							{
								indexDialogueNew = DialogueContent.ElementList[indexDialogue].Branching.ChoiceList[i].FollowUpDialogueElement;
								updateText = true;
								updateIsTotal = true;
							}
						}
						indexChoix++;
					}
				}
				if (indexChoix == 1 && Input.anyKeyDown)
				{
					indexDialogueNew = DialogueContent.ElementList[indexDialogue].FollowUpDialogueElement;
					updateText = true;
					updateIsTotal = true;
					clickedChoice = -1;
				}
			}
			else
			{
				if (Input.anyKeyDown)
				{
					indexDialogueNew = DialogueContent.ElementList[indexDialogue].FollowUpDialogueElement;
					updateText = true;
					updateIsTotal = true;
					clickedChoice = -1;
				}
			}
			if (updateText)
			{
				clickedChoice = -1;
				if (updateIsTotal)
				{
					if (indexDialogue != indexDialogueNew)
					{
						AudioManagerClic.Instance.GetComponent<AudioSource>().pitch = 1f;
						AudioManagerClic.Instance.PlayClicSound();
					}
					if (0 >= indexDialogueNew || indexDialogueNew >= DialogueContent.ElementList.Count)
					{
						indexDialogueNew = 0;
					}
					IsReady = false;
					FadeInNotOut = false;
				}
				else
				{
					AudioManagerClic.Instance.GetComponent<AudioSource>().pitch = 0.8f;
					AudioManagerClic.Instance.PlayClicSound();
				}
				UpdateText();
			}
		}
		else
		{
			if (FadeInNotOut)
			{
				opacity += Time.deltaTime * 2;
				if (opacity >= 1f)
				{
					opacity = 1;
					IsReady = true;
					FadeInNotOut = false;
				}
				UpdateText();
			}
			else
			{
				opacity -= Time.deltaTime * 2;
				if (opacity <= 0f)
				{
					opacity = 0;
					indexDialogue = indexDialogueNew;
					FadeInNotOut = true;
				}
				UpdateText();
			}
		}
	}

	private void UpdateText()
	{
		text = DialogueContent.ElementList[indexDialogue].Content;
		text = string.Concat(text, "\n");
		Color Couleur;
		Couleur = DialogueContent.ElementList[indexDialogue].Couleur;
		Couleur.a = opacity;
		gameObject.GetComponent<Text>().color = Couleur;
		gameObject.GetComponent<Text>().text = text;
	}
}
