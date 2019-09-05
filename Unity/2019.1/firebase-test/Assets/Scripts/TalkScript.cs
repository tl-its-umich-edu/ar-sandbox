using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkScript : MonoBehaviour
{
    private AudioSource whoVoiceLine, whereVoiceLine;
    private bool whoVoiceLinePlayedLast = false;
    private GameObject textBox;

    // Start is called before the first frame update
    void Start()
    {
        whoVoiceLine = transform.Find("Who Voice Line").gameObject.GetComponent<AudioSource>();
        whereVoiceLine = transform.Find("Where Voice Line").gameObject.GetComponent<AudioSource>();
        textBox = transform.Find("Quad").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTouch();
    }

    private void CheckForTouch()
    {
        if (Input.touchCount >= 1)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit touchRayHit;
                    bool touchRayDidHit = Physics.Raycast(touchRay, out touchRayHit);
                    if (touchRayDidHit && touchRayHit.transform.gameObject.name == this.name)
                    {
                        if (!whoVoiceLinePlayedLast)
                        {
                            whoVoiceLine.Play();

                            if (name == "Teacher")
                            {
                                textBox.GetComponentInChildren<TMP_Text>().SetText("I am a teacher.");
                            }
                            else
                            {
                                textBox.GetComponentInChildren<TMP_Text>().SetText("I am a doctor.");
                            }
                        }
                        else
                        {
                            whereVoiceLine.Play();

                            if (name == "Teacher")
                            {
                                textBox.GetComponentInChildren<TMP_Text>().SetText("I work at the University of Michigan.");
                            }
                            else
                            {
                                textBox.GetComponentInChildren<TMP_Text>().SetText("I work at St. Joseph Mercy Hospital.");
                            }
                        }

                        textBox.SetActive(true);

                        whoVoiceLinePlayedLast = !whoVoiceLinePlayedLast;
                    }
                }
            }
        }
    }
}
