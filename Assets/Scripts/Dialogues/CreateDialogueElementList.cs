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

        AssetDatabase.CreateAsset(asset, "Assets/DialogueElementList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
#endif