using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    private GameObject Selected;
    



    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        Selected = PointingAt();

        if (Selected != null && Input.GetKeyDown(KeyCode.Space))
            {
            Selected.GetComponent<ISelectable>().ClickedOn();
            }





    }


    //function checks infront of the player for items that can be interacted with.
    public GameObject PointingAt()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * 2, out hit))
        {
            //selection range from player
            if (hit.distance < 2)
            {
                GameObject go = hit.collider.gameObject;
                if (go.GetComponent<ISelectable>() != null)
                {
                    if (Selected != null)
                    {
                        Selected.GetComponent<ISelectable>().UnSelected();
                    }
                    go.GetComponent<ISelectable>().Selected();
                    return go;
                }
                else
                {
                    if (Selected != null)
                    {
                        Selected.GetComponent<ISelectable>().UnSelected();
                    }
                    return null;
                }
            }

        }
        if (Selected != null)
        {
            Selected.GetComponent<ISelectable>().UnSelected();
        }
        
         return null;
        
    }

}
