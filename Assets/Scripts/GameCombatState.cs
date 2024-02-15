using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCombatState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;
    private EnemyManager enemyManager;
    private PartyManager partyManager;
    private int timer;
    public GameObject trail;
    private GamePlayState playState;
    private bool damagableEnemy;

    public GameCombatState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("CombatState");
        timer = 0;
        trail = GameObject.Find("Trail");
        if(playState.combatMode == 1)
        {
            trail.SetActive(true);
        }
        damagableEnemy = true;
    }

    public override void Exit()
    {
        if (playState.combatMode == 1)
        {
            trail.SetActive(false);
        }
        damagableEnemy = false;
        base.Exit();
    }

    public override void FixedTick()
    {
        base.FixedTick();
        //This is where I differentiate the combat modes' input methods
        if (damagableEnemy == true)
        {
            if (playState.combatMode == 1)
            {
                enemyManager.enemyHealth -= partyManager.ninjaDamage;
                Debug.Log("Enemy's Health: " + enemyManager.enemyHealth);
            }
            else if (playState.combatMode == 2)
            {

            }
            else if (playState.combatMode == 3)
            {

            }
        }
    }

    public override void Tick()
    {
        base.Tick();
        timer++;
        if (enemyManager.enemyHealth > 0 && timer == 5000)
        {
            _stateMachine.ChangeState(_stateMachine.DodgeState);
        }
        else if(enemyManager.enemyHealth <= 0 && timer == 5000)
        {
            _stateMachine.ChangeState(_stateMachine.WinState);
        }
    }
}
