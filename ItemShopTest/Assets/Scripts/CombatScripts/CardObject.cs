using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardObject : MonoBehaviour, ICombatSelectable
{
    public BaseCard myCard;

    public Text abilty1Text;
    public Text abilty2Text;


    private void Awake()
    {
        
    }
    public void HoveredOver()
    {
        
    }

    public void Default()
    {
       
    }

    protected void Update()
    {
        GetComponent<Image>().sprite = myCard.cardBackground;
        abilty1Text.text = myCard.myAbility1.abilityText;
        abilty2Text.text = myCard.myAbility2.abilityText;
    }

    public void ClickedOn()
    {
        if (FindObjectOfType<CombatManager>().playerTurn)
        {
            FindObjectOfType<CombatUIManager>().DisplaySelectedCard(myCard);
        }
    }
}
