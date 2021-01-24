using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public CombatUnit myUnit;

    public Text text;


    public Image manaBar;

    public float number;

    private void Start()
    {

    }
    private void Update()
    {
        ChangeMana();

    }

    private void ChangeMana()
    {
        manaBar.fillAmount = (float)myUnit.CurrentStamina / (float)myUnit.MaxStamina;

        text.text = myUnit.MaxStamina + "/" + myUnit.MaxStamina;
    }
}
