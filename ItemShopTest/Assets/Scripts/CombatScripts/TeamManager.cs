using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    //Dependents:
    //*CombatManager


    [SerializeField]
    private bool isEnemyTeam = false;
    //reference to unit spot object
    public UnitySpot spotObject;
    public GameObject combatUnit;
    //test object to spawn inplace of units
    
    public BaseClass[] units;
    //how many units are
    [SerializeField]
    private int unitCap = 4;
    //list of spots
    public List<UnitySpot> mySpots = new List<UnitySpot>();
    private List<GameObject> myUnits = new List<GameObject>();

    void Start()
    {
        createSpaces();
        populateTeam();
    }

    // Update is called once per frame
    void Update()
    {
        ManageSpaces();
        TrackTurns();
    }

    private void TrackTurns()
    {
        int counter = 0;
        if(GameObject.FindObjectOfType<CombatManager>().playerTurn)
        {
            if(!isEnemyTeam)
            {
                foreach(GameObject unit in myUnits)
                {
                    if(unit.GetComponent<CombatUnit>().turnTaken)
                    {
                        counter++;
                    }
                }
            }
        }
        if (!GameObject.FindObjectOfType<CombatManager>().playerTurn)
        {
            if (isEnemyTeam)
            {
                foreach (GameObject unit in myUnits)
                {
                    if (unit.GetComponent<CombatUnit>().turnTaken)
                    {
                        counter++;
                    }
                }
            }
        }
        if(counter == myUnits.Count)
        {
            GameObject.FindObjectOfType<CombatManager>().EndRound();
        }
    }

    //creates empty spots to house units
    private void createSpaces()
    {
        if (!isEnemyTeam)
        {
            for (int i = 0; i < unitCap; i++)
            {
                UnitySpot tempSpot = Instantiate(spotObject);
                tempSpot.transform.position = transform.position;
                tempSpot.transform.parent = transform;
                tempSpot.transform.localPosition = new Vector3(i + i * .5f, 0, 0);
                mySpots.Add(tempSpot);

            }
        }
        else
        {
            for (int i = 0; i < unitCap; i++)
            {
                UnitySpot tempSpot = Instantiate(spotObject);
                tempSpot.transform.position = transform.position;
                tempSpot.transform.parent = transform;
                tempSpot.transform.localPosition = new Vector3(-i - i * .5f, 0, 0);
                mySpots.Add(tempSpot);

            }
        }
    }

    //Will populate team with units from unit array
    //TODO: Will have to be re-written once scriptable objects are created for units.
    // Eventually will spawn an empty unitGameobject and apply values from scriptable object
    private void populateTeam()
    {
        UnitySpot tempSpot = null;
        foreach(BaseClass unit in units)
        {
            foreach(UnitySpot spot in mySpots)
            {
                if (spot.myUnit == null)
                {
                    tempSpot = spot;
                    break;
                }
            }
            if(tempSpot == null || tempSpot.myUnit != null)
            {
                Debug.Log("NoSpot Available");
                break;
            }
            else
            {
                GameObject tempUnit = Instantiate(combatUnit,transform);
                tempUnit.transform.position = tempSpot.transform.position;
                tempUnit.GetComponent<CombatUnit>().myClass = unit;
                GameObject tempUnitModel = Instantiate(tempUnit.GetComponent<CombatUnit>().myClass.myModel, tempUnit.transform);
                tempSpot.myUnit = tempUnit;
                tempUnit.GetComponent<CombatUnit>().mySpot = mySpots.IndexOf(tempSpot);
                if(isEnemyTeam)
                {
                    tempUnit.GetComponent<CombatUnit>().isEnemy = true;
                }
                tempUnit.GetComponent<CombatUnit>().Init();
                myUnits.Add(tempUnit);
            }
        }
    }
    public void MoveUnit(int position,int spaces)
    {

    }

    public void RemoveUnit(GameObject unit)
    {
        int unitsSpot = unit.GetComponent<CombatUnit>().mySpot;
        mySpots[unitsSpot].myUnit = null;
        myUnits.RemoveAt(unitsSpot);
        
    }

    //if there is an open space units will move toward the front
    public void ManageSpaces()
    {
        foreach(UnitySpot spot in mySpots)
        {
            if(spot.myUnit == null)
            {
                if (mySpots.IndexOf(spot) + 1 < unitCap)
                {
                    if (mySpots[mySpots.IndexOf(spot) + 1].myUnit != null)
                    {
                        //Move Unit
                        mySpots[mySpots.IndexOf(spot) + 1].myUnit.transform.position = spot.transform.position;
                        //change Unit's spot
                        mySpots[mySpots.IndexOf(spot) + 1].myUnit.GetComponent<CombatUnit>().mySpot = mySpots.IndexOf(spot);
                        //re-assign current Spot's Unit
                        spot.myUnit = mySpots[mySpots.IndexOf(spot) + 1].myUnit;
                        //change spot's unit to null
                        mySpots[mySpots.IndexOf(spot) + 1].myUnit = null;
                    }
                }
            }
        }
    }
}
