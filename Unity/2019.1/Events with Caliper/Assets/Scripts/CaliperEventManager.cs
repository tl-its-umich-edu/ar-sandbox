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
using System.Net.Http.Headers;
using System.IO;

public class CaliperEventManager : MonoBehaviour
{
    private string deviceID;

    public Button caliperNetButton;
    public Button customPushButton;

    public string userBearerToken = "";

    // Start is called before the first frame update
    void Start()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier; // unique for every device

        caliperNetButton.onClick.AddListener(CaliperNetEvent);
        customPushButton.onClick.AddListener(CustomPushButtonEventAsync);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetBearerToken()
    {

    }

    private void CaliperNetEvent()
    {
        TestCaliperNetEventAsync();
    }

    private async void TestCaliperNetEventAsync()
    {
        // this is the example code from the caliper-net readme file with some small changes.
        // this code will run from the editor and successfully send a caliper event to the
        // endpoint specified but does not work correctly when built to an iOS device.
        
        var sensor = new CaliperSensor("milk-unity-caliper-test");
        System.Uri endpointURI = new System.Uri("https://lti.tools/caliper/event?key=milk");
        string endpointID = sensor.RegisterEndpoint(new CaliperEndpointOptions(endpointURI));

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



    private async void CustomPushButtonEventAsync()
    {
        await PushCaliperEventAsync("http://lti.tools/caliper/event?key=milk", userBearerToken);
    }

    private async System.Threading.Tasks.Task PushCaliperEventAsync(string pushURL, string bearerTokenPath = "")
    {
        // create json file manually and send as PUSH request to endpoint

        // fill in data fields

        CaliperEvent caliperEvent = new CaliperEvent();
        caliperEvent.sensor = "sensor";
        caliperEvent.dataVersion = "http://purl.imsglobal.org/ctx/caliper/v1p1";

        // todo: make sure hour is correct for format
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

        HttpClient client = new HttpClient();

        if (bearerTokenPath != "")
        {
            string bearerTokenValue = new StreamReader(bearerTokenPath).ReadToEnd(); // get text from file containing bearer token
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerTokenValue); // add bearer token header
        }

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
