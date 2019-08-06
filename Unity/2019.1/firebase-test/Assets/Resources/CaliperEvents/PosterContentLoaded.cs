using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PosterContentLoaded : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string CreateEvent(string posterContentObjectName, string posterContentObjectId, string posterContentObjectDesc)
    {
        CaliperEventPosterContentLoaded ce = new CaliperEventPosterContentLoaded();
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

        CaliperEventPosterContentLoadedData data = new CaliperEventPosterContentLoadedData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "Event";

        CaliperEventPosterContentLoadedDataActor actor = new CaliperEventPosterContentLoadedDataActor();
        actor.id = "urn:umich:artool:feedbackcreator:unauthenticated";
        actor.type = "Person";

        CaliperEventPosterContentLoadedDataActorExtensions actorExtensions = new CaliperEventPosterContentLoadedDataActorExtensions();
        actorExtensions.deviceId = SystemInfo.deviceUniqueIdentifier;
        actorExtensions.deviceName = SystemInfo.deviceName;
        actorExtensions.deviceModel = SystemInfo.deviceModel;
        actorExtensions.deviceType = SystemInfo.deviceType.ToString();

        actor.extensions = actorExtensions;

        data.actor = actor;
        data.action = "Retrieved";

        CaliperEventPosterContentLoadedDataObject _object = new CaliperEventPosterContentLoadedDataObject();
        _object.id = "urn:umich:artool:feedbackcreator:" + posterContentObjectId;
        _object.name = posterContentObjectName;
        _object.description = posterContentObjectDesc;
        _object.type = "DigitalResource";

        data._object = _object;
        data.eventTime = ce.sendTime;
        data.edApp = "posterContent-creator_ar";

        ce.data.Add(data);

        // convert object to json string and edit typos

        string json = JsonUtility.ToJson(ce);
        json = json.Replace("\"context\"", "\"@context\""); // add @ to content
        json = json.Replace("\"_object\"", "\"object\""); // remove underscore from object

        return json;
    }
}

[Serializable]
public class CaliperEventPosterContentLoaded
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventPosterContentLoadedData> data = new List<CaliperEventPosterContentLoadedData>();
}

[Serializable]
public class CaliperEventPosterContentLoadedData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventPosterContentLoadedDataActor actor;
    public string action;
    public CaliperEventPosterContentLoadedDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
}

[Serializable]
public class CaliperEventPosterContentLoadedDataActor
{
    public string id;
    public string type;
    public CaliperEventPosterContentLoadedDataActorExtensions extensions;
}

[Serializable]
public class CaliperEventPosterContentLoadedDataActorExtensions
{
    public string deviceId;
    public string deviceName;
    public string deviceModel;
    public string deviceType;
}

[Serializable]
public class CaliperEventPosterContentLoadedDataObject
{
    public string name;
    public string id;
    public string description;
    public string type;
}
