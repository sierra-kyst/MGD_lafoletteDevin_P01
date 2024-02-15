using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetupState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public Button endlessMode;

    // this is our 'constructor', called when this state is created
    public GameSetupState(GameFSM stateMachine, GameController controller)
    {
        // hold on to our parameters in our class variables for reuse
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("SetupState");
        endlessMode = GameObject.Find("EndlessMode_btn").GetComponent<Button>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
    }

    public override void Tick()
    {
        base.Tick();
        //_stateMachine.ChangeState(_stateMachine.PlayState);
        endlessMode.onClick.AddListener(GetOut);
    }

    private void GetOut()
    {
        _stateMachine.ChangeState(_stateMachine.PlayState);
    }
}
