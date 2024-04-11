using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("CloneInfo")]
    [SerializeField] GameObject clonePrefabs;
    [SerializeField] float cloneDuration;
    [SerializeField] bool canAttack;


   /* [SerializeField] bool createCloneOnDashStart;
    [SerializeField] bool createClineOffDashOver;*/
    [SerializeField] bool canDuplicateClone;
    [SerializeField] bool createCloneOnCounterAttack;
    [SerializeField] float chanceToCreateClone; //chanceToDuplicateMoreThen2clone

    [Header("CreateCrystalInsteadOfClone")]
   public bool crystalInsteadOfClone;



    public void CreatClone( Transform _clonePosition,Vector3 _offset)
    {
        if(crystalInsteadOfClone)
        {
            SkillManager.instance.crystal_Skill.CreateCrystal();
            //SkillManager.instance.crystal_Skill.CurrentCrystalChooseRandomTarget();
            return;
        }

        GameObject newClone = Instantiate(clonePrefabs);

        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition,cloneDuration,canAttack, _offset , FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToCreateClone,player);
    }

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if(createCloneOnCounterAttack)
        {
            StartCoroutine(createCloneWithDelay(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
        }
    }

    private IEnumerator createCloneWithDelay(Transform _enemyTransform , Vector3 _offset)
    {
        yield return new WaitForSeconds(.4f);
            CreatClone(_enemyTransform, _offset);

    }





}
