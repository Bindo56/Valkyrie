using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private Enemy_Skeleton Skeleton;
    public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _Skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.Skeleton = _Skeleton;
    }

    public override void Enter()
    {
        base.Enter();

        Skeleton.anim.SetBool(Skeleton.lastAnimBoolName, true);
        Skeleton.anim.speed = 0;
        Skeleton.cd.enabled = false;

        stateTimer = .15f;

    }

    public override void Update()
    {
        base.Update();
        if(stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 5);   
        }
    }
}
