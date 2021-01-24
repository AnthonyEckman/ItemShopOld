using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CombatUnit myUnit;

    public Text text;

    
    public Image healthBar;

    public float number;

    private void Start()
    {
        
    }
    private void Update()
    {
        ChangeHealth();
        
    }

    private void ChangeHealth()
    {
        healthBar.fillAmount = (float)myUnit.CurrentHealth / (float)myUnit.MaxHealth;

        text.text = myUnit.CurrentHealth + "/" + myUnit.MaxHealth;
    }
}
