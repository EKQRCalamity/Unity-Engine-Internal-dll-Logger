# Unity-Engine-Internal-dll-Logger
This is a simple CSharp file for adding a Injection / Logging window to your internal dll. 

## Building with your Project
I have written this for development of internal unity cheats. So I just use the UnityEngine dll shipped with most Mono Games.

## How does this work?
First call SetupConsole() then add the Logger object to the GameObject as component
Example Init file:
```cs
gameObject = new GameObject();
Logger.SetupConsole();
Logger.Log("Console Setup!", LogType.Magenta);
Logger.Log("Adding Logger Component...", LogType.Warning);
gameObject.AddComponent<Logger>();
Logger.Log("Set obj to not destroy on load...", LogType.Warning);
GameObject.DontDestroyOnLoad(gameObject);
Logger.Log("Everything setup!", LogType.Success);
```
