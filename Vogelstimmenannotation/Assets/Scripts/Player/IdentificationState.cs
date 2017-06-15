using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;
using Object = System.Object;

public class IdentificationState : PlayerState
{
    private PlayerStateMachine _playerStateMachine;
    private Dictionary<KeyCode, Commands.Player> _keyBindings;
    private KeyCode[] _keys;

    public IdentificationState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        EventCatalogue.IdentificationClosed += OnIdentificationClosedEvent;

        BindKeys();
    }

    private void BindKeys()
    {
        _keyBindings = new Dictionary<KeyCode, Player>()
        {
            
        };

        _keys = _keyBindings.Keys.ToArray();
    }

    private void OnIdentificationClosedEvent(Object obj, EventArgs eventArgs)
    {
        HandleCommands(Player.IdentificationClosed);
    }

    public void OnStateEnter()
    {
        
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

    }

    private void HandleCommands(Player command)
    {
        if (command == Player.None) return;
        if (command == Player.IdentificationClosed) _playerStateMachine.HandleStateOutput(StateOutput.TransitionToExplorationState);
    }
}