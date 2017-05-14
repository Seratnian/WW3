using System;
using System.Collections.Generic;

public class PlayerState
{
    public bool Testing { set; get; }

    public Dictionary<Commands.Player?, Action> ActionsStateUpdate { get; set; }
    public Action[] OnStateEnterActions { get; set; }
    public Action[] OnStateExitActions { get; set; }
    private Action _bufferedAction;

    public PlayerState()
    {
        ActionsStateUpdate = null;
        OnStateEnterActions = null;
        OnStateExitActions = null;
    }

    public PlayerState(Dictionary<Commands.Player?, Action> actionsStateUpdate, Action[] onStateEnterActions, Action[] onStateExitActions)
    {
        ActionsStateUpdate = actionsStateUpdate;
        OnStateEnterActions = onStateEnterActions;
        OnStateExitActions = onStateExitActions;
    }

    public virtual void OnStateEnter()
    {
        if (OnStateEnterActions == null) return;
        foreach (var action in OnStateEnterActions)
        {
            action();
        }
    }

    public virtual void OnStateUpdate(Commands.Player? command)
    {
        if (command == null) return;

        if (ActionsStateUpdate.TryGetValue(command, out _bufferedAction))
            _bufferedAction();
    }

    public virtual void OnStateExit()
    {
        if (OnStateExitActions == null) return;
        foreach (var action in OnStateExitActions)
        {
            action();
        }
    }
}