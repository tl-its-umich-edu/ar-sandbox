using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToTouchKeyboard : MonoBehaviour
{
    private RectTransform rt;
    private float rtOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        rtOffsetY = rt.anchoredPosition.y;

        TouchScreenKeyboard.hideInput = true; // doesn't work?? idk :P
    }

    // Update is called once per frame
    void Update()
    {
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, TouchScreenKeyboard.area.height + rtOffsetY);
    }
}
