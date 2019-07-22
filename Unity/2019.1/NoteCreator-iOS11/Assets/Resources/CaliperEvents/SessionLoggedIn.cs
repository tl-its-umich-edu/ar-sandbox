using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class SessionLoggedIn : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string CreateEvent()
    {
        CaliperEventSessionLoggedIn ce = new CaliperEventSessionLoggedIn();
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

        CaliperEventSessionLoggedInData data = new CaliperEventSessionLoggedInData();
        data.context = "http://purl.imsglobal.org/ctx/caliper/v1p1";
        data.id = "urn:uuid:" + Guid.NewGuid().ToString();
        data.type = "SessionEvent";

        CaliperEventSessionLoggedInDataActor actor = new CaliperEventSessionLoggedInDataActor();
        actor.id = "urn:umich:artool:notecreator:" + SystemInfo.deviceName;
        actor.type = "Person";

        data.actor = actor;
        data.action = "LoggedIn";

        CaliperEventSessionLoggedInDataObject _object = new CaliperEventSessionLoggedInDataObject();
        _object.id = "urn:umich:artool:notecreator";
        _object.type = "SoftwareApplication";

        data._object = _object;
        data.eventTime = ce.sendTime;
        data.edApp = "note-creator_ar";

        CaliperEventSessionLoggedInDataSession session = new CaliperEventSessionLoggedInDataSession();
        session.id = "urn:umich:artool:notecreator:" + AnalyticsSessionInfo.sessionId;
        session.type = "Session";

        data.session = session;

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
public class CaliperEventSessionLoggedIn
{
    public string sensor;
    public string sendTime;
    public string dataVersion;
    public List<CaliperEventSessionLoggedInData> data = new List<CaliperEventSessionLoggedInData>();
}

[Serializable]
public class CaliperEventSessionLoggedInData
{
    public string context; // needs to begin with @ when converted to json string
    public string id;
    public string type;
    public CaliperEventSessionLoggedInDataActor actor;
    public string action;
    public CaliperEventSessionLoggedInDataObject _object; // remove _ when converted to json string
    public string eventTime;
    public string edApp;
    public CaliperEventSessionLoggedInDataSession session;
}

[Serializable]
public class CaliperEventSessionLoggedInDataActor
{
    public string id;
    public string type;
}

[Serializable]
public class CaliperEventSessionLoggedInDataObject
{
    public string id;
    public string type;
}

[Serializable]
public class CaliperEventSessionLoggedInDataSession
{
    public string id;
    public string type;
}
