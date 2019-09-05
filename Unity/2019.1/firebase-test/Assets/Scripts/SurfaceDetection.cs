using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

public class SurfaceDetection : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;

    private Pose placementPose;
    private bool placementPoseIsValid;

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
    }

    private void UpdatePlacementPose()
    {
        // send raycast from center of screen to detected surfaces

        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        // adjust height to fit visible area (add feedback panel height and touch keyboard height to screenCenter Y)
        var feedbackPanelHeight = GameObject.Find("FeedbackPanel").GetComponent<RectTransform>().sizeDelta.y;
        screenCenter.y += feedbackPanelHeight / 2 + TouchScreenKeyboard.area.height / 2;

        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, (UnityEngine.XR.ARSubsystems.TrackableType)TrackableType.PlaneWithinBounds);

        // get pose of detected surface

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            if (placementPose.rotation.eulerAngles.x < 10) // if looking at horizontal plane
            {
                // rotate pose to face camera

                var cameraForward = Camera.main.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
    }

    public bool GetPlacementPoseIsValid()
    {
        return placementPoseIsValid;
    }

    public Pose GetPlacementPose()
    {
        return placementPose;
    }
}
