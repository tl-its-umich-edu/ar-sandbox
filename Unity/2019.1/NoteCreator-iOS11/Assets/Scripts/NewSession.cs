using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is meant to just run on the first frame of the app and then destroy itself.

public class NewSession : MonoBehaviour
{
    private CaliperEventCreator caliperEventCreatorScript;

    // Start is called before the first frame update
    void Start()
    {
        caliperEventCreatorScript = GetComponent<CaliperEventCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        // send SessionLoggedIn event for the beginning of the app
        caliperEventCreatorScript.SessionLoggedIn();

        Destroy(this);
    }
}
