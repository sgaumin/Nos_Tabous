using System.Collections;
using UnityEngine;

public class LevelManager4 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator jadeAnimator;
    [SerializeField] private Animator car;
    [SerializeField] private Animator insideCar;


    // UI
    [SerializeField] private GameObject dialogues;
    [SerializeField] private GameObject nameDialogues;

    [SerializeField] private AudioManager4 audioManager;

    private Character mathiasCharacter;
    private Character jadeCharacter;
    private DialogueBox dialogueBox;
    private int indexCount;

    private bool isStarting = true;

    void Start()
    {
        // Deactivate fad in animation for Mathias
        mathiasAnimator.SetBool("IsFadIn", false);

        // Asign Character components
        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        jadeCharacter = jadeAnimator.gameObject.GetComponent<Character>();

        // Asign DialogueBox component
        dialogueBox = dialogues.GetComponent<DialogueBox>();

        // Hide
        jadeCharacter.gameObject.SetActive(false);
        car.gameObject.SetActive(false);

        // Index for checking the current IndexDialogue of DialogueSystemScript script 
        indexCount = 999;

        // Hide Dialogue Box
        ShowDialogues(false);
    }

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
        if (isStarting)
        {
            //DialogueSystemScript.indexDialogue = 0; // TO report
            indexCount = 0;
            StartCoroutine(StartLevel());
            isStarting = false;
        }

        // Mathias Speaking Steps
        if (indexCount == 2 || indexCount == 3 || indexCount == 4 || indexCount == 6 || indexCount == 9 || indexCount == 11 || indexCount == 14 || indexCount == 16 || indexCount == 18 || indexCount ==19 || indexCount == 21)
            StartCoroutine(MathiasTalking());

        // Jade Speaking Steps
        if (indexCount == 1 || indexCount == 5 || indexCount == 7 || indexCount == 10 || indexCount == 12 || indexCount == 15 || indexCount == 17 || indexCount == 20 || indexCount == 22)
            StartCoroutine(JadeTalking());

        // facing right Jade's animation
        /*if (indexCount == 6)
            StartCoroutine(JadeFacingRight());*/

        // End Level
        if (indexCount == 22)
            StartCoroutine(FinalStepLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);

        // Play car starting sound
        audioManager.PlayStartDrivingSound(true);
        yield return new WaitForSeconds(6f);

        // Show jade Character
        jadeCharacter.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        // Show Background
        car.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        car.SetTrigger("Move");

        // Show dialogues box 
        ShowDialogues(true);
        yield return new WaitForSeconds(0.5f);

        // Show Texts into the dialogues box
        dialogueBox.ShowTexts(true);

        // Play car ambience sound
        audioManager.PlayAmbianceSound(true);
        yield return new WaitForSeconds(0.5f);

        // Add animation by parents to characters
        mathiasCharacter.transform.SetParent(insideCar.transform);
        jadeCharacter.transform.SetParent(insideCar.transform);

        // Coroutine End
        yield break;
    }

    IEnumerator MathiasTalking()
    {
        // Set jade idle animation
        jadeAnimator.SetTrigger("Reset");
        yield return new WaitForSeconds(0.5f);

        // Set Mathias talking animation
        mathiasAnimator.SetTrigger("Talking");

        // Coroutine End
        yield break;
    }

    IEnumerator JadeTalking()
    {
        // Set Mathias  idle animation
        if (indexCount != 1)
            mathiasAnimator.SetTrigger("Reset");

        // Set jade  talking animation
        yield return new WaitForSeconds(0.5f);
        jadeAnimator.SetTrigger("Talking");

        // Coroutine End
        yield break;
    }

    IEnumerator JadeFacingRight() {

        // Set Mathias  idle animation
        if (indexCount != 1)
            mathiasAnimator.SetTrigger("Reset");

        // Set jade  talking animation
        yield return new WaitForSeconds(0.5f);
        jadeAnimator.SetTrigger("FacingRight");

        // Coroutine End
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
        // Set jade  talking animation
        yield return new WaitForSeconds(0.5f);
        jadeAnimator.SetTrigger("Talking");

        // Reset Mathias animation
        mathiasAnimator.SetTrigger("Reset");

        // Set jade idle animation
        yield return new WaitForSeconds(0.5f);
        jadeAnimator.SetTrigger("Reset");

        // Stop car animation
        car.SetTrigger("Idle");
        // Stop animation by parents to characters
        mathiasCharacter.transform.SetParent(null);
        jadeCharacter.transform.SetParent(null);

        // Waiting time
        yield return new WaitForSeconds(2f);

        // Play sound car door closed
        audioManager.PlayClosedDoorSound(true);

        // jade Fad Out animation
        jadeAnimator.SetTrigger("FadOut");
        yield return new WaitForSeconds(2f);

        // Fad Out Car
        car.SetTrigger("FadOut");

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
        car.SetTrigger("FadOut");
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
