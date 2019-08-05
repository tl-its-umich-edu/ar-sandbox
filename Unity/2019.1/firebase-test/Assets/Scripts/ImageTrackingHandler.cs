using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingHandler : MonoBehaviour
{
    public GameObject arPrefab, seleniumPrefab, heatmapPrefab, documentaryPrefab, mylaPrefab, chatbotPrefab;

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
            
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (arNoteCreation.GetAnchorObject() == null && trackedImage.trackingState == TrackingState.Tracking)
            {
                switch (trackedImage.referenceImage.name)
                {
                    case "ar":
                        GameObject arObj = Instantiate(arPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                        arObj.transform.parent = trackedImage.transform;
                        arObj.name = "ar";

                        arNoteCreation.SetAnchorObject(arObj);

                        break;

                    case "selenium":
                        GameObject seleniumObj = Instantiate(seleniumPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                        seleniumObj.transform.parent = trackedImage.transform;
                        seleniumObj.name = "selenium";

                        arNoteCreation.SetAnchorObject(seleniumObj);

                        break;

                    case "heatmap":
                        GameObject heatmapObj = Instantiate(heatmapPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                        heatmapObj.transform.parent = trackedImage.transform;
                        heatmapObj.name = "heatmap";

                        arNoteCreation.SetAnchorObject(heatmapObj);

                        break;

                    case "documentary":
                        GameObject documentaryObj = Instantiate(documentaryPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                        documentaryObj.transform.parent = trackedImage.transform;
                        documentaryObj.name = "documentary";

                        arNoteCreation.SetAnchorObject(documentaryObj);

                        break;

                    case "myla":
                        GameObject mylaObj = Instantiate(mylaPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                        mylaObj.transform.parent = trackedImage.transform;
                        mylaObj.name = "myla";

                        arNoteCreation.SetAnchorObject(mylaObj);

                        break;

                    case "chatbot":
                        GameObject chatbotObj = Instantiate(chatbotPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                        chatbotObj.transform.parent = trackedImage.transform;
                        chatbotObj.name = "chatbot";

                        arNoteCreation.SetAnchorObject(chatbotObj);

                        break;
                }

                Debug.Log(">>>>> Image found: " + trackedImage.referenceImage.name);

                arNoteCreation.LoadExistingFeedbackAsync();
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            
        }
    }
}
