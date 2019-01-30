﻿using System.Collections;
using UnityEngine;

public class LevelManager1 : MonoBehaviour
{
    // TO DO: Create an abract class to avoid repetition in variables between all LevelManager
    // TO DO: Create objet Event per Step
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator henriAnimator;
    [SerializeField] private GameObject disclaimer;
    [SerializeField] private GameObject dialogues;
    [SerializeField] private GameObject nameDialogues;

    [SerializeField] private AudioManager1 audioManager;

    private Character mathiasCharacter;
    private Character henriCharacter;
    private DialogueBox dialogueBox;
    private int indexCount;

    void Start()
    {
        // Asign Character components
        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

        // Asign DialogueBox component
        dialogueBox = dialogues.GetComponent<DialogueBox>();

        // Hide Mathias & Henri Character at Starting
        mathiasCharacter.gameObject.SetActive(false);
        henriCharacter.gameObject.SetActive(false);

        // Hide
        disclaimer.SetActive(false);

        // Index for checking the current IndexDialogue of DialogueSystemScript script 
        indexCount = 999;

        // Hide Dialogue Box
        ShowDialogues(false);
    }

    // TO DO: Create a Delegate function on DialogueSystemScript for isTabou & indexDialogue variables
    private void Update()
    {
        // Check if the choice is Tabou and set the animation
        if (DialogueSystemScript.isTabou)
            StartCoroutine(TabouStepLevel());

        // Set Animation and Sound according to the Dialogue Index
        if (DialogueSystemScript.indexDialogue == indexCount)
            return;

        indexCount = DialogueSystemScript.indexDialogue;

        // Start Level scripting
        if (indexCount == 0)
            StartCoroutine(StartLevel());

        // Mathias Talking
        if (indexCount == 2 || indexCount == 4 || indexCount == 5 || indexCount == 8 || indexCount == 10 || indexCount == 13)
            StartCoroutine(MathiasTalkingStep());

        // Mathias Anger
        if (indexCount == 15)
            StartCoroutine(AngerStepLevel());

        // Mathias AngryIdle
        if (indexCount == 14)
            AngerIdleStep();


        // Henri Talking
        if (indexCount == 1 || indexCount == 3 || indexCount == 6  || indexCount == 9 || indexCount == 12 || indexCount == 14)
            StartCoroutine(HenriTalkingStep());

        // Final Step
        if (indexCount == 15)
            StartCoroutine(LastStepLevel());
    }

    IEnumerator StartLevel()
    {
        // Waiting before starting
        yield return new WaitForSeconds(1f);

        //Show Disclaimer
        disclaimer.SetActive(true);
        yield return new WaitForSeconds(13f);
        disclaimer.SetActive(false);

        // Show Henri
        yield return new WaitForSeconds(2f);
        henriCharacter.gameObject.SetActive(true);

        // Henri phone call animation
        yield return new WaitForSeconds(2f);
        henriAnimator.SetTrigger("BackCallStarting");

        // Phone sound playing
        yield return new WaitForSeconds(1f);
        audioManager.PlayPhoneSound(true);

        // Show Mathias character
        yield return new WaitForSeconds(2f);
        mathiasCharacter.gameObject.SetActive(true);

        // Activate fad in animation for Mathias
        mathiasAnimator.SetBool("IsFadIn", true);

        // Mathias pick up the phone animation
        yield return new WaitForSeconds(2f);
        mathiasAnimator.SetTrigger("CallStarting");

        // End phone sound
        audioManager.PlayPhoneSound(false);
        yield return new WaitForSeconds(0.5f);

        // Henri turn
        henriCharacter.Flip();
        yield return new WaitForSeconds(1f);

        // Henri start talking animation
        henriAnimator.SetTrigger("BackCallTalking");
        yield return new WaitForSeconds(0.5f);

        // Show dialogues box 
        ShowDialogues(true);
        yield return new WaitForSeconds(0.5f);

        // Show Texts into the dialogues box
        dialogueBox.ShowTexts(true);

        // Coroutine End
        yield break;
    }

    IEnumerator MathiasTalkingStep()
    {
        // Set Henri calling idle animation
        henriAnimator.SetTrigger("BackCallIdle");
        yield return new WaitForSeconds(0.2f);

        // Set Mathias calling talking animation
        mathiasAnimator.SetTrigger("CallTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator HenriTalkingStep()
    {
        // Set Mathias calling idle animation
        if (indexCount != 1)
        {
            mathiasAnimator.SetTrigger("CallIdle");

            // Set Henri calling talking animation
            yield return new WaitForSeconds(0.2f);
            henriAnimator.SetTrigger("BackCallTalking");
        }

        // Coroutine End
        yield break;
    }

    IEnumerator LastStepLevel()
    {
        // Set Henri calling idle animation
        henriAnimator.SetTrigger("BackCallIdle");
        yield return new WaitForSeconds(0.2f);

        // Animation hang up Mathias
        mathiasAnimator.SetTrigger("CallIdle");
        yield return new WaitForSeconds(0.2f);
        mathiasAnimator.SetTrigger("CallEnding");
        yield return new WaitForSeconds(0.2f);

        // Set Mathias Anger animation
        mathiasAnimator.SetTrigger("AngerIdle");
        yield return new WaitForSeconds(1.5f);

        // Henri turn
        henriCharacter.Flip();
        yield return new WaitForSeconds(1f);

        // Animation hang up Henri
        henriAnimator.SetTrigger("BackCallEnding");
        yield return new WaitForSeconds(1f);

        // Animation FadOut Henri
        henriAnimator.SetTrigger("FadOut");
        yield return new WaitForSeconds(2f);

        // Set Mathias Anger animation
        mathiasAnimator.SetTrigger("Reset");

        // Hide Texts into the dialogues box
        dialogueBox.ShowTexts(false);
        yield return new WaitForSeconds(0.5f);

        // Animation FadOut dialogues box
        Animator dialoguesAnimator = dialogues.GetComponent<Animator>();
        dialoguesAnimator.SetTrigger("FadOut");
        Animator dialoguesNameAnimator = nameDialogues.GetComponent<Animator>();
        dialoguesNameAnimator.SetTrigger("FadOut");

        // Quit PLay Mode
        yield return new WaitForSeconds(2f);
        GameSystem.instance.PlayNextScene();

        // Coroutine End
        yield break;
    }

    IEnumerator TabouStepLevel()
    {
        mathiasAnimator.SetTrigger("CallIdle");
        mathiasAnimator.SetTrigger("CallTabou");

        yield return new WaitForSeconds(0.2f);
        mathiasAnimator.SetTrigger("CallTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator AngerStepLevel()
    {
        // Set Henri calling idle animation
        henriAnimator.SetTrigger("BackCallIdle");
        yield return new WaitForSeconds(0.5f);

        // Set Mathias calling anger animation
        mathiasAnimator.SetTrigger("CallAnger");

        // Coroutine End
        yield break;
    }

    void AngerIdleStep()
    {
        // Set Mathias calling anger animation
        mathiasAnimator.SetTrigger("CallAngerIdle");
    }

    // TO DO: Create a Utilities class with this Method used by all Level Managers
    void ShowDialogues(bool activated)
    {
        if (dialogues != null)
        {
            if (activated)
            {
                dialogues.SetActive(true);
                nameDialogues.SetActive(true);
            }

            if (!activated)
            {
                dialogues.SetActive(false);
                nameDialogues.SetActive(false);
            }
        }
    }
}
