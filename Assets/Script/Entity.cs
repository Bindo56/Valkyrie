 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("CollisionInfo")]
    public Transform attackChecks;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("KnockBack")]
    [SerializeField] protected Vector2 knockBackDir;
    [SerializeField] protected float knockBackDur;
    protected bool isKnocked;


    #region Component
    public Rigidbody2D rb { get; private set; }

    public Animator anim { get; private set; }
    public CapsuleCollider2D cd { get; private set; }

    public EntityFX fx { get; private set; }

    public SpriteRenderer sr { get; private set; }

    public ChracterStats stats { get; private set; }

    #endregion



    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;


    public System.Action OnFlipped;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<ChracterStats>();
        cd = GetComponent<CapsuleCollider2D>();
        

    }

    protected virtual void Update()
    {


    }

    public virtual void SlowEntityBy(float _slowPercentage , float _slowDuration)
    {



    }

    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1f;

    }



    public virtual void DamageEffect()
    {
       
        StartCoroutine("HitKnockback");
        //Destroy(GameObject.
        Debug.Log(gameObject.name + " was damage");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        //Debug.Log(knockBackDir.x + knockBackDir.y);
        rb.velocity = new Vector2(knockBackDir.x * - facingDir , knockBackDir.y);

        yield return new WaitForSeconds(knockBackDur);

        isKnocked = false;
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if(isKnocked) 
            return;
        
        


        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        flipController(_xVelocity);
    }

    #region(LockPlayer)
    public void LockPlayer()
    {
        if (isKnocked) 
            return;
        
        

        rb.velocity = new Vector2(0, 0);
    }

    #endregion

    #region(flip)
    public virtual void flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(OnFlipped != null)
        OnFlipped();
    }

    public virtual void flipController(float _x)
    {
        if (_x > 0 && !facingRight)
            flip();

        else if (_x < 0 && facingRight)
            flip();

    }

    #endregion

    #region(Collision)

    public virtual bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround); //facingDir


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackChecks.position, attackCheckRadius);
    }

    #endregion



    public virtual void Die()
    {

    }
}
