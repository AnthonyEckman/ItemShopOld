using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    //variables that help keep track of items, recipients, and what buttons are pressed during a transaction
    public static GameObject lastButtonPressed;
    public static BaseItem lastItemRequested;
    public static GameObject lastRecipient;
    //Hard coded references to player inventory and the Inventory UI
    public  GameObject InventoryUI;
    public GameObject GoldAmmount;
    public GameObject StartShoppPhaseButton;
    public  InventoryScript playerInventory;
    public InventoryGenerator generator;
    

    //flags that are checked
    public static bool openPlayerInventory = false;
    public static bool sendingItems = false;
    public bool inventoryGenerated = false;
    //could be obsolete, might be able to just use sending items for all transactions
    public static bool recievingItems = false;


    public void Awake()
    {
        generator = FindObjectOfType<InventoryGenerator>();
        generator.myManager = this;
    }
    public void Update()
    {

        ModifyGoldAmmount();
        //controles inventory UI
        if(openPlayerInventory)
        {

            InventoryUI.SetActive(true);
            if (!inventoryGenerated)
            {
                generator.GenerateInventory();
                inventoryGenerated = true;
                
            }
        }
        else
        {
            InventoryUI.SetActive(false);
            inventoryGenerated = false;
        }
    }

    private void ModifyGoldAmmount()
    {
        GoldAmmount.GetComponent<Text>().text = "Gold: " + playerInventory.Money;
    }
    

    //Will be called whenever the player is transfering item from their inventory to somewhere else.
    //see InventoryItemButton script for further details
    public static void PlayerSending(GameObject recipient)
    {
        openPlayerInventory = true;
        sendingItems = true;
        lastRecipient = recipient;
        
    }

    // Refreshes the manager to ready itself for a new transaction, is usually called at the end of each transaction
    //
    public static void Refresh()
    {
        sendingItems = false;
        openPlayerInventory = false;
        recievingItems = false;

        lastButtonPressed = null;
        lastItemRequested = null;
        lastRecipient = null;
    }

    // will usually be called within PlayerSending() function
    //TODO:Create Interface for sending and recieving items.
    

    
}
