using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(player.blackholeState);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1) &&  HasNoSword())
        {
            stateMachine.ChangeState(player.AimSwordState);
        }

        if(Input.GetKeyDown(KeyCode.Q) && player.skill.Parry.parryUnlocked)
        {
            stateMachine.ChangeState(player.CounterAttack);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            stateMachine.ChangeState(player.PrimayAttack);
        }

       if(!player.isGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        if(Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected()) 
        {
            stateMachine.ChangeState(player.jumpState);
        }

    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }


}
