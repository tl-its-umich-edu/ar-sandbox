using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateGroup : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject groupPrefab;

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    private Pose placementPose;
    private bool placementPoseIsValid;

    public GameObject startPanel;
    public Button placeButton;
    public GameObject waveIndicatorPanel;

    private RawImage waveIndicator;
    private RectTransform waveIndicatorRT;
    private Color defaultPanelColor;

    public bool runUpdateRoutines = true;

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();

        placeButton.onClick.AddListener(PlaceGroup);

        startPanel.SetActive(true);

        defaultPanelColor = startPanel.GetComponent<Image>().color;

        waveIndicatorRT = waveIndicatorPanel.GetComponent<RectTransform>();

        waveIndicator = startPanel.GetComponentInChildren<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (runUpdateRoutines)
        {
            UpdateStartPanel();
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
    }

    private void UpdateStartPanel()
    {
        if (placementPoseIsValid)
        {
            startPanel.GetComponent<Image>().color = Color.clear;
            startPanel.GetComponentInChildren<TMP_Text>().enabled = false;
            waveIndicator.enabled = false;

            placeButton.interactable = true;
        }
        else
        {
            startPanel.GetComponent<Image>().color = defaultPanelColor;
            startPanel.GetComponentInChildren<TMP_Text>().enabled = true;
            waveIndicator.enabled = true;

            waveIndicatorRT.localPosition = new Vector3(
                Mathf.Sin((float)(Time.time * 1.5)) * Screen.width / 4,
                waveIndicatorRT.localPosition.y,
                waveIndicatorRT.localPosition.z
                );

            placeButton.interactable = false; // false when not debugging
        }
    }

    private void UpdatePlacementPose() // note: AR Plane Manager is only set to look for horizontal planes
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);

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
            placementIndicator.GetComponent<MeshRenderer>().enabled = true;
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void PlaceGroup()
    {
        GameObject groupObj = Instantiate(groupPrefab, placementPose.position, placementPose.rotation) as GameObject;
        groupObj.name = "Group"; // or else would be "Group(Clone)"

        startPanel.SetActive(false);
        placementIndicator.GetComponent<MeshRenderer>().enabled = false;
        runUpdateRoutines = false;
    }
}
