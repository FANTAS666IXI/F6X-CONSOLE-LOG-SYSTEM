using System.Diagnostics;
using UnityEngine;

public class ConsoleLogSystemController : MonoBehaviour
{
    [Header("Console Log System Settings")]
    public bool consoleLog;
    public bool consoleLogSystem;
    public Color logColor;

    private void Awake()
    {
        ConsoleLog("Console Log System Ready!", true);
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }

    public void ConsoleLogSystem(string message, Color logColor, bool showFrame, int infoLevel, int traceFrame = 1)
    {
        if (consoleLogSystem)
        {
            StackTrace stackTrace = new();
            StackFrame stackFrame = stackTrace.GetFrame(traceFrame);
            string callingScript = stackFrame.GetMethod().DeclaringType.Name;
            logColor.a = 1;
            string stringLogColor = "#" + ColorUtility.ToHtmlStringRGBA(logColor);
            switch (infoLevel)
            {
                case 0:
                    if (showFrame)
                        UnityEngine.Debug.Log($"<b>[<color={stringLogColor}>{callingScript}</color>]: (FRM: {Time.frameCount}) {message}</b>\n");
                    else
                        UnityEngine.Debug.Log($"<b>[<color={stringLogColor}>{callingScript}</color>]: {message}</b>\n");
                    break;
                case 1:
                    if (showFrame)
                        UnityEngine.Debug.LogWarning($"<b>[<color={stringLogColor}>{callingScript}</color>]: (FRM: {Time.frameCount}) {message}</b>\n");
                    else
                        UnityEngine.Debug.LogWarning($"<b>[<color={stringLogColor}>{callingScript}</color>]: {message}</b>\n");
                    break;
                case 2:
                    if (showFrame)
                        UnityEngine.Debug.LogError($"<b>[<color={stringLogColor}>{callingScript}</color>]: (FRM: {Time.frameCount}) {message}</b>\n");
                    else
                        UnityEngine.Debug.LogError($"<b>[<color={stringLogColor}>{callingScript}</color>]: {message}</b>\n");
                    break;
                default:
                    UnityEngine.Debug.LogError($"<b>[<color={stringLogColor}>{callingScript}</color>]: (FRM: {Time.frameCount}) Info Level Must Be Between 0 And 2</b>\n");
                    break;
            }
        }
    }
}