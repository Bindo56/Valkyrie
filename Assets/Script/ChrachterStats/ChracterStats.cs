
using System.Collections;
using UnityEngine;

public enum StatType
{
    strenght, //1 point increase damage by 1 and crit.power by 1
    agility,//1 point increase damage by 1 and crit.chance by 1
    intelligence, //1 point increase magic damage by 1 and magic resistance by 3
    vitality, // 1 point increase health by 3 or 5 point

    damage,
    critChance,
    critPower, //default value 150%


    maxHealth,
    armour,
    evasion,
    magicResistance,

    fireDamage,
    iceDamage,
    lightingDamage


}

public class ChracterStats : MonoBehaviour
{
    EntityFX FX;


    [Header("Major Stat")]
    public stats strenght;  //1 point increase damage by 1 and crit.power by 1
    public stats agility; //1 point increase damage by 1 and crit.chance by 1
    public stats intelligence; //1 point increase magic damage by 1 and magic resistance by 3
    public stats vitality; // 1 point increase health by 3 or 5 point

    [Header("Offensive stats")]
    public stats damage;
    public stats critChance;
    public stats critPower; //default value 150%

    [Header("Defensive stats")]
    public stats maxHealth;
    public stats armour;
    public stats evasion;
    public stats magicResistance;

    [Header("Magic Stat")]
    public stats fireDamage;
    public stats iceDamage;
    public stats lightingDamage;

    public bool isIgnited; //does damage over time
    public bool isChilled; //reduce armour by 20%
    public bool isShocked; //reduce accuacy 20%

    float iginitedTimer;
    float chilledTimer;
    float shockedTimer;



    
    float iginiteDamageCooldown;
    float iginiteDamageTimer;
    int igniteDamage;

    int shockDamage;
    [SerializeField] GameObject shockStrikePrefab;


    public  int currentHealth;

    public System.Action onHealthChanged;
    public bool isDead {  get; private set; }

  


    // Start is called before the first frame update
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();

        FX = GetComponent<EntityFX>();

        //damage.AddModifier(4);
    }

    protected virtual void Update()
    {
        iginitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;


        iginiteDamageTimer -= Time.deltaTime;

        if (iginitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
            isShocked = false;

        if(isIgnited)
              ApplyIgniteDamage();
    }

    public virtual void IncreaseStatBy(int _modifier , float _duration , stats _startToModify)
    {
        StartCoroutine(StatModCouroutine(_modifier, _duration, _startToModify));

    }   
    
    private IEnumerator StatModCouroutine(int _modifier, float _duration, stats _startToModify)
    {
        _startToModify.AddModifier( _modifier );

        yield return new WaitForSeconds( _duration );

        _startToModify.RemoveModifier( _modifier );


    }




    private void ApplyIgniteDamage()
    {
        if (iginiteDamageTimer < 0)
        {

            DecreasesHealthBy(igniteDamage);

            if (currentHealth < 0 && !isDead)
                Die();

            iginiteDamageTimer = iginiteDamageCooldown;
           // Debug.Log("TakeBurnDamage" + igniteDamage);
        }
    }

    public virtual void DoDamage(ChracterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;


        int totalDamage = damage.GetValue() + strenght.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCritDamage(totalDamage);
            Debug.Log(totalDamage);
        }


        totalDamage = targetArmour(_targetStats, totalDamage);
    
        _targetStats.TakeDamage(totalDamage);


        //ifinventory have fire dmage then 
        DoMagicDamage(_targetStats); //remove if you dont want to do magic in primary hit

    }

    public virtual void DoMagicDamage(ChracterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        int totalMagicDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();
        totalMagicDamage = CheckTargetResistance(_targetStats, totalMagicDamage);

        _targetStats.TakeDamage(totalMagicDamage);


        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
            return;
       
        AttempToApplyElements(_targetStats, _fireDamage, _iceDamage, _lightingDamage);

    }

    private  void AttempToApplyElements(ChracterStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        bool _canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool _canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool _canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!_canApplyIgnite && !_canApplyChill && !_canApplyShock)
        {
            if (Random.value < .3f && _fireDamage > 0)
            {
                _canApplyIgnite = true;
                _targetStats.ApplyElements(_canApplyIgnite, _canApplyChill, _canApplyShock);
                //Debug.Log("ignite");
                return;
            }
            if (Random.value < .5f && _iceDamage > 0)
            {
                _canApplyChill = true;
                _targetStats.ApplyElements(_canApplyIgnite, _canApplyChill, _canApplyShock);
               // Debug.Log("chill");
                return;
            }
            if (Random.value < .5f && _lightingDamage > 0)
            {
                _canApplyShock = true;
                _targetStats.ApplyElements(_canApplyIgnite, _canApplyChill, _canApplyShock);
               // Debug.Log("shock");
                return;
            }



        }

        if (_canApplyIgnite)
        {
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        }

        if (_canApplyShock)
        {
            _targetStats.SetupShockDamage(Mathf.RoundToInt(_lightingDamage * .1f));
        }


        _targetStats.ApplyElements(_canApplyIgnite, _canApplyChill, _canApplyShock);
    }

    private  int CheckTargetResistance(ChracterStats _targetStats, int totalMagicDamage)
    {
        totalMagicDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    public void ApplyElements(bool _ignite,bool _chill,bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChilled = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;


        if (_ignite && canApplyIgnite)
        {
            iginitedTimer = 4;
            isIgnited = _ignite;

            FX.IgniteFxFor(4);


        }

        if(_chill && canApplyChilled)
        {
            chilledTimer = 4;
            isChilled = _chill;
            FX.ChilledFxFor(4);

            float _slowPercentage = .2f;
            float _slowDuration = 4;
            GetComponent<Entity>().SlowEntityBy(_slowPercentage, _slowDuration);

        }
        if (_shock && canApplyShock)
        {
            if(!isShocked)
            {
                ApplyShock(_shock);

            }
            else
            {
                if (GetComponent<Player>() != null)
                    return;

                HitNearestTargetWithShock();

            }




        }

        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shock;


    }

    public  void ApplyShock(bool _shock)
    {
        if (isShocked)
            return;

        shockedTimer = 4;
        isShocked = _shock;
        FX.ShockedFxFor(4);
    }

    private void HitNearestTargetWithShock()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
            else
                if (closestEnemy == null)
            {
                closestEnemy = transform;
            }



        }

        if (closestEnemy != null)
        {
            GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);

            newShockStrike.GetComponent<ShockStrike_Controller>().Setup(shockDamage, closestEnemy.GetComponent<ChracterStats>());
        }
    }

    public void SetupIgniteDamage( int _Damage)
    {
        igniteDamage = _Damage;
    }

    public void SetupShockDamage( int _Damage)
    {
        shockDamage = _Damage;
    }



    private int targetArmour(ChracterStats _targetStats, int totalDamage)
    {
        if(_targetStats.isChilled)
        {
            totalDamage -= Mathf.RoundToInt(_targetStats.armour.GetValue() * .8f);

        }
        else
           totalDamage -= _targetStats.armour.GetValue();


        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(ChracterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20; 

        if (Random.Range(0, 100) < totalEvasion)
        {
            // Debug.Log("attack evasion");
            return true;
        }
        return false;
    }

    // Update is called once per frame
    public virtual void TakeDamage(int _damage)
    {
        DecreasesHealthBy(_damage);

        GetComponent<Entity>().DamageEffect();
        FX.StartCoroutine("FlashFX"); 

        // Debug.Log(_damage);
        if (currentHealth < 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;


        if(currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if(onHealthChanged != null)
            onHealthChanged();

    }

    protected virtual void DecreasesHealthBy(int _damage)
    {
        currentHealth -= _damage;
        if (onHealthChanged != null)
            onHealthChanged();
    }



    protected virtual void Die()
    {
        isDead = true;


    }

    private bool CanCrit()
    {
        int totalCritChance = critChance.GetValue() + agility.GetValue();

        if(Random.Range(0, 100) <= totalCritChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCritDamage(int _damage)
    {

        float totalCritPower = (critPower.GetValue() + strenght.GetValue()) * .01f;

        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);

    }

    public int GetMaxHealthValue()
    {

        return maxHealth.GetValue() + vitality.GetValue() * 5;

    }
    public stats GetStat(StatType _statType)
    {
        if (_statType == StatType.strenght) return strenght;
        else if (_statType == StatType.agility)  return agility;
        else if (_statType == StatType.intelligence) return intelligence;
        else if (_statType == StatType.vitality) return vitality;
        else if (_statType == StatType.damage) return damage;
        else if (_statType == StatType.critChance) return critChance;
        else if (_statType == StatType.critPower) return critPower;
        else if (_statType == StatType.maxHealth) return maxHealth;
        else if (_statType == StatType.armour) return armour;
        else if (_statType == StatType.evasion) return evasion;
        else if (_statType == StatType.magicResistance) return magicResistance;
        else if (_statType == StatType.iceDamage) return iceDamage;
        else if (_statType == StatType.fireDamage) return fireDamage;
        else if (_statType == StatType.lightingDamage) return lightingDamage;

        return null;


    }



}
