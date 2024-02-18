using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public enum State
    {
        Play, Combat, Dodge, Win, Lose
    }

    public State CurrentState = State.Play;

    [SerializeField] private PartyManager partyManager;
    [SerializeField] private EnemyManager enemyManager;

    [SerializeField] public Button swordBtn;
    [SerializeField] public Button continueBtn;

    [SerializeField] public GameObject playerTurnUI;
    [SerializeField] public GameObject winUI;
    [SerializeField] public GameObject loseUI;

    [SerializeField] public TextMeshProUGUI stateText;

    private bool buttonIsPressed = false;
    private int timer;

    private void Awake()
    {
        CurrentState = State.Play;
        swordBtn = GameObject.Find("Sword_btn").GetComponent<Button>();
        //continueBtn = GameObject.Find("Continue_btn").GetComponent<Button>();
        playerTurnUI = GameObject.Find("PlayerTurn_pnl");
        winUI = GameObject.Find("Win_pnl");
        loseUI = GameObject.Find("Lose_pnl");
        stateText = GameObject.Find("StateChangingText").GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        switch(CurrentState)
        {
            case State.Play:
                DoPlayState();
                break;
            case State.Combat:
                DoCombatState();
                break;
            case State.Dodge:
                DoDodgeState();
                break;
            case State.Win:
                DoWinState();
                break;
            case State.Lose:
                DoLoseState();
                break;
        }
    }

    private void DoPlayState()
    {
        //Debug.Log("Play");
        stateText.text = "Play";
        swordBtn.onClick.AddListener(buttonPressed);
        if(buttonIsPressed == true)
        {
            playerTurnUI.SetActive(false);
            ChangeState(State.Combat);
        }
        else
        {
            buttonIsPressed = false;
            playerTurnUI.SetActive(true);
        }
    }

    private void DoCombatState()
    {
        //Debug.Log("Combat");
        stateText.text = "Combat";
        timer++;
        if (enemyManager.enemyHealth > 0 && timer >= 300)
        {
            ChangeState(State.Dodge);
        }
        else if (enemyManager.enemyHealth <= 0 && timer >= 300)
        {
            ChangeState(State.Win);
        }
    }

    private void DoDodgeState()
    {
        //Debug.Log("Dodge");
        stateText.text = "Dodge";
        timer++;
        if (partyManager.partyHealth > 0 && timer >= 300)
        {
            ChangeState(State.Play);
        }
        else if (partyManager.partyHealth <= 0)
        {
            ChangeState(State.Lose);
        }
    }

    private void DoWinState()
    {
        //Debug.Log("Win");
        stateText.text = "Win";
        continueBtn.onClick.AddListener(buttonPressed);
        if(buttonIsPressed == true)
        {
            ChangeState(State.Play);
        }
        else
        {
            buttonIsPressed = false;
        }
    }

    private void DoLoseState()
    {
        //Debug.Log("Lose");
        stateText.text = "Lose";
    }

    public void ChangeState(State newState)
    {
        if (CurrentState == newState)
        {
            return;
        }
        timer = 0;
        CurrentState = newState;
    }

    public void buttonPressed()
    {
        buttonIsPressed = true;
    }

    public void buttonNotPressed()
    {
        buttonIsPressed = false;
    }
}
