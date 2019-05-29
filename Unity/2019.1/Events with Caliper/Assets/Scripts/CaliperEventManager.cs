using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImsGlobal.Caliper;
using ImsGlobal.Caliper.Entities.Agent;
using ImsGlobal.Caliper.Entities.Media;
using ImsGlobal.Caliper.Events;
using ImsGlobal.Caliper.Events.Media;
using NodaTime;
using UnityEngine.UI;

public class CaliperEventManager : MonoBehaviour
{
    private string endpointID;

    private string deviceID;

    public Button actionButton;

    // Start is called before the first frame update
    void Start()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier; // unique for every device

        actionButton.onClick.AddListener(TestCaliperButton);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TestCaliperButton()
    {
        TestCaliperEventAsync();
    }

    async void TestCaliperEventAsync()
    {
        var sensor = new CaliperSensor("milk-unity-caliper-test");
        System.Uri endpointURI = new System.Uri("https://lti.tools/caliper/event?key=milk");
        string endpointId = sensor.RegisterEndpoint(new CaliperEndpointOptions(endpointURI));

        var now = System.DateTime.Now;

        Debug.Log(">>>>> Attempt async at: " + now);

        // dummy event taken from example
        var mediaEvent = new MediaEvent("event id", Action.Paused)
        {
            Actor = new Person(deviceID),
            Object = new VideoObject("https://example.com/super-media-tool/video/1225"),
            Target = new MediaLocation("Action button"),
            EventTime = Instant.FromUtc(now.Year, now.Month, now.Day, now.TimeOfDay.Hours, now.TimeOfDay.Minutes),
            EdApp = new SoftwareApplication("https://example.com/super-media-tool")
            {
                Name = "Super Media Tool"
            }
        };

        bool success = await sensor.SendAsync(mediaEvent);
        Debug.Log("Success: " + success);
    }
}
