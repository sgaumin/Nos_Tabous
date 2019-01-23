using UnityEngine;

public class EndManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GameSystem.instance.LoadMenu();
        }
    }
}
