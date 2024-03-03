using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

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
    [SerializeField] public TextMeshProUGUI timerText;

    [SerializeField] public GameObject hitParticle = null;
    private Vector2 mousePos;

    private Camera cam = null;

    [SerializeField] public AudioSource stateChangeSound;
    [SerializeField] public AudioSource enemyDamageSound;
    [SerializeField] public AudioSource playerDamageSound;

    private bool buttonIsPressed = false;
    private int timer;
    private float visibleTimer;
    private float roundedVisibleTimer;
    private int randomTimeStampStart;
    private int randomTimeStampEnd;
    private float timerROF;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        CurrentState = State.Play;
        hitParticle.SetActive(false);
        randomTimeStampStart = Random.Range(0, 250);
        randomTimeStampEnd = randomTimeStampStart + 50;
        playerTurnUI = GameObject.Find("PlayerTurn_pnl");
        stateText = GameObject.Find("StateChangingText").GetComponent<TextMeshProUGUI>();
        dodgeText = GameObject.Find("DodgeFlavorText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        hitParticle = GameObject.Find("Hit Particle").GetComponent<GameObject>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        roundedVisibleTimer = Mathf.Round(visibleTimer);
        switch (CurrentState)
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
        timerText.text = " ";
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
        visibleTimer -= 0.015f;
        timerText.text = "" + roundedVisibleTimer;
        //if screen is touched deal enemy damage
        if (inputManager.touched == true)
        {
            if(timerROF == 0)
            {
                enemyManager.enemyHealth -= 1;
                if (hitParticle != null)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    hitParticle.SetActive(true);
                    hitParticle.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
                }
                enemyDamageSound.Play();
                dodgeText.text = "Enemy's Health: " + enemyManager.roundedHealth;
            }
            timerROF += 0.02f;
            if (timerROF >= 1)
            {
                timerROF = 0;
            }
        }
        else if(inputManager.touched == false)
        {
            timerROF = 0;
            hitParticle.SetActive(false);
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
        timerText.text = "";
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
            playerDamageSound.Play();
            ChangeState(State.Play);
        }
        else if(timer > randomTimeStampEnd && inputManager.touched == true)
        {
            dodgeText.text = "Dodged Too Late!";
        }
        else if (timer > randomTimeStampEnd)
        {
            dodgeText.text = "Didn't Dodge in Time!";
            
            if(timer >= 300)
            {
                partyManager._ninjaHealth -= Random.Range(5, 20);
                playerDamageSound.Play();
                ChangeState(State.Play);
            }
        }
        else if (partyManager._ninjaHealth <= 0)
        {
            ChangeState(State.Lose);
        }
        else
        {
            dodgeText.text = "Wait...";
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
        visibleTimer = 5;
        timerROF = 0;
        stateChangeSound.Play();
        CurrentState = newState;
    }

    public void buttonPressed()
    {
        buttonIsPressed = true;
    }
}
