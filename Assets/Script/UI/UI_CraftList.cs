using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] Transform craftSlotParent;
    [SerializeField] GameObject craftSlotPrefab;


    [SerializeField] List <ItemDATAEquipment> craftEquipment;
      
    // Start is called before the first frame update
    void Start()
    {

        transform.parent.GetChild(0).GetComponent<UI_CraftList>().SetupCraftList();
        SetupDefaultCraftWindow();
    }

    public void SetupCraftList()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
          Destroy(craftSlotParent.GetChild(i).gameObject);

        }

      //  craftSlot = new List<CraftUI>();

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

    public void SetupDefaultCraftWindow()
    {
        if (craftEquipment[0] != null)
        {
            GetComponentInParent<UI>().craftWindow.SetupCraftWindow(craftEquipment[0]);
        }
    }

}
