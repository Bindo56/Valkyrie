using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class itemDrop : MonoBehaviour
{
    [SerializeField] int PossibleitemDrop;


    [SerializeField]
    ItemData[] possibleDrop;

    List<ItemData> dropList = new List<ItemData>();


    [SerializeField] GameObject dropPrefab;
    

    public virtual void GenerateDrop()
    {
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if(Random.Range(0,100) <= possibleDrop[i].DropChance)
            {
                dropList.Add(possibleDrop[i]); 
            }
        }

        for (int i = 0; i < PossibleitemDrop; i++)
        {
            ItemData randomItem = dropList[Random.Range(0 ,dropList.Count )]; //Random.Range(0,dropList.Count-1) replace it with this

            dropList.Remove(randomItem);
            DropItem(randomItem);

        }





    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab,transform.position,Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5,5),Random.Range(5,8));


        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);

    }

}
