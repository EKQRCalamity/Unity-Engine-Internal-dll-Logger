using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AliceCradles2
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
        internal static bool isShown = true;

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

        [DllImport("kernel32.dll",
            EntryPoint = "FreeConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int FreeConsole();

        [DllImport("kernel32.dll",
            EntryPoint = "GetConsoleWindow",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll",
            EntryPoint = "ShowWindow",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        private const int STD_OUTPUT_HANDLE = -11;

        private const int SW_HIDE = 0;
        private const int SW_NORMAL = 1;
        private const int SW_SHOWMINIMZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_SHOWNOACTIVE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;

        void OnEnable() { Application.logMessageReceived += LogF; }
        void OnDisable() { Application.logMessageReceived -= LogF; }

        private void LogF(string condition, string stackTrace, UnityEngine.LogType type)
        {
            if (isAlloc)
            {
                switch (type)
                {
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
                Console.WriteLine($"[{Assembly.GetCallingAssembly().GetName().Name}] - [Logger - {DateTime.Now.ToString("HH:mm:ss")}]: {print.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void LogNoNewLine(System.Object print, LogType logtype = LogType.Default)
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
                Console.Write($"[{Assembly.GetCallingAssembly().GetName().Name}] - [Logger - {DateTime.Now.ToString("HH:mm:ss")}]: {print.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void LogReplaceLine(System.Object print, LogType logtype = LogType.Default)
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
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
                Console.Write($"[{Assembly.GetCallingAssembly().GetName().Name}] - [Logger - {DateTime.Now.ToString("HH:mm:ss")}]: {print.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static void ToggleConsole()
        {
            IntPtr handle = GetConsoleWindow();
            if (isShown)
                ShowWindow(handle, SW_HIDE);
            else
                ShowWindow(handle, SW_SHOWNOACTIVE);
            isShown = !isShown;

        }

        internal static void SetupConsole()
        {
            if (GetConsoleWindow() != IntPtr.Zero)
                FreeConsole();
            if (GetConsoleWindow() == IntPtr.Zero)
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

        internal static void UnloadConsole()
        {
            if (GetConsoleWindow() != IntPtr.Zero)
            {
                FreeConsole();
                isAlloc = false;
                Console.SetOut(Console.Out);
            }
        }
    }
}
