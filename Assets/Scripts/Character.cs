using UnityEngine;

public class Character : MonoBehaviour
{
    public void Flip() {
        transform.Rotate(0f, 180f, 0f);
    }
}
