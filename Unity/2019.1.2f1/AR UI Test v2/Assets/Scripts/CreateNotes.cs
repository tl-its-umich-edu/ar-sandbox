using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateNotes : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectToPlace;
    public TMP_InputField inputField;
    public Button placeButton, deleteButton;

    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid;

    // Start is called before the first frame update
    void Start()
    {
        placeButton.onClick.AddListener(PlaceNote);
        deleteButton.onClick.AddListener(DeleteNote);

        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void PlaceNote()
    {
        GameObject newNote = Instantiate(objectToPlace, placementPose.position, placementPose.rotation) as GameObject;
        NoteBehavior newNoteScript = newNote.GetComponent<NoteBehavior>();
        newNoteScript.changeNoteText(inputField.text);
        inputField.text = "";
    }

    private void DeleteNote()
    {

    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

            placeButton.interactable = true;
        }
        else
        {
            placementIndicator.SetActive(false);

            placeButton.interactable = true;
        }
    }
}
