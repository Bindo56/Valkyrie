using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal_skill_controller : MonoBehaviour
{
    [SerializeField] Animator anim;
    CircleCollider2D cd;

    float crystalTimer;
    Player player;


    bool canExplode;
    bool canMove;
    float moveSpeed;
    bool canGrow;
    float growSpeed = 4.5f;

    Transform closestEnemy;
    [SerializeField] LayerMask whatIsEnemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupCrystal(float _crystalDur,bool _explode,bool _canMove,float _moveSpeed, Transform _closestEnemy,Player _player)
    {
        crystalTimer = _crystalDur;
        player = _player;
        canExplode = _explode;
        canMove = _canMove;
        moveSpeed = _moveSpeed;
        closestEnemy = _closestEnemy;
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManager.instance.blackhole.GetBlackholeRadius();

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, radius,whatIsEnemy);


        if(collider.Length > 0)
            closestEnemy = collider[Random.Range(0,collider.Length)].transform;

    }


    private void Update()
    {
        crystalTimer -= Time.deltaTime;

        if (crystalTimer < 0)
        { 
            crystalexplode();
           // selfDestroy();

        }
        if(canMove)
        {
            transform.position =  Vector2.MoveTowards(transform.position,closestEnemy.position,moveSpeed * Time.deltaTime);

            if(Vector2.Distance (transform.position,closestEnemy.position) < 1)
            {
                crystalexplode();
                canMove = false;
            }
        }
        if(canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale , new Vector2(3,3),growSpeed * Time.deltaTime);
        }

         
    }

    private void AnimationExplode()
    {

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
               player.stats.DoMagicDamage(hit.GetComponent<ChracterStats>());

                ItemDATAEquipment equipedAmulet = Inventory.instance.GetEquipment(EquipmentType.Amulet);    


                if(equipedAmulet != null)
                {
                    equipedAmulet.ExcuteItemEffect(hit.transform);
                }


            }
        }
    }

    public void crystalexplode()
    {
        if (canExplode)
        {
            canGrow = true;
            // Debug.Log("anim");
            anim.SetTrigger("Explode");
            Invoke("selfDestroy", .5f);
        }
       // else
    }

    private void selfDestroy()
    {
          //  anim.SetTrigger("Explode");
        Destroy(gameObject);
    }
}
