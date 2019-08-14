using UnityEngine;

public abstract class LevelSequence : MonoBehaviour
{
	protected void ShowDialogues(bool activated) => DialogueBox.Instance.gameObject?.SetActive(activated);
}
