using UnityEngine;
using UnityEngine.UI;

public class GrenierObjects : MonoBehaviour
{
	[TextArea(3, 10)]
	[SerializeField] private string comment;
	[SerializeField] private bool isLetter;
	[SerializeField] private bool isHenriLetter;
	[SerializeField] private bool canBeSelected;
	[SerializeField] private Text commentsBox;
	[SerializeField] private Text lettersBox;

	private LevelManager8 levelManager;
	private Animator animator;

	public bool IsChecked { get; private set; }

	private void Start()
	{
		levelManager = FindObjectOfType<LevelManager8>();
		animator = GetComponent<Animator>();
		canBeSelected = true;
	}

	public bool CanBeSelected
	{
		get
		{
			return canBeSelected;
		}
		set
		{
			if (!value)
			{
				animator = GetComponent<Animator>();
				animator.SetBool("isHighlighted", false);
				if (IsChecked)
				{
					animator.SetTrigger("IsIdle");
				}
				else
				{
					animator.SetTrigger("IsWaiting");
				}
			}
			canBeSelected = value;
		}
	}

	private void OnMouseOver()
	{
		if (canBeSelected)
		{
			animator.SetBool("isHighlighted", true);
		}
	}

	private void OnMouseExit()
	{
		if (canBeSelected)
		{
			animator.SetBool("isHighlighted", false);
			if (IsChecked)
			{
				animator.SetTrigger("IsIdle");
			}
			else
			{
				animator.SetTrigger("IsWaiting");
			}
		}
	}

	private void OnMouseDown()
	{
		if (canBeSelected)
		{
			IsChecked = true;
			if (isLetter)
			{
				lettersBox.text = comment;
				StartCoroutine(levelManager.ShowLetter(isHenriLetter));
			}
			else
			{
				commentsBox.text = comment;
			}
		}
	}
}
