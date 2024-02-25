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
    private InputManager inputManager;

    [SerializeField] public GameObject playerTurnUI;
    [SerializeField] public GameObject winUI;
    [SerializeField] public GameObject loseUI;
    [SerializeField] public GameObject enemy;

    [SerializeField] public TextMeshProUGUI stateText;
    [SerializeField] public TextMeshProUGUI dodgeText;

    private bool buttonIsPressed = false;
    private int timer;
    private int randomTimeStampStart;
    private int randomTimeStampEnd;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        randomTimeStampStart = Random.Range(0, 250);
        randomTimeStampEnd = randomTimeStampStart + 50;
        CurrentState = State.Play;
        playerTurnUI = GameObject.Find("PlayerTurn_pnl");
        //winUI = GameObject.Find("Win_pnl").GetComponent<GameObject>();
        //loseUI = GameObject.Find("Lose_pnl").GetComponent<GameObject>();
        stateText = GameObject.Find("StateChangingText").GetComponent<TextMeshProUGUI>();
        dodgeText = GameObject.Find("DodgeFlavorText").GetComponent<TextMeshProUGUI>();
        //winUI.SetActive(false);
        //loseUI.SetActive(false);
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
        dodgeText.text = " ";
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
        //if screen is touched deal enemy damage
        if(inputManager.touched == true)
        {
            enemyManager.enemyHealth -= 0.05f;
            dodgeText.text = "Enemy's Health: " + enemyManager.roundedHealth;
        }
        if (enemyManager.enemyHealth > 0 && timer >= 300)
        {
            ChangeState(State.Dodge);
        }
        else if (enemyManager.enemyHealth <= 0)
        {
            ChangeState(State.Win);
        }
    }

    private void DoDodgeState()
    {
        //Debug.Log("Dodge");
        stateText.text = "Dodge";
        timer++;
        dodgeText.text = "Wait...";
        //if screen is not pressed within time limit, deal damage to player
        if (timer >= randomTimeStampStart && timer <= randomTimeStampEnd)
        {
            dodgeText.text = "Tap Now!";
            if (inputManager.touched == true)
            {
                dodgeText.text = "Dodged Enemy Attack!";
                ChangeState(State.Play);
            }
        }
        else if(timer < randomTimeStampStart && inputManager.touched == true)
        {
            dodgeText.text = "Dodged Too Early!";
            partyManager._ninjaHealth -= Random.Range(5, 20);
        }
        else if(timer > randomTimeStampEnd && inputManager.touched == true)
        {
            dodgeText.text = "Dodged Too Late!";
            partyManager._ninjaHealth -= Random.Range(5, 20);
        }
        else if (timer >= 300)
        {
            dodgeText.text = "Didn't Dodge in Time!";
            partyManager._ninjaHealth -= Random.Range(5, 20);
            if(timer >= 400)
            {
                ChangeState(State.Play);
            }
        }
        else if (partyManager._ninjaHealth <= 0)
        {
            ChangeState(State.Lose);
        }
    }

    private void DoWinState()
    {
        //Debug.Log("Win");
        stateText.text = "Win";
        if(buttonIsPressed == true)
        {
            //winUI.SetActive(false);
            ChangeState(State.Play);
        }
        else
        {
            buttonIsPressed = false;
            winUI.SetActive(true);
        }
    }

    private void DoLoseState()
    {
        //Debug.Log("Lose");
        stateText.text = "Lose";
        loseUI.SetActive(true);
    }

    public void ChangeState(State newState)
    {
        if (CurrentState == newState)
        {
            return;
        }
        buttonIsPressed = false;
        timer = 0;
        CurrentState = newState;
    }

    public void buttonPressed()
    {
        buttonIsPressed = true;
    }
}
