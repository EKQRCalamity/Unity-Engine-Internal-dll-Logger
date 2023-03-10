# Unity-Engine-Internal-dll-Logger
This is a simple CSharp file for adding a Injection / Logging window to your internal dll. 
## How does this work?
First call SetupConsole() then add the Logger object to the GameObject as component
```cs
GameObject gObj = new GameObject();
Base.Logger.SetupConsole();
gObj.AddComponent<Base.Logger>();
```
