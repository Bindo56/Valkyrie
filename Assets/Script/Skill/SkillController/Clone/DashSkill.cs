using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill

{
    [Header("Dash")]
    public bool dashUnlocked;
    [SerializeField] UI_Skilltree_Slot dashUnlockBtn;

    [Header("Clone On Dash")]
    public bool cloneOnDashUnlocked;
    [SerializeField] UI_Skilltree_Slot cloneOnDashUnlockBtn;

    [Header("Clone on arrival")]
    public bool cloneOnArrivalUnlocked;
    [SerializeField] UI_Skilltree_Slot cloneOnArrivalUnlockBtn;

    public override void UseSkill()
    {
        base.UseSkill();

        Debug.Log("Crete Clone");
    }

 

    protected override void Start()
    {
        base.Start();
        dashUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockDash);
        cloneOnDashUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        cloneOnArrivalUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);

    }

    private void UnlockDash()
    {
        Debug.Log("Attemp to Unlock");

        if(dashUnlockBtn.unlocked)
        {

          Debug.Log("Unlock");
           dashUnlocked = true;

        }
    }

    private void UnlockCloneOnDash()
    {
        if(cloneOnDashUnlockBtn.unlocked)
        {
         cloneOnDashUnlocked = true;

        }
    }

    private void UnlockCloneOnArrival()
    {
        if(cloneOnArrivalUnlockBtn.unlocked)
        {
          cloneOnArrivalUnlocked = true;

        }
    }
    public void CloneOnDash()
    {
        if (cloneOnDashUnlocked)
        {
           SkillManager.instance.clone.CreatClone(player.transform, Vector3.zero);
        }

    }

    public void CloneOnArrival()
    {

        if (cloneOnArrivalUnlocked)
        {
            SkillManager.instance.clone.CreatClone(player.transform, Vector3.zero);
        }
    }
}
