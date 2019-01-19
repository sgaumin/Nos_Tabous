using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private Text[] textDialogues;

    public void ShowTexts(bool show) {
        for (int i = 0; i < textDialogues.Length; i++)
        {
            if (show)
            {
                textDialogues[i].gameObject.SetActive(true);
            }
            else
            {
                textDialogues[i].gameObject.SetActive(false);
            }
        }
    }

}
