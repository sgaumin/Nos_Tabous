using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class TextChoixScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int ChoiceNumber;
    public OneDialogueElementList DialogueContent;
    private string text;
    private int indexChoice;
    private bool firstClick;
    private bool firstClickbis;
    private bool isHighlighted;


    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEvent)
    {
            DialogueSystemScript.clickedChoice = ChoiceNumber;      
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        isHighlighted = true;
               
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        isHighlighted = false;
    }
    // Update is called once per frame
    void Update()
    {
       

        text = "";
        indexChoice = 1;

        if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].IsThereChoices)
        {
            for (int i = 0; i < DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList.Count; i++)
            {
                if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].IsThere || DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].LessTabouContent != "")
                {
                    if (indexChoice == ChoiceNumber)
                    {
                        if (isHighlighted||ScriptAreYouOverTextBox.WhoIsHighlighted ==ChoiceNumber)
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
        gameObject.GetComponent<Text>().text = text;
        
        
    }


}
