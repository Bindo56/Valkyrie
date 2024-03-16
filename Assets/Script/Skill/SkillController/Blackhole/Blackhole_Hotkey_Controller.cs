using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blackhole_Hotkey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private Blackhole_skill_controller blackhole;


    public void SetupHotkey(KeyCode _hotKey,Transform _myEnemy,Blackhole_skill_controller _myBlackhole)
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = _myEnemy;
        blackhole = _myBlackhole;

        myHotKey = _hotKey;
        myText.text = myHotKey.ToString();


    }

    private void Update()
    { 
        if(Input.GetKeyUp(myHotKey))
        {
            blackhole.AddEnemyToList(myEnemy);
          //  Debug.Log(myHotKey);

            myText.color = Color.clear;
            //sr.color = Color.clear;

        }


    }

}
