using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BarterWindow : MonoBehaviour
{

    public bool showWindow = false;
    public ShoppingManager myShoppingManager;
    public BaseItem myItem;
    public Image myItemImage;
    public GameObject barterView;
    public GameObject shoppingTimeView;
    public Text ItemValueText;
    public Text Heading;
    public Text ItemPriceText;
    public Text ShoppingTimeLimit;

    public string itemName;
    public float startingPercentage = 1;
    public float currentPercentage = 1;
    public float trueItemValue;

    public float currentItemValue;
    



    private void Awake()
    {
        
    }

    private void Update()
    {
        ItemValueText.text = "Value %" + Mathf.Floor(currentPercentage * 100);
        
        ItemPriceText.text = currentItemValue + " Gold";
        
        if(showWindow)
        {
            barterView.SetActive(true);
        }
        else
        {
            barterView.SetActive(false);
        }
    }
    public void StartBarter(BaseItem theItem,float itemWorth)
    {
        currentPercentage = startingPercentage;
        myItem = theItem;
        myItemImage.sprite = theItem.icon;
        trueItemValue = itemWorth;
        currentItemValue = theItem.price;
        Heading.text = "I would like to buy " + myItem.name;

        showWindow = true;
    }

    public void ChangePercentage(float newValue)
    {
        currentPercentage = newValue;
        currentItemValue = Mathf.Floor(currentPercentage * myItem.price);
    }

    public void ClickConfirm()
    {
        
        if (myShoppingManager.ConfirmBarter(currentPercentage,trueItemValue))
        {
            myShoppingManager.BarterComplete((int)currentItemValue);
        }
        else
        {
            Debug.Log("THAT WAAAAY TOO MUCH YO");
        }
    }
}
