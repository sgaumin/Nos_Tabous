using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemScript : MonoBehaviour
{
    public OneDialogueElementList DialogueContent;
    private int indexDialogue;
    private int indexDialogueNew;
    private int indexChoix;
    public static int indexBranching;
    private string text;
    private bool updateText;

    // Start is called before the first frame update
    void Start()
    {
        indexDialogue = 0;
        indexDialogueNew = 0;
        indexChoix = 0;
        indexBranching = -1;

        for(int i = 0; i < DialogueContent.BranchingList.Count; i++)
        {
            for(int j = 0; j< DialogueContent.BranchingList[i].ChoiceList.Count; j++)
            {
                DialogueContent.BranchingList[i].ChoiceList[j].IsThere = true;
            }
        }
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        updateText = false;
        if (DialogueContent.ElementList[indexDialogue].IsThereChoices)
        {
            indexChoix = 1;
            for (int i = 0; i < DialogueContent.BranchingList[DialogueContent.ElementList[indexDialogue].ChoiceID].ChoiceList.Count; i++)
            {
                if (DialogueContent.BranchingList[DialogueContent.ElementList[indexDialogue].ChoiceID].ChoiceList[i].IsThere)
                {
                    if (Input.GetKeyDown(indexChoix.ToString()) || Input.GetKeyDown(string.Concat("[", indexChoix.ToString(), "]")))
                    {
                        indexDialogueNew = DialogueContent.BranchingList[DialogueContent.ElementList[indexDialogue].ChoiceID].ChoiceList[i].FollowUpDialogueElement;
                        DialogueContent.BranchingList[DialogueContent.ElementList[indexDialogue].ChoiceID].ChoiceList[i].IsThere = false;
                        updateText = true;

                    }
                    indexChoix++;
                }
                    
                
            }
            if (indexChoix == 1 && Input.anyKeyDown)
            {
                indexDialogueNew = DialogueContent.ElementList[indexDialogue].FollowUpDialogueElement;
                updateText = true;
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                indexDialogueNew = DialogueContent.ElementList[indexDialogue].FollowUpDialogueElement;
                updateText = true;
            }
        }
        if (updateText)
        {

            indexDialogue = indexDialogueNew;
            
            UpdateText();
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
            indexBranching = DialogueContent.ElementList[indexDialogue].ChoiceID;
        }
        else
        {
            indexBranching = -1;
        }
        /*
        if (DialogueContent.ElementList[indexDialogue].IsThereChoices)
        {
            text = string.Concat(text, "\n");
            indexChoix = 1;

            for(int i = 0; i< DialogueContent.BranchingList[DialogueContent.ElementList[indexDialogue].ChoiceID].ChoiceList.Count; i++)
            {
                if (DialogueContent.BranchingList[DialogueContent.ElementList[indexDialogue].ChoiceID].ChoiceList[i].IsThere)
                {
                    text = string.Concat(text, "\n", indexChoix.ToString(), ". ", DialogueContent.BranchingList[DialogueContent.ElementList[indexDialogue].ChoiceID].ChoiceList[i].Content);
                    indexChoix++;
                }
                 
            }
        }*/
        gameObject.GetComponent<Text>().text = text;
    }
}
