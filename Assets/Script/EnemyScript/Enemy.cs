using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
   [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stunned")]
    public float stunDur;
    public Vector2 stunDir;
    [SerializeField] public GameObject CounterStunnedImage;
    protected bool canBeStunned;


    [Header("MoveInfo")]
    public float moveSpeed;
    public float idleTime;
    public float defaultSpeed;
    public float battleTime;


    [Header("Attack Info")]
    public float PlayerDetectionDistance;
    public float attackDistance;
    public float attackCoolDown;
   [HideInInspector] public float lastTimeAttack;



    public EnemyStateMachine stateMachine { get; private set; }

    public string lastAnimBoolName { get; private set; }


    protected override void Awake()
    {

        base.Awake();
        stateMachine = new EnemyStateMachine();
       CounterStunnedImage.SetActive(false);
        defaultSpeed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();



       // Debug.Log(IsPlayerDetected() + "Playerseen");
    }

    public virtual void AssignBoolName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        base.SlowEntityBy(_slowPercentage, _slowDuration);

        moveSpeed = moveSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
        
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultSpeed;
        anim.speed = 1f;



    }

    public virtual void FreezeTime(bool _timeFrozen)
    {

        if (_timeFrozen)
        {
          anim.speed = 0;
          moveSpeed = 0;

        }
        else
        {
            anim.speed = 1;
            moveSpeed = defaultSpeed;
        }

    }

    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTime(_duration));

    public virtual IEnumerator FreezeTime(float _sec)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_sec);

        FreezeTime(false);

    }


    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        CounterStunnedImage.SetActive(true);
        
    }



    public virtual void ClosedCounterAttackWindow()
    {

        canBeStunned = false;
        CounterStunnedImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            ClosedCounterAttackWindow();
            return true;
        }

        return false;


    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, PlayerDetectionDistance, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

}
