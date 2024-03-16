using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObject_trigger : MonoBehaviour
{
    ItemObject myitemObject => GetComponentInParent<ItemObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<ChracterStats>().isDead)
                return;


            myitemObject.PickupItem();

        }
    }
}
