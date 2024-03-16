using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{

    bool canCreateClone;
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canCreateClone = true;
        stateTimer = player.counterAttackDur;
        player.anim.SetBool("SuccessfulCounter", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.LockPlayer();

        Collider2D[] collider = Physics2D.OverlapCircleAll(player.attackChecks.position, player.attackCheckRadius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if(hit.GetComponent<Enemy>().CanBeStunned()) 
                {
                    stateTimer = 10;
                   player.anim.SetBool("SuccessfulCounter", true);
                    if(canCreateClone)
                    {
                        canCreateClone = false;
                      player.skill.clone.CreateCloneOnCounterAttack(hit.transform);

                    }


                }
            }
        }

        if(stateTimer < 0 || triggerCalled) 
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
