using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneDialogueElement
{
    public bool PlayerIsTalking;
    public bool IsThereChoices;
    public int FollowUpDialogueElement;
    public string Content;
    public List<OneDialogueChoice> ChoiceList;
}
