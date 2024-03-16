using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerGroundedState
{
    public PlayerMove(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        /*if(player.isWallDetected())
            stateMachine.ChangeState(player.wallSlide);*/

        //Debug.Log("Run");
       //if(player.isGroundDetected())
        
          player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
       
        
        if (xInput == 0 || player.isWallDetected())
        {
            stateMachine.ChangeState(player.idleState);

        }
    }
}
