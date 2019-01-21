using System.Collections;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] private Animator fadScreen;

    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {

        yield return new WaitForSeconds(5f);

        fadScreen.SetTrigger("FadOut");
        yield return new WaitForSeconds(2f);

        GameSystem.instance.PlayNextScene();

        yield break;
    }
}
