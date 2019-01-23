using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestFitScript2 : MonoBehaviour
{
    public GameObject BestFitObject;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().fontSize = BestFitObject.GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().fontSize = BestFitObject.GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;
    }
}
