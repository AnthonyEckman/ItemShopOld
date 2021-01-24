using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public abstract class BaseShopper : ScriptableObject
{
    public string myName;
    public Sprite icon = null;
    public string description = "description";
    public float basePersonalityMultiplier;
    public List<BaseItem> myFavorites = new List<BaseItem>();
    public TextAsset myFile;
    

    //TODO: Create a function that reads from a text file and populates the myFavorites list with scriptable objects of varrying catagories.
    private void PopulateFavorites()
    {

    }
}
