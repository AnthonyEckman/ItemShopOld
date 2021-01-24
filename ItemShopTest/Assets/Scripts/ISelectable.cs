using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
     void Selected();

    void UnSelected();

    void ClickedOn();

    void RecieveItem(BaseItem item);
   
}
