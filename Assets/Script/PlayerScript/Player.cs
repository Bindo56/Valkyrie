using System.Collections;
using UnityEditor;
using UnityEngine;

public class Player : Entity
{

    public bool isBusy {  get; private set; }
    public Vector2[] attackMovement;
    public float counterAttackDur = .2f;


    [Header("MoveInfo")]
    public float moveSpeed = 8f;
    public float jumpForce;
    public float swordImpact;
    public float deaultMoveSpeed;
    public float deaultJumpSpeed;




    [Header("DashInfo")]   
    public float dashSpeed;
    public float defaultDashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; }

   
    public SkillManager skill {  get; private set; }


    public GameObject sword { get; private set; }

    public PlayerStateMachine stateMachine {  get; private set; }
    public  PlayerIdle idleState { get; private set; }
    public PlayerMove moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerWallSlide wallSlide { get; private set; }
    
    public PlayerPrimaryAttackState  PrimayAttack { get; private set; }
    public PlayerCounterAttackState CounterAttack { get; private set; }
    public PlayerAimSwordState AimSwordState { get; private set; }
    public PlayerCatchSwordState CatchSwordState { get; private set; }

    public PlayerBlackhole_state blackholeState { get; private set; }
    public PlayerDeadState deadState { get; private set; }



    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdle(this,stateMachine,"Idle");
        moveState = new PlayerMove(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlide(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

        PrimayAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        CounterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        AimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        CatchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackholeState = new PlayerBlackhole_state(this, stateMachine, "Jump");
        blackholeState = new PlayerBlackhole_state(this, stateMachine, "Jump");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
          
        
        
    }

    protected override void Start()
    {
       base.Start();

        skill = SkillManager.instance;

        deaultMoveSpeed = moveSpeed;
        deaultJumpSpeed = jumpForce;
        defaultDashSpeed = dashSpeed;
        anim.speed = 1;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        dashInputCheck();

        if(Input.GetKeyDown(KeyCode.G)) 
        {
            skill.crystal_Skill.CanUseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventory.instance.UseFlask();
        }
        
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 -  _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
        
        
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = deaultMoveSpeed;
        jumpForce = deaultJumpSpeed;
        dashSpeed = defaultDashSpeed;
        anim.speed = 1.1f;


    }


    public void ExitBlackHole()
    {
        stateMachine.ChangeState(airState);
    }

        

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;

    }
       
    

    public void CatchTheSword()
    {
        stateMachine.ChangeState(CatchSwordState);
        Destroy(sword);
    }


    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
       // Debug.Log("Busy");
        yield return new WaitForSeconds(_seconds);
        //Debug.Log("NotBusy");
        isBusy = false;

    }


    private void dashInputCheck()
    {
        if(isWallDetected())
        {
            return;
        }

        if(skill.dash.dashUnlocked == false)
        {
            return;
        }

       

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}

   



