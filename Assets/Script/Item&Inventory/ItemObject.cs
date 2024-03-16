using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] Vector2 velocity;
    [SerializeField] ItemData itemData;

  

    private void SetVisualitem()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object " + itemData.itemName;
    }

    public void SetupItem(ItemData _itemData , Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;
        SetVisualitem();

    }

   

   

    public void PickupItem()
    {
        if(!Inventory.instance.CanAddItem() && itemData.Type == ItemType.Equipment) //  for not picking up the drop after inventory is full 
        {
            rb.velocity = new Vector2(0, 7);
            return;
        }

        Inventory.instance.Additem(itemData);
        Destroy(gameObject);
    }
}
