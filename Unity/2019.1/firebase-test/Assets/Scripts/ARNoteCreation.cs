using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARNoteCreation : MonoBehaviour
{
    public Button placeButton;
    public TMP_InputField noteTextField;
    public GameObject placementIndicator, notePrefab;

    private float lerpSpeed = .5f;

    private SurfaceDetection surfaceDetection;
    private CaliperEventHandler caliperEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        surfaceDetection = GetComponent<SurfaceDetection>();
        caliperEventHandler = GetComponent<CaliperEventHandler>();

        placeButton.onClick.AddListener(placeButtonEvent);

        caliperEventHandler.SessionLoggedIn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtons();
        UpdatePlacementPose();
    }

    private void placeButtonEvent() // place note object
    {
        PlaceNote(noteTextField.text, placementIndicator.transform.position, placementIndicator.transform.rotation);
        noteTextField.text = "";
    }

    private void UpdateButtons()
    {
        if (surfaceDetection.GetPlacementPoseIsValid() && noteTextField.text != "")
        {
            placeButton.interactable = true;
        }
        else
        {
            placeButton.interactable = false;
        }
    }

    private void UpdatePlacementPose()
    {
        if (surfaceDetection.GetPlacementPoseIsValid())
        {
            var placementPose = surfaceDetection.GetPlacementPose();

            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, Quaternion.Lerp(placementIndicator.transform.rotation, placementPose.rotation, lerpSpeed));
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void PlaceNote(string noteText, Vector3 position, Quaternion rotation)
    {
        GameObject newNote = Instantiate(notePrefab, position, rotation) as GameObject;
        newNote.GetComponentInChildren<TMP_Text>().text = noteText;
    }
}
