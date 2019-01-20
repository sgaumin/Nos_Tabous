using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneDialogueElement
{
    public string WhoIsSpeaking;
    [TextArea(3, 10)]
    public string Content;
    public bool IsThereChoices;
    public Color Couleur;
    public OneDialogueBranching Branching;
    public int FollowUpDialogueElement;
}
