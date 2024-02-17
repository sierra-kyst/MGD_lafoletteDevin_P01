using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static Button swordBtn;
    public static Button sniperBtn;
    public static Button spellBtn;

    //public bool buttonPressed;

    private GamePlayState playState;

    private GameFSM _stateMachine;

    public int combatMode;

    public GameController (GameFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Awake()
    {
        swordBtn = GameObject.Find("Sword_btn").GetComponent<Button>();
        //sniperBtn = GameObject.Find("Sniper_btn").GetComponent<Button>();
        //spellBtn = GameObject.Find("Spell_btn").GetComponent<Button>();
    }

    private void Update()
    {
        swordBtn.onClick.AddListener(Sword);
        //sniperBtn.onClick.AddListener(playState.Sniper);
        //spellBtn.onClick.AddListener(playState.Spell);
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
