using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    public int curreny;

    public void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
        instance = this; 
    }

    public bool HaveEnoughMoney(int price)
    {
        if(price > curreny)
        {
            Debug.Log("Dont have enough money");
            return false;
        }

        curreny = curreny - price;
        return true; 


    }

}
