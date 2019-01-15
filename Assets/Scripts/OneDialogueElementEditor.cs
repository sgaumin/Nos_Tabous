using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OneDialogueElementEditor : EditorWindow
{
    public OneDialogueElementList DialogueElementList;
    private int viewindex = 0;

    [MenuItem("Window/Dialogue Element Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(OneDialogueElementEditor));
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Dialogue Element Editor", EditorStyles.boldLabel);
        if (DialogueElementList != null)
        {
            if (GUILayout.Button("Show Element List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = DialogueElementList;
            }
        }
        if (GUILayout.Button("Open Element List"))
        {
            OpenElementList();
        }
        if (GUILayout.Button("New Element List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = DialogueElementList;
        }
        GUILayout.EndHorizontal();

        if (DialogueElementList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Element List", GUILayout.ExpandWidth(false)))
            {
                CreateNewDialogueElementList();
            }
            if (GUILayout.Button("Open Existing Element List", GUILayout.ExpandWidth(false)))
            {
                OpenElementList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (DialogueElementList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewindex > 0)
                    viewindex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewindex < DialogueElementList.ElementList.Count-1)
                {
                    viewindex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Element", GUILayout.ExpandWidth(false)))
            {
                AddElement();
            }
            if (GUILayout.Button("Delete Element", GUILayout.ExpandWidth(false)))
            {
                DeleteElement(viewindex);
            }

            GUILayout.EndHorizontal();
            if (DialogueElementList.ElementList == null)
                Debug.Log("wtf");
            if (DialogueElementList.ElementList.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewindex = Mathf.Clamp(EditorGUILayout.IntField("Current Element", viewindex, GUILayout.ExpandWidth(false)), 0, DialogueElementList.ElementList.Count-1);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField("on   " + (DialogueElementList.ElementList.Count-1).ToString(), "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                DialogueElementList.ElementList[viewindex].Content = EditorGUILayout.TextArea(DialogueElementList.ElementList[viewindex].Content as string, GUILayout.Height(100.0f));

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                /*DialogueElementList.ElementList[viewindex].PlayerIsTalking = EditorGUILayout.Toggle("PlayerIsTalking", DialogueElementList.ElementList[viewindex].PlayerIsTalking, GUILayout.ExpandWidth(false));*/
                DialogueElementList.ElementList[viewindex].IsThereChoices = EditorGUILayout.Toggle("IsThereChoices", DialogueElementList.ElementList[viewindex].IsThereChoices/*, GUILayout.ExpandWidth(false)*/);
                GUILayout.Space(5);
                DialogueElementList.ElementList[viewindex].FollowUpDialogueElement = EditorGUILayout.IntField("Followup Element", DialogueElementList.ElementList[viewindex].FollowUpDialogueElement/*, GUILayout.ExpandWidth(false)*/);
                GUILayout.EndHorizontal();

                if (DialogueElementList.ElementList[viewindex].IsThereChoices)
                {
                    if (DialogueElementList.ElementList[viewindex].ChoiceID < 0)
                    {
                        DialogueElementList.ElementList[viewindex].ChoiceID = 0;
                    }

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
                    {
                        if (DialogueElementList.ElementList[viewindex].ChoiceID > 0)
                            DialogueElementList.ElementList[viewindex].ChoiceID--;
                    }
                    GUILayout.Space(5);
                    
                    DialogueElementList.ElementList[viewindex].ChoiceID = Mathf.Clamp(EditorGUILayout.IntField("Branching ID", DialogueElementList.ElementList[viewindex].ChoiceID, GUILayout.ExpandWidth(false)),0,DialogueElementList.BranchingList.Count - 1);

                    GUILayout.Space(5);

                    if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
                    {
                        DialogueElementList.ElementList[viewindex].ChoiceID++;
                        if (DialogueElementList.ElementList[viewindex].ChoiceID >= DialogueElementList.BranchingList.Count)
                        {
                            DialogueElementList.BranchingList.Add(new OneDialogueBranching());
                            DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList = new List<OneDialogueChoice>();
                        }
                    }
                    GUILayout.EndHorizontal();



                    GUILayout.Space(10);
                    
                    if (GUILayout.Button("Add Choice", GUILayout.ExpandWidth(false)))
                    {
                        AddChoice();
                    }
                
                    GUILayout.Space(10);
                    
                    if (DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList.Count > 0)
                    {
                        for(int index = 0; index < DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList.Count; index++)
                        {
                            DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList[index].Content = EditorGUILayout.TextArea(DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList[index].Content as string, GUILayout.Height(50.0f));
                            DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList[index].FollowUpDialogueElement = EditorGUILayout.IntField(DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList[index].FollowUpDialogueElement);
                            if (GUILayout.Button("Delete Choice", GUILayout.ExpandWidth(false)))
                            {
                                DeleteChoice(index);
                            }
                            GUILayout.Space(10);
                        }

                    }
                    else
                    {
                        GUILayout.Label("There is no choice yet.");
                    }
                }
                 

            }
            else
            {
                GUILayout.Label("This Dialogue Element List is Empty.");
            }
        }
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            DialogueElementList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(OneDialogueElementList)) as OneDialogueElementList;
        }

    }

    void CreateNewDialogueElementList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewindex = 0;
        DialogueElementList = CreateDialogueElementList.Create();
        if (DialogueElementList)
        {
            DialogueElementList.ElementList = new List<OneDialogueElement>();
            DialogueElementList.BranchingList = new List<OneDialogueBranching>
            {
                new OneDialogueBranching()
            };
            DialogueElementList.BranchingList[0].ChoiceList = new List<OneDialogueChoice>();
            string relPath = AssetDatabase.GetAssetPath(DialogueElementList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenElementList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Dialogue Element List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            DialogueElementList = AssetDatabase.LoadAssetAtPath(relPath, typeof(OneDialogueElementList)) as OneDialogueElementList;
            if (DialogueElementList.ElementList == null)
                DialogueElementList.ElementList = new List<OneDialogueElement>();
            if (DialogueElementList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddElement()
    {
        OneDialogueElement newElement = new OneDialogueElement();
        newElement.PlayerIsTalking = false;
        newElement.IsThereChoices = false;
        newElement.Content = "Text";
        newElement.ChoiceID = -1;
        newElement.FollowUpDialogueElement = viewindex + 1;
        DialogueElementList.ElementList.Add(newElement);
        viewindex = DialogueElementList.ElementList.Count;
    }

    void DeleteElement(int index)
    {
        DialogueElementList.ElementList.RemoveAt(index);
    }

    void AddChoice()
    {
        OneDialogueChoice newChoice = new OneDialogueChoice();
        newChoice.Content = "Text";
        newChoice.FollowUpDialogueElement = viewindex + 1;
        newChoice.IsThere = true;
        DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList.Add(newChoice);

    }

    void DeleteChoice(int index)
    {
        DialogueElementList.BranchingList[DialogueElementList.ElementList[viewindex].ChoiceID].ChoiceList.RemoveAt(index);
    }
}
