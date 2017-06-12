using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;

public class IdentificationState : PlayerState
{
    private GameObject _identificationScreen;

    private PlayerStateMachine _playerStateMachine;
    private Dictionary<KeyCode, Commands.Player> _keyBindings;
    private KeyCode[] _keys;

    public IdentificationState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _identificationScreen = GameObject.FindGameObjectWithTag("IdentificationScreen");
        _identificationScreen.SetActive(false);

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

    public void OnStateEnter()
    {
        _identificationScreen.SetActive(true);
    }

    public void OnStateUpdate()
    {
        if (_identificationScreen.activeSelf == false)
            _playerStateMachine.HandleStateOutput(StateOutput.TransitionToExplorationState);

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
        _identificationScreen.GetComponent<BirdIdentificationScreen>().CancelIdentification();
        _identificationScreen.SetActive(false);
    }

    private void HandleCommands(Player command)
    {
        if (command == Player.None) return;
        if (command == Player.CloseMenu) _playerStateMachine.HandleStateOutput(StateOutput.TransitionToExplorationState);
    }
}