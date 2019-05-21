using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class TrackedImageManipulator : MonoBehaviour
{
    ARTrackedImageManager trackedImageManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(trackedImageManager.trackables.count);
    }
}
