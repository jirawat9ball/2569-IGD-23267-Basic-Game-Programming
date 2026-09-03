using System.Collections.Generic;
using UnityEngine;

namespace Workspace.Core
{
    public static class SimpleDebugConsole
    {
        private static List<string> outputLines = new List<string>();

        public static void Log(object message)
        {
            if (message != null)
            {
                outputLines.Add(message.ToString());
                UnityEngine.Debug.Log(message);
            }
        }

        public static void LogError(object message)
        {
            if (message != null)
            {
                outputLines.Add(message.ToString());
                UnityEngine.Debug.LogError(message);
            }
        }

        public static string GetOutput()
        {
            return string.Join("\n", outputLines);
        }

        public static void Clear()
        {
            outputLines.Clear();
        }
    }
}
