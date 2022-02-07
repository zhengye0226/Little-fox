using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerCommandBase
{
    private string _commandId;
    private string _commandDescription;
    private string _commandFormat;

    public string commandId{ get{ return _commandId; } }
    public string commandDescription{ get{ return _commandDescription; } }
    public string commandFormat{ get{ return _commandFormat; } }

    public DebuggerCommandBase(string id, string description, string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebuggerCommand : DebuggerCommandBase
{
    private Action command;
    public DebuggerCommand(string id, string description, string format, Action command):base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}
