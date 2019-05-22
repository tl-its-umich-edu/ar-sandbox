using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class TrackedImageManipulator : MonoBehaviour
{
    public GameObject mustachePrefab;
    public GameObject doorNotesPrefab;

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
            Debug.Log(trackedImage.referenceImage.name);
            switch (trackedImage.referenceImage.name)
            {
                case "mcard":
                    GameObject mustache = Instantiate(mustachePrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                    mustache.transform.parent = trackedImage.transform;
                    break;
                case "210":
                    GameObject doorNotesInst = Instantiate(doorNotesPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                    doorNotesInst.transform.parent = trackedImage.transform;
                    break;
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {

        }

        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(trackedImage);
        }
    }
}
