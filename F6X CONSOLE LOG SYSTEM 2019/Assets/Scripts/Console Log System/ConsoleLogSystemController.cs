using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

// [ConsoleLogSystemController]
// Manages the Console Log System.
public class ConsoleLogSystemController : MonoBehaviour
{
    [Header("Console Log System Settings")]
    // <bool> - [consoleLogSystem]
    // Enables Console Log System for itself.
    public bool consoleLogSystem;
    // <bool> - [consoleLogSystemMaster]
    // Enables Console Log System for the entire project.
    public bool consoleLogSystemMaster;
    // <Color> - [logColor]
    // The color used for the logs.
    public Color logColor;
    // <int> - [currentFrame]
    // The current frame being executed.
    private int currentFrame;
    // <string> - [logsFilePath]
    // The path of the logs file.
    private string logsFilePath;
    // <string> - [infoLevelException]
    // Warning message for info level errors.
    private readonly string infoLevelException = "Info Level Must Be Between 0 And 2";

    // <void> - [Awake]
    // Runs at the initialization of the GameObject.
    private void Awake()
    {
        InitializeLogsFilePath();
        ConsoleLog("Console Log System Ready!", true);
    }

    // (function) - <void> - [InitializelogsFilePath]
    // Initialize the path of the logs file.
    private void InitializeLogsFilePath()
    {
        logsFilePath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "ConsoleLogSystem");
        if (!Directory.Exists(logsFilePath))
            Directory.CreateDirectory(logsFilePath);
        logsFilePath = Path.Combine(logsFilePath, "Log-" + DateTime.Now.ToString("yy-MM-dd-HH-mm") + ".log");
        using (StreamWriter writer = new StreamWriter(logsFilePath))
        {
            writer.WriteLine("--- Console Log System ---");
        }
    }

    // (function) - <void> - [ConsoleLog]
    // Sends the logs to the Console Log System.
    // (param) - <string> - [message] - 'Test'
    // The message to use for the log.
    // (param) - <bool> - [showFrame] - 'false'
    // Checks if the frame must be send in the log.
    // (param) - <int> - [infoLevel] - '0'
    // The level of info for the log.
    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLogSystem)
            ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }

    // (function) - <void> - [ConsoleLogSystem]
    // Registers the logs and send them by the console.
    // (param) - <string> - [message]
    // The message to use for the log.
    // (param) - <Color> - [logColor]
    // The color to use for the log.
    // (param) - <bool> - [showFrame]
    // Checks if the frame must be send in the log.
    // (param) - <int> - [infoLevel]
    // The level of info for the log.
    // (param) - <int> - [traceFrame] - '1'
    // The stack trace depth.
    public void ConsoleLogSystem(string message, Color logColor, bool showFrame, int infoLevel, int traceFrame = 1)
    {
        if (consoleLogSystemMaster)
        {
            currentFrame = Time.frameCount;
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(traceFrame);
            string callingScript = stackFrame.GetMethod().DeclaringType.Name;
            logColor.a = 1;
            string stringLogColor = "#" + ColorUtility.ToHtmlStringRGBA(logColor);
            string logMessage = $"<b>[<color={stringLogColor}>{callingScript}</color>]: ";
            using (StreamWriter writer = new StreamWriter(logsFilePath, true))
            {
                if (-1 < infoLevel && infoLevel < 3)
                {
                    logMessage += showFrame ? $"FRM ({currentFrame}) " : "";
                    logMessage += $"{message}</b>\n";
                    writer.WriteLine("[" + callingScript + "] FRM (" + currentFrame + ") " + message);
                }
                else
                {
                    logMessage += $"FRM: {currentFrame} ";
                    logMessage += $"{infoLevelException}</b>\n";
                    writer.WriteLine("[" + callingScript + "] FRM (" + currentFrame + ") " + infoLevelException);
                }
            }
            switch (infoLevel)
            {
                case 0:
                    UnityEngine.Debug.Log(logMessage);
                    break;
                case 1:
                    UnityEngine.Debug.LogWarning(logMessage);
                    break;
                case 2:
                    UnityEngine.Debug.LogError(logMessage);
                    break;
                default:
                    UnityEngine.Debug.LogError(logMessage);
                    break;
            }
        }
    }
}