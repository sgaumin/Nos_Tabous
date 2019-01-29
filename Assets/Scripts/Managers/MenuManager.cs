using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameSystem.instance.LoadSceneByName("7- Grenier");
        }
    }
}
