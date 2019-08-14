using UnityEngine;
using UnityEngine.UI;

public class TextNameScript : MonoBehaviour
{
	public OneDialogueElementList DialogueContent;
	private Color Couleur;

	void Update()
	{
		Couleur = DialogueContent.ElementList[DialogueSystemScript.indexDialogue].Couleur;
		Couleur.a = DialogueSystemScript.opacity;
		gameObject.GetComponent<Text>().fontSize = BestFitText.BestFitFrontSize * 6 / 5;
		gameObject.GetComponent<Text>().color = Couleur;
		gameObject.GetComponent<Text>().text = DialogueContent.ElementList[DialogueSystemScript.indexDialogue].WhoIsSpeaking;
	}
}


