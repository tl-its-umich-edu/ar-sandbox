using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using ImsGlobal.Caliper;
using ImsGlobal.Caliper.Entities.Agent;
using ImsGlobal.Caliper.Entities.Media;
using ImsGlobal.Caliper.Events.Media;
using NodaTime;
using System;
using System.Text;

public class CaliperEventManager : MonoBehaviour
{
    private string endpointID;

    private string deviceID;

    public Button actionButton;
    public Button testPushButton;
    public Button customCaliperButton;

    private static readonly HttpClient client = new HttpClient();

    // Start is called before the first frame update
    void Start()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier; // unique for every device

        actionButton.onClick.AddListener(ActionButtonEvent);
        testPushButton.onClick.AddListener(TestPushButtonEventAsync);
        customCaliperButton.onClick.AddListener(CustomCaliperButtonEventAsync);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ActionButtonEvent()
    {
        TestCaliperEventAsync();
    }

    async void TestCaliperEventAsync() // should not return void
    {
        // this is the example code from the caliper-net readme file with some small changes.
        // this code will run from the editor and successfully send a caliper event to the
        // endpoint specified but does not work correctly when built to an iOS device.
        
        var sensor = new CaliperSensor("milk-unity-caliper-test");
        System.Uri endpointURI = new System.Uri("https://lti.tools/caliper/event?key=milk");
        string endpointId = sensor.RegisterEndpoint(new CaliperEndpointOptions(endpointURI));

        var now = System.DateTime.Now;

        Debug.Log(">>>>> Attempt async at: " + now);

        // dummy event taken from example
        var mediaEvent = new MediaEvent("event id", ImsGlobal.Caliper.Events.Action.Paused)
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
        Debug.Log(">>>>> Success: " + success);
    }

    private async void TestPushButtonEventAsync() // should not return void
    {
        await PostPushyamiEndpointAsync();
    }

    private async System.Threading.Tasks.Task PostPushyamiEndpointAsync()
    {
        // This will send a POST request to the endpoint specified
        // This was written as a test to make sure that Unity and iOS
        // could handle sending data to a server.

        var values = new Dictionary<string, string>
        {
           { "data", "valid" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("https://3eec5ec8-3d63-4c36-9789-52475ebc46d2.mock.pstmn.io/simpledata", content);

        var responseString = await response.Content.ReadAsStringAsync();

        Debug.Log(responseString);
    }

    private async void CustomCaliperButtonEventAsync()
    {
        await PushCaliperEventAsync("http://lti.tools/caliper/event?key=milk");
        //await PushMediaEventAsync("https://postman-echo.com/post");
    }

    private async System.Threading.Tasks.Task PushCaliperEventAsync(string pushURL)
    {
        // fill in data fields

        CaliperEvent caliperEvent = new CaliperEvent();
        caliperEvent.sensor = "sensor";
        caliperEvent.dataVersion = "http://purl.imsglobal.org/ctx/caliper/v1p1";

        // todo: hour offset, current time recorded is in local timezone
        var now = System.DateTime.Now;
        caliperEvent.sendTime =
            now.Year + "-" +
            now.Month.ToString().PadLeft(2, '0') + "-" +
            now.Day.ToString().PadLeft(2, '0') + "T" +
            now.TimeOfDay.Hours.ToString().PadLeft(2, '0') + ":" +
            now.TimeOfDay.Minutes.ToString().PadLeft(2, '0') + ":" +
            now.TimeOfDay.Seconds.ToString().PadLeft(2, '0') + "Z";

        CaliperEventData caliperEventData = new CaliperEventData();
        caliperEvent.data.Add(caliperEventData);

        caliperEventData.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        caliperEventData.actor = "actor";
        caliperEventData.action = "action";
        caliperEventData._object = "object";

        // convert object to json string and edit typos

        string json = JsonUtility.ToJson(caliperEvent);
        json = json.Replace("\"context\"", "\"@context\""); // add @ to content
        json = json.Replace("\"_object\"", "\"object\""); // remove underscore from object

        Debug.Log(">>>>> Content: " + json);

        // push json to endpoint

        Debug.Log(">>>>> Destination: " + pushURL);

        var content = new StringContent(json, Encoding.UTF8, "application/json"); // middleman converting string to HTTPContent
        var response = await client.PostAsync(pushURL, content);
        var responseString = await response.Content.ReadAsStringAsync();
        Debug.Log(">>>>> Status: " + response.StatusCode);
        Debug.Log(">>>>> Response: " + responseString);
    }
}

[Serializable]
public class CaliperEvent
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventData> data = new List<CaliperEventData>();
}

[Serializable]
public class CaliperEventData
{
    public string context; // needs to begin with @ when converted to json string
    public string actor;
    public string action;
    public string _object; // remove _ when converted to json string
}
