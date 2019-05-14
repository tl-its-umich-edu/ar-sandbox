# AR Foundation Unity Workbench Tutorial

[Tutorial from Unity Workbench](https://www.youtube.com/watch?v=Ml2UakwRxjk)

# Description

App detects surfaces as device is moved around.

User aims device and a crosshair is placed where a ray cast from the center of the screen and a detected surface intersects.

When the screen is tapped, the app places a banana model on the surface where the crosshair is.

# Notes

<strong>This project is set up to build to iOS (Xcode version 10.2.1).</strong> It has not been set up for or tested on Android.

Unity is designed to work with a scale that is very large compared to real life. To compensate for this, scaling the AR Session Origin up will scale the Unity scene down.

1 unit in Unity == 1 meter in real life.

## iOS building setup

Go to player settings. (Edit -> Project Settings -> Player)

Fill in Company Name. (optional)

Select the iOS settings tab. <img style="width:20px;height:20px;" src="mobiledeviceicon.png" alt="mobile tab icon">

Fill in Bundle Identifier. (example: edu.umich.milk.ProjectName)

Uncheck Automatic Signing. (optional but recommended if using multiple developer identities)

Check Requires ARKit Support. (Automatically fills in Camera Usage Description text field)

Change Target minimum iOS Version to at least 11.

Change Architecture from Universal to ARM64.

Go to quality settings. (Edit -> Project Settings -> Quality)

Select Medium quality. (Medium quality is the default settings for iOS and Android deployments.)

Change Shadow Distance to a smaller number like 8.

Change Shadow Resolution to High Resolution.

# Resource Credits

[Banana model by Jerad Bitner](https://poly.google.com/view/09X62Qc9i9o)

[Crosshair texture by Elionas](https://pixabay.com/illustrations/crosshair-target-visor-sightings-1345868/)
