# Setup guide

## Requirements

- Unity Hub and Unity Editor **2023.1.13f1** (`ProjectSettings/ProjectVersion.txt`).
- Git, or a downloaded ZIP of this repository.
- Internet access on first open so Unity Package Manager can install the dependencies in `Packages/manifest.json`.

## Open the project

1. Clone or download this repository.
2. In Unity Hub, choose **Add project from disk** and select the repository root (the folder containing `Assets`, `Packages`, and `ProjectSettings`).
3. Select Editor 2023.1.13f1 and open the project.
4. Wait for Unity to import the assets and resolve packages before entering Play mode.

Unity generates local `Library`, `Temp`, and other cache folders automatically. Do not copy those from another machine to set up the project.

## Run and build

1. Open `Assets/Scenes/SampleScene.unity`.
2. Press **Play** in the Editor to try the scene.
3. For a standalone build, open **File → Build Settings**. `SampleScene` is the enabled scene in `ProjectSettings/EditorBuildSettings.asset`; select a target platform and build.

The README describes the gameplay features. Control bindings and platform-specific build requirements should be checked in the Unity Editor for the target machine.
