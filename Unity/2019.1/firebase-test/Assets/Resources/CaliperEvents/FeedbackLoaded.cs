using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FeedbackLoaded : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string CreateEvent(int feedbackRetrievedCount, string feedbackObjectName, string feedbackObjectId, string feedbackObjectDesc)
    {
        CaliperEventFeedbackLoaded ce = new CaliperEventFeedbackLoaded();
        ce.sensor = "sensor";
        ce.dataVersion = "http://purl.imsglobal.org/ctx/caliper/v1p1";

        var now = System.DateTime.UtcNow;
        ce.sendTime =
            now.Year + "-" +
            now.Month.ToString().PadLeft(2, '0') + "-" +
            now.Day.ToString().PadLeft(2, '0') + "T" +
            now.TimeOfDay.Hours.ToString().PadLeft(2, '0') + ":" +
            now.TimeOfDay.Minutes.ToString().PadLeft(2, '0') + ":" +
            now.TimeOfDay.Seconds.ToString().PadLeft(2, '0') + "." +
            now.TimeOfDay.Milliseconds.ToString().PadLeft(3, '0') + "Z";

        CaliperEventFeedbackLoadedData data = new CaliperEventFeedbackLoadedData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "Event";

        CaliperEventFeedbackLoadedDataActor actor = new CaliperEventFeedbackLoadedDataActor();
        actor.id = "urn:umich:artool:feedbackcreator:unauthenticated";
        actor.type = "Person";

        CaliperEventFeedbackLoadedDataActorExtensions actorExtensions = new CaliperEventFeedbackLoadedDataActorExtensions();
        actorExtensions.feedbackRetrievedCount = feedbackRetrievedCount;
        actorExtensions.deviceId = SystemInfo.deviceUniqueIdentifier;
        actorExtensions.deviceName = SystemInfo.deviceName;
        actorExtensions.deviceModel = SystemInfo.deviceModel;
        actorExtensions.deviceType = SystemInfo.deviceType.ToString();

        actor.extensions = actorExtensions;

        data.actor = actor;
        data.action = "Retrieved";

        CaliperEventFeedbackLoadedDataObject _object = new CaliperEventFeedbackLoadedDataObject();
        _object.id = "urn:umich:artool:feedbackcreator:" + feedbackObjectId;
        _object.name = feedbackObjectName;
        _object.description = feedbackObjectDesc;
        _object.type = "DigitalResource";

        data._object = _object;
        data.eventTime = ce.sendTime;
        data.edApp = "feedback-creator_ar";

        ce.data.Add(data);

        // convert object to json string and edit typos

        string json = JsonUtility.ToJson(ce);
        json = json.Replace("\"context\"", "\"@context\""); // add @ to content
        json = json.Replace("\"_object\"", "\"object\""); // remove underscore from object

        return json;
    }
}

[Serializable]
public class CaliperEventFeedbackLoaded
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventFeedbackLoadedData> data = new List<CaliperEventFeedbackLoadedData>();
}

[Serializable]
public class CaliperEventFeedbackLoadedData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventFeedbackLoadedDataActor actor;
    public string action;
    public CaliperEventFeedbackLoadedDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
}

[Serializable]
public class CaliperEventFeedbackLoadedDataActor
{
    public string id;
    public string type;
    public CaliperEventFeedbackLoadedDataActorExtensions extensions;
}

[Serializable]
public class CaliperEventFeedbackLoadedDataActorExtensions
{
    public int feedbackRetrievedCount;
    public string deviceId;
    public string deviceName;
    public string deviceModel;
    public string deviceType;
}

[Serializable]
public class CaliperEventFeedbackLoadedDataObject
{
    public string name;
    public string id;
    public string description;
    public string type;
}
