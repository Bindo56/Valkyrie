using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

   public  int comboCounter { get; private set; }
    float lastTimeAttacked;
    float comboWindow = 2;  

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {


    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0; //reset Player Position from Move State....

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;


        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

      //  Debug.Log(comboWindow);
        player.anim.SetInteger("ComboCounter", comboCounter);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttacked = Time.time;

        player.StartCoroutine("BusyFor", .15f);
    }

    public override void Update()
    {
        base.Update();


        if(stateTimer < 0)
        {
            player.LockPlayer();
        }

        if(triggerCalled) 
        {
          stateMachine.ChangeState(player.idleState);
        }

    }
}
