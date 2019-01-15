using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChoixScript : MonoBehaviour
{
    public int ChoiceNumber;
    public OneDialogueElementList DialogueContent;
    private string text;
    private int indexChoice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text = "";
        indexChoice = 1;
        if (DialogueSystemScript.indexBranching >= 0)
        {
            for (int i = 0; i < DialogueContent.BranchingList[DialogueSystemScript.indexBranching].ChoiceList.Count; i++)
            {
                if (DialogueContent.BranchingList[DialogueSystemScript.indexBranching].ChoiceList[i].IsThere)
                {
                    if (indexChoice == ChoiceNumber)
                    {
                        text = string.Concat(ChoiceNumber.ToString(), ". ", DialogueContent.BranchingList[DialogueSystemScript.indexBranching].ChoiceList[i].Content);
                    }
                    indexChoice++;
                }
            }
        }
        

        gameObject.GetComponent<Text>().text = text;
    }
}
