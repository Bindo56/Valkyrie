using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
     SpriteRenderer sr;

    [Header("FlashFX")]
    [SerializeField] Material hitMat;
    [SerializeField] float flashDur;

    [Header("AlimentColor")]
    [SerializeField] Color[] igniteColor;
    [SerializeField] Color[] chillColor;
    [SerializeField] Color[] shockColor;

     

    Material ogMat;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        ogMat = sr.material;

    }
    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }

    private IEnumerator FlashFX() 
    {
      sr.material = hitMat;
        Color curtColor = sr.color;

        sr.color= Color.white;


        yield return new WaitForSeconds(flashDur);


        sr.color = curtColor;

        sr.material = ogMat;
    }

    private void RedBlink()
    {
        if(sr.color !=Color.white)
        {
            sr.color = Color.white;
        }

        else 
        {
            sr.color = Color.red;
        }
    }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

    public void IgniteFxFor(float _second)
    {
        InvokeRepeating("IgniteColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _second);

    }

    public void ChilledFxFor(float _second)
    {
        InvokeRepeating("ChilledColorFX", 0, 0.3f);
        Invoke("CancelColorChange", _second);
    }

    public void ShockedFxFor(float _second)
    {
        InvokeRepeating("ShockColorFx", 0, 0.3f);
        Invoke("CancelColorChange", _second);

    }


    private void IgniteColorFx()
    {
        if(sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }

    private void ChilledColorFX()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }

    private void ShockColorFx()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];

    }


}
