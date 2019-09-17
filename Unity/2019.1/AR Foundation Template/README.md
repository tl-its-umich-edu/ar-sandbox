# Contents

1. [Overview](#overview)
2. [Getting Started](#getting-started)
3. [Running This Project on an iOS Device](#running-this-project-on-an-ios-device)
4. [Using This Template to Create Other Projects](#using-this-template-to-create-other-projects)
4. [Components of this Project](#components-of-this-project)
5. [Differences Between This Project and a Blank Project](#differences-between-this-project-and-a-blank-project)

# Overview

This is a template for developing iOS apps in Unity with AR Foundation.

# Getting Started

## Placing Objects in Augmented Reality

To place an object in augmented reality, add an object to the scene and move it
in front of the AR Camera's view. Then follow the build instructions and you
should be able to view your object in AR with your device.

#### Other uses of AR Foundation can be seen from the [AR Foundation Samples repository](https://github.com/Unity-Technologies/arfoundation-samples)

# Running This Project on an iOS Device

## Requirements

- An iOS device that has iOS 13 and has support for ARKit 3
- A Mac with:
    - Xcode 11 installed (Beta can be found [here](https://developer.apple.com/download/) under Applications)
    - Unity 2019.1.8f1 installed with the iOS export plugin(Can be downloaded from [here](https://unity3d.com/get-unity/download/archive))
- Your Apple Developer Signing Team ID (Can be found [here](https://developer.apple.com/account/#/membership))

## Building the project

1. Open this project in Unity.
2. Open the main scene in the Unity Editor. It can be found at Assets/Scenes/Main in the Project window.
3. Add your Signing Team ID to the project.
    1. Open Project Settings... window found in the edit menu.
    2. Select the Player section on the left.
    3. Click the button that has a tiny iPhone icon to open up the iOS settings.
    4. Expand the section titled Other Settings.
    5. In the Identification subsection, paste your Signing Team ID into the Signing Team ID field.
    6. Close the Project Settings window.
4. Select the correct version of Xcode to export the project to.
    1. Open the Build Settings... window found in the file menu.
    2. Select iOS in the platform selection.
    3. Change the Run In Xcode option to Xcode 11.
    4. Close the Build Settings window.
5. Build the project with &#8984; + B or by clicking the Build and Run button in the file menu.
6. Select a folder to save the exported project into.

(The built project should just be displaying the camera feed with no AR elements
because no objects have been placed in the scene.) 

# Using This Template to Create New Projects

To start developing new projects with this template, duplicate this folder and
rename it.

Also, make sure to change the Company Name and the iOS Bundle Identifier in
Project Settings.

# Components of This Project

There are a few components of the sample scene that need to be taken into
consideration when developing an AR app:

## AR Session

The AR Session object holds important scripts necessary for the AR Foundation
package to work.

## Scripts Attached to the AR Session

### ARSession.cs

This just needs to exist in the scene. It oversees the operation of the AR
Foundation package.

### SetTargetFramerate.cs

Sets the framerate of the camera feed to be something more comfortable than the
default.

### ARPlaneManager.cs

This just needs to exist to tell AR Foundation to detect surfaces. An object set
to the Plane Prefab field will be created wherever a surface is detected (Useful
for debugging). It has different detection modes that can be set depending on
your needs.

### ARRaycastManager.cs

Assists with raycasting to planes discovered by the ARPlaneManager.cs script.

## AR Session Origin

The AR Session Origin object is used to communicate between the session space
(the real-life space that the user can move around in) and the Unity space (the
space that objects created by the app exist in). The position and rotation of
this object is determined by the device's position and rotation when the object
is initialized. The object will stay in place even when the user moves the
device around, creating a sort of anchor between the two spaces. This is useful
for positioning objects in the app so that they look like they exist in the
session space. A simple example of this working is that when a cube is created
at the AR Session Origin's origin, it will stay in place while the user can move
around and look at the cube from different angles.

Note: The AR Session Origin is set at a scale of 10 along each axis. This means
that objects in the Unity editor are 10x the size of how they appear in the app.
(A 1 cubic meter cube will be downscaled to a 10 cubic centimeter cube in the
app.) This is done so that development doesn't have to happen on tiny scales in
the editor. It is also supposed to help the physics engine since it has trouble
working at small scales. However, the AR Session Origin can be set to a scale of
1 to simplify development.

## AR Camera

This is the object that simulates the user's device's view. It moves around the
Unity space based on how the user moves their device in the session space. It
has an ARCameraBackground.cs script attached that renders the device's camera
feed as the background of the scene.

# Differences Between This Project and a Blank Project

## Project Settings

### Player Settings:

- Company Name has been changed to "Teaching and Learning Group"

### iOS Settings:

- Bundle Identifier has been changed to "edu.umich.milk.ARFoundationTemplate"
- Requires ARKit Support has been checked
- Target minimum iOS Version has been changed to 13.0
- Architecture has been changed to ARM64

### Quality Settings

#### Medium Quality

- Shadow Distance has been changed to 8
- Shadow Resolution has been changed to High Resolution

## Packages

- AR Foundation version 2.2.0 preview 2 has been added
- ARKit XR Plugin version 2.2.0 preview 1 has been added

## Build Settings

- The platform has been switched to iOS

## Scenes

- A minimal AR scene has been created.
- An AR Plane Manager and AR Raycast Manager component has been added to the AR Session object.
- AR Session Origin has been scaled to 10 (helps with physics(?), all object in the scene display as 1/10th the size of what is shown in the editor)