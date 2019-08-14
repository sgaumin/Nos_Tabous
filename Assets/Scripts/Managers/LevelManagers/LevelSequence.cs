using UnityEngine;

public abstract class LevelSequence : MonoBehaviour
{
	protected void ShowDialogueBoxBackground(bool activated) => DialogueBox.Instance.FadBackground(activated);
}
