using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager5 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator sylvieAnimator;
    [SerializeField] private Animator henriAnimator;

    [SerializeField] private Animator background;

    [SerializeField] private AudioManager5 audioManager;

    // UI
    [SerializeField] private GameObject dialogues;
    [SerializeField] private GameObject nameDialogues;

    private Character mathiasCharacter;
    private Character sylvieCharacter;
    private Character henriCharacter;

    private DialogueBox dialogueBox;
    private int indexCount;

    private bool isStarting = true;

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate fad in animation for Mathias
        mathiasAnimator.SetBool("IsFadIn", false);

        // Asign Character components
        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        sylvieCharacter = sylvieAnimator.gameObject.GetComponent<Character>();
        henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

        // Asign DialogueBox component
        dialogueBox = dialogues.GetComponent<DialogueBox>();

        // Hide
        sylvieCharacter.gameObject.SetActive(false);
        henriCharacter.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        // Index for checking the current IndexDialogue of DialogueSystemScript script 
        indexCount = 999;

        // Hide Dialogue Box
        ShowDialogues(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DialogueSystemScript.indexDialogue);

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
            //DialogueSystemScript.indexDialogue = 0; // TO report
            indexCount = 0;
            StartCoroutine(StartLevel());
            isStarting = false;
        }

        // Mathias Speaking Steps
        if (indexCount == 1 || indexCount == 3 || indexCount == 5 || indexCount == 8 || indexCount == 10 || indexCount == 12 || indexCount == 15 || indexCount == 21)
            StartCoroutine(MathiasTalking());

        // Sylvie Speaking Steps
        if (indexCount == 2 || indexCount == 4 || indexCount == 9)
            StartCoroutine(sylvieTalking());

        // Henri Speaking Steps
        if (indexCount == 14 || indexCount == 17)
            StartCoroutine(sylvieTalking());

        // Entrance Henri
        if (indexCount == 6)
            StartCoroutine(HenriEntrance());

        // End Level
        if (indexCount == 18)
            StartCoroutine(FinalStepLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);

        // Play wind sound
        audioManager.PlayWindSound(true);
        yield return new WaitForSeconds(3f);

        // Play foot sound
        audioManager.PlayFootSound(true);
        yield return new WaitForSeconds(7f);

        // Play knock sound
        audioManager.PlayKnockSound(true);
        yield return new WaitForSeconds(3f);

        // Disalble all sounds
        audioManager.PlayWindSound(false);
        audioManager.PlayFootSound(false);
        audioManager.PlayKnockSound(false);

        // Show Background
        background.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Show sylvie Character
        sylvieCharacter.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Show dialogues box 
        ShowDialogues(true);
        yield return new WaitForSeconds(0.5f);

        // Show Texts into the dialogues box
        dialogueBox.ShowTexts(true);

        // Coroutine End
        yield break;
    }

    IEnumerator MathiasTalking()
    {
        if (indexCount != 1)
        {
            // Set sylvie idle animation
            sylvieAnimator.SetTrigger("ResetBack");
            henriAnimator.SetTrigger("ResetBack");
            yield return new WaitForSeconds(0.5f);
        }

        // Set Mathias talking animation
        mathiasAnimator.SetTrigger("Talking");

        // Coroutine End
        yield break;
    }

    IEnumerator sylvieTalking()
    {
        // Set Mathias  idle animation
        mathiasAnimator.SetTrigger("Reset");
        henriAnimator.SetTrigger("ResetBack");
        yield return new WaitForSeconds(0.5f);

        // Set sylvie  talking animation
        sylvieAnimator.SetTrigger("BackTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator HenriTalking()
    {
        mathiasAnimator.SetTrigger("Reset");
        //sylvieAnimator.SetTrigger("ResetBack");
        yield return new WaitForSeconds(0.5f);

        // Set sylvie  talking animation
        henriAnimator.SetTrigger("BackTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator HenriEntrance()
    {
        // Show Henri
        henriCharacter.gameObject.SetActive(true);

        // Henri Talking
        yield return StartCoroutine(HenriTalking());

        // End Coroutine
        yield break;
    }

    IEnumerator TabouStepLevel()
    {
        // Retrieve the current animation state
        AnimatorClipInfo[] m_CurrentClipInfo;
        m_CurrentClipInfo = mathiasAnimator.GetCurrentAnimatorClipInfo(0);
        string m_ClipName = m_CurrentClipInfo[0].clip.name;

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

    IEnumerator FinalStepLevel()
    {
        // Reset Mathias animation
        mathiasAnimator.SetTrigger("Reset");

        // sylvie Fad Out animation
        sylvieAnimator.SetTrigger("FadOut");

        // Hide Texts into the dialogues box
        dialogueBox.ShowTexts(false);
        yield return new WaitForSeconds(0.5f);

        // Animation FadOut dialogues box
        Animator dialoguesAnimator = dialogues.GetComponent<Animator>();
        dialoguesAnimator.SetTrigger("FadOut");
        Animator dialoguesNameAnimator = nameDialogues.GetComponent<Animator>();
        dialoguesNameAnimator.SetTrigger("FadOut");

        yield return new WaitForSeconds(0.5f);

        // Background fad out animation
        background.SetTrigger("FadOut");
        yield return new WaitForSeconds(2f);

        // Coroutine End
        GameSystem.instance.PlayNextScene();
        yield break;
    }

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
