using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemScript : MonoBehaviour
{
    public OneDialogueElementList DialogueContent;
    public static int indexDialogue;
    public static bool isTabou;

    private int indexDialogueNew;
    private int indexChoix;
    public static int clickedChoice;
    private string text;
    private bool updateText;
    public static int numberedChoiceMode = 1; //0 means : no number. 1 means : 1. 3. 4. if 2 has disappear. 2 means : 3. becomes 2., and 4. becomes 3., if 2 has disappear.


    // Start is called before the first frame update
    void Start()
    {
        indexDialogue = DialogueContent.startingIndex;
        indexDialogueNew = DialogueContent.startingIndex;

        indexChoix = 0;
        clickedChoice = -1;

        for (int i = 0; i < DialogueContent.ElementList.Count; i++)
        {
            for (int j = 0; j < DialogueContent.ElementList[i].Branching.ChoiceList.Count; j++)
            {
                DialogueContent.ElementList[i].Branching.ChoiceList[j].IsThere = true;
            }
        }

        //DialogueContent.ElementList[0].FollowUpDialogueElement = DialogueContent.startingIndex;

        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        updateText = false;

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
                            DialogueContent.ElementList[indexDialogue].Branching.ChoiceList[i].IsThere = false;
                            updateText = true;
                            isTabou = true;
                        }
                        else
                        {
                            indexDialogueNew = DialogueContent.ElementList[indexDialogue].Branching.ChoiceList[i].FollowUpDialogueElement;
                            updateText = true;
                        }
                    }
                    indexChoix++;
                }
            }
            if (indexChoix == 1 && Input.anyKeyDown)
            {
                indexDialogueNew = DialogueContent.ElementList[indexDialogue].FollowUpDialogueElement;
                updateText = true;
                clickedChoice = -1;
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                indexDialogueNew = DialogueContent.ElementList[indexDialogue].FollowUpDialogueElement;
                updateText = true;
                clickedChoice = -1;
            }
        }
        if (updateText)
        {
            clickedChoice = -1;
            if (0 < indexDialogueNew && indexDialogueNew < DialogueContent.ElementList.Count)
            {
                indexDialogue = indexDialogueNew;
            }
            else
            {
                indexDialogue = 0;
            }

            UpdateText();
        }
    }

    void UpdateText()
    {

        text = DialogueContent.ElementList[indexDialogue].Content;

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
