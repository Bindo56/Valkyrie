using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Move_State : SkeletonGroundedState
{
    public Skeleton_Move_State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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

        enemy.SetVelocity( enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if(enemy.isWallDetected() || !enemy.isGroundDetected()) 
        {
            enemy.flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
