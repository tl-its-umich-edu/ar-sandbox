using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingHandler : MonoBehaviour
{
    public GameObject excomPrefab, exorgPrefab;

    private ARTrackedImageManager trackedImageManager;

    private ARNoteCreation arNoteCreation;

    // Start is called before the first frame update
    void Start()
    {
        arNoteCreation = GameObject.Find("Script Manager").GetComponent<ARNoteCreation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            switch (trackedImage.referenceImage.name)
            {
                case "excom":
                    GameObject excomObj = Instantiate(excomPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                    excomObj.transform.parent = trackedImage.transform;
                    excomObj.name = "excom";

                    arNoteCreation.SetAnchorObject(excomObj);

                    break;


                case "exorg":
                    GameObject exorgObj = Instantiate(exorgPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                    exorgObj.transform.parent = trackedImage.transform;
                    exorgObj.name = "exorg";

                    arNoteCreation.SetAnchorObject(exorgObj);

                    break;
            }

            Debug.Log(">>>>> Image found: " + trackedImage.referenceImage.name);

            arNoteCreation.LoadExistingFeedbackAsync();
        }

        foreach (var trackedImage in eventArgs.updated)
        {

        }

        foreach (var trackedImage in eventArgs.removed)
        {

        }
    }
}
