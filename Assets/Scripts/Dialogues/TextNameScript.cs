using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TextNameScript : MonoBehaviour
{

    public OneDialogueElementList DialogueContent;
    private Color Couleur;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Couleur = DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Couleur;
        Couleur.a = DialogueSystemScript.opacity;
        gameObject.GetComponent<Text>().fontSize = BestFitScript.fontsize*6/5;
        gameObject.GetComponent<Text>().color = Couleur;
        gameObject.GetComponent<Text>().text = DialogueContent.ElementList[DialogueSystemScript.indexDialogue].WhoIsSpeaking;
    }
}


