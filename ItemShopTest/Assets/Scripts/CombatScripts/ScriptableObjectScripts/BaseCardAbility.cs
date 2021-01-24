using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityEffect
{
    damage,heal,move
}

[System.Serializable]
public class AbilityModifier
{
    public AbilityEffect type;
    
    public int ammount;
    

}



[System.Serializable]
[CreateAssetMenu(menuName = "CardGenerator/NewAbility")]
public class BaseCardAbility : ScriptableObject
{
    public string abilityText;

    public Sprite abilityImage;
    public int castCost;
    public int[] castLocations;
    public int[] targetLocations;
    public bool targeted;
    public bool affectFriendly = false;

    public List<AbilityModifier> effects = new List<AbilityModifier>();


}
