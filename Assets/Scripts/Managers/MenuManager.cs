using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameSystem.Instance.LoadSceneByName("7- Grenier");
        }
    }
}
