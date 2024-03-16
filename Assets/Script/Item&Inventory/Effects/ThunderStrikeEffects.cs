using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Thunder Strike effects ", menuName = "Data/Effects/Thunder Strike")]

public class ThunderStrikeEffects : ItemEffects
{
   [SerializeField] GameObject ThunderStrikePrefab;

    public override void ExcuteEffect(Transform _enemyPosition)
    {
       GameObject newThunderStrike = Instantiate(ThunderStrikePrefab,_enemyPosition.position,Quaternion.identity);

        Destroy(newThunderStrike,1f);

    }
}
