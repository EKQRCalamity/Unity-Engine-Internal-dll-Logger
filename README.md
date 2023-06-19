# Unity-Engine-Internal-dll-Logger
This is a simple CSharp file for adding a Injection / Logging window to your internal dll. 

## Building with your Project
I have written this for development of internal unity cheats. So I just use the UnityEngine dll shipped with most Mono Games.

## How does this work?
First call SetupConsole() then add the Logger object to the GameObject as component
```cs
GameObject gObj = new GameObject();
Base.Logger.SetupConsole();
gObj.AddComponent<Base.Logger>();
```
