using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager9 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator henryAnimator;
    [SerializeField] private Animator sylvieAnimator;

    [SerializeField] private Animator background;

    // UI
    [SerializeField] private GameObject dialogues;
    [SerializeField] private GameObject nameDialogues;
    [SerializeField] private Animator fadOutScreen;

    private Character mathiasCharacter;
    private Character henryCharacter;
    private Character sylvieCharacter;

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
        henryCharacter = henryAnimator.gameObject.GetComponent<Character>();
        sylvieCharacter = sylvieAnimator.gameObject.GetComponent<Character>();

        // Asign DialogueBox component
        dialogueBox = dialogues.GetComponent<DialogueBox>();

        // Hide
        mathiasCharacter.gameObject.SetActive(false);
        henryCharacter.gameObject.SetActive(false);
        sylvieCharacter.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        //fadOutScreen.SetActive(false);

        // Index for checking the current IndexDialogue of DialogueSystemScript script 
        indexCount = 999;

        // Hide Dialogue Box
        ShowDialogues(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Set Animation and Sound according to the Dialogue Index
        if (DialogueSystemScript.indexDialogue == indexCount)
            return;

        indexCount = DialogueSystemScript.indexDialogue;

        if ((indexCount == 10 || indexCount == 9 || indexCount == 0) && isStarting)
        {
            //DialogueSystemScript.indexDialogue = 0; // TO report
            indexCount = 0;
            StartCoroutine(StartLevel());
            isStarting = false;
        }

        // Mathias Speaking Steps
        if (indexCount == 2 || indexCount == 5)
            StartCoroutine(MathiasTalking());

        // Mathias Surpris
        // if(indexCount =1)

        //Mathias Triste
        // if(indexCount = 4)
        //pensez à reset henri

        //henri et Sylvie surpris
        // if(indexCount =5)

        // henry Speaking Steps
        if (indexCount == 1 || indexCount == 3)
            StartCoroutine(HenryTalking());

        // End Level
        if (indexCount == 6)
            StartCoroutine(FinalStepLevel());
    }

    IEnumerator StartLevel()
    {
        // Waiting time
        yield return new WaitForSeconds(1f);
        mathiasCharacter.gameObject.SetActive(true);
        mathiasAnimator.SetBool("IsFadIn", true);
        yield return new WaitForSeconds(1f);

        // Show Background
        background.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Show Henry & Sylvie Characters
        henryCharacter.gameObject.SetActive(true);
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

    IEnumerator HenryTalking()
    {
        // Mathias Idle animation
        if (indexCount != 1)
        {
            mathiasAnimator.SetTrigger("Reset");
            yield return new WaitForSeconds(0.5f);

        }
        // Set henry  talking animation
        henryAnimator.SetTrigger("BackTalking");

        // Coroutine End
        yield break;
    }

    IEnumerator MathiasTalking()
    {
        // Set henry idle animation
        henryAnimator.SetTrigger("ResetBack");
        yield return new WaitForSeconds(0.5f);

        // Mathias Idle animation
        mathiasAnimator.SetTrigger("Talking");

        // Coroutine End
        yield break;
    }

    IEnumerator FinalStepLevel()
    {
        // Set Henry & Mathias idle animation
        mathiasAnimator.SetTrigger("Reset");
        henryAnimator.SetTrigger("ResetBack");
        yield return new WaitForSeconds(0.5f);

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

        // Characters Fad Out animation
        henryAnimator.SetTrigger("FadOut");
        sylvieAnimator.SetTrigger("FadOut");
        
        mathiasAnimator.SetTrigger("FadOut");
        yield return new WaitForSeconds(1f);

        fadOutScreen.SetTrigger("FadOut");
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
