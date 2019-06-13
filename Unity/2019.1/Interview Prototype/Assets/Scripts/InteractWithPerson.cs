// todo: highlight character based on selection


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithPerson : MonoBehaviour
{
    public GameObject chatPanel;
    public RawImage chatIndicator;
    public Button whoButton;
    public Button whereButton;

    private bool scriptActive = false;

    private RaycastHit talkRayHit;

    private GameObject teacherPrefab;
    private GameObject doctorPrefab;
    private GameObject sharkPrefab;

    // Start is called before the first frame update
    void Start()
    {
        chatPanel.SetActive(false);

        whoButton.onClick.AddListener(WhoButtonAction);
        whereButton.onClick.AddListener(WhereButtonAction);
    }

    // Update is called once per frame
    void Update()
    {
        if (!scriptActive && GameObject.Find("Group") != null)
        {
            scriptActive = true;
            chatPanel.SetActive(true);
        }
        else
        {
            var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var talkRayDidHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out talkRayHit, Mathf.Infinity);

            if (talkRayDidHit && talkRayHit.collider.name.ToLower().Contains("teacher"))
            {
                teacherPrefab = talkRayHit.collider.gameObject;
                chatIndicator.GetComponent<RawImage>().CrossFadeAlpha(1, .3f, false);
            }
            else if (talkRayDidHit && talkRayHit.collider.name.ToLower().Contains("doctor"))
            {
                doctorPrefab = talkRayHit.collider.gameObject;
                chatIndicator.GetComponent<RawImage>().CrossFadeAlpha(1, .3f, false);
            }
            else if (talkRayDidHit && talkRayHit.collider.name.ToLower().Contains("shark"))
            {
                sharkPrefab = talkRayHit.collider.gameObject;
                chatIndicator.GetComponent<RawImage>().CrossFadeAlpha(1, .3f, false);
            }
            else
            {
                chatIndicator.GetComponent<RawImage>().CrossFadeAlpha(.4f, .3f, false);
            }
        }
    }

    // todo: clean up talk code, it's all hardcoded and messy

    private void WhoButtonAction()
    {
        if (talkRayHit.collider.name.ToLower().Contains("teacher"))
        {
            var textBox = teacherPrefab.transform.Find("Quad").gameObject;
            textBox.SetActive(true);

            teacherPrefab.GetComponentInChildren<TMP_Text>().SetText("I am a teacher.");

            teacherPrefab.transform.Find("Who Voice Line").gameObject.GetComponent<AudioSource>().Play();
        }
        else if (talkRayHit.collider.name.ToLower().Contains("doctor"))
        {
            var textBox = doctorPrefab.transform.Find("Quad").gameObject;
            textBox.SetActive(true);

            doctorPrefab.GetComponentInChildren<TMP_Text>().SetText("I am a doctor.");

            doctorPrefab.transform.Find("Who Voice Line").gameObject.GetComponent<AudioSource>().Play();
        }
        else if (talkRayHit.collider.name.ToLower().Contains("shark"))
        {
            sharkPrefab.transform.Find("Who Voice Line").gameObject.GetComponent<AudioSource>().Play();
        }

    }
    private void WhereButtonAction()
    {
        if (talkRayHit.collider.name.ToLower().Contains("teacher"))
        {
            var textBox = teacherPrefab.transform.Find("Quad").gameObject;
            textBox.SetActive(true);

            teacherPrefab.GetComponentInChildren<TMP_Text>().SetText("I work at the University of Michigan.");
            teacherPrefab.transform.Find("Where Voice Line").gameObject.GetComponent<AudioSource>().Play();
        }
        else if (talkRayHit.collider.name.ToLower().Contains("doctor"))
        {
            var textBox = doctorPrefab.transform.Find("Quad").gameObject;
            textBox.SetActive(true);

            doctorPrefab.GetComponentInChildren<TMP_Text>().SetText("I work at St. Joseph Mercy Hospital.");
            doctorPrefab.transform.Find("Where Voice Line").gameObject.GetComponent<AudioSource>().Play();
        }
        else if (talkRayHit.collider.name.ToLower().Contains("shark"))
        {
            sharkPrefab.transform.Find("Where Voice Line").gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
