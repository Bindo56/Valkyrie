using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : PlayerState
{
    public PlayerWallSlide(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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


        if(Input.GetKeyDown(KeyCode.Space))
        {
          stateMachine.ChangeState(player.wallJump);
            return;
        }


       if(yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);

       else
        rb.velocity = new Vector2(0,rb.velocity.y * .7f);
        
        if(xInput !=0 && player.facingDir !=xInput) 
            stateMachine.ChangeState(player.idleState);

        /*if(xInput == -1 && player.facingDir == -1)
            rb.velocity = new Vector2(0,rb.velocity.y);*/

        if(player.isGroundDetected()) 
          stateMachine.ChangeState(player.idleState);

    }
        
}


    
