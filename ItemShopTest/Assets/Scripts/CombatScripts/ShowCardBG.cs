using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCardBG : MonoBehaviour,ICombatSelectable
{
    public void ClickedOn()
    {
        FindObjectOfType<CombatUIManager>().ReturnView();
    }

    public void Default()
    {
        throw new System.NotImplementedException();
    }

    public void HoveredOver()
    {
        
    }

    
}
