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
    public static DebuggerCommand CREATEPOSITION;
    public static DebuggerCommand<int, int> ADD_ENEMY;
    public static DebuggerCommand<int> FIND_ENEMY;
    public List<object> commandlist;
    bool showMessage;
    Resolution[] resolutions;
    Vector2 scroll;
    List<string> messageTable;
    List<string> commandTable;
    int commandTableNum = 0;
    int commandTableSubNum = 0;
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
            showMessage = true;
        }
    }

    public void OnReturnPreviousCommand()
    {
        if (showConsole)
        {
            if(commandTable.Count!=0)
            {
                ReturnSubNum(-1);
                input = commandTable[commandTableSubNum];
            }
        }
    }

    public void OnReturnNextCommand()
    {
        if (showConsole)
        {
            if(commandTable.Count!=0)
            {
                ReturnSubNum(1);
                input = commandTable[commandTableSubNum];
            }
        }
    }

    public void ReturnSubNum(int changeNumber)
    {
        if(changeNumber < 0)
        {
            if(commandTableSubNum >0 && commandTableSubNum <= commandTableNum-1)
            {
               commandTableSubNum += changeNumber; 
            }
            else
            {
                commandTableSubNum = commandTableNum-1;
            }
        }
        else
        {
            if(commandTableSubNum >0 && commandTableSubNum < commandTableNum-1)
            {
               commandTableSubNum += changeNumber; 
            }
            else
            {
                commandTableSubNum = 0;
            }
        }
    }

    private void Awake()
    {
        messageTable = new List<string>();
        commandTable = new List<string>();
        RESTION_SHOW = new DebuggerCommand("RESTION_SHOW", "show all resolution about your minitor", "resolution_show", () =>
          {
              foreach (var item in Screen.resolutions)
              {
                  messageTable.Add(item.width + "X" + item.height);
              }
          });

        NEW_GAME = new DebuggerCommand("NEW_GAME", "change PlayerPrefs.isNewGame", "panel test", () =>
        {
            messageTable.Add("NEW_GAME");
        });

        CREATEPOSITION = new DebuggerCommand("CREATEPOSITION", "Show the area that we can build enemy", "Enemy Create Area", () =>
        {
            foreach (var item in GameManger._instance.createEnemyAre.Keys)
            {
                messageTable.Add("minWith:" + item[0] + " maxWidth:" + item[1] + " minHeight:" + GameManger._instance.createEnemyAre[item][0] + " maxHeight:" + GameManger._instance.createEnemyAre[item][1]);
            }
        });
        ADD_ENEMY = new DebuggerCommand<int, int>("ADD_ENEMY", "Add enemy in the map", "add_enemy <enemy_type, enemy_amount>", (x, y) =>{
            EnemyPosition._instance.AddEnemy(x, y);
        });
        FIND_ENEMY = new DebuggerCommand<int>("FIND_ENEMY", "Find a enenmy and go there", "find_enemy <enemy_type>", (x) => {
            EnemyPosition._instance.FindEnemy(x);
        });

        commandlist = new List<object>
        {
            RESTION_SHOW,
            NEW_GAME,
            CREATEPOSITION,
            ADD_ENEMY,
            FIND_ENEMY
        };
    }

    private void OnGUI()
    {

        if (!showConsole)
        {
            input = "";
            //messageTable.Clear();
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

            if (i == messageTable.Count - 1 && showMessage)
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
        string[] properties = input.Split(' ');
        for (int i = 0; i < commandlist.Count; i++)
        {
            DebuggerCommandBase commandBase = commandlist[i] as DebuggerCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                messageTable.Add(input);
                if (commandlist[i] as DebuggerCommand != null)
                {
                    (commandlist[i] as DebuggerCommand).Invoke();
                }
                else if(commandlist[i] as DebuggerCommand<int, int> != null)
                {
                    int type = int.Parse(properties[1]);
                    int number = int.Parse(properties[2]);
                    (commandlist[i] as DebuggerCommand<int, int>).Invoke(type, number);
                }
                else if(commandlist[i] as DebuggerCommand<int> != null)
                {
                    int type = int.Parse(properties[1]);
                    (commandlist[i] as DebuggerCommand<int>).Invoke(type);
                }
                commandTable.Add(input);
                commandTableNum += 1;
                commandTableSubNum = commandTableNum - 1;
            }

        }

    }

}
