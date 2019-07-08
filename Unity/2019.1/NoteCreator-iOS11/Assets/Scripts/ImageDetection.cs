using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class ImageDetection : MonoBehaviour
{
    public GameObject nameplatePrefab;
    public GameObject hiroPrefab;

    private ARTrackedImageManager trackedImageManager;

    // Start is called before the first frame update
    void Start()
    {
        
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
            CreateNotes createNotesScript = GameObject.Find("Interaction").GetComponent<CreateNotes>();

            switch (trackedImage.referenceImage.name)
            {
                case "nameplate amanda":
                    GameObject nameplateObj = Instantiate(nameplatePrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                    nameplateObj.transform.parent = trackedImage.transform;
                    nameplateObj.name = "nameplate";

                    createNotesScript.SetAnchorObject(nameplateObj);

                    break;

                case "hiro":
                    GameObject hiroObj = Instantiate(hiroPrefab, trackedImage.transform.position, trackedImage.transform.rotation) as GameObject;
                    hiroObj.transform.parent = trackedImage.transform;
                    hiroObj.name = "hiro";

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
