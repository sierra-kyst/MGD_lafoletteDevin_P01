using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameFSM : StateMachineMB
{
    private GameController _controller;

    // state variables here
    public GameSetupState SetupState { get; private set; }
    public GamePlayState PlayState { get; private set; }
    public GameWinState WinState { get; private set; }
    public GameLoseState LoseState { get; private set; }
    public GameCombatState CombatState { get; private set; }
    public GameDodgeState DodgeState { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<GameController>();
        // state instantiation here
        SetupState = new GameSetupState(this, _controller);
        PlayState = new GamePlayState(this, _controller);
        WinState = new GameWinState(this, _controller);
        LoseState = new GameLoseState(this, _controller);
        CombatState = new GameCombatState(this, _controller);
        DodgeState = new GameDodgeState(this, _controller);
    }

    private void Start()
    {
        ChangeState(SetupState);
    }
}
