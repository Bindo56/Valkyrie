using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Enemy Freeze effects ", menuName = "Data/Effects/Freeze Enemy Effects")]

public class FreezeEnemyEffects : ItemEffects
{
    [SerializeField] float duration;


    public override void ExcuteEffect(Transform _transform)
    {
        PlayerStat playerstat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        if (playerstat.currentHealth > playerstat.GetMaxHealthValue() * .1f)
            return;


        if(!Inventory.instance.CanUseArmour()) 
            return;


        Collider2D[] collider = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach (var hit in collider)
        {
            hit.GetComponent<Enemy>()?.FreezeTimeFor(duration);
            
        }

    }


}
