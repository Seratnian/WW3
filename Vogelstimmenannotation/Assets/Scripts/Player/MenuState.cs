using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;

public class MenuState : PlayerState
{
    private GameObject _menuScreen;

    private PlayerStateMachine _playerStateMachine;
    private Dictionary<KeyCode, Commands.Player> _keyBindings;
    private KeyCode[] _keys;

    public MenuState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _menuScreen = GameObject.FindGameObjectWithTag("MenuScreen");
        _menuScreen.SetActive(false);

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
        _menuScreen.SetActive(true);
    }

    public void OnStateUpdate()
    {
        if(_menuScreen.activeSelf==false)
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
        _menuScreen.SetActive(false);
    }

    private void HandleCommands(Commands.Player command)
    {
        if (command == Player.None) return;
        if (command == Player.CloseMenu) _playerStateMachine.HandleStateOutput(StateOutput.TransitionToExplorationState);
    }
}