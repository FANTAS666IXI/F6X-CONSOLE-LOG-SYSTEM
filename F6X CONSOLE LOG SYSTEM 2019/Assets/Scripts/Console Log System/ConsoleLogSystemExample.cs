using UnityEngine;

// [ConsoleLogSystemExample]
// Example of how to use the Console Log System.
public class ConsoleLogSystemExample : MonoBehaviour
{
    [Header("Console Log Settings")]
    // <bool> - [consoleLogSystem]
    // Enables Console Log System for itself.
    public bool consoleLogSystem;
    // <Color> - [logColor]
    // The color used for the logs.
    public Color logColor;
    // <ConsoleLogSystemController> - [consoleLogSystemController]
    // The Console Log System Controller component.
    private ConsoleLogSystemController consoleLogSystemController;

    // <void> - [Awake]
    // Runs at the initialization of the GameObject.
    private void Awake()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    // <void> - [Update]
    // Runs once per frame.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ConsoleLog();
            ConsoleLog("Message 1");
            ConsoleLog("Message 2", false);
            ConsoleLog("Message 3", true);
            ConsoleLog("Message 4", false, 0);
            ConsoleLog("Message 5", true, 0);
            ConsoleLog("Message 6", false, 1);
            ConsoleLog("Message 7", true, 1);
            ConsoleLog("Message 8", false, 2);
            ConsoleLog("Message 9", true, 2);
            ConsoleLog("Message 10", false, 3);
            ConsoleLog("Message 11", true, 3);
            ConsoleLog("Message 12", false, -1);
            ConsoleLog("Message 13", true, -1);
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
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}