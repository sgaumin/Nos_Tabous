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
    }

    IEnumerator StartLevel()
    {
        // Animation d'appel d'Henri
        yield return new WaitForSeconds(1f);
        henriAnimator.SetTrigger("BackCallStarting");

        // Sonnerie de vibreur
        yield return new WaitForSeconds(1f);
        audioLevel.PlayPhoneSound(true);

        // Mathias décroche le téléphone
        yield return new WaitForSeconds(2f);
        mathiasAnimator.SetTrigger("CallStarting");

        // Fin Sonnerie de vibreur
        audioLevel.PlayPhoneSound(false);
        yield return new WaitForSeconds(0.5f);

        // Henri se tounre
        henriCharacter.Flip();

        // Henri commence à discuter
        henriAnimator.SetTrigger("BackCallTalking");
        yield return new WaitForSeconds(0.5f);

        // Affichage des dialogues
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
