using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneDialogueElement
{
    [TextArea(3, 10)]
    public string Content;
    public bool IsThereChoices;
    public OneDialogueBranching Branching;
    public int FollowUpDialogueElement;
}
