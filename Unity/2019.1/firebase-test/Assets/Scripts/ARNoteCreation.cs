using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARNoteCreation : MonoBehaviour
{
    public Button placeButton, resetButton;
    public TMP_InputField noteTextField, noteAuthorField;
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
        GameObject createdFeedback = PlaceNote(noteTextField.text, noteAuthorField.text, placementIndicator.transform.position, placementIndicator.transform.rotation);

        caliperEventHandler.FeedbackCreated(createdFeedback.GetInstanceID().ToString(), "Feedback object created by user", noteTextField.text, noteAuthorField.text);

        noteTextField.text = "";
    }

    private void UpdateButtons()
    {
        if (surfaceDetection.GetPlacementPoseIsValid() && noteTextField.text != "" && anchorObject != null)
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

    private GameObject PlaceNote(string noteText, string authorText, Vector3 position, Quaternion rotation, bool sendToFirebase = true)
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
        // kills camera feed in arfoundation 2.2 :(
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Destroy(anchorObject);

        posterIndicatorText.text = "No QR Code Detected";
    }

    public void SetAnchorObject(GameObject anchorObject)
    {
        if (this.anchorObject == null)
        {
            this.anchorObject = anchorObject;

            posterIndicatorText.text = "Currently viewing: " + anchorObject.name;

            Debug.Log(">>>>> Anchor Object: " + anchorObject.name);
        }

        caliperEventHandler.ImageIdentified(anchorObject.name, anchorObject.GetInstanceID().ToString());
    }

    public GameObject GetAnchorObject()
    {
        return anchorObject;
    }

    public async void LoadExistingFeedbackAsync()
    {
        List<FeedbackData> existingFeedback = await firebaseHandler.GetFeedbackData(anchorObject.name);

        foreach (var x in existingFeedback)
        {
            PlaceNote(x.text, x.author, x.position + anchorObject.transform.position, x.rotation * anchorObject.transform.rotation, false);
        }
    }
}
