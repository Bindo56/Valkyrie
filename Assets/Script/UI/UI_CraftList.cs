using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] Transform craftSlotParent;
    [SerializeField] GameObject craftSlotPrefab;


    [SerializeField] List <ItemDATAEquipment> craftEquipment;
    [SerializeField] List<CraftUI> craftSlot;    
    // Start is called before the first frame update
    void Start()
    {
        AssignCraftSlot();
    }


    private void AssignCraftSlot()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
            craftSlot.Add(craftSlotParent.GetChild(i).GetComponent<CraftUI>());



        }
    }


    public void SetupCraftList()
    {
        for (int i = 0; i < craftSlot.Count; i++)
        {
            Destroy(craftSlot[i].gameObject);

        }

        craftSlot = new List<CraftUI>();

        for (int i = 0; i < craftEquipment.Count; i++)
        {

            GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
            newSlot.GetComponent<CraftUI>().SetupCraftSlot(craftEquipment[i]);
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
       SetupCraftList();
    }
}
