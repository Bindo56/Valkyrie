using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkill : Skill
{
    [SerializeField] int amountOfAttack;
    [SerializeField] float cloneCooldown;
    [SerializeField] float blackholeDur;
    [Space]
    [SerializeField] GameObject blackholeprefab;
    [SerializeField] float maxSize;
    [SerializeField] float growSpeed;
    [SerializeField] float shrinkSpeed;
 
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newblackhole = Instantiate(blackholeprefab,player.transform.position,Quaternion.identity);

        Blackhole_skill_controller newBlackHoleScript = newblackhole.GetComponent<Blackhole_skill_controller>();

        newBlackHoleScript.SetupBlackhole(maxSize, growSpeed,shrinkSpeed,amountOfAttack,cloneCooldown,blackholeDur); 
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public float GetBlackholeRadius()
    {
        return maxSize / 2;
    }
}
