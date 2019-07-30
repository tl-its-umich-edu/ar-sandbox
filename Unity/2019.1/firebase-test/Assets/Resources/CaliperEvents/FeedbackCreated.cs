using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FeedbackCreated : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string CreateEvent(
        string feedbackObjectId,
        string feedbackObjectDesc,
        string text,
        string author)
    {
        CaliperEventFeedbackCreated ce = new CaliperEventFeedbackCreated();
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

        CaliperEventFeedbackCreatedData data = new CaliperEventFeedbackCreatedData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "Event";

        CaliperEventFeedbackCreatedDataActor actor = new CaliperEventFeedbackCreatedDataActor();
        actor.id = "urn:umich:artool:feedbackcreator:" + SystemInfo.deviceName;
        actor.type = "Person";

        data.actor = actor;
        data.action = "Created";

        CaliperEventFeedbackCreatedDataObject _object = new CaliperEventFeedbackCreatedDataObject();
        _object.id = "urn:umich:artool:feedbackcreator:" + feedbackObjectId;
        _object.description = feedbackObjectDesc;
        _object.type = "DigitalResource";

        CaliperEventFeedbackCreatedDataObjectExtensions objectExtensions = new CaliperEventFeedbackCreatedDataObjectExtensions();
        objectExtensions.text = text;
        objectExtensions.author = author;

        _object.extensions = objectExtensions;

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
public class CaliperEventFeedbackCreated
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventFeedbackCreatedData> data = new List<CaliperEventFeedbackCreatedData>();
}

[Serializable]
public class CaliperEventFeedbackCreatedData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventFeedbackCreatedDataActor actor;
    public string action;
    public CaliperEventFeedbackCreatedDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
}

[Serializable]
public class CaliperEventFeedbackCreatedDataActor
{
    public string id;
    public string type;
}

[Serializable]
public class CaliperEventFeedbackCreatedDataObject
{
    public string id;
    public string description;
    public string type;
    public CaliperEventFeedbackCreatedDataObjectExtensions extensions;
}

[Serializable]
public class CaliperEventFeedbackCreatedDataObjectExtensions
{
    public string text;
    public string author;
}
