using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenierObjects : MonoBehaviour
{
    [TextArea(3, 10)]
    public string comment;
    public bool isLetter;
    public bool isHenriLetter;

    [HideInInspector] public bool isChecked;
    [HideInInspector] public bool canBeSelected;

    [SerializeField] private Text commentsBox;
    [SerializeField] private Text lettersBox;

    private LevelManager8 levelManager;
    private Animator animator;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager8>();
        animator = GetComponent<Animator>();
        canBeSelected = true;
    }

    private void OnMouseOver()
    {
        if (canBeSelected)
            animator.SetBool("isHighlighted", true);
    }

    private void OnMouseExit()
    {
        if (canBeSelected)
        {
            animator.SetBool("isHighlighted", false);
            if (isChecked)
                animator.SetTrigger("IsIdle");

            if (!isChecked)
                animator.SetTrigger("IsWaiting");
        }
    }

    private void OnMouseDown()
    {
        if (canBeSelected)
        {
            isChecked = true;
            if (!isLetter)
                commentsBox.text = comment;

            if (isLetter) {
                lettersBox.text = comment;
                StartCoroutine(levelManager.ShowLetter(isHenriLetter));
            }
        }
    }
}
