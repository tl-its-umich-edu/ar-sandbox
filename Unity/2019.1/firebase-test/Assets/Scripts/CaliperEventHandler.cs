using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;

public class CaliperEventHandler : MonoBehaviour
{
	public TextAsset thisBearerTokenFile;

	private string thisPushURL = "https://umich.caliper.dev.cloud.unizin.org/"; // udp
	//private string thisPushURL = "http://lti.tools/caliper/event?key=milk"; // test endpoint

	private SessionLoggedIn sessionLoggedIn;
	private FeedbackCreated feedbackCreated;
	private ImageIdentified imageIdentified;
    private FeedbackLoaded feedbackLoaded;
    private PosterContentLoaded posterContentLoaded;

    private bool showDebug = true;

	// Start is called before the first frame update
	void Start()
	{
		// load caliper event templates

		sessionLoggedIn = (SessionLoggedIn)gameObject.AddComponent(typeof(SessionLoggedIn));
		feedbackCreated = (FeedbackCreated)gameObject.AddComponent(typeof(FeedbackCreated));
		imageIdentified = (ImageIdentified)gameObject.AddComponent(typeof(ImageIdentified));
        feedbackLoaded = (FeedbackLoaded)gameObject.AddComponent(typeof(FeedbackLoaded));
        posterContentLoaded = (PosterContentLoaded)gameObject.AddComponent(typeof(PosterContentLoaded));

        // send caliper event signalling beginning of session

        SessionLoggedIn();
    }

	// Update is called once per frame
	void Update()
	{

	}

	public async void SessionLoggedIn()
	{
		string json = sessionLoggedIn.CreateEvent();

		await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
	}

	public async void FeedbackCreated(string posterName, string feedbackObjectId, string feedbackObjectDesc, string text, string author)
	{
		string json = feedbackCreated.CreateEvent(posterName, feedbackObjectId, feedbackObjectDesc, text, author);

		await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

    public async void ImageIdentified(string imageName, string imageId, string imageDescription)
    {
        string json = imageIdentified.CreateEvent(imageName, imageId, imageDescription);

        await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

	public async void FeedbackLoaded(int feedbackRetrievedCount, string objectName, string objectId, string objectDescription)
	{
		string json = feedbackLoaded.CreateEvent(feedbackRetrievedCount, objectName, objectId, objectDescription);

		await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
	}

    public async void PosterContentLoaded(string posterContentObjectName, string posterContentObjectId, string posterContentObjectDesc)
    {
        string json = posterContentLoaded.CreateEvent(posterContentObjectName, posterContentObjectId, posterContentObjectDesc);

        await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

    private async System.Threading.Tasks.Task PushCaliperEventAsync(string json, string pushURL, TextAsset bearerTokenFile)
	{
        if (showDebug)
        {
            Debug.Log(">>>>> Content: " + json);
            Debug.Log(">>>>> Destination: " + pushURL);
        }

		var content = new StringContent(json, Encoding.UTF8, "application/json"); // middleman converting string to HTTPContent

		HttpClient client = new HttpClient();

		if (bearerTokenFile != null)
		{
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerTokenFile.text); // add bearer token header
			if (showDebug) Debug.Log(">>>>> Bearer Token Loaded");
		}
		else
		{
            if (showDebug) Debug.Log(">>>>> No Bearer Token Path");
		}

		var response = await client.PostAsync(pushURL, content);

		var responseString = await response.Content.ReadAsStringAsync();

        if (showDebug)
        {
            Debug.Log(">>>>> Status: " + response.StatusCode);
            Debug.Log(">>>>> Response: " + responseString);
        }
	}
}