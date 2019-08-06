using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Threading.Tasks;
using System;

public class FirebaseHandler : MonoBehaviour
{
    private string dbURL = "https://ar-tl-umich-project.firebaseio.com/";
    private DatabaseReference dbRef;

    private long feedbackCount;
    private FeedbackData[] feedbackDataArray;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dbURL);
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        // listen for changes to feedback branch (this is called when initialized and when changes happen)
        FirebaseDatabase.DefaultInstance.GetReference("Feedback").ValueChanged += HandleValueChanged;

        // dbRef.Child("test").SetRawJsonValueAsync("{\"Test\":\"asdf\",\"john\":{null,\"doe\",\"smith\"}}"); nested data doesnt work for some reason
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddFeedback(string posterName, FeedbackData fd)
    {
        var feedbackIndex = (feedbackCount + 1).ToString();

        // todo: minimize calls to firebase to reduce triggers to listener events

        dbRef.Child("Posters").Child(posterName).Child(feedbackIndex).SetValueAsync(true);

        dbRef.Child("Feedback").Child(feedbackIndex).Child("Text").SetValueAsync(fd.text);
        dbRef.Child("Feedback").Child(feedbackIndex).Child("Author").SetValueAsync(fd.author);

        dbRef.Child("Feedback").Child(feedbackIndex).Child("Position").Child("x").SetValueAsync(fd.position.x);
        dbRef.Child("Feedback").Child(feedbackIndex).Child("Position").Child("y").SetValueAsync(fd.position.y);
        dbRef.Child("Feedback").Child(feedbackIndex).Child("Position").Child("z").SetValueAsync(fd.position.z);

        dbRef.Child("Feedback").Child(feedbackIndex).Child("Rotation").Child("w").SetValueAsync(fd.rotation.w);
        dbRef.Child("Feedback").Child(feedbackIndex).Child("Rotation").Child("x").SetValueAsync(fd.rotation.x);
        dbRef.Child("Feedback").Child(feedbackIndex).Child("Rotation").Child("y").SetValueAsync(fd.rotation.y);
        dbRef.Child("Feedback").Child(feedbackIndex).Child("Rotation").Child("z").SetValueAsync(fd.rotation.z);
    }

    private void UpdateNoteCount()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Feedback").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...

                Debug.Log(">>>>> Could not get firebase data.");
            }
            else if (task.IsCompleted)
            {
                // Do something with snapshot...

                feedbackCount = task.Result.ChildrenCount;
            }
        });
    }

    public async Task<List<FeedbackData>> GetFeedbackData(string posterName)
    {
        List<FeedbackData> retrievedFeedback = new List<FeedbackData>();

        List<int> feedbackIndexes = new List<int>();

        // request data from firebase

        await FirebaseDatabase.DefaultInstance.GetReference("/").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...

                Debug.Log(">>>>> Could not get firebase data.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...

                // find note ids that are associated with the poster

                foreach (var x in snapshot.Child("Posters/" + posterName).Children)
                {
                    // don't load note if index set to false
                    if ((bool)x.Value)
                    {
                        feedbackIndexes.Add(int.Parse(x.Key));
                    }
                }

                // build a list of feedback data objects

                for (int i = 1; i <= feedbackCount; i++)
                {
                    if (feedbackIndexes.Contains(i))
                    {
                        FeedbackData feedbackData = new FeedbackData("", "", Vector3.zero, new Quaternion(0, 0, 0, 0));

                        feedbackData.text = snapshot.Child("Feedback/" + i + "/Text").Value.ToString();
                        feedbackData.author = snapshot.Child("Feedback/" + i + "/Author").Value.ToString();

                        feedbackData.position.x = float.Parse(snapshot.Child("Feedback/" + i + "/Position/x").Value.ToString());
                        feedbackData.position.y = float.Parse(snapshot.Child("Feedback/" + i + "/Position/y").Value.ToString());
                        feedbackData.position.z = float.Parse(snapshot.Child("Feedback/" + i + "/Position/z").Value.ToString());

                        feedbackData.rotation.w = float.Parse(snapshot.Child("Feedback/" + i + "/Rotation/w").Value.ToString());
                        feedbackData.rotation.x = float.Parse(snapshot.Child("Feedback/" + i + "/Rotation/x").Value.ToString());
                        feedbackData.rotation.y = float.Parse(snapshot.Child("Feedback/" + i + "/Rotation/y").Value.ToString());
                        feedbackData.rotation.z = float.Parse(snapshot.Child("Feedback/" + i + "/Rotation/z").Value.ToString());

                        retrievedFeedback.Add(feedbackData);
                    }
                }
            }
        });

        return retrievedFeedback;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot

        // update feedback count

        feedbackCount = args.Snapshot.ChildrenCount;

        Debug.Log(feedbackCount);
    }


}

public class FeedbackData
{
    public string text;
    public string author;
    public Vector3 position;
    public Quaternion rotation;

    public FeedbackData(string text, string author, Vector3 position, Quaternion rotation)
    {
        this.text = text;
        this.author = author;
        this.position = position;
        this.rotation = rotation;
    }
}
