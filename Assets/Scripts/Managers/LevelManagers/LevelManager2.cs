using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager2 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator carolineAnimator;
    [SerializeField] private GameObject dialogues;

    [SerializeField] private AudioManager2 audioLevel;

    private Character mathiasCharacter;
    private Character carolineCharacter;
    private DialogueBox dialogueBox;
    private int indexCount;

    void Start()
    {
        // Deactivate fad in animation for Mathias
        mathiasAnimator.SetBool("IsFadIn", false);

        // Asign Character components
        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        carolineCharacter = carolineAnimator.gameObject.GetComponent<Character>();

        // Asign DialogueBox component
        dialogueBox = dialogues.GetComponent<DialogueBox>();

        // Hide Caroline Character at Starting
        carolineCharacter.gameObject.SetActive(false);

        // Index for checking the current IndexDialogue of DialogueSystemScript script 
        indexCount = 999;

        // Hide Dialogue Box
        ShowDialogues(false);
    }

    void Update()
    {
        if (DialogueSystemScript.indexDialogue == indexCount)
            return;

        indexCount = DialogueSystemScript.indexDialogue;

        // TO DO: Implement more flexible IF statement
        if (DialogueSystemScript.indexDialogue == 10)
            StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel() {

        yield return new WaitForSeconds(2f);

        // Show Caroline Character
        carolineCharacter.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Flip Mathias
        mathiasCharacter.Flip();
        yield return new WaitForSeconds(0.5f);

        // Caroline start talking animation
        carolineAnimator.SetTrigger("Talking");
        yield return new WaitForSeconds(0.5f);

        // Show dialogues box 
        ShowDialogues(true);
        yield return new WaitForSeconds(0.5f);

        // Show Texts into the dialogues box
        dialogueBox.ShowTexts(true);

        // Coroutine End
        yield break;
    }

    void ShowDialogues(bool activated)
    {
        if (dialogues != null)
        {
            if (activated)
                dialogues.SetActive(true);

            if (!activated)
                dialogues.SetActive(false);
        }
    }
}
