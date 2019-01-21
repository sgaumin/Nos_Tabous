using System.Collections;
using UnityEngine;

public class LevelManager7 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private GameObject dialogues;
    [SerializeField] private Animator background;

    [SerializeField] private AudioManager7 audioManager;

    private Character mathiasCharacter;
    private DialogueBox dialogueBox;
    private int indexCount;

    private bool isStarting = true;

    // Start is called before the first frame update
    void Start()
    {
        // Asign Character components
        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();

        // Asign DialogueBox component
        dialogueBox = dialogues.GetComponent<DialogueBox>();

        // Hide the background at starting
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

        // Set Animation and Sound according to the Dialogue Index
        if (DialogueSystemScript.indexDialogue == indexCount)
            return;

        indexCount = DialogueSystemScript.indexDialogue;

        if ((indexCount == 10 || indexCount == 0) && isStarting)
        {
            DialogueSystemScript.indexDialogue = 0; // TO report
            indexCount = 0;
            StartCoroutine(StartLevel());
            isStarting = false;
        }

        // Mathias Speaking Steps
        if (indexCount == 1 || indexCount == 2)
            StartCoroutine(MathiasTalkingStep());

        // Open Box
        if (indexCount == 3)
            StartCoroutine(LastStepLevel(false));

        // End Game
        if (indexCount == 4)
            StartCoroutine(LastStepLevel(true));
    }

    IEnumerator StartLevel()
    {
        // Flip Mathias and play Back animation
        mathiasAnimator.SetTrigger("Back");
        mathiasCharacter.Flip();

        //// Stairs sound playing
        //audioManager.PlayStairsSound(true);
        //yield return new WaitForSeconds(audioManager.lenghtSound - 1f);

        //// Door sound playing
        //audioManager.PlayOpenDoorSound(true);
        //yield return new WaitForSeconds(2.5f);

        // Show the background at starting
        background.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

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
        // Set Mathias calling talking animation
        mathiasAnimator.SetTrigger("BackTalking");
        yield return new WaitForSeconds(2f);

        // Set Mathias calling talking animation
        mathiasAnimator.SetTrigger("Back");

        // Coroutine End
        yield break;
    }

    IEnumerator LastStepLevel(bool isEnd)
    {
        // Box sound playing
        if (!isEnd)
        {
            audioManager.PlayOpenBoxSound(true);
            yield return new WaitForSeconds(2f);
        }

        // Hide Texts into the dialogues box
        dialogueBox.ShowTexts(false);
        yield return new WaitForSeconds(0.5f);

        // Animation FadOut dialogues box
        Animator dialoguesAnimator = dialogues.GetComponent<Animator>();
        dialoguesAnimator.SetTrigger("FadOut");
        yield return new WaitForSeconds(0.5f);

        // Background fad out animation
        background.SetTrigger("FadOut");
        yield return new WaitForSeconds(1f);

        // Load end Game Screen is End
        if (isEnd)
        {
            // Hide Mathias 
            mathiasAnimator.SetTrigger("BackFadOut");
            yield return new WaitForSeconds(2f);

            GameSystem.instance.LoadSceneByName("10- Fin");
        }

        if (!isEnd)
            GameSystem.instance.QuitGame();

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
