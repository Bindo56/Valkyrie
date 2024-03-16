using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
   
    public Skeleton_IdleState idleState {  get; private set; }
    public Skeleton_Move_State moveState { get; private set; }

    public SkeletonBattleState battleState { get; private set; }

    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    public SkeletonDeadState skeletonDeadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState =   new Skeleton_IdleState(this, stateMachine, "Idle", this);
        moveState =   new Skeleton_Move_State(this, stateMachine, "Walk", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Walk", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        skeletonDeadState = new SkeletonDeadState(this, stateMachine, "Idle", this);


    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.U)) 
        {
            stateMachine.ChangeState(stunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(skeletonDeadState);
    }
}
