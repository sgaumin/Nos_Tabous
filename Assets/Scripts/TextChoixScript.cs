using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class TextChoixScript : MonoBehaviour
{
    public int ChoiceNumber;
    public OneDialogueElementList DialogueContent;
    private string text;
    private int indexChoice;
    

    void OnMouseDown()
    {
        DialogueSystemScript.clickedChoice = ChoiceNumber;
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
                if (DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].IsThere || DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Branching.ChoiceList[i].LessTabouContent!="")
                {
                    if (indexChoice == ChoiceNumber)
                    {
                        if (DialogueSystemScript.numberedChoiceMode == 1)
                        {
                            text = string.Concat((i + 1).ToString(), ". ");
                        }
                        if (DialogueSystemScript.numberedChoiceMode == 2)
                        {
                            text = string.Concat(indexChoice.ToString(), ". ");
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
