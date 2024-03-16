using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(enemy.attackChecks.position, enemy.attackCheckRadius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStat _target = hit.GetComponent<PlayerStat>();

                enemy.stats.DoDamage(_target);

               // hit.GetComponent<Player>().Damage();
            }
        }

    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

    private void CloseCounterWindow() => enemy.ClosedCounterAttackWindow();
}
