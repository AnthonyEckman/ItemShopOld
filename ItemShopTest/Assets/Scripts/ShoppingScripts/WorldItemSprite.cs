using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemSprite : MonoBehaviour
{
    public Sprite mySprite;

    private void Update()
    {
        transform.Rotate(0,20 * Time.deltaTime, 0);
    }


}
