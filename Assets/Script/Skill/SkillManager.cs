using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;


    public DashSkill dash {  get; private set; }
    public Clone_Skill clone { get; private set; }  

    public SwordSkill sword { get; private set; }
    public BlackholeSkill blackhole { get; private set; }
    public crystal_skill crystal_Skill { get; private set; }

    public parry_skill Parry { get; private set; }


    public void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
          instance = this; 
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<Clone_Skill>();
        sword = GetComponent<SwordSkill>();
        blackhole = GetComponent<BlackholeSkill>();
        crystal_Skill = GetComponent<crystal_skill>();
        Parry = GetComponent<parry_skill>();

    }
}
