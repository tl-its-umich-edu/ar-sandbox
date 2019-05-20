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
        Debug.Log(noteTextBox.Length);
        for (var i = 0; i < noteTextBox.Length; i++)
        {
            noteTextBox[i].text = newText;
        }
    }
}
