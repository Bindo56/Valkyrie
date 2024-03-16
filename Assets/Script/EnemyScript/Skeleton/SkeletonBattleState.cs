using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    Transform player;
    private int moveDir;

    Enemy_Skeleton enemy;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName , Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        // Debug.Log("inBattle");

        player = PlayerManager.instance.player.transform;
        //enemy.ClosedCounterAttackWindow();
    }
    public override void Update()
    {
        base.Update();


        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance )
            {
                if (CanAttack())
                {
                  stateMachine.ChangeState(enemy.attackState);

                }
            }
        }

        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position) > 12) 
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;


        if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);


    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttack + enemy.attackCoolDown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        else return false;
          
        
    }

}
