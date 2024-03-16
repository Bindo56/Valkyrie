using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .4f;

        rb.velocity = new Vector2(5 * -player.facingDir, player.jumpForce);

        player.flip();  

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
