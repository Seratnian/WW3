using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;
using Object = System.Object;

public class ExplorationPlayerState : PlayerState
{
    private PlayerMovement _playerMovement;
    private PlayerStateMachine _playerStateMachine;
    private Dictionary<KeyCode, Player> _keyBindings;
    private KeyCode[] _keys;
    private GameObject _ball;
    private GameObject _ballSpawn;

    public ExplorationPlayerState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _ball = Resources.Load("Ball", typeof(GameObject)) as GameObject;
        _ballSpawn = GameObject.FindGameObjectWithTag("BallSpawn");
        EventCatalogue.IdentificationOpened += OnIdentificationOpened;

        BindKeys();
    }

    private void OnIdentificationOpened(Object obj, EventArgs eventArgs)
    {
        HandleCommands(Player.IdentificationOpened);
    }

    private void BindKeys()
    {
        _keyBindings = new Dictionary<KeyCode, Player>
        {
            {KeyCode.Mouse0, Player.ThrowBall},
            {KeyCode.Space, Player.Interact},
            {KeyCode.Escape, Player.OpenMenu}
        };

        _keys = _keyBindings.Keys.ToArray();
    }

    public void OnStateEnter()
    {
        _playerMovement.enabled = true;
    }

    public void OnStateUpdate()
    {
        if (Input.anyKeyDown)
        {
            var command = Player.None;
            for (var i = 0; i < _keys.Length; i++)
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
        _playerMovement.enabled = false;
    }

    private void HandleCommands(Player command)
    {
        if (command == Player.None) return;

        switch (command)
        {
            case Player.ThrowBall:
                ThrowBall();
                break;
            case Player.OpenMenu:
                _playerStateMachine.HandleStateOutput(StateOutput.TransitionToMenuState);
                break;
            case Player.IdentificationOpened:
                _playerStateMachine.HandleStateOutput(StateOutput.TransitionToIdentificationState);
                break;
            case Player.Interact:
                Interact();
                break;
        }
    }

    private void ThrowBall()
    {
        float throwForce = 50;
        var spawnPosition = _ballSpawn.transform.position;
        var throwDirection = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).direction;

        var ball = GameObject.Instantiate(_ball);
        ball.transform.position = spawnPosition;


        ball.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }

    private void Interact()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        Debug.DrawRay(_playerMovement.transform.position, ray.direction, Color.blue, 5f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
            hit.transform.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
    }
}