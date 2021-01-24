using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    CombatUIManager playerUI;
    //Defined In Inspector
    public TeamManager playerTeam;
    public TeamManager enemyTeam;

    public GameObject selectedUnit;
    public GameObject selectedTarget;

    public bool playerTurn = true;
    public bool targeting = false;
    public bool friendlyTargeting = false;


    // Start is called before the first frame update
    void Start()
    {
        playerUI = FindObjectOfType<CombatUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndRound()
    {
        playerTurn = !playerTurn;
        Debug.Log("Turn Over, Changing Sides");
    }


    public void UnitSelected(GameObject unit)
    {
        if (unit.GetComponent<CombatUnit>() != null)
        {
            if (selectedUnit != null && !unit.Equals(selectedUnit))
            {
                selectedUnit.GetComponentInChildren<Renderer>().material.color = Color.white;
            }
            if (playerTurn)
            {
                selectedUnit = unit;
                selectedUnit.GetComponentInChildren<Renderer>().material.color = Color.blue;
                playerUI.PopulateHand(selectedUnit.GetComponent<CombatUnit>().myClass.myCards);
            }
        }
        else
        {
            throw new System.Exception("GameObject is not a combat unit dawg");
        }
    }

    public void UnitDie(GameObject unit)
    {
        if(!unit.GetComponent<CombatUnit>().isEnemy)
        {
            playerTeam.RemoveUnit(unit);
        }
        else
        {
            enemyTeam.RemoveUnit(unit);
        }
    }

    public IEnumerator TargetSelection(SelectedCard card,BaseCardAbility ability)
    {
        while (selectedTarget == null)
        {
            

            yield return new WaitForFixedUpdate();
        }
        friendlyTargeting = false;
        targeting = false;
        card.ActivateBitches(ability);
        yield break;
    }

    public void SelectTarget(SelectedCard card, BaseCardAbility ability,bool friendly)
    {
        if (friendly)
        {
            friendlyTargeting = true;
        }
        else
        {
            targeting = true;
        }
        selectedTarget = null;
        StartCoroutine(TargetSelection(card,ability));
    }


}
