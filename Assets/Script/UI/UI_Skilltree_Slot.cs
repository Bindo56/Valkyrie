using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Skilltree_Slot : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler
{
    UI ui;
    public int skillPrice;

    [SerializeField] string skillName;
    [TextArea]
    [SerializeField] string skillDescription;
    [SerializeField] Color lockSkillColor;

    public bool unlocked;

    [SerializeField] UI_Skilltree_Slot[] shouldBeUnlocked;
    [SerializeField] UI_Skilltree_Slot[] shouldBeLocked;

    Image skillImage;


    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockedSkillSlot());
        
    }
    private void Start()
    {
        skillImage = GetComponent<Image>();

        skillImage.color = lockSkillColor;

        ui = GetComponentInParent<UI>();


    }

    public void UnlockedSkillSlot()
    {
        if (PlayerManager.instance.HaveEnoughMoney(skillPrice) == false)
            return;


        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].unlocked  == false)
            {
                Debug.Log("Cannot Unlock the skill");
                return;
            }
        }

        for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            if (shouldBeLocked[i].unlocked == true)
            {
                Debug.Log("Cannot Unlock the skill");
               
                return;
            }
        }

        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillTooltip.ShowToolTip(skillDescription , skillName );

        Vector2 mousePos = Input.mousePosition;

        float offsetx = 0;
        float offsety = 0;


        if (mousePos.x < 400)
        {
            offsetx = -130;

        }
        else
            offsetx = 150;

        if (mousePos.y > 220)
        {
            offsety = -150;

        }
        else
            offsety = 150;

        ui.skillTooltip.transform.position = new Vector2(mousePos.x + offsetx , mousePos.y + offsety);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillTooltip.HideToolTip();
    }
}
