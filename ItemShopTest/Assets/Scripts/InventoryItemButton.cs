using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class InventoryItemButton : MonoBehaviour
{

    //References to the item the button represents
    public BaseItem myitem;
    public GameObject myParent;
    public InventoryGenerator myInventory;

    public void OnClick()
    {
        //checks the inventory manager flag, if it is in sending mode, whatever button is clicked on will return its respective item
        // to whoever is the active the active recipient
        if(InventoryManager.sendingItems)
        {
            //specific case for counter agents since they can only recieve one item, should rework to become decoupled.
            if (InventoryManager.lastRecipient.GetComponent<Counter>())
            {
                InventoryManager.lastRecipient.GetComponent<ISelectable>().RecieveItem(myitem);
                myInventory.playerInventory.RemoveItem(myitem, 1);
                InventoryManager.Refresh();
                Destroy(myParent);
            }


        }
    }

}
