using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;

public class PlayerController : MonoBehaviour, PlayerStateMachine
{
    private PlayerState _currentState;
    private PlayerState _explorationState;
    private PlayerState _identificationState;
    private PlayerState _menuState;

    private void Start()
    {
        _currentState = null;
        _explorationState = new ExplorationPlayerState(this);
        _menuState = new MenuState(this);
        _identificationState = new IdentificationState(this);

        TransitionToState(_explorationState);
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnStateUpdate();
    }

    public void HandleStateOutput(StateOutput output)
    {
        switch (output)
        {
            case StateOutput.TransitionToExplorationState:
                TransitionToState(_explorationState);
                break;
            case StateOutput.TransitionToMenuState:
                TransitionToState(_menuState);
                break;
            case StateOutput.TransitionToIdentificationState:
                TransitionToState(_identificationState);
                break;
        }
    }

    private void TransitionToState(PlayerState playerState)
    {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = playerState;
        if (_currentState != null) _currentState.OnStateEnter();
    }
}