using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ImageIdentified : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string CreateEvent(string imageName, string imageId)
    {
        CaliperEventImageIdentified ce = new CaliperEventImageIdentified();
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

        CaliperEventImageIdentifiedData data = new CaliperEventImageIdentifiedData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "Event";

        CaliperEventImageIdentifiedDataActor actor = new CaliperEventImageIdentifiedDataActor();
        actor.id = "urn:umich:artool:feedbackcreator";
        actor.type = "SoftwareApplication";

        CaliperEventImageIdentifiedDataActorExtensions actorExtensions = new CaliperEventImageIdentifiedDataActorExtensions();
        actorExtensions.deviceId = SystemInfo.deviceUniqueIdentifier;
        actorExtensions.deviceName = SystemInfo.deviceName;
        actorExtensions.deviceModel = SystemInfo.deviceModel;
        actorExtensions.deviceType = SystemInfo.deviceType.ToString();

        actor.actorExtensions = actorExtensions;

        data.actor = actor;
        data.action = "Identified";

        CaliperEventImageIdentifiedDataObject _object = new CaliperEventImageIdentifiedDataObject();
        _object.id = "urn:umich:artool:feedbackcreator:" + imageId;
        _object.description = imageName;
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
public class CaliperEventImageIdentified
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventImageIdentifiedData> data = new List<CaliperEventImageIdentifiedData>();
}

[Serializable]
public class CaliperEventImageIdentifiedData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventImageIdentifiedDataActor actor;
    public string action;
    public CaliperEventImageIdentifiedDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
}

[Serializable]
public class CaliperEventImageIdentifiedDataActor
{
    public string id;
    public string type;
    public CaliperEventImageIdentifiedDataActorExtensions actorExtensions;
}

[Serializable]
public class CaliperEventImageIdentifiedDataActorExtensions
{
    public string deviceId;
    public string deviceName;
    public string deviceModel;
    public string deviceType;
}

[Serializable]
public class CaliperEventImageIdentifiedDataObject
{
    public string id;
    public string description;
    public string type;
}
