using UnityEngine;
using UnityEngine.UI;

public class GrenierObjects : MonoBehaviour
{
	[Header("Comment Translations")]
	[TextArea(3, 10)]
	[SerializeField] private string frenchComment;
	[TextArea(3, 10)]
	[SerializeField] private string englishComment;
	[TextArea(3, 10)]
	[SerializeField] private string japaneseComment;

	[Header("Letter content Translations")]
	[TextArea(3, 10)]
	[SerializeField] private string frenchLetterContent;
	[TextArea(3, 10)]
	[SerializeField] private string englishLetterContent;
	[TextArea(3, 10)]
	[SerializeField] private string japaneseLetterContent;

	[SerializeField] private bool isLetter;
	[SerializeField] private bool isHenriLetter;
	[SerializeField] private bool canBeSelected;
	[SerializeField] private Text commentsBox;
	[SerializeField] private Text lettersBox;

	private LevelManager8 levelManager;
	private Animator animator;
	private string comment;
	private string letterContent;

	public bool IsChecked { get; private set; }

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

	private void Start()
	{
		levelManager = FindObjectOfType<LevelManager8>();
		animator = GetComponent<Animator>();
		canBeSelected = true;

		switch (LanguageData.Language)
		{
			case Languages.French:
				comment = frenchComment;
				letterContent = frenchLetterContent;
				break;
			case Languages.English:
				comment = englishComment;
				letterContent = englishLetterContent;
				break;
			case Languages.Japanese:
				comment = japaneseComment;
				letterContent = japaneseLetterContent;
				break;
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
				lettersBox.text = letterContent;
				StartCoroutine(levelManager.ShowLetter(isHenriLetter));
			}
			commentsBox.text = comment;
		}
	}
}
