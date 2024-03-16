using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{

    private Entity entity;
    private ChracterStats chracterStats;
    private RectTransform myTransform;
    private Slider Slider;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInParent<Entity>();
        Slider = GetComponentInChildren<Slider>();
        myTransform = GetComponent<RectTransform>();
        chracterStats = GetComponentInParent<ChracterStats>();

        chracterStats.onHealthChanged += UpdateHealthUI;

        entity.OnFlipped += FlipUI;
        UpdateHealthUI();

    }

    private void FlipUI()
    {
        myTransform.Rotate(0,180,0);
       // Debug.Log("Entity is Flipped");
    }

    private void UpdateHealthUI()
    {
        Slider.maxValue = chracterStats.GetMaxHealthValue();
        Slider.value = chracterStats.currentHealth;
    }


    private void OnDisable()
    {
        chracterStats.onHealthChanged -= UpdateHealthUI;
        entity.OnFlipped -= FlipUI;

    }
}
