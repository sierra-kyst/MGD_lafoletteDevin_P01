using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public Button continueBtn;
    public Button exitBtn;

    public GameWinState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("WinState");
        continueBtn = GameObject.Find("Continue_btn").GetComponent<Button>();
        exitBtn = GameObject.Find("Exit_btn").GetComponent<Button>();
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
        continueBtn.onClick.AddListener(continueCommand);
    }

    private void continueCommand()
    {
        _stateMachine.ChangeState(_stateMachine.PlayState);
    }
}
