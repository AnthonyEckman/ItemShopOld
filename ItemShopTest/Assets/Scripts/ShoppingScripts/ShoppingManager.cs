using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingManager : MonoBehaviour
{
    //REFERENCES
    public GameObject theExit;
    public List<GameObject> myShoppers;
    public List<Counter> activeCounters = new List<Counter>();
    public BarterWindow myBarterScreen;
    public EconomyManager myEconomy;
    public InventoryManager myInventory;
    private ShopperAI beingHelped;
    public GameObject shoppingCombo;
    //VARIABLES
    public int shopperCount;
    public int shopperLimit;
    public float spawnShopperDelay = 0;
    public float spawnShopperDelayAmmount = 8.0f;
    public float shoppingTimeLimit = 210.0f;
    public float currentShoppingTimer = 0.0f;
    public float shoppingEndTime;
    private int shoppingComboAmmount;
    public float itemPriceMultiplier;

    //FLAGS
    public bool shoppingTime = false;


    private void Awake()
    {
        theExit = GameObject.FindGameObjectWithTag("Exit");
        myBarterScreen = GameObject.FindGameObjectWithTag("BarterWindow").GetComponent<BarterWindow>();
        myBarterScreen.myShoppingManager = this;
        myEconomy = GameObject.FindGameObjectWithTag("EconomyManager").GetComponent<EconomyManager>();
        myInventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();

    }

    // Update is called once per frame
    void Update()
    {
        currentShoppingTimer = Time.time;
        if (shoppingTime)
        {
            TimerCountDown();
            if (spawnShopperDelay < Time.time)
            {

                SpawnShopper();

            }
            if(shoppingEndTime < Time.time)
            {
                EndShoppingPhase();
            }
            
        }
        UpdateCombo();
    }

    private void UpdateCombo()
    {
        shoppingCombo.GetComponent<Text>().text = "COMBO: x" + shoppingComboAmmount;

    }
    private void TimerCountDown()
    {
        myBarterScreen.ShoppingTimeLimit.text = "Time Left: " + Mathf.Floor(shoppingEndTime - currentShoppingTimer);
    }
    public void StartShoppingPhase()
    {
        shoppingEndTime = Time.time + shoppingTimeLimit;
        myBarterScreen.shoppingTimeView.SetActive(true);
        shoppingTime = true;
        
        myInventory.StartShoppPhaseButton.SetActive(false);
    }
    public void EndShoppingPhase()
    {
        shoppingTime = false;
        myBarterScreen.shoppingTimeView.SetActive(false);
        myInventory.StartShoppPhaseButton.SetActive(true);
    }

    //Generates shoppers during the shopping phase
    public void SpawnShopper()
    {
        if (shopperCount < shopperLimit && shoppingTime)
        {

            GameObject temp = myShoppers[Random.Range(0, myShoppers.Count)];
            GameObject tempshopper = Instantiate(temp);
            tempshopper.transform.position = theExit.transform.position;
            spawnShopperDelay = Time.time + spawnShopperDelayAmmount;
            shopperCount++;
        }
        
    }
    //when the player selects a shopper that is ready to checkout this fucntion will be called
    public void CheckoutShopper(ShopperAI customer)
    {
        beingHelped = customer;
        BaseItem theItem = customer.mySelection.Item;
        BaseShopper theirPreference = customer.myPreference;
        float priceMultiplier = 1;
        //Will access each entry in the economy manager and look at the multiplier values for each item category
        foreach(EconomyManager.EconomyEntry category in myEconomy.EconomyList)
        {
            //Looks at each category the item has and checks it against the economy manager's list
            //Once a match is found, it will take the multiplier from the economy manager and add it to the multiplier
            foreach(Categories cat in theItem.myCategory)
            {
                if(cat.ToString() == category.category)
                {
                    priceMultiplier = priceMultiplier * category.multiplier;

                }
            }
        }
        priceMultiplier = Random.Range(priceMultiplier - .05f, priceMultiplier + .11f);
        myBarterScreen.StartBarter(theItem,priceMultiplier);
        itemPriceMultiplier = priceMultiplier;

    }

    //Called on when the player clicks confirm in the bart screen.
    // it will look at the offered price that the player inputs and compare it to the item's true value
    //The funciton will take the true item value, modify with the shoppers basic bias value, and then look at the shopper's mood
    //if the offered price is lower or equal to the calculated "Ceiling Value"
    public bool ConfirmBarter(float offered,float truevalue)
    {
        float temp = beingHelped.myPreference.basePersonalityMultiplier * truevalue;
        float CeilingValue = temp;
        
        if (beingHelped.patience == 2)
        {
            Debug.Log("What took you so long?");
            CeilingValue -= CeilingValue * .10f;
        }

        
        if ( offered <= CeilingValue)
        {
            if (offered >= CeilingValue - .05f)
            {
                if (shoppingComboAmmount < 4)
                {
                    shoppingComboAmmount++;
                }
            }
            return true;
        }
        else
        {
            shoppingComboAmmount = 0;
            return false;
        }
        
    }

    public void BarterComplete(int money)
    {
        myBarterScreen.showWindow = false;
        beingHelped.mySelection.RemoveItem();
        myInventory.playerInventory.Money += money;

        beingHelped.SatisfiedCustomer();
    }
}
