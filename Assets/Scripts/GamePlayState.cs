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

    public ButtonToStateInteraction bTSI;

    public int combatMode;

    public GameObject enemy;

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
        //swordAttack = GameObject.Find("Sword_btn").GetComponent<Button>();
        sniperAttack = GameObject.Find("Sniper_btn").GetComponent<Button>();
        spellAttack = GameObject.Find("Spell_btn").GetComponent<Button>();
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
        //bTSI.swordBtn.onClick.AddListener(Sword);
        sniperAttack.onClick.AddListener(Sniper);
        spellAttack.onClick.AddListener(Spell);
    }

    public void Sword()
    {
        combatMode = 1;
        _stateMachine.ChangeState(_stateMachine.CombatState);
    }
    public void Sniper()
    {
        combatMode = 2;
        _stateMachine.ChangeState(_stateMachine.CombatState);
    }
    public void Spell()
    {
        combatMode = 3;
        _stateMachine.ChangeState(_stateMachine.CombatState);
    }
}
