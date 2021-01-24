using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class InventoryGenerator : MonoBehaviour
{
    public InventoryScript playerInventory;
    public InventoryScript requestedInventory;
    public GameObject button;
    private List<GameObject> garbageCollector = new List<GameObject>();
    public InventoryManager myManager;
    public int InventoryCount = 0;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        //myManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        playerInventory = myManager.playerInventory;
        playerInventory.myGenerator = this;
        GenerateInventory();
    }
    private void Update()
    {
        if (InventoryCount != playerInventory.items.Count)
        {
            InventoryCount = playerInventory.items.Count;
            GenerateInventory();
        }
    }

    //generates the players inventory, if called again after already being generated it will tear it down and rebuid it.
    public void GenerateInventory(InventoryScript otherInventory = null)
    {
        if(otherInventory != null)
        {
            requestedInventory = otherInventory;
        }
        else
        {
            requestedInventory = playerInventory;
        }
        if(garbageCollector.Count > 0)
        {
            foreach(GameObject button in garbageCollector)
            {
                Destroy(button);
            }
            garbageCollector.Clear();
        }
        List<BaseItem> list = requestedInventory.items;

        foreach(BaseItem item in list)
        {
            //Instantiating each inventory slot and sending references of the item. it's parent, and the generator itself.
            GameObject temp = Instantiate(button, gameObject.transform);
            temp.GetComponentInChildren<Button>().image.sprite = item.icon;
            temp.GetComponentInChildren<InventoryItemButton>().myitem = item;
            temp.GetComponentInChildren<InventoryItemButton>().myParent = temp;
            temp.GetComponentInChildren<InventoryItemButton>().myInventory = this;
            garbageCollector.Add(temp);
        }
    }

}
