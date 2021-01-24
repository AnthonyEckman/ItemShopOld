using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ItemGenerator/newItem")]
public abstract class BaseItem : ScriptableObject
{
    public string myName;
    public Sprite icon = null;
    public string description = "description";
    public int price = 0;

    //Determins what categories each item belongs to by looking ad the Categories enumerator
    //can be edited in inspector
    public List<Categories> myCategory = new List<Categories>();
    
}
