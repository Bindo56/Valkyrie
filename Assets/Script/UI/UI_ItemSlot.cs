using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UI_ItemSlot : MonoBehaviour , IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
{
  [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;


    public InventoryItem item;

   [SerializeField] protected UI ui;
  //  [SerializeField] UI_ItemToolTip uiTip;


    // Start is called before the first frame update


    protected virtual void Start()
    {  
       


        ui = GetComponentInParent<UI>();
    }


    public void UpdateSLot( InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.itemIcon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
        }
        else
        {
            itemText.text = "";
        }
    }


    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";


    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.instance.RemoveItem(item.data);
            return;
        }


        if (item.data.Type == ItemType.Equipment)
            Inventory.instance.EquipItem(item.data);

        ui.itemToolTip.HideToolTip();   

       /* else if (item.data.Type != ItemType.Equipment)
            return;*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //uiTip.gameObject.SetActive(true);
       

        if(item == null)
        {
            return;
        }
        ui.itemToolTip.ShowToolTip(item.data as ItemDATAEquipment);
       // Debug.Log("Show");
    }

    

    public void OnPointerExit(PointerEventData eventData)
    {
        if(item == null) 
        {
            return;
        }

        ui.itemToolTip.HideToolTip();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
/*        if (item == null)
        {
            return;
        }
        ui.itemToolTip.ShowToolTip(item.data as ItemDATAEquipment);*/
    }
}
