using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if (UNITY_EDITOR) 
public class CreateDialogueElementList
{
    [MenuItem("Assets/Create/Dialogue Element List")]
    public static OneDialogueElementList Create()
    {
        OneDialogueElementList asset = ScriptableObject.CreateInstance<OneDialogueElementList>();
        asset.ElementList = new List<OneDialogueElement>();
        asset.startingIndex = 1;

        OneDialogueElement newElement = new OneDialogueElement();
        newElement.IsThereChoices = false;
        newElement.Content = "ERROR ! \n Si vous arrivez à cette embranchement, cela signifie que quelque chose s'est mal passé, où que vous avez explorer du contenu en cours de construction. Appuyez sur n'importe quelle touche pour revenir au début.";
        newElement.Branching = new OneDialogueBranching();
        newElement.FollowUpDialogueElement = 0;
        asset.ElementList.Add(newElement);
        AssetDatabase.CreateAsset(asset, "Assets/DialogueElementList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
#endif