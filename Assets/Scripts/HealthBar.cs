using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barImage;
    public int maxHealth;
    public int currentHealth;
    private void Start()
    {   
        setMaxHealth(transform.parent.GetComponent<Character>().maxHealth);
    }

    public void Update()
    {
        barImage.fillAmount = (float) currentHealth/maxHealth;
        
    }

    public void setMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;


    }

    public void UpdateHealth(int health)
    {
        currentHealth += health;

    }
}
