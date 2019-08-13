using UnityEngine;

public class MenuManager : MonoBehaviour
{
	void Update()
	{
		if (Input.GetButtonDown("Grenier"))
		{
			GameSystem.Instance.LoadSceneByName("7- Grenier");
		}
	}
}
