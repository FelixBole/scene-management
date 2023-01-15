# Scene Management

A robust scene management framework allowing to create loadings and unloadings of scenes easily by placing entrance and exit points on a scene and specifying in scriptable objects what scene it should lead to. This Scene Management system was made with inspiration from some of Unity's Open Collaborative Projects.

## Installation

1. In Unity, from the package manager, click the `+` icon
2. Select `Add package from git URL...`.
3. In the text box that appears, enter this projects git url `https://github.com/FelixBole/scene-management.git` 
4. Import the sample asset pack containing elements to help get you started

## Getting started

To get started, I recommend importing the asset pack in the samples containing all the necessary stuff to get going. Example scenes, scripts and prefabs to help understand how everything is linked.

For instance, the sample pack contains the following already setup scenes :
- Initialization
- PersistentManagers
- MainMenu
- Gameplay
- Two example location scenes

### Overview

Main scripts overview

| Script | Description 
|---|---|
| `InitializationLoader` | Loads all persistent manages and raises event to load main menu
| `StartGame` | Responsible for launching the game with the specified location / level
| `SceneLoader` | Management of scene loading and unloading. Also provides a tool for cold starting to launch the initialization from an existing location.
| `LocationEntrance` | The entry point of a Path going from A in one scene to B in another scene. The path is a two-way ride though so it can also be the exit point.
| `LocationExit` | The exit point of a Path going from A in one scene to B in another scene. The path is a two-way ride though so it can also be the exit point.