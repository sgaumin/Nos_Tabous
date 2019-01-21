using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptAreYouOverTextBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static int WhoIsHighlighted;
    private bool MouseIsOverHere;
    private float timeCount;

    void Start()
    {
        MouseIsOverHere = true;
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        MouseIsOverHere = false;
        timeCount = 0;
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        MouseIsOverHere = true;
        
    }

    void Update()
    {
        if (MouseIsOverHere)
        {
            timeCount += 5*Time.deltaTime;
            if (timeCount > 6)
            {
                timeCount -= 6;
            }
            WhoIsHighlighted = (int) Mathf.Floor(timeCount);
        }
        else
        {
            WhoIsHighlighted = 0;
        }
    }
}
