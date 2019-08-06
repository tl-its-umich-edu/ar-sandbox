using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARNoteCreation : MonoBehaviour
{
    public Button placeButton, resetButton;
    public TMP_InputField feedbackTextField, feedbackAuthorField;
    public TMP_Text posterIndicatorText;
    public GameObject placementIndicator, notePrefab;

    private float lerpSpeed = .3f;

    private SurfaceDetection surfaceDetection;
    private CaliperEventHandler caliperEventHandler;
    private FirebaseHandler firebaseHandler;

    private GameObject anchorObject = null;

    // Start is called before the first frame update
    void Start()
    {
        surfaceDetection = GetComponent<SurfaceDetection>();
        caliperEventHandler = GetComponent<CaliperEventHandler>();
        firebaseHandler = GetComponent<FirebaseHandler>();

        placeButton.onClick.AddListener(PlaceButtonEvent);
        resetButton.onClick.AddListener(ResetAnchor);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtons();
        UpdatePlacementPose();
    }

    private void PlaceButtonEvent() // place note object
    {
        GameObject createdFeedback = PlaceFeedback(feedbackTextField.text, feedbackAuthorField.text, placementIndicator.transform.position, placementIndicator.transform.rotation);

        caliperEventHandler.FeedbackCreated(anchorObject.name, createdFeedback.GetInstanceID().ToString(), "Feedback object created by user", feedbackTextField.text, feedbackAuthorField.text);

        feedbackTextField.text = "";
    }

    private void UpdateButtons()
    {
        if (surfaceDetection.GetPlacementPoseIsValid() && feedbackTextField.text != "" && anchorObject != null)
        {
            placeButton.interactable = true;
        }
        else
        {
            placeButton.interactable = false;
        }
    }

    private void UpdatePlacementPose()
    {
        if (surfaceDetection.GetPlacementPoseIsValid())
        {
            var placementPose = surfaceDetection.GetPlacementPose();

            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, Quaternion.Lerp(placementIndicator.transform.rotation, placementPose.rotation, lerpSpeed));
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private GameObject PlaceFeedback(string noteText, string authorText, Vector3 position, Quaternion rotation, bool sendToFirebase = true)
    {
        // create note object

        GameObject newNote = Instantiate(notePrefab, position, rotation) as GameObject;
        newNote.transform.parent = anchorObject.transform;
        newNote.name = "note";

        // set text of note

        TMP_Text[] newNoteText = newNote.GetComponentsInChildren<TMP_Text>();
        newNoteText[0].text = noteText;
        newNoteText[1].text = "- " + authorText;

        // send note data to firebase

        if (sendToFirebase)
        {
            FeedbackData fd = new FeedbackData(
                noteText,
                authorText,
                newNote.transform.position - anchorObject.transform.position,
                newNote.transform.rotation * Quaternion.Inverse(anchorObject.transform.rotation));

            firebaseHandler.AddFeedback(anchorObject.name, fd);
        }

        return newNote;
    }

    private void ResetAnchor()
	{
        Destroy(anchorObject);

        posterIndicatorText.text = "No QR Code Detected";

        feedbackTextField.text = "";
        feedbackAuthorField.text = "";
    }

    public void SetAnchorObject(GameObject anchorObject)
    {
        if (this.anchorObject == null)
        {
            this.anchorObject = anchorObject;

            posterIndicatorText.text = "Currently viewing: " + anchorObject.name;
        }

        caliperEventHandler.ImageIdentified(anchorObject.name, anchorObject.GetInstanceID().ToString(), "The image detected is a qr code on the " + anchorObject.name + " poster.");
    }

    public GameObject GetAnchorObject()
    {
        return anchorObject;
    }

    public async void LoadExistingFeedbackAsync() // todo: clean up mess
    {
        List<FeedbackData> existingFeedback = await firebaseHandler.GetFeedbackData(anchorObject.name);

        // foreach (var x in existingFeedback)
        //for (int i = 0; i < existingFeedback.Count; i++)

        int loadLimit = 5; // hard coded... maybe put up at the top
        for (int i = 0; i < loadLimit; i++)
        {
            //PlaceFeedback(x.text, x.author, x.position + anchorObject.transform.position, x.rotation * anchorObject.transform.rotation, false);

            // dont place loaded notes in saved position, instead place them organized next to poster

            // create note object

            GameObject newNote = Instantiate(notePrefab, anchorObject.transform.position, anchorObject.transform.rotation) as GameObject;
            newNote.transform.parent = anchorObject.transform;
            newNote.name = "note";

            // neat feedback positioning (some hard coded values...)

            newNote.transform.localPosition += new Vector3(-.15f, 0, -.03f + .12f * i);

            // set text of note

            TMP_Text[] newNoteText = newNote.GetComponentsInChildren<TMP_Text>();
            newNoteText[0].text = existingFeedback[existingFeedback.Count - 1 - i].text;
            newNoteText[1].text = "- " + existingFeedback[existingFeedback.Count - 1 - i].author;

            // set note wobble behavior

            newNote.GetComponent<NoteSpawnBehavior>().pauseBeforeWobble = i / 3f;
        }

        caliperEventHandler.FeedbackLoaded(existingFeedback.Count, anchorObject.name, anchorObject.GetInstanceID().ToString(), "Feedback data for the " + anchorObject.name + " poster was loaded into the app.");
    }
}
