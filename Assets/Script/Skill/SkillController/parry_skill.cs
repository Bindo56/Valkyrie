using UnityEngine;
using UnityEngine.UI;

public class parry_skill : Skill
{
    [Header("Parry")]
    [SerializeField] UI_Skilltree_Slot parryUnlockBtn;
    public bool parryUnlocked;


    [Header("parry restore")]
    [SerializeField] UI_Skilltree_Slot restoreUnlockBtn;
    [Range(0f, 1f)]
    [SerializeField] float retoreHealthPer;
    public bool restoreUnlocked;

    [Header("Parry with mirage")]
    [SerializeField] UI_Skilltree_Slot parryWithMirageUnlockBtn;
    public bool parryWithMirageUnlocked;

    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * retoreHealthPer);

            player.stats.IncreaseHealthBy(restoreAmount);
        }

    }

    protected override void Start()
    {
        base.Start();

        parryUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);

    }



    private void UnlockParry()
    {
        if (parryUnlockBtn.unlocked)
        {
            parryUnlocked = true;
        }
    }

    private void UnlockParryRestore()
    {
        if (restoreUnlockBtn.unlocked)
        {
            restoreUnlocked = true;
        }
    }

    private void UnlockParryWithMirage()
    {
        if (parryWithMirageUnlockBtn.unlocked)
        {
            parryWithMirageUnlocked = true;
        }
    }

    public void MakeMirageParry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked)
        {
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
        }


    }


}
