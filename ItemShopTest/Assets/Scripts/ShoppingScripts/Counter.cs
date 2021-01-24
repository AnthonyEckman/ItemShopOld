using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, ISelectable
{
    public BaseItem Item;
    public GameObject showing;
    public GameObject ItemImage;
    public Material highlight;
    public Material defaultMaterial;
    public InventoryScript playerInventory;
    


    private void Update()
    {
        CheckItem();

       
    }
    private void Start()
    {
        playerInventory = FindObjectOfType<InventoryManager>().playerInventory;
    }



    public void SetItem (BaseItem newItem)
    {
        Item = newItem;
        
    }

    public void RecieveItem(BaseItem item)
    {
        Item = item;
        ItemImage = Instantiate(showing, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        ItemImage.GetComponent<Renderer>().material.SetTexture("_MainTex", Item.icon.texture);

        ItemImage.transform.SetParent(transform);
        InventoryManager.Refresh();

    }
    public void CheckItem()
    {
        if(Item !=  null && ItemImage == null)
        {
            ItemImage = Instantiate(showing, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            ItemImage.GetComponent<Renderer>().material.SetTexture("_MainTex", Item.icon.texture);
        }
    }

    public void RemoveItem()
    {
        Item = null;
        if(ItemImage != null)
        {
            Destroy(ItemImage);
        }
    }

    public void Selected()
    {
        GetComponent<Renderer>().material = highlight;
    }

    public void ClickedOn()
    {

        //Checks if the inventory is already open, if it is, it will just close the window
        if (InventoryManager.openPlayerInventory)
        {
            
            InventoryManager.openPlayerInventory = false;
            InventoryManager.Refresh();
        }
        //If the invetory is not already open, it will check if there is an item already slotted into the counter
        //If there is an item, it will return it to the player inventory.
        else
        {
            if (Item != null)
            {
                playerInventory.AddItem(Item, 1);
                RemoveItem();

            }
            //Flags the inventory manager to start a recieve an item and sends itself as a reference
            InventoryManager.PlayerSending(gameObject);
        }
        
      
    }

    public void UnSelected()
    {
        GetComponent<Renderer>().material = defaultMaterial;
    }
}
