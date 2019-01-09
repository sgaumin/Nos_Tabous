using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemScript : MonoBehaviour
{
    public OneDialogueElementList DialogueContent;
    private int indexDialogue;
    private int indexDialogueNew;
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        indexDialogue = 0;
        indexDialogueNew = 0;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueContent.ElementList[indexDialogue].IsThereChoices)
        {
            for (int i = 0; i < DialogueContent.ElementList[indexDialogue].ChoiceList.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()) || Input.GetKeyDown(string.Concat("[", (i + 1).ToString(), "]")))
                {
                    indexDialogueNew = DialogueContent.ElementList[indexDialogue].ChoiceList[i].FollowUpDialogueElement;
                }
                if (indexDialogue != indexDialogueNew)
                {
                    indexDialogue = indexDialogueNew;
                    UpdateText();                    
                }
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                indexDialogue= DialogueContent.ElementList[indexDialogue].FollowUpDialogueElement;
                indexDialogueNew = indexDialogue;
                UpdateText();
            }
        }
    }

    void UpdateText()
    {
        if (DialogueContent.ElementList[indexDialogue].PlayerIsTalking)
        {
            text = string.Concat("<i>",DialogueContent.ElementList[indexDialogue].Content,"</i>");
        }
        else
        {
            text = DialogueContent.ElementList[indexDialogue].Content;
        }
        

        if (DialogueContent.ElementList[indexDialogue].IsThereChoices)
        {
            text = string.Concat(text, "\n");

            for(int i = 0; i< DialogueContent.ElementList[indexDialogue].ChoiceList.Count; i++)
            {
                text = string.Concat(text,"\n", (i+1).ToString(), ". ", DialogueContent.ElementList[indexDialogue].ChoiceList[i].Content); 
            }
        }
        gameObject.GetComponent<Text>().text = text;
    }
}
