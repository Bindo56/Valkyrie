using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " Ice And Fire Effect ", menuName = "Data/Effects/Ice And Fire Effect")]


public class IceAndFireEffect : ItemEffects
{
    [SerializeField] GameObject iceAndFirePrefab;
    [SerializeField] float xVelocity;

    public override void ExcuteEffect(Transform _respawnPosition)
    {
        Player player = PlayerManager.instance.player;

        bool thirdAttack = player.PrimayAttack.comboCounter == 2;

        if(thirdAttack)
        {
            GameObject newIceAndfire = Instantiate(iceAndFirePrefab, _respawnPosition.position,player.transform.rotation);

            newIceAndfire.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDir, 0);
            Destroy(newIceAndfire,5f);
        }


    }



}
