using System.Diagnostics;
using UnityEngine;

public class ConsoleLogSystemController : MonoBehaviour
{
    [Header("Console Log System Settings")]
    public Color classColor;
    public bool consoleLog;
    public bool consoleLogSystem;

    private void Awake()
    {
        ConsoleLog("Console Log System Ready!", true);
    }

    private void Start()
    {
        ConsoleLog("Info");
        ConsoleLog("Info With FRM", true);
        ConsoleLog("Warning", false, 1);
        ConsoleLog("Warning With FRM", true, 1);
        ConsoleLog("Error", false, 2);
        ConsoleLog("Error With FRM", true, 2);
    }

    private void ConsoleLog(string message, bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            ConsoleLogSystem($"{message}", classColor, showFrame, infoLevel);
    }

    public void ConsoleLogSystem(string message, Color classColor, bool showFrame, int infoLevel, int traceFrame = 1)
    {
        if (consoleLogSystem)
        {
            StackTrace stackTrace = new();
            StackFrame stackFrame = stackTrace.GetFrame(traceFrame);
            string callingScript = stackFrame.GetMethod().DeclaringType.Name;
            string stringClassColor = ("#" + ColorUtility.ToHtmlStringRGBA(classColor));
            switch (infoLevel)
            {
                case 0:
                    if (showFrame)
                        UnityEngine.Debug.Log($"<b>[<color={stringClassColor}>{callingScript}</color>]: (FRM: {Time.frameCount}) {message}</b>");
                    else
                        UnityEngine.Debug.Log($"<b>[<color={stringClassColor}>{callingScript}</color>]: {message}</b>");
                    break;
                case 1:
                    if (showFrame)
                        UnityEngine.Debug.LogWarning($"<b>[<color={stringClassColor}>{callingScript}</color>]: (FRM: {Time.frameCount}) {message}</b>");
                    else
                        UnityEngine.Debug.LogWarning($"<b>[<color={stringClassColor}>{callingScript}</color>]: {message}</b>");
                    break;
                case 2:
                    if (showFrame)
                        UnityEngine.Debug.LogError($"<b>[<color={stringClassColor}>{callingScript}</color>]: (FRM: {Time.frameCount}) {message}</b>");
                    else
                        UnityEngine.Debug.LogError($"<b>[<color={stringClassColor}>{callingScript}</color>]: {message}</b>");
                    break;
            }
        }
    }
}