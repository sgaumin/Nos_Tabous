using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneDialogueChoice
{
    [TextArea(3, 10)]
    public string Content;
    public int FollowUpDialogueElement;
    public bool IsTabou;
    public bool IsThere;
    public string LessTabouContent;
}
