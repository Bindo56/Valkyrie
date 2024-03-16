using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        // player.skill.sword.DotsActive(false);

        player.StartCoroutine("BusyFor", .2f);

    }

    public override void Update()
    {
        base.Update();

        player.LockPlayer();

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.idleState);
        }


        Vector2 mousePosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
            player.flip();

        else if (player.transform.position.x < mousePosition.x && player.facingDir == -1)
            player.flip();


    }
}
