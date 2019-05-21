using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeNoteText(string newText)
    {
        TMP_Text[] noteTextBox = GetComponentsInChildren<TMP_Text>();

        if (noteTextBox[0].isTextOverflowing)
        {
            // do something to increase cube z scale to cover text height
        }

        for (var i = 0; i < noteTextBox.Length; i++)
        {
            noteTextBox[i].text = newText;
        }
    }
}
