using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager8b : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartStep());        
    }

    IEnumerator StartStep() {

        yield return new WaitForSeconds(20f);

        GameSystem.Instance.LoadNextScene();

        yield break;
    }
}
