using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Skilltree_Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skillDes;
    [SerializeField] TextMeshProUGUI skillname;

    public void ShowToolTip(string _skillDes, string skillName)
    {
        skillname.text = skillName;
        skillDes.text = _skillDes;
        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
