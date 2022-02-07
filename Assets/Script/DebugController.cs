using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;
    public static DebuggerCommand RESTION_SHOW;
    public List<object> commandlist;
    bool showMessage;
    Resolution[] resolutions;
    Vector2 scroll;
    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }

    private void Awake()
    {
        RESTION_SHOW = new DebuggerCommand("RESTION_SHOW", "show all resolution about your minitor", "resolution_show", () =>
          {
              showMessage = true;
          });
        commandlist = new List<object>
        {
            RESTION_SHOW,
        };
    }

    private void OnGUI()
    {

        if (!showConsole) { return; }

        float y = 0f;

        if (showMessage)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            resolutions = Screen.resolutions;

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * resolutions.Length);

            scroll = GUI.BeginScrollView(new Rect(0, 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < resolutions.Length; i++)
            {
                string resolution = resolutions[i] + "\r\n";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, resolution);

            }

            GUI.EndScrollView();
            y += 100f;
        }
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

    }

    public void HandleInput()
    {

        for (int i = 0; i < commandlist.Count; i++)
        {
            DebuggerCommandBase commandBase = commandlist[i] as DebuggerCommandBase;

            if (input.Contains(commandBase.commandId))
            {

                if (commandlist[i] as DebuggerCommand != null)
                {
                    (commandlist[i] as DebuggerCommand).Invoke();

                }

            }

        }

    }

}
