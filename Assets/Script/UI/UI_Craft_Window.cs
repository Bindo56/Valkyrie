using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Craft_Window : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] Image itemIcon;
    [SerializeField] Button craftButton;


    [SerializeField] Image[] materialImage;




    public void SetupCraftWindow(ItemDATAEquipment _date)
    {

        craftButton.onClick.RemoveAllListeners();


        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;

            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;

        }

        for (int i = 0; i < _date.crafthingMatrials.Count; i++)
        {
            if (_date.crafthingMatrials.Count > materialImage.Length)
                Debug.LogWarning("remove material you have more");

            materialImage[i].sprite = _date.crafthingMatrials[i].data.itemIcon;
            materialImage[i].color = Color.white;

            TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren <TextMeshProUGUI>();

            materialSlotText.text = _date.crafthingMatrials[i].stackSize.ToString();
            materialSlotText.color = Color.white;




        }
        
            itemIcon.sprite = _date.itemIcon;
            itemName.text = _date.itemName;
            itemDescription.text = _date.GetDescription();

        craftButton.onClick.AddListener(() => Inventory.instance.CanCraft(_date,_date.crafthingMatrials));






    }

}
