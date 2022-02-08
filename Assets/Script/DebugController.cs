using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;
    public static DebuggerCommand RESTION_SHOW;
    public static DebuggerCommand NEW_GAME;
    public List<object> commandlist;
    bool showMessage;
    Resolution[] resolutions;
    Vector2 scroll;
    List<string> messageTable;
    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole && commandlist.Contains(input))
        {
            HandleInput();
            input = "";
            showMessage = true;
        }
        else if(showConsole)
        {
            messageTable.Add("This command is not useful");
            input = "";
            showMessage = true;
        }
    }

    private void Awake()
    {
        messageTable = new List<string>();
        RESTION_SHOW = new DebuggerCommand("RESTION_SHOW", "show all resolution about your minitor", "resolution_show", () =>
          {
                 foreach(var item in Screen.resolutions)
                 {
                     messageTable.Add(item.width + "X" + item.height);
                 } 
          });

        NEW_GAME = new DebuggerCommand("NEW_GAME", "change PlayerPrefs.isNewGame", "panel test", () =>
        {
            messageTable.Add("NEW_GAME");
        });
        commandlist = new List<object>
        {
            RESTION_SHOW,
            NEW_GAME
        };
    }

    private void OnGUI()
    {

        if (!showConsole) 
        { 
            input = "";
            messageTable.Clear();
            return; 
        }
        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 100), "");

        Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * messageTable.Count);

        scroll = GUI.BeginScrollView(new Rect(0, 5f, Screen.width, 90), scroll, viewport);

        for (int i = 0; i < messageTable.Count; i++)
        {
            string message = messageTable[i] + "\r\n";

            Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

            GUI.Label(labelRect, message);

            if(i == messageTable.Count - 1 && showMessage)
            {
                //make sure ScrollView show in the lastest message
                GUI.ScrollTo(labelRect);
            }
        }
        GUI.EndScrollView();
        y += 100f;

        //make sure ScrollView can smooth
        showMessage = false;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
                
        // scroll = GUI.BeginScrollView(new Rect(10, 10, 100, 50), scroll, new Rect(0, 0, 220, 10));

        // if (GUI.Button(new Rect(0, 0, 100, 20), "Go Right"))
        //     GUI.ScrollTo(new Rect(120, 0, 100, 20));

        // if (GUI.Button(new Rect(120, 0, 100, 20), "Go Left"))
        //     GUI.ScrollTo(new Rect(0, 0, 100, 20));

        // GUI.EndScrollView();
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
                    messageTable.Add("Command sucess");
                    (commandlist[i] as DebuggerCommand).Invoke();
                }
                else
                {
                    messageTable.Add("This command is not ues in hall");
                }

            }

        }

    }

}
