using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;

public class CaliperEventCreator : MonoBehaviour
{
    public string destinationURL = "http://lti.tools/caliper/event?key=milk";
    public string thisBearerTokenPath = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void CreateCaliperEventAsync(string actor, string action, string _object, string id = "", string type = "")
	{
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
        caliperEventData.id = id;
        caliperEventData.type = type;
        caliperEventData.actor = actor;
		caliperEventData.action = action;
		caliperEventData._object = _object;

		// convert object to json string and edit typos

		string json = JsonUtility.ToJson(caliperEvent);
		json = json.Replace("\"context\"", "\"@context\""); // add @ to content
		json = json.Replace("\"_object\"", "\"object\""); // remove underscore from object

		Debug.Log(">>>>> Content: " + json);

        await PushCaliperEventAsync(json, destinationURL, thisBearerTokenPath);
	}

	private async System.Threading.Tasks.Task PushCaliperEventAsync(string json, string pushURL, string bearerTokenPath = "")
	{
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
    public string id;
    public string type;
	public string actor;
	public string action;
	public string _object; // remove _ when converted to json string
}
