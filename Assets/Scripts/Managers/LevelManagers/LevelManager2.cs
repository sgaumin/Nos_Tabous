using System.Collections;
using UnityEngine;

public class LevelManager2 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator carolineAnimator;
    [SerializeField] private Animator background;
    [SerializeField] private GameObject dialogues;

    [SerializeField] private AudioManager2 audioManager;

    private Character mathiasCharacter;
    private Character carolineCharacter;
    private DialogueBox dialogueBox;
    private int indexCount;

    private bool isStarting = true;

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
        // Check if the choice is Tabou and set the animation
        if (DialogueSystemScript.isTabou)
            StartCoroutine(TabouStepLevel());

        // Set Animation and Sound according to the Dialogue Index
        if (DialogueSystemScript.indexDialogue == indexCount)
            return;

        indexCount = DialogueSystemScript.indexDialogue;

        // TO DO: Implement more flexible IF statement regarding previous previous scnene loading and indexCount was not reset
        if ((indexCount == 10 || indexCount == 0) && isStarting)
        {
            DialogueSystemScript.indexDialogue = 0; // TO report
            indexCount = 0;
            StartCoroutine(StartLevel());
            isStarting = false;
        }

        // Mathias Speaking Steps
        if ( indexCount == 3)
            StartCoroutine(SecondStepLevel());

        // Mathias Anger Steps
        if (indexCount == 1 || indexCount == 6 || indexCount == 8)
            StartCoroutine(AngerStepLevel());

        // Mathias Very Anger Steps
        if (indexCount == 10)
            StartCoroutine(VeryAngerStepLevel());

        // Caroline Speaking Steps
        if (indexCount == 2 || indexCount == 4 || indexCount == 7 || indexCount == 9 || indexCount == 12)
            StartCoroutine(ThirdStepLevel());

        if (indexCount == 13)
            StartCoroutine(FinalStepLevel());
    }

    IEnumerator StartLevel()
    {

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

    IEnumerator SecondStepLevel()
    {
        // Set Caroline idle animation
        carolineAnimator.SetTrigger("Reset");
        yield return new WaitForSeconds(0.5f);

        // Set Mathias talking animation
        mathiasAnimator.SetTrigger("Talking");

        // Coroutine End
        yield break;
    }

    IEnumerator ThirdStepLevel()
    {
        // Set Mathias  idle animation
        mathiasAnimator.SetTrigger("Reset");

        // Set Caroline  talking animation
        yield return new WaitForSeconds(0.5f);
        carolineAnimator.SetTrigger("Talking");

        // Coroutine End
        yield break;
    }

    IEnumerator TabouStepLevel()
    {
        // Retrieve the current animation state
        AnimatorClipInfo[] m_CurrentClipInfo;
        m_CurrentClipInfo = mathiasAnimator.GetCurrentAnimatorClipInfo(0);
        string  m_ClipName = m_CurrentClipInfo[0].clip.name;

        // Set Mathias Tabou animation
        mathiasAnimator.SetTrigger("Reset");
        mathiasAnimator.SetTrigger("Tabou");
        yield return new WaitForSeconds(2f);

        // Set the same animation as the start of this dialogue
        if (m_ClipName == "Mathias_Talking")
            mathiasAnimator.SetTrigger("Talking");

        if (m_ClipName == "Mathias_Anger")
            mathiasAnimator.SetTrigger("Anger");

        if (m_ClipName == "Mathias_VeryAnger")
            mathiasAnimator.SetTrigger("VeryAnger");

        // Coroutine End
        yield break;
    }

    IEnumerator AngerStepLevel()
    {
        // Set Caroline calling idle animation
        carolineAnimator.SetTrigger("Reset");
        yield return new WaitForSeconds(0.5f);

        // Set Mathias calling anger animation
        mathiasAnimator.SetTrigger("Anger");

        // Coroutine End
        yield break;
    }

    IEnumerator VeryAngerStepLevel()
    {
        // Set Caroline calling idle animation
        carolineAnimator.SetTrigger("Reset");
        yield return new WaitForSeconds(0.5f);

        // Set Mathias calling anger animation
        mathiasAnimator.SetTrigger("VeryAnger");

        // Coroutine End
        yield break;
    }

    IEnumerator FinalStepLevel()
    {
        // Waiting time
        yield return new WaitForSeconds(2f);

        // Flip Mathias
        mathiasCharacter.Flip();
        //mathiasAnimator.SetTrigger("Reset");
        mathiasAnimator.SetTrigger("AngerIdle");
        yield return new WaitForSeconds(0.5f);

        // Reset Caroline animation 
        carolineAnimator.SetTrigger("Reset");
        yield return new WaitForSeconds(0.5f);

        // Caroline Fad Out animation
        carolineAnimator.SetTrigger("FadOut");

        // Play sound Shoes
        audioManager.PlayHeelShoesSound(true);
        yield return new WaitForSeconds(2f);
        audioManager.PlayHeelShoesSound(false);

        // Play sound door
        audioManager.PlayClosedDoorSound(true);
        yield return new WaitForSeconds(4f);

        // Flip Mathias
        mathiasCharacter.Flip();
        yield return new WaitForSeconds(0.5f);

        // Reset Mathias animation
        mathiasAnimator.SetTrigger("Reset");
        yield return new WaitForSeconds(0.5f);

        // Hide Texts into the dialogues box
        dialogueBox.ShowTexts(false);
        yield return new WaitForSeconds(0.5f);

        // Animation FadOut dialogues box
        Animator dialoguesAnimator = dialogues.GetComponent<Animator>();
        dialoguesAnimator.SetTrigger("FadOut");
        yield return new WaitForSeconds(0.5f);

        // Background fad out animation
        background.SetTrigger("FadOut");
        yield return new WaitForSeconds(2f);

        // Coroutine End
        GameSystem.instance.QuitGame();
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
