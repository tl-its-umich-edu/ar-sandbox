using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    public GameObject hiroTrackerPrefab;

    private ARTrackedImageManager trackedImageManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            switch (trackedImage.referenceImage.name)
            {
                case "hiro tracker image":
                    GameObject hiroTrackerObj = Instantiate(hiroTrackerPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                    hiroTrackerObj.transform.parent = trackedImage.transform;
                    hiroTrackerObj.name = "hiro tracker object";
                    Debug.Log(">>>>>hiro tracker object added - instance id: " + hiroTrackerObj.GetInstanceID());
                    break;
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {

        }

        foreach (var trackedImage in eventArgs.removed)
        {

        }
    }
}
