using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : ChracterStats 
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

      
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    protected override void DecreasesHealthBy(int _damage)
    {
        base.DecreasesHealthBy(_damage);

        ItemDATAEquipment currentArmour = Inventory.instance.GetEquipment(EquipmentType.Armor);
        
        if(currentArmour != null )
        {
            currentArmour.ExcuteItemEffect(player.transform);
        }

    }
}
