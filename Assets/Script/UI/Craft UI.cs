using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftUI : UI_ItemSlot
{

    protected override void Start()
    {
        base.Start();


    }

 public void SetupCraftSlot(ItemDATAEquipment _data)
    {

        if (_data == null)
        {
            return;
        }

        item.data = _data;

        itemImage.sprite = _data.icon;
        itemText.text = _data.itemName;


    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemDATAEquipment craftData = item.data as ItemDATAEquipment;

        Inventory.instance.CanCraft(craftData, craftData.crafthingMatrials);
    }
}
