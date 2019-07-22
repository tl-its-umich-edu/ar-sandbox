# Unity AR Foundation Template

Unity version: 2019.1.8f1

This Unity project is the same as an empty 3D Unity project aside from the following changes:

## Project settings

### Player settings:

- Company Name has been changed to "Teaching and Learning Group"

### iOS settings:

- Bundle Identifier has been changed to "edu.umich.milk.ARFoundationTemplate"
- Requires ARKit Support has been checked
- Target minimum iOS Version has been changed to 13.0
- Architecture has been changed to ARM64

### Quality settings

#### Medium quality

- Shadow Distance has been changed to 8
- Shadow Resolution has been changed to High Resolution

## Packages

- AR Foundation version 2.2.0 preview 3 has been added
- ARKit XR Plugin version 2.2.0 preview 3 has been added

## Build Settings

- The platform has been switched to iOS

## Scenes

- A minimal AR scene has been created.
- An AR Plane Manager and AR Raycast Manager component has been added to the AR Session object.
- AR Session Origin has been scaled to 10 (helps with physics, all object in the scene display as 1/10th the size of what is shown in the editor)