using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCard : CardObject
{
    public CombatManager cManager;
    

    public void ActivateAbility1()
    {
        if (!cManager.selectedUnit.GetComponent<CombatUnit>().turnTaken)
        {
            if (myCard.myAbility1.targeted)
            {
                if (!myCard.myAbility1.affectFriendly)
                {
                    cManager.SelectTarget(this, myCard.myAbility1, false);
                }
                else
                {
                    cManager.SelectTarget(this, myCard.myAbility1, false);
                }

            }
            else
            {
                ActivateBitches(myCard.myAbility1);
            }
        }
        else
        {
            Debug.Log("Turn Already Taken");
        }
    }

    public void ActivateAbility2()
    {
        if (!cManager.selectedUnit.GetComponent<CombatUnit>().turnTaken)
        {
            if (myCard.myAbility2.targeted)
            {
                if (!myCard.myAbility1.affectFriendly)
                {
                    cManager.SelectTarget(this, myCard.myAbility2, false);
                }
                else
                {
                    cManager.SelectTarget(this, myCard.myAbility2, false);
                }
            }
            else
            {
                ActivateBitches(myCard.myAbility2);
            }
        }
        else
        {
            Debug.Log("Turn Already Taken");
        }
    }

    public void ActivateBitches(BaseCardAbility ability)
    {
        bool inRange = false;
        bool validTarget = false;
        
        if(cManager == null)
        {
            cManager = GameObject.FindObjectOfType<CombatManager>();
        }
        //checks if caster is in the right casting location
        foreach(int spot in ability.castLocations)
        {
            if(spot == cManager.selectedUnit.GetComponent<CombatUnit>().mySpot)
            {
                Debug.Log("in Range");
                inRange = true;
                break;
            }
           
        }
        if(inRange && ability.castCost <= cManager.selectedUnit.GetComponent<CombatUnit>().CurrentStamina)
        {
            if(ability.targeted)
            {
                //checks if target is valid
                foreach(int spot in ability.targetLocations)
                {
                    if(spot == cManager.selectedTarget.GetComponent<CombatUnit>().mySpot)
                    {
                        Debug.Log("Valid Target");
                        validTarget = true;
                        break;
                    }
                }
                if (validTarget)
                {
                    foreach (AbilityModifier modifier in ability.effects)
                    {
                        CastAbility(modifier, cManager.selectedTarget);
                    }
                    cManager.selectedUnit.GetComponent<CombatUnit>().ReduceStamina(ability.castCost);
                    cManager.selectedUnit.GetComponent<CombatUnit>().EndTurn();
                }
                else
                {
                    Debug.Log("invalid Target");
                }
            }
            else
            {
                if(ability.affectFriendly)
                {
                    foreach(int spot in ability.targetLocations)
                    {
                        GameObject target = cManager.playerTeam.mySpots[spot].myUnit.gameObject;
                        foreach(AbilityModifier modifier in ability.effects)
                        {
                            CastAbility(modifier, target);
                        }
                    }
                    cManager.selectedUnit.GetComponent<CombatUnit>().ReduceStamina(ability.castCost);
                    cManager.selectedUnit.GetComponent<CombatUnit>().EndTurn();
                }
                else
                {
                    foreach (int spot in ability.targetLocations)
                    {
                        GameObject target = cManager.enemyTeam.mySpots[spot].myUnit.gameObject;
                        foreach (AbilityModifier modifier in ability.effects)
                        {
                            CastAbility(modifier, target);
                        }
                    }
                    cManager.selectedUnit.GetComponent<CombatUnit>().ReduceStamina(ability.castCost);
                    cManager.selectedUnit.GetComponent<CombatUnit>().EndTurn();
                }

            }
        }
        else
        {
            Debug.Log("Unit Out Of Range/Out of Mana");
        }
        GameObject.FindObjectOfType<CombatUIManager>().ReturnView();

    }

    private void CastAbility(AbilityModifier modifier, GameObject target)
    {
        if (modifier.type == AbilityEffect.damage)
        {
            target.GetComponent<CombatUnit>().DealDamage(modifier.ammount);
        }
        if (modifier.type == AbilityEffect.heal)
        {
            target.GetComponent<CombatUnit>().HealDamage(modifier.ammount);
        }
        if (modifier.type == AbilityEffect.move)
        {

        }
    }


    
}


    


