using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager1 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator henriAnimator;
    [SerializeField] private GameObject dialogues;

    [SerializeField] private AudioManager1 audioLevel;

    private Character mathiasCharacter;
    private Character henriCharacter;
    private int indexCount;

    void Start()
    {
        ShowDialogues(false);
        StartCoroutine(StartLevel());

        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

        // Hide Mathias & Henri Character at Starting
        mathiasCharacter.gameObject.SetActive(false);
        henriCharacter.gameObject.SetActive(false);

        indexCount = 0;
    }

    // TO DO: Launch Tabou animation when clicking on tabou choice
    // TO DO: Create a Delegate function on DialogueSystemScript
    private void Update()
    {
        // Set Animation and Sound according to the Dialogue Index
        if (DialogueSystemScript.indexDialogue == indexCount)
            return;

        indexCount = DialogueSystemScript.indexDialogue;

        if (DialogueSystemScript.indexDialogue == 2)
            StartCoroutine(SecondStepLevel());

        if (DialogueSystemScript.indexDialogue == 3)
            StartCoroutine(ThirdStepLevel());

        if (DialogueSystemScript.indexDialogue == 4)
            StartCoroutine(SecondStepLevel());

        if (DialogueSystemScript.indexDialogue == 5)
            StartCoroutine(ThirdStepLevel());

        if (DialogueSystemScript.indexDialogue == 6)
            StartCoroutine(SecondStepLevel());

        if (DialogueSystemScript.indexDialogue == 7)
            StartCoroutine(ThirdStepLevel());

        if (DialogueSystemScript.indexDialogue == 8)
            StartCoroutine(SecondStepLevel());

        if (DialogueSystemScript.indexDialogue == 9)
            StartCoroutine(ThirdStepLevel());

        if (DialogueSystemScript.indexDialogue == 10)
            StartCoroutine(LastStepLevel());
    }

    IEnumerator StartLevel()
    {

        // Waiting before starting
        yield return new WaitForSeconds(0.5f);
        henriCharacter.gameObject.SetActive(true);

        // Henri phone call animation
        yield return new WaitForSeconds(1f);
        henriAnimator.SetTrigger("BackCallStarting");

        // Phone sound playing
        yield return new WaitForSeconds(1f);
        audioLevel.PlayPhoneSound(true);

        // Show Mathias character
        yield return new WaitForSeconds(2f);
        mathiasCharacter.gameObject.SetActive(true);

        // Mathias pick up the phone animation
        yield return new WaitForSeconds(2f);
        mathiasAnimator.SetTrigger("CallStarting");

        // End phone sound
        audioLevel.PlayPhoneSound(false);
        yield return new WaitForSeconds(0.5f);

        // Henri turn
        henriCharacter.Flip();
        yield return new WaitForSeconds(1f);

        // Henri start talking animation
        henriAnimator.SetTrigger("BackCallTalking");
        yield return new WaitForSeconds(0.5f);

        // Show dialogues box 
        ShowDialogues(true);

        // Coroutine End
        yield break;
    }

    IEnumerator SecondStepLevel()
    {
        // Set Henri calling idle animation
        henriAnimator.SetTrigger("BackCallIdle");
        yield return new WaitForSeconds(0.5f);

        // Set Mathias calling talking animation
        mathiasAnimator.SetTrigger("CallTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator ThirdStepLevel()
    {

        // Set Mathias calling idle animation
        mathiasAnimator.SetTrigger("CallIdle");

        // Set Henri calling talking animation
        yield return new WaitForSeconds(0.5f);
        henriAnimator.SetTrigger("BackCallTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator LastStepLevel()
    {
        // Animation hang up Mathias
        mathiasAnimator.SetTrigger("CallIdle");
        mathiasAnimator.SetTrigger("CallEnding");
        yield return new WaitForSeconds(0.5f);

        // Henri stop talking animation
        henriAnimator.SetTrigger("BackCallIdle");
        yield return new WaitForSeconds(0.5f);

        // Henri turn
        henriCharacter.Flip();
        yield return new WaitForSeconds(1f);

        // Animation hang up Henri
        henriAnimator.SetTrigger("BackCallEnding");
        yield return new WaitForSeconds(0.5f);

        // Animation FadOut Henri
        henriAnimator.SetTrigger("FadOut");

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
