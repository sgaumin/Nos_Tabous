using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
	public static DialogueBox Instance { get; private set; }

	[SerializeField] private Text[] textDialogues;

	private void Awake() => Instance = this;

	public void ShowTexts(bool show) => Array.ForEach(textDialogues, x => x.gameObject.SetActive(show));
}
