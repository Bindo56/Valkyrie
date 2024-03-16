using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapons,
    Armor,
    Amulet,
    Flask
}




[CreateAssetMenu(fileName = "New Item Data ", menuName = "Data/Equipment")]

public class ItemDATAEquipment : ItemData
{
    public EquipmentType equipmentType;


    [Header("Craft requirements")]
    public List<InventoryItem> crafthingMatrials;

    public ItemEffects[] effects;
    public float itemCooldown;

    [Header("Major Stat")]
    public int strenght; 
    public int agility; 
    public int intelligence; 
    public int vitality; 

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower; //default value 150%

    [Header("Defensive stats")]
    public int maxHealth;
    public int armour;
    public int evasion;
    public int magicResistance;

    [Header("Magic Stat")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;



    int DescriptionLenght;

    public void ExcuteItemEffect(Transform _enemyPosition)
    {

        foreach (var item in effects)
        {
            item.ExcuteEffect(_enemyPosition);
        }

    }

    public void AddModifiers()
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        playerStat.strenght.AddModifier(strenght);
        playerStat.agility.AddModifier(agility);
        playerStat.intelligence.AddModifier(intelligence);
        playerStat.vitality.AddModifier(vitality);

        playerStat.damage.AddModifier(damage);
        playerStat.critChance.AddModifier(critChance);
        playerStat.critPower.AddModifier(critPower);

        playerStat.maxHealth.AddModifier(maxHealth);
        playerStat.armour.AddModifier(armour);
        playerStat.evasion.AddModifier(evasion);
        playerStat.magicResistance.AddModifier(magicResistance);

        playerStat.fireDamage.AddModifier(fireDamage);
        playerStat.iceDamage.AddModifier(iceDamage);
        playerStat.lightingDamage.AddModifier(lightingDamage);

    }

    public void RemoveModifiers()
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();
      
        playerStat.strenght.RemoveModifier(strenght);
        playerStat.agility.RemoveModifier(agility);
        playerStat.intelligence.RemoveModifier(intelligence);
        playerStat.vitality.RemoveModifier(vitality);

        playerStat.damage.RemoveModifier(damage);
        playerStat.critChance.RemoveModifier(critChance);
        playerStat.critPower.RemoveModifier(critPower);

        playerStat.maxHealth.RemoveModifier(maxHealth);
        playerStat.armour.RemoveModifier(armour);
        playerStat.evasion.RemoveModifier(evasion);
        playerStat.magicResistance.RemoveModifier(magicResistance);

        playerStat.fireDamage.RemoveModifier(fireDamage);
        playerStat.iceDamage.RemoveModifier(iceDamage);
        playerStat.lightingDamage.RemoveModifier(lightingDamage);

    }

    public override string GetDescription()
    {
        sb.Length = 0;

        AddItemDescription(strenght, "strenght");
        AddItemDescription(agility, "agility");
        AddItemDescription(intelligence, "intelligence");
        AddItemDescription(vitality, "vitality");

        AddItemDescription(damage, "damage");
        AddItemDescription(critChance, "crit.Chance");
        AddItemDescription(critPower, "crit.Power");

        AddItemDescription(maxHealth, "Health");
        AddItemDescription(evasion, "evasion");
        AddItemDescription(armour, "armour");
        AddItemDescription(magicResistance, "magic.Resist");

        AddItemDescription(fireDamage, "FireDamage");
        AddItemDescription(iceDamage, "IceDamage");
        AddItemDescription(lightingDamage, "lighting Dmg");


        if(DescriptionLenght < 5)
        {
            for (int i = 0; i < 5 - DescriptionLenght; i++)
            {
                sb.AppendLine();
                sb.AppendLine("");
            }
        }



        return sb.ToString();

    }

    private void AddItemDescription(int _value, string _name)
    {
        if(_value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }

            if(_value > 0)
            {
                sb.AppendLine("+ "  + _value + " "  + _name);
            }

            DescriptionLenght++;
        }
        


    }





}


