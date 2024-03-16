using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemTypeText;
    [SerializeField] TextMeshProUGUI itemDescription;

    [SerializeField] int defaultFontSize = 25;

    private void Start()
    {
       

       // Invoke("Disable", 0.01f);
    }
    public void ShowToolTip(ItemDATAEquipment item)
    {
        if(itemNameText == null)
        {
            return;
        }

        itemNameText.text = item.itemName;
        itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();

        if(itemNameText.text.Length > 9 )
        {
            itemNameText.fontSize = itemNameText.fontSize * .7f;
        }
        else
        {
            itemNameText.fontSize = defaultFontSize;
        }


        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);

    }

   /* private void Disable()
    {
        gameObject.SetActive(false);
    }*/



}
