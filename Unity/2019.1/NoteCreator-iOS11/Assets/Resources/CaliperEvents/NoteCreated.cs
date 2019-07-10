using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NoteCreated : MonoBehaviour
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
        CaliperEventNoteCreated ce = new CaliperEventNoteCreated();
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

        CaliperEventNoteCreatedData data = new CaliperEventNoteCreatedData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "Event";

        CaliperEventNoteCreatedDataActor actor = new CaliperEventNoteCreatedDataActor();
        actor.id = "urn:umich:artool:notecreator:" + SystemInfo.deviceName;
        actor.type = "Person";

        data.actor = actor;
        data.action = "Deleted";

        CaliperEventNoteCreatedDataObject _object = new CaliperEventNoteCreatedDataObject();
        _object.id = "urn:umich:artool:notecreator:" + noteObjectId;
        _object.description = noteObjectDesc;
        _object.type = "DigitalResource";

        CaliperEventNoteCreatedDataObjectExtensions objectExtensions = new CaliperEventNoteCreatedDataObjectExtensions();
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
public class CaliperEventNoteCreated
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventNoteCreatedData> data = new List<CaliperEventNoteCreatedData>();
}

[Serializable]
public class CaliperEventNoteCreatedData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventNoteCreatedDataActor actor;
    public string action;
    public CaliperEventNoteCreatedDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
}

[Serializable]
public class CaliperEventNoteCreatedDataActor
{
    public string id;
    public string type;
}

[Serializable]
public class CaliperEventNoteCreatedDataObject
{
    public string id;
    public string description;
    public string type;
    public CaliperEventNoteCreatedDataObjectExtensions extensions;
}

[Serializable]
public class CaliperEventNoteCreatedDataObjectExtensions
{
    public string orientation;
    public string notePlaced;
}
