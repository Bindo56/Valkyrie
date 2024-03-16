using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : ChracterStats
{
    private Enemy Enemy;
    [SerializeField] itemDrop myDropSystem;

    [Header("LevelDetails")]
    [SerializeField] int level = 1;

    [Range(0f,1f)]
    [SerializeField] float percentageModifier = .4f; 
    protected override void Start()
    {
        ApplyLevelModifier();

        base.Start();
        Enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<itemDrop>();
    }

    private void ApplyLevelModifier()
    {
       /* Modify(strenght);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);*/

        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(maxHealth);
        Modify(armour);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);

    }

    private void Modify(stats stat)
    {
        for (int i = 1; i < level; i++)
        {

            float modifier = stat.GetValue() * percentageModifier;

            stat.AddModifier(Mathf.RoundToInt(modifier));
        }

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

       
    }

    protected override void Die()
    {
        base.Die();
        Enemy.Die();

        myDropSystem.GenerateDrop();
    }
}
