using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCombatState : State
{
    private GameFSM _stateMachine;
    private GameController _controller;

    public GameCombatState(GameFSM stateMachine, GameController controller)
    {
        _stateMachine = stateMachine;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
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
