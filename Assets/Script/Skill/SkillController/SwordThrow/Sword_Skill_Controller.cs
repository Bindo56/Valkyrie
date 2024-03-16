
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    CircleCollider2D cd;
    Player player;

    private bool canRotate = true;
    private bool isReturning;


     float returnSpeed = 12f;
    private float freezeTimeDuration;

    [Header("Pierce")]
    [SerializeField] float pierceAmount;


    [Header("Bounce")]
      float bounceSpeed;
     int amountOfBounce = 4;
     bool isBouncing;
     List<Transform>  enemyTarget;
    int targetIndex;

    [Header("Spin")]
    float maxTravelDistance;
    float spinDuration;
    float spinTimer;
    bool wasStopped;
    bool isSpinning;
    float hitTimer;
    float hitCooldown;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player, float _freezeTimeDuration , float _returnSpeed)
    {
        freezeTimeDuration = _freezeTimeDuration;
        returnSpeed = _returnSpeed;
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;


        anim.SetBool("spin", true);

        Invoke("Destroy", 7);
    }
     
    public void SetupBounce(bool _isBouncing , int _amountOfBounces, float _bounceSpeed) 
    { 
        bounceSpeed = _bounceSpeed;
      isBouncing = _isBouncing;
        amountOfBounce = _amountOfBounces;

        enemyTarget = new List<Transform>();
    }
     
    public void SetupPierce(int _amountOfPierce)
    {
        pierceAmount = _amountOfPierce;
    }

    public void SetUpSpin(bool _isSpinning, float _maxTravelDistance,float _spinDuration , float _hitCooldown)
    {
        spinDuration = _spinDuration;
        isSpinning = _isSpinning;
        maxTravelDistance = _maxTravelDistance;
        hitCooldown = _hitCooldown;
    }



    public void ReturnSword()
    {
        //rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchTheSword();
        }

        BounceLogic();
        SpinLogic();

    }

    private void SpinLogic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();
            }

            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;

                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }

                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;
                    Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in collider)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                           SwordSkillDamage(hit.GetComponent<Enemy>());
                    }
                }

            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                SwordSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());
               // enemyTarget[targetIndex].GetComponent<Enemy>().Damage();

                targetIndex++;
                amountOfBounce--;

                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        if(collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            SwordSkillDamage(enemy);
        }


        SetupTargetBounce(collision);

        SwordStuck(collision);
    }

    private void SwordSkillDamage(Enemy enemy)
    {
        player.stats.DoDamage(enemy.GetComponent<ChracterStats>());
        enemy.StartCoroutine("FreezeTime", freezeTimeDuration);


        ItemDATAEquipment equipedAmulet = Inventory.instance.GetEquipment(EquipmentType.Amulet);


        if (equipedAmulet != null)
        {
            equipedAmulet.ExcuteItemEffect(enemy.transform);
        }
    }

    private void SetupTargetBounce(Collider2D collision)
    {
        //collision.GetComponent<Enemy>() ! = null);

        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in collider)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTarget.Add(hit.transform);
                }

            }
        }
    }

    private void SwordStuck(Collider2D collision)
    {

        if(pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--; 
            return;
        }

        if (isSpinning)
        {
            StopWhenSpinning();
            return;
        }

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if(isBouncing && enemyTarget.Count > 0)
        {
            return;
        }
        anim.SetBool("spin", false);
        transform.parent = collision.transform;
    }
}
