using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "InventoryGenerator/Inventory")]
public class InventoryScript : ScriptableObject
{
    public List<BaseItem> items = new List<BaseItem>();
    public InventoryGenerator myGenerator;
    public int Money;

    public string inventoryName;
    public int inventoryLimit;
    

    public void AddItem(BaseItem item, int ammount)
    {

        for (int i = 0; i < ammount; i++)
        {
            items.Add(item);
        }
        //myGenerator.GenerateInventory();
        
    }

    //remove an item from the inventory, returns false if the action is not valid.
    public bool RemoveItem(BaseItem item, int ammount)
    {
        if (items.Contains(item))
        {
            //counts how many there are of the requested item in the inventory
            int counter = 0;
            for (int i=0; i < items.Count ;i++ )
            {
                BaseItem temp = items[i];
                if(temp == item)
                {
                    counter += 1;
                }
            }
            //checks to see if the ammount requested is equal or less to how many are in the invetory
            if (ammount <= counter)
            {
                //if it passes it will start removing items from the list
                for(int i = 0; i < ammount; i++)
                {
                    items.Remove(item);
                }
                return true;
            }
            else
            {
                Debug.Log("not enough items in inventory");
                return false;

            }
        }
        else
        {
            Debug.Log("item Not Present");
            return false;
        }
    }

    

}
