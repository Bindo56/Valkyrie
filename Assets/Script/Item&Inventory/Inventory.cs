    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemData> startingEquipment;

    public List<InventoryItem> equipment;
    public Dictionary<ItemDATAEquipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary <ItemData, InventoryItem> stashDictionary;


    [Header("Inventory UI")]
    [SerializeField] Transform inventorySlotParent;
    [SerializeField] Transform stashSlotParent;
    [SerializeField] Transform equipmentSlotParent;
    [SerializeField] Transform statSlotParent;

    UI_ItemSlot[] inventoryItemSlot;
    UI_ItemSlot[] stashItemSlot;
    UI_EquipmentSlot[] equipmentSlot;
  [SerializeField ]  UI_StatSlot[] statSlot;


    float lastTimeUseFlask;
    float lastTimeUeArmour;

    float flaskCooldown;
    float armourCooldown;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemDATAEquipment, InventoryItem>();


        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();


        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>(); 

        startingItem();

    }

    private void startingItem()
    {
        for (int i = 0; i < startingEquipment.Count; i++)
        {
            Additem(startingEquipment[i]);
        }
    }
        
    public void EquipItem( ItemData _item)
    {
        ItemDATAEquipment newEquipment = _item as ItemDATAEquipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemDATAEquipment oldEquipment = null;

        foreach (KeyValuePair<ItemDATAEquipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = item.Key;
        }

        if(oldEquipment != null)
        {

          UnEquipItem(oldEquipment);
            Additem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();
        RemoveItem(_item);
        UpdateSlotUI();
        


    }

    public void UnEquipItem(ItemDATAEquipment itemToDelete)
    {
        if (equipmentDictionary.TryGetValue(itemToDelete, out InventoryItem value))
        {
            equipment.Remove(value);
            itemToDelete.RemoveModifiers();
            equipmentDictionary.Remove(itemToDelete);

        }
    }

    private void UpdateSlotUI()
    {
/*        if(equipmentSlot == null) //added 
        {
            return;
        }*/

        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemDATAEquipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].slotType)
                    equipmentSlot[i].UpdateSLot(item.Value);
                    
            }


        }


        for(int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();

        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }




        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSLot(inventory[i]);    
        }

        for (int i = 0;i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSLot(stash[i]);
        }

        for (int i = 0; i < statSlot.Length; i++) // update ui 
        {
            statSlot[i].UpdateStatValueUI();
        }

    }

    public void Additem(ItemData _item)
    {
        if (_item.Type == ItemType.Equipment && CanAddItem())
        {
          AddToInventory(_item);

        }
        else if (_item.Type == ItemType.Matrial)
        {
            AddToStash(_item);

        }





        UpdateSlotUI();

    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if(inventoryDictionary.TryGetValue(_item,out InventoryItem value))
        {
            if(value.stackSize <=1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else 
                value.RemoveStack();

        }
        else if (stashDictionary.TryGetValue(_item,out InventoryItem stashValue))
        {
            if(stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else 
                stashValue.RemoveStack();

        }

        UpdateSlotUI();

    }


    public bool CanCraft(ItemDATAEquipment _itemToCraft , List<InventoryItem> _requiredMaterials)
    {

        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(_requiredMaterials[i].data,out InventoryItem stashValue))
            {
                if(stashValue.stackSize < _requiredMaterials[i].stackSize)
                {
                    Debug.Log("not enough matrial");
                    return false;
                }
                else
                {
                    materialsToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("not enough matrials");
                return false;
            }


        }

        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            RemoveItem(materialsToRemove[i].data);
        }

        Additem(_itemToCraft);
        Debug.Log("Here is Your item" + _itemToCraft.name);
        return true;

    }

    public bool CanAddItem()
    {
        if(inventory.Count >= inventoryItemSlot.Length)
        {
            Debug.Log("No More Space");
            return false;
        }
        return true;

    }

    public List<InventoryItem> GetEquipmentList() => equipment;

    public List<InventoryItem> GetStashList() => stash;

    public ItemDATAEquipment GetEquipment(EquipmentType _type)
    {
        ItemDATAEquipment equipedItem = null;

        foreach (KeyValuePair<ItemDATAEquipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
                equipedItem = item.Key;
        }

        return equipedItem;

    }

    public void UseFlask()
    {
        ItemDATAEquipment currentFlask = GetEquipment(EquipmentType.Flask);

        if(currentFlask == null)
        {
            return;
        }

        bool canUseFlask = Time.time > lastTimeUseFlask + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = currentFlask.itemCooldown;
            currentFlask.ExcuteItemEffect(null);
            lastTimeUseFlask = Time.time;

        }
        else
            Debug.Log("flaskisoncolldown");

    }

    public bool CanUseArmour()
    {
        ItemDATAEquipment currentArmour = GetEquipment(EquipmentType.Armor);

        if(Time.time < lastTimeUeArmour + armourCooldown)
        {
            armourCooldown = currentArmour.itemCooldown;
            lastTimeUeArmour = Time.time;
            return true;
        }
        Debug.Log("Armour on CoolDown");
            return false;


    }


}
