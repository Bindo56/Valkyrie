using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;
    Player player;

    [SerializeField] float colorLosingSpeed;
    float cloneTimer;
   [SerializeField] Transform attackChecks;
    float attackCheckRadius = .8f;

    Transform closestEnemy;
    bool canDuplicateClone;
    float chanceToCreateClone; //chanceToDuplicateMoreThen2clone

    float facingDir = 1;




    

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if(cloneTimer < 0)
            sr.color = new Color(1,1,1,sr.color.a -(Time.deltaTime * colorLosingSpeed));

        if (sr.color.a <= 0)
            Destroy(gameObject);
    }

    public void SetupClone(Transform _newTransform, float _cloneDuration,bool _canAttack,Vector3 _offset,Transform _closestEnemy,bool _canDuplicateClone,float _chanceToCreateClone,Player _player)
    {
        if(_canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }

        player = _player;
        transform.position = _newTransform.position + _offset;
        cloneTimer = _cloneDuration;
        closestEnemy = _closestEnemy;
        canDuplicateClone = _canDuplicateClone;
        chanceToCreateClone = _chanceToCreateClone;

        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer = .1f;

    }

    private void AttackTrigger()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackChecks.position, attackCheckRadius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                player.stats.DoDamage(hit.GetComponent<ChracterStats>());

                if (canDuplicateClone)
                {

                    if(Random.Range(0,100 ) < chanceToCreateClone)
                    {
                      SkillManager.instance.clone.CreatClone(hit.transform, new Vector2(1 * facingDir, 0));

                    }
                }
            }
        }

    }

    private void FaceClosestTarget()
    {
/*        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;
        
        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }*/
        if(closestEnemy !=null)
        {
            if(transform.position.x > closestEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }

    }

}
