using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR) 
public class OneDialogueElementEditor : EditorWindow
{
    public OneDialogueElementList DialogueElementList;
    private int viewindex = 0;
    Vector2 scrollPos= new Vector2(0,0);

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
                if (viewindex > 1)
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
                viewindex = Mathf.Clamp(EditorGUILayout.IntField("Current Element", viewindex, GUILayout.ExpandWidth(false)), 1, DialogueElementList.ElementList.Count-1);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField("on   " + (DialogueElementList.ElementList.Count-1).ToString(), "", GUILayout.ExpandWidth(false));

                GUILayout.Space(5);

                DialogueElementList.startingIndex = EditorGUILayout.IntField("StartingElement", DialogueElementList.startingIndex, GUILayout.ExpandWidth(false));

                if(0< DialogueElementList.startingIndex && DialogueElementList.startingIndex<DialogueElementList.ElementList.Count && GUILayout.Button("Go to", GUILayout.ExpandWidth(false)))
                {
                    viewindex = DialogueElementList.startingIndex;
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("This element is accessible from : ");
                for(int i = 1; i < DialogueElementList.ElementList.Count; i++)
                {
                    bool b = false;
                    if (DialogueElementList.ElementList[i].FollowUpDialogueElement == viewindex)
                    {
                        b = true;
                    }
                    else if (DialogueElementList.ElementList[i].IsThereChoices)
                    {
                        for(int j = 0; j< DialogueElementList.ElementList[i].Branching.ChoiceList.Count; j++)
                        {
                            if (DialogueElementList.ElementList[i].Branching.ChoiceList[j].FollowUpDialogueElement == viewindex && (DialogueElementList.ElementList[i].Branching.ChoiceList[j].IsTabou == false || DialogueElementList.ElementList[i].Branching.ChoiceList[j].LessTabouContent != ""))
                            {
                                b = true;
                            }
                        }
                    }

                    if (b)
                    {
                        if (GUILayout.Button(i.ToString(), GUILayout.ExpandWidth(false)))
                        {
                            viewindex = i;
                        }
                        GUILayout.Space(1);
                    }
                    
                }
                EditorGUILayout.LabelField(" ");
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
                DialogueElementList.ElementList[viewindex].WhoIsSpeaking = EditorGUILayout.TextField("Name", DialogueElementList.ElementList[viewindex].WhoIsSpeaking as string);

                GUILayout.Space(10);

                DialogueElementList.ElementList[viewindex].Content = EditorGUILayout.TextArea(DialogueElementList.ElementList[viewindex].Content as string, GUILayout.Height(100.0f));

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                DialogueElementList.ElementList[viewindex].IsThereChoices = EditorGUILayout.Toggle("IsThereChoices", DialogueElementList.ElementList[viewindex].IsThereChoices, GUILayout.ExpandWidth(false));
                GUILayout.Space(5);
                DialogueElementList.ElementList[viewindex].FollowUpDialogueElement = EditorGUILayout.IntField("Followup Element", DialogueElementList.ElementList[viewindex].FollowUpDialogueElement, GUILayout.ExpandWidth(false));
                if (0 < DialogueElementList.ElementList[viewindex].FollowUpDialogueElement && DialogueElementList.ElementList[viewindex].FollowUpDialogueElement < DialogueElementList.ElementList.Count && GUILayout.Button("Go to", GUILayout.ExpandWidth(false)))
                {
                    viewindex = DialogueElementList.ElementList[viewindex].FollowUpDialogueElement;
                }
                GUILayout.Space(5);
                DialogueElementList.ElementList[viewindex].Couleur = EditorGUILayout.ColorField("Couleur", DialogueElementList.ElementList[viewindex].Couleur);

                GUILayout.EndHorizontal();

                if (DialogueElementList.ElementList[viewindex].IsThereChoices)
                {
                                     

                    GUILayout.Space(10);
                    
                    if (GUILayout.Button("Add Choice", GUILayout.ExpandWidth(false)))
                    {
                        AddChoice();
                    }
                
                    GUILayout.Space(10);
                    
                    if (DialogueElementList.ElementList[viewindex].Branching.ChoiceList.Count > 0)
                    {
                        
                        scrollPos = EditorGUILayout.BeginScrollView(scrollPos/*, GUILayout.Width(), GUILayout.Height(100)*/);
                        for(int index = 0; index < DialogueElementList.ElementList[viewindex].Branching.ChoiceList.Count; index++)
                        {
                            DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].Content = EditorGUILayout.TextArea(DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].Content as string, GUILayout.Height(50.0f));
                            DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].IsTabou = EditorGUILayout.Toggle("IsTabou", DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].IsTabou);
                            if (DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].IsTabou)
                            {
                                GUILayout.Space(5);
                                DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].LessTabouContent = EditorGUILayout.TextArea(DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].LessTabouContent as string, GUILayout.Height(50.0f));
                            }
                            GUILayout.Space(5);
                            GUILayout.BeginHorizontal();
                            DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].FollowUpDialogueElement = EditorGUILayout.IntField(DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].FollowUpDialogueElement, GUILayout.ExpandWidth(false));
                            if(0< DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].FollowUpDialogueElement && DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].FollowUpDialogueElement < DialogueElementList.ElementList.Count && GUILayout.Button("Go to", GUILayout.ExpandWidth(false)))
                            {
                                viewindex = DialogueElementList.ElementList[viewindex].Branching.ChoiceList[index].FollowUpDialogueElement;
                            }
                            GUILayout.EndHorizontal();
                            if (GUILayout.Button("Delete Choice", GUILayout.ExpandWidth(false)))
                            {
                                DeleteChoice(index);
                            }
                            GUILayout.Space(10);
                        }
                        EditorGUILayout.EndScrollView() ;

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
        DialogueElementList = CreateDialogueElementList.Create();
        if (DialogueElementList)
        {
            DialogueElementList.ElementList = new List<OneDialogueElement>();
            DialogueElementList.startingIndex = 1;

            OneDialogueElement newElement = new OneDialogueElement();
            newElement.IsThereChoices = false;
            newElement.Content = "ERROR ! \n Si vous arrivez à cette embranchement, cela signifie que quelque chose s'est mal passé, où que vous avez explorer du contenu en cours de construction. Appuyez sur n'importe quelle touche pour revenir au début.";
            newElement.Branching = new OneDialogueBranching();
            newElement.FollowUpDialogueElement = 0;
            DialogueElementList.ElementList.Add(newElement);

            AddElement();
            string relPath = AssetDatabase.GetAssetPath(DialogueElementList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
        viewindex = 1;
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
        newElement.IsThereChoices = false;
        newElement.Content = "Put some text here";
        newElement.Branching = new OneDialogueBranching();
        newElement.Branching.ChoiceList = new List<OneDialogueChoice>();
        newElement.FollowUpDialogueElement = viewindex + 1;
        newElement.Couleur = Color.white;
        DialogueElementList.ElementList.Add(newElement);
        viewindex = DialogueElementList.ElementList.Count-1;
    }

    void DeleteElement(int index)
    {
        for(int i = 1; i< DialogueElementList.ElementList.Count; i++)
        {
            if (DialogueElementList.ElementList[i].FollowUpDialogueElement == index) DialogueElementList.ElementList[i].FollowUpDialogueElement = 0;
            if (DialogueElementList.ElementList[i].FollowUpDialogueElement > index) DialogueElementList.ElementList[i].FollowUpDialogueElement--;
            for(int j = 0; j< DialogueElementList.ElementList[i].Branching.ChoiceList.Count; j++)
            {
                if (DialogueElementList.ElementList[i].Branching.ChoiceList[j].FollowUpDialogueElement == index) DialogueElementList.ElementList[i].Branching.ChoiceList[j].FollowUpDialogueElement = 0;
                if (DialogueElementList.ElementList[i].Branching.ChoiceList[j].FollowUpDialogueElement > index) DialogueElementList.ElementList[i].Branching.ChoiceList[j].FollowUpDialogueElement--;
            }
        }
        DialogueElementList.ElementList.RemoveAt(index);
    }

    void AddChoice()
    {
        OneDialogueChoice newChoice = new OneDialogueChoice();
        int i = DialogueElementList.ElementList[viewindex].Branching.ChoiceList.Count;
        newChoice.Content = "Choice " + i.ToString();
        newChoice.FollowUpDialogueElement = viewindex + 1;
        newChoice.IsThere = true;
        newChoice.IsTabou = false;
        newChoice.LessTabouContent = "";
        DialogueElementList.ElementList[viewindex].Branching.ChoiceList.Add(newChoice);

    }

    void DeleteChoice(int index)
    {
        DialogueElementList.ElementList[viewindex].Branching.ChoiceList.RemoveAt(index);
    }
}
#endif