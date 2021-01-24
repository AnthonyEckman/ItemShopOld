using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "ClassGenerator/NewClass")]
public class BaseClass : ScriptableObject
{

    public List<BaseCard> myCards = new List<BaseCard>();

    public GameObject myModel;


    public int MaxHealth;
    public int MaxStamina;
    
}
