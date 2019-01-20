﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager3 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator henriAnimator;
    [SerializeField] private GameObject dialogues;

    [SerializeField] private AudioManager3 audioManager;

    private Character mathiasCharacter;
    private Character henriCharacter;
    private DialogueBox dialogueBox;
    private int indexCount;

    private bool isStarting = true;

    void Start()
    {
        // Asign Character components
        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

        // Asign DialogueBox component
        dialogueBox = dialogues.GetComponent<DialogueBox>();

        // Hide Henri Character at Starting
        henriCharacter.gameObject.SetActive(false);

        // Index for checking the current IndexDialogue of DialogueSystemScript script 
        indexCount = 999;

        // Hide Dialogue Box
        ShowDialogues(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the choice is Tabou and set the animation
        if (DialogueSystemScript.isTabou)
            StartCoroutine(TabouStepLevel());

        // Set Animation and Sound according to the Dialogue Index
        if (DialogueSystemScript.indexDialogue == indexCount)
            return;

        indexCount = DialogueSystemScript.indexDialogue;

        if ((indexCount == 13 || indexCount == 0) && isStarting)
        {
            DialogueSystemScript.indexDialogue = 0; // TO report
            indexCount = 0;
            StartCoroutine(StartLevel());
            isStarting = false;
        }

        // Mathias Speaking Steps
        if (indexCount == 2 || indexCount == 4 || indexCount == 6)
            StartCoroutine(SecondStepLevel());

        // Henri Speaking Steps
        if (indexCount == 1 || indexCount == 3 || indexCount == 5 || indexCount == 7 )
            StartCoroutine(ThirdStepLevel());

        // Final Step
        if (indexCount == 8)
            StartCoroutine(LastStepLevel());
    }

    IEnumerator StartLevel()
    {
        // Waiting before starting
        yield return new WaitForSeconds(1f);

        // Mathias pick up the phone animation
        mathiasAnimator.SetTrigger("CallStarting");
        yield return new WaitForSeconds(1f);

        // Phone sound playing
        audioManager.PlayPhoneSound(true);
        yield return new WaitForSeconds(8f);

        // Fad In Animation Henri
        henriCharacter.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        // Henri phone call animation
        henriAnimator.SetTrigger("BackCallStarting");
        yield return new WaitForSeconds(2f);

        // End phone sound
        audioManager.PlayPhoneSound(false);
        yield return new WaitForSeconds(2f);

        // Mathias start talking animation
        mathiasAnimator.SetTrigger("CallTalking");
        yield return new WaitForSeconds(1f);

        // Henri turn
        henriCharacter.Flip();
        yield return new WaitForSeconds(1f);

        // Show dialogues box 
        ShowDialogues(true);
        yield return new WaitForSeconds(0.5f);

        // Show Texts into the dialogues box
        dialogueBox.ShowTexts(true);

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

    IEnumerator TabouStepLevel()
    {
        mathiasAnimator.SetTrigger("CallIdle");
        mathiasAnimator.SetTrigger("CallTabou");

        yield return new WaitForSeconds(0.2f);
        mathiasAnimator.SetTrigger("CallTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator LastStepLevel()
    {
        // Set Henri calling idle animation
        henriAnimator.SetTrigger("BackCallIdle");
        yield return new WaitForSeconds(0.5f);

        // Mathias talking animation for last dialogue
        mathiasAnimator.SetTrigger("CallTalking");
        yield return new WaitForSeconds(2f);

        // Mathias stop talking animation
        mathiasAnimator.SetTrigger("CallIdle");
        yield return new WaitForSeconds(0.5f);

        // Henri stop talking animation
        henriAnimator.SetTrigger("BackCallIdle");
        yield return new WaitForSeconds(0.5f);

        // Animation hang up Henri
        henriAnimator.SetTrigger("BackCallEnding");
        yield return new WaitForSeconds(1f);

        // Animation FadOut Henri
        henriAnimator.SetTrigger("FadOut");
        yield return new WaitForSeconds(2f);

        // Mathias hang up animation
        mathiasAnimator.SetTrigger("CallEnding");
        yield return new WaitForSeconds(0.5f);

        // Hide Texts into the dialogues box
        dialogueBox.ShowTexts(false);
        yield return new WaitForSeconds(0.5f);

        // Animation FadOut dialogues box
        Animator dialoguesAnimator = dialogues.GetComponent<Animator>();
        dialoguesAnimator.SetTrigger("FadOut");

        // Quit PLay Mode
        yield return new WaitForSeconds(2f);
        GameSystem.instance.QuitGame(); ;

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