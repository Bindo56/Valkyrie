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

        itemImage.sprite = _data.itemIcon;
        itemText.text = _data.itemName;

        if (itemText.text.Length < 7)
        {
            itemText.fontSize = itemText.fontSize * .9f;
        }
        else
            itemText.fontSize = 10;


    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemDATAEquipment);
    }
}
