using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState : SkeletonGroundedState
{
    public Skeleton_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {


    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        //Debug.Log("Idle");
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }

    }
}
