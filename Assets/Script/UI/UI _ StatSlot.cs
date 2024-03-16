using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    UI ui;

    [SerializeField] string statName;
    [SerializeField] StatType statType;
    [SerializeField] TextMeshProUGUI statValueText;
    [SerializeField] TextMeshProUGUI statNameText;


    [TextArea]
    [SerializeField] string statDescription;
    private void OnValidate()
    {
        gameObject.name = "Stat  - " + statName;

        if (statNameText != null)
            statNameText.text = statName;

    }


    void Start()
    {
        UpdateStatValueUI();
        ui = GetComponentInParent<UI>();
    }

    // Update is called once per frame
    public void UpdateStatValueUI()
    {
        PlayerStat playerstat = PlayerManager.instance.player.GetComponent<PlayerStat>();
          //  statValueText.text = playerstat.GetStat(statType).GetValue().ToString();

        if (playerstat != null)
        {
            statValueText.text = playerstat.GetStat(statType).GetValue().ToString();

            if(statType == StatType.maxHealth)
            {
                statValueText.text = playerstat.GetMaxHealthValue().ToString();
            }

            if(statType == StatType.damage)
            {
                statValueText.text = (playerstat.damage.GetValue() + playerstat.strenght.GetValue()).ToString();
            }


            if(statType == StatType.critPower)
            {
                statValueText.text = (playerstat.critPower.GetValue() + playerstat.strenght.GetValue()).ToString();
            }

            if(statType == StatType.critChance)
            {
                statValueText.text = (playerstat.critChance.GetValue() + playerstat.agility.GetValue()).ToString();
            }

            if(statType == StatType.evasion)
            {
                statValueText.text = (playerstat.evasion.GetValue() + playerstat.agility.GetValue()).ToString();
            }

            if(statType == StatType.magicResistance)
            {
                statValueText.text = (playerstat.magicResistance.GetValue() + playerstat.intelligence.GetValue() * 5).ToString();
            }


        }
       

    } 

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowToStatToolTip(statDescription);
    }
}
