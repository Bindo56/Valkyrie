using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    Enemy_Skeleton enemy;

    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
       // enemy.CounterStunnedImage.SetActive(true);
        stateTimer = enemy.stunDur;
        enemy.fx.InvokeRepeating("RedBlink", 0, 0.1f);
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDir.x , enemy.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
       // enemy.CounterStunnedImage.SetActive(true);

        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
          stateMachine.ChangeState(enemy.idleState);
        }
    }
}
