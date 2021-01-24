using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "CardGenerator/NewCard")]
public class BaseCard : ScriptableObject
{

    public BaseCardAbility myAbility1;
    public BaseCardAbility myAbility2;

    public Sprite cardBackground;





}
