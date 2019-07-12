using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NoteSaved : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string CreateEvent(string noteObjectId, string noteObjectDesc, string orientation = "horizontal", string notePlaced = "vertical")
    {
        CaliperEventNoteSaved ce = new CaliperEventNoteSaved();
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

        CaliperEventNoteSavedData data = new CaliperEventNoteSavedData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "Event";

        CaliperEventNoteSavedDataActor actor = new CaliperEventNoteSavedDataActor();
        actor.id = "urn:umich:artool:notecreator:" + SystemInfo.deviceName;
        actor.type = "Person";

        data.actor = actor;
        data.action = "Posted";

        CaliperEventNoteSavedDataObject _object = new CaliperEventNoteSavedDataObject();
        _object.id = "urn:umich:artool:notecreator:" + noteObjectId;
        _object.description = noteObjectDesc;
        _object.type = "DigitalResource";

        CaliperEventNoteSavedDataObjectExtensions objectExtensions = new CaliperEventNoteSavedDataObjectExtensions();
        objectExtensions.orientation = orientation;
        objectExtensions.notePlaced = notePlaced;

        _object.extensions = objectExtensions;

        data._object = _object;
        data.eventTime = ce.sendTime;
        data.edApp = "note-creator_ar";

        ce.data.Add(data);

        // convert object to json string and edit typos

        string json = JsonUtility.ToJson(ce);
        json = json.Replace("\"context\"", "\"@context\""); // add @ to content
        json = json.Replace("\"_object\"", "\"object\""); // remove underscore from object

        Debug.Log(">>>>> Content: " + json);

        return json;
    }
}

[Serializable]
public class CaliperEventNoteSaved
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventNoteSavedData> data = new List<CaliperEventNoteSavedData>();
}

[Serializable]
public class CaliperEventNoteSavedData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventNoteSavedDataActor actor;
    public string action;
    public CaliperEventNoteSavedDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
}

[Serializable]
public class CaliperEventNoteSavedDataActor
{
    public string id;
    public string type;
}

[Serializable]
public class CaliperEventNoteSavedDataObject
{
    public string id;
    public string description;
    public string type;
    public CaliperEventNoteSavedDataObjectExtensions extensions;
}

[Serializable]
public class CaliperEventNoteSavedDataObjectExtensions
{
    public string orientation;
    public string notePlaced;
}
