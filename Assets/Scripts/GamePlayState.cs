using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    public GameObject playerTurnUI;

    public Button swordAttack;
    public Button sniperAttack;
    public Button spellAttack;

    public GameObject enemy;

    public bool buttonPressed;

    public GamePlayState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayState");
        //Instantiate(enemy);
        
        playerTurnUI = GameObject.Find("PlayerTurn_pnl");
        playerTurnUI.SetActive(true);
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
    }
}
