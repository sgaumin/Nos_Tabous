using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneDialogueElement
{
    public bool PlayerIsTalking;
    public string Content;
    public bool IsThereChoices;
    public int ChoiceID;
    public int FollowUpDialogueElement;
}
