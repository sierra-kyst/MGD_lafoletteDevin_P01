using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDodgeState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private int timer;
    private PartyManager partyManager;
    private EnemyManager enemyManager;

    public GameDodgeState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("DodgeState");
        timer = 0;
        enemyManager.targetPartyMember = Random.Range(1, 4);
        partyManager._ninjaHealth -= enemyManager.enemyDamage;
        Debug.Log("Ninja's health: " + partyManager._ninjaHealth);
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
        timer++;
        if (partyManager.partyHealth > 0 && timer >= 5000)
        {
            _stateMachine.ChangeState(_stateMachine.PlayState);
        }
        else if (partyManager.partyHealth <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.LoseState);
        }
    }
}
