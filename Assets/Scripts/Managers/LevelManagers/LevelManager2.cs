using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager2 : MonoBehaviour
{
    [SerializeField] private Animator mathiasAnimator;
    [SerializeField] private Animator carolineAnimator;
    [SerializeField] private GameObject dialogues;

    [SerializeField] private AudioManager2 audioLevel;

    private Character mathiasCharacter;
    private Character carolineCharacter;
    private int indexCount;

    // Start is called before the first frame update
    void Start()
    {
        ShowDialogues(false);

        mathiasCharacter = mathiasAnimator.gameObject.GetComponent<Character>();
        carolineCharacter = carolineAnimator.gameObject.GetComponent<Character>();

        // Hide Caroline Character at Starting
        carolineCharacter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
