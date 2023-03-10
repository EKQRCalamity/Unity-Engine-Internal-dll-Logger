using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Base
{
    enum LogType
    {
        Default = 0,
        Warning = 1,
        Error = 2,
        Success = 3,
        Blue = 4,
        Magenta = 5
    }

    internal class Logger : MonoBehaviour
    {
        internal static bool isAlloc = false;

        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        
        private const int STD_OUTPUT_HANDLE = -11;
        
        void OnEnable() { Application.logMessageReceived += LogF; }
        void OnDisable() { Application.logMessageReceived -= LogF; }

        private void LogF(string condition, string stackTrace, UnityEngine.LogType type)
        {
            if (isAlloc)
            {
                switch (type) {
                    case UnityEngine.LogType.Error:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        condition = $"[Unity Error] - {condition}";
                        break;
                    case UnityEngine.LogType.Assert:
                        Console.ForegroundColor = ConsoleColor.Red;
                        condition = $"[Unity Assert] - {condition}";
                        break;
                    case UnityEngine.LogType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        condition = $"[Unity Warning] - {condition}";
                        break;
                    case UnityEngine.LogType.Log:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        condition = $"[Unity Log] - {condition}";
                        break;
                    case UnityEngine.LogType.Exception:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        condition = $"[Unity Exception] - {condition}";
                        break;
                }
                Console.WriteLine(condition);
            }
        }

        public static void Log(System.Object print, LogType logtype = LogType.Default)
        {
            if (isAlloc)
            {
                switch (logtype)
                {
                    case LogType.Default:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case LogType.Warning:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogType.Error:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case LogType.Success:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case LogType.Blue:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case LogType.Magenta:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                }
                Console.WriteLine(print.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static void SetupConsole()
        {
            AllocConsole();
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            StreamWriter standardOutput = new StreamWriter(fileStream);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            isAlloc = true;
        }
    }
}
