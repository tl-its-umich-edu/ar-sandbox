using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;

public class CaliperEventCreator : MonoBehaviour
{
    public TextAsset thisBearerTokenFile;

    private string thisPushURL = "https://umich.caliper.dev.cloud.unizin.org/"; // udp
    //private string thisPushURL = "http://lti.tools/caliper/event?key=milk"; // test endpoint

    private SessionLoggedIn sessionLoggedIn;
    private NoteCreated noteCreated;
    private NoteDeleted noteDeleted;
    private NoteSaved noteSaved;
    private NoteLoaded noteLoaded;
    private ImageIdentified imageIdentified;

    // Start is called before the first frame update
    void Start()
    {
        // load caliper event templates

        sessionLoggedIn = (SessionLoggedIn)gameObject.AddComponent(typeof(SessionLoggedIn));
        noteCreated = (NoteCreated)gameObject.AddComponent(typeof(NoteCreated));
        noteDeleted = (NoteDeleted)gameObject.AddComponent(typeof(NoteDeleted));
        noteSaved = (NoteSaved)gameObject.AddComponent(typeof(NoteSaved));
        noteLoaded = (NoteLoaded)gameObject.AddComponent(typeof(NoteLoaded));
        imageIdentified = (ImageIdentified)gameObject.AddComponent(typeof(ImageIdentified));
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

    public async void NoteCreated(string noteObjectId, string noteObjectDesc, string deviceOrientation = "N/A", string noteScale = "1", string noteOrientation = "N/A")
    {
        string json = noteCreated.CreateEvent(noteObjectId, noteObjectDesc, deviceOrientation, noteScale, noteOrientation);

        await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

    public async void NoteDeleted(string noteObjectId, string noteObjectDesc)
    {
        string json = noteDeleted.CreateEvent(noteObjectId, noteObjectDesc);

        await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

    public async void NoteSaved(string noteObjectId, string noteObjectDesc)
    {
        string json = noteSaved.CreateEvent(noteObjectId, noteObjectDesc);

        await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

    public async void NoteLoaded(string noteObjectId, string noteObjectDesc)
    {
        string json = noteLoaded.CreateEvent(noteObjectId, noteObjectDesc);

        await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

    public async void ImageIdentified(string imageName, string imageId)
    {
        string json = imageIdentified.CreateEvent(imageName, imageId);

        await PushCaliperEventAsync(json, thisPushURL, thisBearerTokenFile);
    }

    private async System.Threading.Tasks.Task PushCaliperEventAsync(string json, string pushURL, TextAsset bearerTokenFile)
	{
		Debug.Log(">>>>> Destination: " + pushURL);

		var content = new StringContent(json, Encoding.UTF8, "application/json"); // middleman converting string to HTTPContent

		HttpClient client = new HttpClient();

		if (bearerTokenFile != null)
		{
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerTokenFile.text); // add bearer token header
            Debug.Log(">>>>> Bearer Token Loaded");
		}
        else
        {
            Debug.Log(">>>>> No Bearer Token Path");
        }

		var response = await client.PostAsync(pushURL, content);

        var responseString = await response.Content.ReadAsStringAsync();

		Debug.Log(">>>>> Status: " + response.StatusCode);
		Debug.Log(">>>>> Response: " + responseString);
	}
}
