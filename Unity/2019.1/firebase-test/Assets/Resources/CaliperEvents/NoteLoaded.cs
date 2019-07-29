using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NoteLoaded : MonoBehaviour
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
        CaliperEventNoteLoaded ce = new CaliperEventNoteLoaded();
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

        CaliperEventNoteLoadedData data = new CaliperEventNoteLoadedData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "Event";

        CaliperEventNoteLoadedDataActor actor = new CaliperEventNoteLoadedDataActor();
        actor.id = "urn:umich:artool:notecreator:" + SystemInfo.deviceName;
        actor.type = "Person";

        data.actor = actor;
        data.action = "Retrieved";

        CaliperEventNoteLoadedDataObject _object = new CaliperEventNoteLoadedDataObject();
        _object.id = "urn:umich:artool:notecreator:" + noteObjectId;
        _object.description = noteObjectDesc;
        _object.type = "DigitalResource";

        data._object = _object;
        data.eventTime = ce.sendTime;
        data.edApp = "note-creator_ar";

        ce.data.Add(data);

        // convert object to json string and edit typos

        string json = JsonUtility.ToJson(ce);
        json = json.Replace("\"context\"", "\"@context\""); // add @ to content
        json = json.Replace("\"_object\"", "\"object\""); // remove underscore from object

        return json;
    }
}

[Serializable]
public class CaliperEventNoteLoaded
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventNoteLoadedData> data = new List<CaliperEventNoteLoadedData>();
}

[Serializable]
public class CaliperEventNoteLoadedData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventNoteLoadedDataActor actor;
    public string action;
    public CaliperEventNoteLoadedDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
}

[Serializable]
public class CaliperEventNoteLoadedDataActor
{
    public string id;
    public string type;
}

[Serializable]
public class CaliperEventNoteLoadedDataObject
{
    public string id;
    public string description;
    public string type;
}
