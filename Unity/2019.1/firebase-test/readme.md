# Contents

1. [Overview](#overview)
2. [How it Works](#how-it-works)
3. [Running This Project on an iOS Device](#running-this-project-on-an-ios-device)
4. [Sources for the 3D Models](#sources-for-the-3d-models)

# Overview

This is the augmented reality (AR) app used during the ITS Internship Showcase
to demonstrate the capabilities of AR.

# How it Works

## Object Placement

In the app, the user can place sticky note objects on any flat surface. This is
done by detecting those flat surfaces with AR Foundation, finding the spot the
user is looking at, saving the position and rotation data, and then using that
object to position the sticky note when the user wants to place one down. The
surface detection is done in the SurfaceDetection.cs script and the note object
creation and repositioning is done with the ARNoteCreation.cs script.

## Image Recognition

The user can scan certain QR codes to make content appear. This is done by using
AR Foundation's image recogintion to detect what qr code the user is scanning
and where to place the content. The ReferenceImageLibrary asset in the
Assets/Tracked Images folder contains what images to look for and is attached to
the AR Session Origin object so that detection happens when the scene is
running. There is also a script called ImageTrackingHandler.cs attached that
determines the name of the image detected and loads content accordingly.

## Sending Caliper Analytics

When the user performs certain actions, the app will send caliper events to the
Unizen Data Platform (UDP). Templates for the events are stored as scripts in
the resources folder under CaliperEvents. When the user performs an action that
triggers and event, the CaliperEventHandler.cs script loads a template, fills in
data from the app, converts it to a JSON string, and sends it as a POST event to
a URL with a bearer token for authorization.

## Sending Data with Firebase

When the user places a note, data about that note is stored in a Firebase
database. Most of the work is done with the Firebase Unity SDK. Whenever the app
wants to send data to the Firebase database, it calls the FirebaseHandler.cs
script to traverse the Firebase JSON data tree and store the data.

## Retrieving Data with Firebase

The QR codes for this app has a name associated with it. Scanning it will tell
the FirebaseHandler.cs script to search the data tree for data linked with this
name. The data pulled is then sent as a list to the ARNoteCreation.cs script,
where it is then converted into 3d objects that the viewer can see.

The QR codes for this app has a name associated with it. Scanning one will first
prompt the ImageTrackingHandler.cs script to get the name of the image, then
will pass it on to the ARNoteCreation.cs script. That script will then ask for
the data from firebase using the FirebaseHandler.cs script and then will convert
the data into note objects and position them to be next to the scanned image.

## Multi User AR

Multi User AR has not been implemented in this project. However, a look at it
working can be seen at the [Unity AR Foundation sample project](https://github.com/Unity-Technologies/arfoundation-samples).
It is part of the AR Collaborative Data Example scene.

# Running This Project on an iOS Device

## Requirements

- An iOS device that has iOS 13 and has support for ARKit 3
- A Mac with:
    - Xcode 11 installed (Beta can be found [here](https://developer.apple.com/download/) under Applications)
    - Unity 2019.1.13f1 installed with the iOS export plugin(Can be downloaded from [here](https://unity3d.com/get-unity/download/archive))
    - CocoaPods installed.
- Your Apple Developer Signing Team ID (Can be found [here](https://developer.apple.com/account/#/membership))
- A GoogleService-Info.plst file for a Firebase database.
- A bearer token for Caliper analytics saved as "caliper.bearertoken.txt".

## Building the project

1. Open this project in Unity.
2. Open the main scene in the Unity Editor. It can be found at Assets/Scenes/Main in the Project window.
3. Place the Caliper bearer token file in the Resources folder.
4. Place the GoogleService-Info.plst file in the Resources folder.
5. Add your Signing Team ID to the project.
    1. Open Project Settings... window found in the edit menu.
    2. Select the Player section on the left.
    3. Click the button that has a tiny iPhone icon to open up the iOS settings.
    4. Expand the section titled Other Settings.
    5. In the Identification subsection, paste your Signing Team ID into the Signing Team ID field.
    6. Close the Project Settings window.
6. Select the correct version of Xcode to export the project to.
    1. Open the Build Settings... window found in the file menu.
    2. Select iOS in the platform selection.
    3. Change the Run In Xcode option to Xcode 11.
    4. Close the Build Settings window.
7. Build the project with &#8984; + B or by clicking the Build and Run button in the file menu.
8. Select a folder to save the exported project into.

# Sources for the 3D Models

3d map model is from openstreetmap

teacher model is from https://poly.google.com/view/5v5j_lqOHTO

camera model is from https://poly.google.com/view/5CHQvf_emuN

doctor model is from https://poly.google.com/view/0N-0gZmlVOb

