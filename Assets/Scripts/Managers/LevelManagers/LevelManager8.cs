using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager8 : MonoBehaviour
{
    [SerializeField] private GrenierObjects henriLetter;
    [SerializeField] private GameObject commentsBox;
    [SerializeField] private GameObject lettersBox;
    [SerializeField] private Animator fadScreen;
    [SerializeField] private Button quitButton;

    private GrenierObjects[] grenierObjects;
    private int countObject;
    private int currentStep;

    void Start()
    {
        // Hide Henri's letter
        henriLetter.gameObject.SetActive(false);

        // Reteive all objects
        grenierObjects = FindObjectsOfType<GrenierObjects>();
        countObject = grenierObjects.Length;

        // Hide UI
        commentsBox.SetActive(false);
        lettersBox.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // Starting level scripting
        StartCoroutine(StartStep());

        currentStep = 0;
    }

    void Update()
    {
        Debug.Log(currentStep);

        // If all objects is checked
        if (currentStep == 1)
            if (!isAllChecked())
            {
                return;
            }
            else
            {
                // Show Henri's letter
                currentStep = 3;
                StartCoroutine(ShowHenriLetter());
                return;
            }

        // If Andre's letter is checked
        if (currentStep == 2)
        {
            if (Input.anyKeyDown)
            {
                currentStep = 1;
                StartCoroutine(HideLetter());
            }
        }

        // If Henry's letter is checked, go to end of this level 
        if (currentStep == 3)
            if (henriLetter.isChecked)
                if (Input.anyKeyDown)
                    StartCoroutine(FinalStep());
    }

    IEnumerator StartStep()
    {
        yield return new WaitForSeconds(1f);
        commentsBox.SetActive(true);

        // Increase step
        currentStep++;

        // End of coroutine
        yield break;
    }

    IEnumerator ShowHenriLetter()
    {
        // Show Henri's letter
        yield return new WaitForSeconds(1f);
        henriLetter.gameObject.SetActive(true);

        // Unclickable Henri's letter
        henriLetter.GetComponent<GrenierObjects>().canBeSelected = false;

        // Hide dialogueBox
        commentsBox.SetActive(false);

        // End of coroutine
        yield break;
    }

    public IEnumerator ShowLetter(bool isHenriLetter)
    {
        // Hide dialogueBox
        commentsBox.SetActive(false);

        // Show lettersBox
        lettersBox.SetActive(true);

        // Stop click interactions
        yield return new WaitForSeconds(0.2f);
        isAllClickable(false);

        // Andre's Letter step
        if (!isHenriLetter)
            currentStep = 2;

        // End of coroutine
        yield break;
    }

    public IEnumerator HideLetter()
    {
        // Hide dialogueBox
        commentsBox.SetActive(true);

        // Hide lettersBox
        lettersBox.SetActive(false);

        // Allow click interaction
        yield return new WaitForSeconds(0.2f);
        isAllClickable(true);

        // End of coroutine
        yield break;
    }

    IEnumerator FinalStep()
    {
        // Waiting time
        yield return new WaitForSeconds(2f);

        // Hide lettersBox
        lettersBox.SetActive(false);

        // Hide dialogue box
        commentsBox.SetActive(false);

        // fad Out animation
        fadScreen.SetTrigger("FadOut");
        yield return new WaitForSeconds(2f);

        // Show Quit Button
        quitButton.gameObject.SetActive(true);

        // End of coroutine
        yield break;
    }

    // Check if all objects is checked
    bool isAllChecked()
    {
        int compteur = 0;
        foreach (GrenierObjects grenierObject in grenierObjects)
        {
            if (grenierObject.isChecked)
                compteur++;
        }

        if (compteur == countObject)
            return true;

        return false;
    }

    void isAllClickable(bool value)
    {
        foreach (GrenierObjects grenierObject in grenierObjects)
        {
            grenierObject.canBeSelected = value;
        }
    }
}
