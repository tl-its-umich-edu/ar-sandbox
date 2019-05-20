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
    private ARPlaneManager arPlaneManager;
    private Pose placementPose;
    private bool placementPoseIsValid;

    private RaycastHit deleteRayHit;

    // Start is called before the first frame update
    void Start()
    {
        placeButton.onClick.AddListener(PlaceNote);
        deleteButton.onClick.AddListener(DeleteNote);

        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        UpdateDeleteButton();
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
        Destroy(deleteRayHit.transform.gameObject);
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

            if (placementPose.rotation.eulerAngles.x < 10) // if looking at horizontal plane
            {
                var cameraForward = Camera.main.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }          
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

            placeButton.interactable = false;
        }
    }

    private void UpdateDeleteButton()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out deleteRayHit, Mathf.Infinity)) // also sets deleteRayHit
        {
            deleteButton.interactable = true;
        }
        else
        {
            deleteButton.interactable = false;
        }
    }
}
