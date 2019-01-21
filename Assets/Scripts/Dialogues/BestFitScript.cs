using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestFitScript : MonoBehaviour
{
    public static int fontsize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fontsize = gameObject.GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;
    }
}
