using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Heal effects ", menuName = "Data/Effects/Heal Effects")]
public class HealEffects : ItemEffects
{
    [Range(0f, 1f)]
    [SerializeField] float healpercent;


    public override void ExcuteEffect(Transform _enemyPositon)
    {
            PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        int HealAmount = Mathf.RoundToInt(playerStat.GetMaxHealthValue() * healpercent);

        playerStat.IncreaseHealthBy(HealAmount);


    }




}
