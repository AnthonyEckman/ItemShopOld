using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatUIManager : MonoBehaviour
{
    //Dependents:
    //*COMBATMANAGER
    
    //Player Input References
    Camera playerView;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    GameObject LookingAt;
    List<RaycastResult> UILookingAt =  new List<RaycastResult>();

    //UI Elements
    public GameObject cardRowUI;
    public GameObject selectedCardUI;
    public GameObject cardObject;
    CardObject selectCardObject;
    //the players hand
    List<GameObject> cardRowObjects = new List<GameObject>();

    //Variables
    BaseCard CurrentCardSelection;

    // Start is called before the first frame update
    void Start()
    {
        playerView = FindObjectOfType<Camera>();
        
        m_EventSystem = FindObjectOfType<EventSystem>();
        m_Raycaster = GetComponent<GraphicRaycaster>();
        selectCardObject = selectedCardUI.GetComponentInChildren<CardObject>();

    }

    // Update is called once per frame
    void Update()
    {
        //Raycast from camera to Canvas
        UILookingAt = UIPointingAt();
        if (UILookingAt.Count > 0)
        {

            foreach (RaycastResult result in UILookingAt)
            {
                if (result.gameObject.GetComponent<ICombatSelectable>() != null)
                {
                    result.gameObject.GetComponent<ICombatSelectable>().HoveredOver();
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (UILookingAt[0].gameObject.GetComponent<ICombatSelectable>() != null)
                {
                    UILookingAt[0].gameObject.GetComponent<ICombatSelectable>().ClickedOn();
                }
            }
        }
        if (LookingAt !=null)
        {
            if(PointingAt() != LookingAt)
            {
                if(LookingAt.GetComponent<ICombatSelectable>() != null)
                {
                    LookingAt.GetComponent<ICombatSelectable>().Default();
                }
            }
        }
        //Raycast from camera to gameworld
        LookingAt = PointingAt();
        if (LookingAt != null)
        {
            if (LookingAt.GetComponent<ICombatSelectable>() != null)
            {
                LookingAt.GetComponent<ICombatSelectable>().HoveredOver();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (LookingAt.GetComponent<ICombatSelectable>() != null)
                {
                    LookingAt.GetComponent<ICombatSelectable>().ClickedOn();
                }
            }
        }
        if (!GameObject.FindObjectOfType<CombatManager>().targeting && GameObject.FindObjectOfType<CombatManager>().playerTurn && !GameObject.FindObjectOfType<CombatManager>().friendlyTargeting)
        {
            if (CurrentCardSelection != null)
            {
                selectedCardUI.SetActive(true);
            }
            else
            {
                selectedCardUI.SetActive(false);
            }
        }
        else if(GameObject.FindObjectOfType<CombatManager>().targeting)
        {
            selectedCardUI.SetActive(false);
        }
        
        
    }
    
    public GameObject PointingAt()
    {
        Ray ray = playerView.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject go = hitInfo.collider.gameObject;
            if (go != null)
            {
                return go;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }


    }

    public List<RaycastResult> UIPointingAt()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);

        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        return results;
    }

    public void DisplaySelectedCard(BaseCard aCard)
    {
        CurrentCardSelection = aCard;
        selectCardObject.myCard = aCard;

    }

    public void ReturnView()
    {
        CurrentCardSelection = null;
    }

    //takes in a list of cards and will populated the player's view with them
    public void PopulateHand(List<BaseCard> unitHand)
    {


        if (cardRowObjects.Count > 0)
        {
            foreach (GameObject cardObject in cardRowObjects)
            {
                Destroy(cardObject);
            }
            cardRowObjects.Clear();
        }
        foreach (BaseCard card in unitHand)
        {
            
            GameObject tempCard = Instantiate(cardObject, cardRowUI.transform);
            tempCard.GetComponent<CardObject>().myCard = card;

            cardRowObjects.Add(tempCard);
        }
    }
    
   
}
