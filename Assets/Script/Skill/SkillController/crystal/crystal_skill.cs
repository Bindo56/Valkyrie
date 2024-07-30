using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crystal_skill : Skill
{
    [SerializeField] GameObject crystalPrefab;
    GameObject currentCrystal;
    [SerializeField] float crystalDur;


    [Header("Explosive Crystal")]
    [SerializeField] UI_Skilltree_Slot explosiveCrystalUnlockBtn;
    [SerializeField]bool canExplode;


    [Header("Crystal mirage")]
    [SerializeField] UI_Skilltree_Slot unlockCloneInsteadBtn;
    [SerializeField] bool createCloneInsteadOfCrystal;

    [Header("Crystal Simple")]
    [SerializeField] UI_Skilltree_Slot unlockCrystalBtn;
    public bool crystalUnlocked { get; private set; }




    [Header("Moving Crystal")]

    [SerializeField] UI_Skilltree_Slot movingCrystalUnlockBtn;
   [SerializeField] bool canMove;
   [SerializeField] float moveSpeed;

    [Header("multi crystal")]
    [SerializeField] UI_Skilltree_Slot multiCrystalUnlockBtn;
    [SerializeField] bool canUseMultiCrystal; 
    [SerializeField] int amountOfGem;
    [SerializeField] float coolDowngem;
    [SerializeField] float useTimeWindow;
    [SerializeField] List<GameObject> crystalleft = new List<GameObject>();


    protected override void Start()
    {
        base.Start();


        unlockCrystalBtn.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
        movingCrystalUnlockBtn.GetComponent<Button>().onClick.AddListener(MovingCry);
        multiCrystalUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockMulti);
        explosiveCrystalUnlockBtn.GetComponent<Button>().onClick.AddListener(explosiveCrystal);
        unlockCloneInsteadBtn.GetComponent<Button>().onClick.AddListener(CloneCrystal);



    }

    #region unlockBtn
    private void UnlockCrystal()
    {
        if(unlockCrystalBtn.unlocked)
        {
            crystalUnlocked = true;
        }
    }


    private void UnlockMulti()
    {
        if(multiCrystalUnlockBtn.unlocked)
        {
            canUseMultiCrystal = true;


        }
    }


    private void MovingCry()
    {
        if (movingCrystalUnlockBtn.unlocked)
        {
            canMove = true;
        }
    }

    private void explosiveCrystal()
    {
        if (explosiveCrystalUnlockBtn.unlocked)
        {
          canExplode = true;

        }

    }

    private void CloneCrystal()
    {
        if(unlockCloneInsteadBtn.unlocked)
        {
            createCloneInsteadOfCrystal = true;
        }
    }

    #endregion
    public override void UseSkill()
    {
        base.UseSkill();

       if(CanUSeMultiCrystal())
        return;

        if(currentCrystal == null )
        {
            CreateCrystal();
        }
        else
        {
            if(canMove)
            {
                return;
            }

            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if(createCloneInsteadOfCrystal)
            {
                SkillManager.instance.clone.CreatClone(currentCrystal.transform,  Vector3.zero);
                Destroy(currentCrystal);

            }
            else
            {
                
              currentCrystal.GetComponent<crystal_skill_controller>()?.crystalexplode();
            }


        } 
    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        crystal_skill_controller currentcrystalscript = currentCrystal.GetComponent<crystal_skill_controller>();

        currentcrystalscript.SetupCrystal(crystalDur, canExplode, canMove, moveSpeed, FindClosestEnemy(currentCrystal.transform), player);
    }

    public void CurrentCrystalChooseRandomTarget() => currentCrystal.GetComponent<crystal_skill_controller>().ChooseRandomEnemy();

    private bool CanUSeMultiCrystal()
    {
        if(canUseMultiCrystal)  
        {
            if(crystalleft.Count > 0)
            {
                if (crystalleft.Count == amountOfGem)
                    Invoke("ResetAbility", useTimeWindow);

                cooldown = 0;   
              GameObject crystalToSpawn = crystalleft[crystalleft.Count - 1];
              GameObject newCrystal = Instantiate(crystalToSpawn,player.transform.position,Quaternion.identity);

              crystalleft.Remove(crystalToSpawn);

                newCrystal.GetComponent<crystal_skill_controller>().SetupCrystal(crystalDur, canExplode,canMove,moveSpeed, FindClosestEnemy(newCrystal.transform),player);

               if(crystalleft.Count <= 0) 
               {
                    cooldown = coolDowngem;
                    RefillCrystal();
                
               }

              return true;

            }

        }
        return false;
    }


    private void RefillCrystal()
    {
        int amountToAdd = amountOfGem - crystalleft.Count;

        for (int i = 0; i < amountToAdd; i++)
        {
            crystalleft.Add(crystalPrefab);
        }
    }

    private void ResetAbility()
    {
        if(cooldownTimer > 0 )
        {
            return;
        }
          RefillCrystal();
           cooldownTimer = coolDowngem;

    }


}
