using System.Text;
using UnityEngine;
using UnityEngine.UI;


public enum  ItemType
{
    Matrial,
    Equipment

}





[CreateAssetMenu(fileName = "New Item Data " , menuName  = "Data/Item" ) ]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public string itemName;
    public Sprite itemIcon;

    [Range(0, 100)]
    public float DropChance;

    protected StringBuilder sb = new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }


}
