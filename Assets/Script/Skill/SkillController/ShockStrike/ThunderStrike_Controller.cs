using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderStrike_Controller : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();
            EnemyStat enemyTarget =  collision.GetComponent<EnemyStat>();
            playerStat.DoMagicDamage(enemyTarget);

        }



    }
}
