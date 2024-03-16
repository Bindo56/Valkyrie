using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptin;
   
    public void ShowToStatToolTip(string _Text)
    {
        descriptin.text = _Text;

        gameObject.SetActive(true);

    }

    public void HideToolTip()
    {
        descriptin.text = "";

        gameObject.SetActive(false);
    }


}
