using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Analytics;

public class PersistentData : MonoBehaviour
{
    private CaliperEventCreator caliperEventCreatorScript;

    // Start is called before the first frame update
    void Start()
    {
        caliperEventCreatorScript = GetComponent<CaliperEventCreator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save(string saveFileName, List<NoteData> notesToSave)
    {
        // convert data to be serializable
        List<SerializableNoteData> serNotesToSave = new List<SerializableNoteData>();

        foreach (NoteData noteData in notesToSave)
        {
            serNotesToSave.Add(new SerializableNoteData(noteData.text, noteData.position, noteData.rotation, noteData.scale));
        }

        // save data
        string destination = Application.persistentDataPath + "/" + saveFileName;
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, serNotesToSave);
        file.Close();

        caliperEventCreatorScript.CreateCaliperEventAsync(SystemInfo.deviceName, "Used", "Save Session Data Tool", AnalyticsSessionInfo.sessionId.ToString(), "ToolUseEvent");
    }

    public List<NoteData> Load(string saveFileName)
    {
        // get data
        string destination = Application.persistentDataPath + "/" + saveFileName;
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<SerializableNoteData> serData = (List<SerializableNoteData>)bf.Deserialize(file);
        file.Close();

        // convert data back to unserializable types -- maybe this is unnecessary?

        List<NoteData> notes = new List<NoteData>();

        foreach (SerializableNoteData serNoteData in serData)
        {
            notes.Add(new NoteData(serNoteData.text, serNoteData.position, serNoteData.rotation, serNoteData.scale));
        }


        caliperEventCreatorScript.CreateCaliperEventAsync(SystemInfo.deviceName, "Used", "Load Session Data Tool", AnalyticsSessionInfo.sessionId.ToString(), "ToolUseEvent");

        return notes;
    }
}

[System.Serializable]
public class NoteData
{
    public string text;
    public Vector3 position;
    public Quaternion rotation;
    public float scale;

    public NoteData(string newText, Vector3 newPosition, Quaternion newRotation, float newScale)
    {
        text = newText;
        position = newPosition;
        rotation = newRotation;
        scale = newScale;
    }
}

[System.Serializable]
public class SerializableNoteData
{
    public string text;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public float scale;

    public SerializableNoteData(string newText, SerializableVector3 newPosition, SerializableQuaternion newRotation, float newScale)
    {
        text = newText;
        position = newPosition;
        rotation = newRotation;
        scale = newScale;
    }
}



// https://answers.unity.com/questions/956047/serialize-quaternion-or-vector3.html
// tldr vector3 and quaternions arent serializable by default so we remake the classes here and use these to save the data
// probably couldve just stored the individual numbers instead of this but whatever it works

/// <summary>
/// Since unity doesn't flag the Vector3 as serializable, we
/// need to create our own version. This one will automatically convert
/// between Vector3 and SerializableVector3
/// </summary>
[System.Serializable]
public struct SerializableVector3
{
    /// <summary>
    /// x component
    /// </summary>
    public float x;

    /// <summary>
    /// y component
    /// </summary>
    public float y;

    /// <summary>
    /// z component
    /// </summary>
    public float z;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="rX"></param>
    /// <param name="rY"></param>
    /// <param name="rZ"></param>
    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// <summary>
    /// Automatic conversion from SerializableVector3 to Vector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    /// <summary>
    /// Automatic conversion from Vector3 to SerializableVector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}

/// <summary>
/// Since unity doesn't flag the Quaternion as serializable, we
/// need to create our own version. This one will automatically convert
/// between Quaternion and SerializableQuaternion
/// </summary>
[System.Serializable]
public struct SerializableQuaternion
{
    /// <summary>
    /// x component
    /// </summary>
    public float x;

    /// <summary>
    /// y component
    /// </summary>
    public float y;

    /// <summary>
    /// z component
    /// </summary>
    public float z;

    /// <summary>
    /// w component
    /// </summary>
    public float w;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="rX"></param>
    /// <param name="rY"></param>
    /// <param name="rZ"></param>
    /// <param name="rW"></param>
    public SerializableQuaternion(float rX, float rY, float rZ, float rW)
    {
        x = rX;
        y = rY;
        z = rZ;
        w = rW;
    }

    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }

    /// <summary>
    /// Automatic conversion from SerializableQuaternion to Quaternion
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Quaternion(SerializableQuaternion rValue)
    {
        return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }

    /// <summary>
    /// Automatic conversion from Quaternion to SerializableQuaternion
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableQuaternion(Quaternion rValue)
    {
        return new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }
}
