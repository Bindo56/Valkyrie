using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackhole_state : PlayerState
{
    float flyTime = .4f;
    bool skillUsed;

    float defaultGravity;

    public PlayerBlackhole_state(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        defaultGravity = player.rb.gravityScale;

        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;


    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = defaultGravity;
        player.fx.MakeTransparent(false);
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 15);

        }

        if(stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -.1f);

            if(!skillUsed)
            {
                if (player.skill.blackhole.CanUseSkill())
                    skillUsed = true;
            }
        }

    }
}



