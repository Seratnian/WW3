using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using Commands;
using UnityEngine;

public class ExplorationPlayerState : PlayerState
{
    private PlayerMovement _playerMovement;
    private PlayerStateMachine _playerStateMachine;
    private Dictionary<KeyCode, Commands.Player> _keyBindings;
    private KeyCode[] _keys;
    private GameObject _ball;
    private GameObject _ballSpawn;

    public ExplorationPlayerState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _ball = Resources.Load("Ball", typeof(GameObject)) as GameObject;
        _ballSpawn = GameObject.FindGameObjectWithTag("BallSpawn");

        BindKeys();
    }

    private void BindKeys()
    {
        _keyBindings = new Dictionary<KeyCode, Player>()
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
            Commands.Player command = Player.None;
            for(int i=0; i<_keys.Length;i++)
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

    private void HandleCommands(Commands.Player command)
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
            case Player.Interact:
                Interact();
                break;

        }
    }

    private void ThrowBall()
    {
        float throwForce = 50;
        Vector3 spawnPosition = _ballSpawn.transform.position;
        Vector3 throwDirection = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).direction;

        GameObject ball = GameObject.Instantiate(_ball);
        ball.transform.position = spawnPosition;
        

        ball.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }

    private void Interact()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        Debug.DrawRay(_playerMovement.transform.position, ray.direction, Color.blue, 5f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            hit.transform.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }
}