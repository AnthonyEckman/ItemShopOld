using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CombatUnit : MonoBehaviour, ICombatSelectable
{

    public GameObject hitTextPrefab;

    public BaseClass myClass;
    Color defaultColor;
    public bool turnTaken;
    public bool isEnemy;
    public int mySpot;


    public int MaxHealth;
    public int CurrentHealth;

    public int MaxStamina;
    public int CurrentStamina;
    


    // Start is called before the first frame update
    void Start()
    {
       if(isEnemy)
        {
            Destroy(GetComponentInChildren<ManaBar>().gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }


    public void Init()
    {
        MaxHealth = myClass.MaxHealth;
        CurrentHealth = MaxHealth;
        MaxStamina = myClass.MaxStamina;
        CurrentStamina = MaxStamina;
    }

    public void Default()
    {
        //throw new System.NotImplementedException();
    }

    public void HoveredOver()
    {
        //throw new System.NotImplementedException();
    }

    public void ClickedOn()
    {
        if (!isEnemy && !GameObject.FindObjectOfType<CombatManager>().targeting)
        {
            GameObject.FindObjectOfType<CombatManager>().UnitSelected(gameObject);
        }
        else if(GameObject.FindObjectOfType<CombatManager>().targeting)
        {
            GameObject.FindObjectOfType<CombatManager>().selectedTarget = gameObject;
        }
        else if((GameObject.FindObjectOfType<CombatManager>().friendlyTargeting))
        {
            GameObject.FindObjectOfType<CombatManager>().selectedTarget = gameObject;
        }
        
    }
    public void EndTurn()
    {
        turnTaken = true;
    }
    public void TurnStart()
    {
        turnTaken = false;
    }

    public void CheckStatus()
    {
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Death();
        }
    }


    public void Death()
    {

        GameObject.FindObjectOfType<CombatManager>().UnitDie(gameObject);
        gameObject.SetActive(false);
    }
    public void SpawnFloatingText(string message,Color color)
    {
        GameObject theText = Instantiate(hitTextPrefab);
        theText.GetComponent<TextMesh>().text = message;
        theText.GetComponent<TextMesh>().color = color;
        theText.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        theText.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100,100), 50, 0));
        Destroy(theText, 3);

    }

    public void ReduceStamina(int ammount)
    {
        CurrentStamina -= ammount;
    }
    public void DealDamage(int damage)
    {
        CurrentHealth -= damage;
        SpawnFloatingText(damage.ToString(), Color.red);
    }

    public void HealDamage(int heal)
    {
        CurrentHealth += heal;
        SpawnFloatingText(heal.ToString(), Color.green);
    }
    
}
