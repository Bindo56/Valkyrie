using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu(fileName = "Buff effects ", menuName = "Data/Effects/Buff Effects")]


public class BuffEffect : ItemEffects
{
    PlayerStat stat;
    [SerializeField] private StatType buffType;
    [SerializeField] int buffAmount; 
    [SerializeField] float buffDuration;



    public override void ExcuteEffect(Transform _enemyPositon)
    {
       
        stat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        stat.IncreaseStatBy(buffAmount, buffDuration, stat.GetStat(buffType));
    }

   



}
