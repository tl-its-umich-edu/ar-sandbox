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
    private CaliperEventHandler caliperEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        arNoteCreation = GameObject.Find("Script Manager").GetComponent<ARNoteCreation>();
        caliperEventHandler = GameObject.Find("Script Manager").GetComponent<CaliperEventHandler>();
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
                GameObject prefabToCreate = null;

                switch (trackedImage.referenceImage.name)
                {
                    case "Augmented Reality Poster":
                        prefabToCreate = arPrefab;
                        break;

                    case "Selenium Poster":
                        prefabToCreate = seleniumPrefab;
                        break;

                    case "Study Spot Finder Poster":
                        prefabToCreate = heatmapPrefab;
                        break;

                    case "Documentary Poster":
                        prefabToCreate = documentaryPrefab;
                        break;

                    case "MyLA Poster":
                        prefabToCreate = mylaPrefab;
                        break;

                    case "Dining Chatbot Poster":
                        prefabToCreate = chatbotPrefab;
                        break;
                }

                GameObject obj = Instantiate(prefabToCreate, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                obj.transform.parent = trackedImage.transform;
                obj.name = trackedImage.referenceImage.name;

                arNoteCreation.SetAnchorObject(obj);

                Debug.Log(">>>>> Image found: " + trackedImage.referenceImage.name);

                arNoteCreation.LoadExistingFeedbackAsync();

                SendContentLoadedAnalytics(obj);
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            
        }
    }

    private void SendContentLoadedAnalytics(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++) {
            if (obj.transform.GetChild(i).gameObject.name == "Content")
            {
                GameObject objContent = obj.transform.GetChild(i).gameObject;

                caliperEventHandler.PosterContentLoaded(obj.name + " Content", objContent.GetInstanceID().ToString(), "This object has " + objContent.transform.childCount + " child objects.");
                break;
            }
            else if (i == obj.transform.childCount - 1)
            {
                Debug.Log(obj.name + " doesn't have a child object named Content.");
            }
        }
    }
}
