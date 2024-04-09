using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text pathText;
    private string logFilePath;

    // Set Console Log Settings Public && Declare Console Log System Controller
    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        // Reference Console Log System Controller By Tag
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();

        logFilePath = Path.GetDirectoryName(Application.dataPath) + "/ConsoleLogSystem";
        string timeStamp = DateTime.Now.ToString("yy-MM-dd-HH-mm");
        logFilePath += "/Log-" + timeStamp + ".log";
        if (logFilePath != null)
            pathText.text = logFilePath;
        else
            pathText.text = "Path Null";
    }

    private void Start()
    {
        ConsoleLog("Starting Game...");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ConsoleLog("Closing Game...");
            Application.Quit();
        }
    }

    // Create Console Log ()
    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}