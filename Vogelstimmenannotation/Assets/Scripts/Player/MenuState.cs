using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;
using Object = System.Object;

public class MenuState : PlayerState
{
    private PlayerStateMachine _playerStateMachine;
    private Dictionary<KeyCode, Commands.Player> _keyBindings;
    private KeyCode[] _keys;

    public MenuState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        EventCatalogue.MenuClosed += OnMenuClosed;

        BindKeys();
    }

    private void BindKeys()
    {
        _keyBindings = new Dictionary<KeyCode, Player>()
        {
            {KeyCode.Escape, Player.CloseMenu}
        };

        _keys = _keyBindings.Keys.ToArray();
    }

    private void OnMenuClosed(Object obj, EventArgs eventArgs)
    {
        _playerStateMachine.HandleStateOutput(StateOutput.TransitionToExplorationState);
    }

    public void OnStateEnter()
    {
        if (!MenuScript.MenuIsOpen())
        {
            MenuScript.OpenMenu();
            EventCatalogue.OnMenuOpened(this, EventArgs.Empty);
        }
    }

    public void OnStateUpdate()
    {
        if (Input.anyKeyDown)
        {
            Commands.Player command = Player.None;
            for (int i = 0; i < _keys.Length; i++)
                if (Input.GetKey(_keys[i]))
                {
                    _keyBindings.TryGetValue(_keys[i], out command);
                    break;
                }
            HandleCommands(command);
        }
    }

    public void OnStateExit()
    {
        if (MenuScript.MenuIsOpen())
        {
            MenuScript.CloseMenu();
            EventCatalogue.OnMenuClosed(this, EventArgs.Empty);
        }
    }

    private void HandleCommands(Commands.Player command)
    {
        if (command == Player.None) return;
        if (command == Player.CloseMenu) _playerStateMachine.HandleStateOutput(StateOutput.TransitionToExplorationState);
    }
}