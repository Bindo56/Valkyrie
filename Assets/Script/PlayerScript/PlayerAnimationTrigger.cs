using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{

    private Player player => GetComponentInParent<Player>();


    private void AnimationTrigger() 
    {
        player.AnimationTrigger();

    }

    private void AttackTrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(player.attackChecks.position, player.attackCheckRadius);

        foreach(var hit in collider)
        {
            if(hit.GetComponent<Enemy>() != null) 
            {
                //hit.GetComponent<Enemy>().Damage();

                EnemyStat _target = hit.GetComponent<EnemyStat>();


                if(_target != null)
                {
                  player.stats.DoDamage(_target);
                }


                ItemDATAEquipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapons);

                if(weaponData != null)
                {
                    weaponData.ExcuteItemEffect(_target.transform); 
                }


                /*hit.GetComponent<ChracterStats>().TakeDamage(player.stats.damage.GetValue());

                Debug.Log(player.stats.damage.GetValue());*/
            }
        }

    }

    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
   
}
