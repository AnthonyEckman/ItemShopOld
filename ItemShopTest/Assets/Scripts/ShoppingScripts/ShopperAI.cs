using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ShopperAI : MonoBehaviour , ISelectable
{

    public ShoppingManager myManager;
    //VARIABLES
    private NavMeshAgent navAgent;
    public Counter mySelection;
    public BaseShopper myPreference;
    public GameObject theExit;
    public Vector3 destination;
    [Header("Roll Modifiers")]
    public int leaveChance= 4;
    public int favoriteChance= 4;
    public int browseChance= 6;
    //TIMERS
    private int browseCount;
    public int minimumBrowse = 3;
    private float timeDelay = 0.0f;
    private float delayAmmount = 4f;

    public int patience = 0;
    public float checkOutTime = 0;
    public float checkoutDelay = 10f;

    [Header("Flags")]
    //FLAGS//
    public bool isMoving = false;
    public bool isShopping = false;
    public bool isCheckingOut = false;
    public bool isBeingHelped = false;
    public bool happyShopper = false;
    public bool leaving = false;


    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        theExit = GameObject.FindGameObjectWithTag("Exit");
        myManager = GameObject.FindGameObjectWithTag("ShoppingManager").GetComponent<ShoppingManager>();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckManager();
        if (isMoving)
        {
            Debug.DrawLine(destination, transform.position);
            
            if (Vector3.Distance(transform.position, destination) <= 1.5 || Time.time > timeDelay)
            {
                
                isMoving = false;
            }

        }
        else
        {
            if (isBeingHelped)
            {

            }
            else if (isShopping)
            {
                ShoppingTime();
            }
            
            if (isCheckingOut)
            {
                CheckOut();
            }
            if (leaving && !isBeingHelped)
            {
                Leave();
            }
        }
    }
    //called after a closed deal
    //removes the currenty selected counter from the active counter list
    public void SatisfiedCustomer()
    {
        myManager.activeCounters.Remove(mySelection);
        leaving = true;
        isShopping = false;
        isBeingHelped = false;
        isCheckingOut = false;
        GetComponent<Renderer>().material.color = Color.green;
    }
    
    private void CheckManager()
    {
        if(myManager.shoppingTime && !isCheckingOut && !leaving)
        {
            isShopping = true;
        }
        else if(!myManager.shoppingTime)
        {
            leaving = true;
        }
    }

    private void ShoppingTime()
    {
        Browse();
        if (browseCount >= minimumBrowse)
        {
            CheckCounters();
        }
        
    }

    //called everyframe the "isCheckingOut" Flag is raised
    // controles the shoppers patience level and times
    private void CheckOut()
    {
        if (patience == 0)
        {
            isCheckingOut = true;
            myManager.activeCounters.Add(mySelection);
            GetComponent<Renderer>().material.color = Color.blue;
            checkOutTime = Time.time + checkoutDelay;
            patience++;
        }
        else if(patience == 1 && Time.time > checkOutTime)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
            checkOutTime = Time.time + checkoutDelay;
            patience++;
        }
        else if(patience == 2 && Time.time > checkOutTime)
        {
            myManager.activeCounters.Remove(mySelection);
            GetComponent<Renderer>().material.color = Color.red;
            
            isCheckingOut = false;
            leaving = true;
        }
    }
    private void Leave()
    {
        navAgent.SetDestination(theExit.transform.position);
        if (Vector3.Distance(transform.position, theExit.transform.position) <= 1)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
            myManager.shopperCount--;
        }
    }

    //Randomly goes from counter to counter
    private void Browse()
    {
        
        List<Counter> counterList = new List<Counter>();
        Counter[] counterstemp = FindObjectsOfType<Counter>();
        Counter tempCounter;
        foreach (Counter counter in counterstemp)
        {
            if (counter.Item != null)
            {
                counterList.Add(counter);
            }
        }
        if(counterList.Count > 0 && Time.time > timeDelay)
        {
            timeDelay = Time.time + delayAmmount;
            tempCounter = counterList[Random.Range(0, counterList.Count)];
            destination = RandomNavSphere(tempCounter.transform.position, 1.5f, -1);
            
            
            while( destination.x == Mathf.Infinity)
            {
                destination = RandomNavSphere(tempCounter.transform.position, 1.5f, -1);
            }
            navAgent.SetDestination(destination);
            isMoving = true;
            browseCount++;
            return;
        }

    }
    //checks each counter and checks if it has items and if those items contains any favorited items.
    //If not, the npc will randomly choose an item or not choose an item at all.
    private void CheckCounters()
    {
        List<Counter> counterList = new List<Counter>();
        Counter[] counterstemp = FindObjectsOfType<Counter>();
        List<Counter> favoriteCounters = new List<Counter>();
        //Constructs list of counters that contain items
        foreach (Counter counter in counterstemp)
        {
            if (counter.Item != null && !myManager.activeCounters.Contains(counter))
            {
                counterList.Add(counter);
            }
        }
        //Checks against the AI's preferences and adds any counters that have favorite items.
        foreach (Counter counter in counterList)
        {
            if (myPreference.myFavorites.Contains(counter.Item) && !myManager.activeCounters.Contains(counter))
            {
                favoriteCounters.Add(counter);
            }
        }
        //if the favorite list is populated at all, the shopper will immediatly go for their favorite item
        //ADD LATER: should probably add a delay.
        
        if(favoriteCounters.Count > 0)
        {
            int randomFavorite = Random.Range(0, favoriteChance);
            if (randomFavorite == 0)
            {
                mySelection = favoriteCounters[Random.Range(0, favoriteCounters.Count)];
                isShopping = false;
                happyShopper = true;
                CheckOut();
                counterList.Clear();
                favoriteCounters.Clear();
            }
        }
        //If there are no favorites it will start randomly selecting items to get. 
        //the shopper has a chance of not buying anything.
        //TODO: Should extend so that chance to not buy can be modified in the inspector.*DONE*
        else
        {
            int choosyShopper = Random.Range(0, browseChance);
            if (choosyShopper == 0)
            {
                mySelection = counterList[Random.Range(0, counterList.Count)];
                isShopping = false;
                CheckOut();
                counterList.Clear();
                favoriteCounters.Clear();

            }

            else
            {
                int randomRoll = Random.Range(0, leaveChance);
                if (randomRoll == 0)
                {
                    isShopping = false;
                    leaving = true;
                    counterList.Clear();
                    favoriteCounters.Clear();
                }
                
                
            }
        }
    }

    //will find a  random point from the navmesh within a certain radious
    private Vector3 RandomNavSphere(Vector3 origin, float radius, int layerMask)
    {

        Vector3 randDir = Random.insideUnitSphere * radius;
        randDir += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, radius, layerMask);

        return navHit.position;

    }

    public void Selected()
    {
        //throw new System.NotImplementedException();
    }

    public void UnSelected()
    {
        //throw new System.NotImplementedException();
    }

    public void ClickedOn()
    {
        
        if (isCheckingOut)
        {
            myManager.CheckoutShopper(this);
            isShopping = true;
            isBeingHelped = true;
            isCheckingOut = false;
            leaving = false;
            
        }


    }

    public void RecieveItem(BaseItem item)
    {
        throw new System.NotImplementedException();
    }
}
