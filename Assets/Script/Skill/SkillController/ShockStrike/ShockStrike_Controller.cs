using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockStrike_Controller : MonoBehaviour
{
    [SerializeField] ChracterStats targetStat;
    [SerializeField] float speed;

    int damage;
    Animator anim;
    bool isTrigged;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        
    }

    public void Setup(int _damage, ChracterStats _targetStat)
    {
        damage = _damage;
        targetStat = _targetStat;


    }

    // Update is called once per frame
    void Update()
    {

        if(!targetStat)
            return;

        if (isTrigged)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetStat.transform.position, speed * Time.deltaTime);
        transform.right = transform.position - targetStat.transform.position; //for right position

        if(Vector2.Distance(transform.position, targetStat.transform.position) < .1f)
        {
            anim.transform.localRotation = Quaternion.identity;
            anim.transform.localPosition = new Vector3(0, .5f); 
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);

            Invoke("DamageAndSelfDestroy", 0.3f); 

            isTrigged = true;
            anim.SetTrigger("Hit");


        }

    }
    private void DamageAndSelfDestroy()
    {
        targetStat.ApplyShock(true);
            targetStat.TakeDamage(damage);
            Destroy(gameObject, .4f);

    }

        
}
