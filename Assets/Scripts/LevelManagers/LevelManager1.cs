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

    void Start()
    {
        ShowDialogues(false);
        StartCoroutine(StartLevel());

        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        henriCharacter = henriAnimator.gameObject.GetComponent<Character>();

        // Hide Mathias Character at Starting
        mathiasCharacter.gameObject.SetActive(false);
    }

    IEnumerator StartLevel()
    {
        // Henri phone call animation
        yield return new WaitForSeconds(1f);
        henriAnimator.SetTrigger("BackCallStarting");

        // Phone sound playing
        yield return new WaitForSeconds(1f);
        audioLevel.PlayPhoneSound(true);

        // Show Mathias character
        yield return new WaitForSeconds(2f);
        mathiasCharacter.gameObject.SetActive(true);

        // Mathias hang phone animation
        yield return new WaitForSeconds(2f);
        mathiasAnimator.SetTrigger("CallStarting");

        // End phone sound
        audioLevel.PlayPhoneSound(false);
        yield return new WaitForSeconds(0.5f);

        // Henri turn
        henriCharacter.Flip();

        // Henri start talking animation
        henriAnimator.SetTrigger("BackCallTalking");
        yield return new WaitForSeconds(0.5f);

        // Show dialogues box 
        ShowDialogues(true);
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
