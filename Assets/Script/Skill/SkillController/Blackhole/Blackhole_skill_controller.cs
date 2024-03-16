using System.Collections.Generic;
using UnityEngine;

public class Blackhole_skill_controller : MonoBehaviour
{
    [SerializeField] GameObject hotKeyPrefabs;
    [SerializeField] List<KeyCode> keyCodeList;

     float maxSize;
     float growSpeed;
    float shrinkSpeed;
     bool canGrow = true;
    bool canShrink;
    float blackholeTimer;


    private bool canCreateHotKey = true;
    bool cloneAttack;
    int amountOfAttack = 1;
    float cloneAttackCooldown = .3f;
    float cloneAttackTimer;
    bool playerCanDisapper = true;

    [SerializeField] List<Transform> targets = new List<Transform>();
    [SerializeField] List<GameObject> createdHotKey = new List<GameObject>();

    public void SetupBlackhole(float _maxSize,float _growSpeed,float _shrinkSpeed, int _amountOfAttack, float _cloneAttackCooldown,float _blackholeDur)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttack = _amountOfAttack;
        cloneAttackCooldown = _cloneAttackCooldown;
        blackholeTimer = _blackholeDur;

        if (SkillManager.instance.clone.crystalInsteadOfClone)
            playerCanDisapper = false;
    }

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;

        if(blackholeTimer < 0 )
        {
            blackholeTimer = Mathf.Infinity;

            if(targets.Count > 0)
            {
                CloneAttackRelease();
            }
            else
            {
                FinishBlackholeAbility();
            }
                
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            CloneAttackRelease();
        }

        AttackLogic();

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void CloneAttackRelease()
    {
        if (targets.Count <= 0)
            return;
        
        DestroyHotKey();
        cloneAttack = true;
        canCreateHotKey = false;

        if (playerCanDisapper)
        {
            playerCanDisapper = false;
          PlayerManager.instance.player.fx.MakeTransparent(true);
        }

    }

    private void AttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttack && amountOfAttack > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
                xOffset = 1;
            else
                xOffset = -1;

            if (SkillManager.instance.clone.crystalInsteadOfClone)
            {
                SkillManager.instance.crystal_Skill.CreateCrystal();
                SkillManager.instance.crystal_Skill.CurrentCrystalChooseRandomTarget();
            }
            else
            {
              SkillManager.instance.clone.CreatClone(targets[randomIndex], new Vector3(xOffset, 0));

            }

            amountOfAttack--;

            if (amountOfAttack <= 0)
            {

                Invoke("FinishBlackholeAbility", 1f);
            }

        }
    }

    private void FinishBlackholeAbility()
    {
        DestroyHotKey();
        canShrink = true;
        cloneAttack = false;
       
        PlayerManager.instance.player.ExitBlackHole();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            Hotkey(collision);


            // targets.Add(collision.transform);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
           collision.GetComponent<Enemy>().FreezeTime(false);
    }


    private void DestroyHotKey()
    {
        if (createdHotKey.Count <= 0)
            return;

        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }
    }

    private void Hotkey(Collider2D collision)
    {
        if(keyCodeList.Count <= 0)
        {
            Debug.LogWarning("Noenemy");
            return;
        }

        if (!canCreateHotKey)
        {
            return;
        }


        GameObject newHotKey = Instantiate(hotKeyPrefabs, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKey.Add(newHotKey);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        Blackhole_Hotkey_Controller newHotKeyScript = newHotKey.GetComponent<Blackhole_Hotkey_Controller>();

        newHotKeyScript.SetupHotkey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
