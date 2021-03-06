﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextChoixScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private int ChoiceNumber;

	private string text;
	private int indexChoice;
	private bool firstClick;
	private bool firstClickbis;
	private bool isHighlighted;
	private bool isReady;
	private bool FadeInNotOut;
	private float opacity;
	private int indexDialogueSave;

	public OneDialogueElementList DialogueContent { get; set; }

	void Start()
	{
		isReady = false;
		FadeInNotOut = true;
		opacity = 0;
	}

	public void OnPointerDown(PointerEventData pointerEvent)
	{
		if (isReady)
		{
			DialogueSystemScript.clickedChoice = ChoiceNumber;
		}
	}

	public void OnPointerEnter(PointerEventData pointerEvent) => isHighlighted = true;

	public void OnPointerExit(PointerEventData pointerEvent) => isHighlighted = false;

	void Update()
	{
		if (isReady)
		{
			if (DialogueSystemScript.choiceToFadeOut == ChoiceNumber || DialogueSystemScript.opacity < 1f)
			{
				isReady = false;
				FadeInNotOut = false;
				DialogueSystemScript.choiceToFadeOut = 0;
				indexDialogueSave = DialogueSystemScript.indexDialogue;
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
					isReady = true;
					FadeInNotOut = false;
				}
			}
			else
			{
				opacity -= Time.deltaTime * 2;
				if (opacity <= 0f)
				{
					opacity = 0;
					indexChoice = 1;
					if (indexDialogueSave == DialogueSystemScript.indexDialogue)
					{
						for (int i = 0; i < DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList.Count; i++)
						{
							if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].IsThere || DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].LessTabouContent != "")
							{
								if (indexChoice == ChoiceNumber)
								{
									DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].IsThere = false;
									if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].LessTabouContent == "")
									{
										opacity = 1f;
										isReady = true;
									}
									else
									{
										FadeInNotOut = true;
									}
								}
								indexChoice++;
							}
						}
					}
					else
					{
						FadeInNotOut = true;
					}
				}
			}
		}

		text = "";
		gameObject.GetComponent<Text>().fontSize = BestFitText.BestFitFrontSize;
		indexChoice = 1;

		if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].IsThereChoices)
		{
			for (int i = 0; i < DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList.Count; i++)
			{
				if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].IsThere || DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].LessTabouContent != "")
				{
					if (indexChoice == ChoiceNumber)
					{
						if (isHighlighted || MouseDetection.WhoIsHighlighted == ChoiceNumber)
						{
							text = "> ";
						}
						else
						{
							text = "   ";
						}

						if (DialogueSystemScript.numberedChoiceMode == 1)
						{
							text = string.Concat(text, (i + 1).ToString(), ". ");
						}
						if (DialogueSystemScript.numberedChoiceMode == 2)
						{
							text = string.Concat(text, indexChoice.ToString(), ". ");
						}
						if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].IsThere)
						{
							text = string.Concat(text, DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].Content);
						}
						else
						{
							text = string.Concat(text, DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].LessTabouContent);
						}
					}
					indexChoice++;
				}
			}
		}
		Color Couleur = Color.white;
		Couleur.a = opacity;
		gameObject.GetComponent<Text>().text = text;
		gameObject.GetComponent<Text>().color = Couleur;
	}
}
