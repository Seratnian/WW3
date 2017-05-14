using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Commands;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState _currentPlayerState;
    private Commands.Player? _command;
    private Dictionary<string, Commands.Player?> _commandBindings;
    private string[] _commandKeys;
    private string[] _commandKeysDownOnly;

    private void Start()
    {
        TransitionToState
            (
            new PlayerState(new Dictionary<Commands.Player?, Action>() {{Commands.Player.Interact, () => { }}},
                new Action[] { () => Debug.Log("Default empty starting state. Press A for testing.") }, 
                null)
                );

        _command = null;
        BuildBindings();
    }

    private void BuildBindings()
    {
        _commandBindings = new Dictionary<string, Commands.Player?> { { "Fire1", Commands.Player.Interact }, {"Jump", Commands.Player.OpenMenu} };        
        _commandKeys = _commandBindings.Keys.ToArray();
        _commandKeysDownOnly = new[] {"Fire1", "Jump"};
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) CreateTestStatesAndRun();

        if (Input.anyKey)
        {
            _currentPlayerState.OnStateUpdate(GetPlayerCommand());
        }
    }

    private void TransitionToState(PlayerState state)
    {
        //Don't set _currentState to null
        if (state == null) return;

        if(_currentPlayerState!=null)
            _currentPlayerState.OnStateExit();

        state.OnStateEnter();
        _currentPlayerState = state;
    }

    private Commands.Player? GetPlayerCommand()
    {
        for (int i = 0; i < _commandKeysDownOnly.Length; i++)
        {
            if (Input.GetButtonDown(_commandKeysDownOnly[i]))
            {
                if (_commandKeys[i] != null)
                    _commandBindings.TryGetValue(_commandKeys[i], out _command);
                return _command;
            }
        }

        for (int i = 0; i < _commandKeys.Length; i++)
        {
            if (Input.GetButton(_commandKeys[i]) && !_commandKeysDownOnly.Contains(_commandKeys[i]))
            {
                if(_commandKeys[i]!=null)
                    _commandBindings.TryGetValue(_commandKeys[i], out _command);
                return _command;
            }
        }

        return null;
    }

    private void CreateTestStatesAndRun()
    {
        Debug.Log("Creating states..");

        PlayerState stateOne = new PlayerState();
        PlayerState stateTwo = new PlayerState( null, new Action[] { () => Debug.Log("Transition complete. State Two entered.") }, null);

        stateTwo.ActionsStateUpdate = new Dictionary<Player?, Action>()
        {
            {Commands.Player.Interact, () => Debug.Log("Press space to transition to State One.")},
            {Commands.Player.OpenMenu, () => TransitionToState(stateOne)}
        };

        Dictionary<Commands.Player?, Action> stateOneDic =
            new Dictionary<Commands.Player?, Action> {{Commands.Player.Interact, () => TransitionToState(stateTwo)}};

        stateOne = new PlayerState(stateOneDic, new Action[] { () => Debug.Log("State One entered. Click the left mouse button to transition to state two.") }, new Action[] { () => Debug.Log("State One exited.") });        

        Debug.Log("States complete.");

        TransitionToState(stateOne);


    }
}