using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTextBehavior : MonoBehaviour
{
    private float timeLeft;
    private TextMeshProUGUI text;

    // Start is called before the first frame updatetime
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        SetMessage(0, Color.clear, "");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            text.color = Color.clear;
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }

    public void SetMessage(float timeToDisappear, Color desiredColor, string message)
    {
        timeLeft = timeToDisappear;
        text.color = desiredColor;
        text.text = message;
    }
}
