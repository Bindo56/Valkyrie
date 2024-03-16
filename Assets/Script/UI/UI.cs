using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject characaterUI;
    [SerializeField] GameObject skillTreeUI;
    [SerializeField] GameObject CraftUI;
    [SerializeField] GameObject optionUI;



    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;


    // Start is called before the first frame update
    void Start()
    {
        switchTo(null);
        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);


     //  itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.C))
        {
            SwitchWithKEy(characaterUI);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchWithKEy(skillTreeUI);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchWithKEy(CraftUI);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchWithKEy(optionUI);
        }
    }

    public void switchTo(GameObject menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if(menu != null)
        {
            menu.SetActive(true);
        } 
    }

    public void SwitchWithKEy(GameObject _menu)
    {
        if( _menu !=null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }

        switchTo(_menu);

    }


}
