using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : itemDrop
{
    [Header("Player's drop")]
    [SerializeField] float chanceToDrop;
    [SerializeField] float chanceToDropMaterial;
    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;  

        List<InventoryItem> currentStash = inventory.GetStashList();
        List<InventoryItem> currentEquipment = inventory.GetEquipmentList();
        List<InventoryItem> itemToUnequip = new List<InventoryItem>();
        List<InventoryItem> materialToLoose = new List<InventoryItem>();

        foreach (InventoryItem item in currentEquipment)
        {
            if (Random.Range(0, 100) < chanceToDrop)
            {
                DropItem(item.data);
                itemToUnequip.Add(item);
            }



        }

        for (int i = 0; i < itemToUnequip.Count; i++)
        {
            inventory.UnEquipItem(itemToUnequip[i].data as ItemDATAEquipment);
        }

        foreach (InventoryItem item in currentStash)
        {
            if(Random.Range(0, 100) < chanceToDropMaterial)
            {
                DropItem(item.data);
                materialToLoose.Add(item);
            }
        }

        for (int i = 0; i < materialToLoose.Count; i++)
        {
            inventory.RemoveItem(materialToLoose[i].data);


        }



    }


}
