using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool player;

    public int attack;
    public int maxHealth;
    public int shield;
    public bool attacking;
    public Transform target;
    public Transform startPosition;
    public Vector3 startingPosition;
    public bool finishAttack;
    public Transform hitPosition;


    public void updateHealth(int health) {
        if (shield > 0 && health < 0)
        {
            updateShield(health);
            return;
        }

        gameObject.GetComponentInChildren<HealthBar>().UpdateHealth(health);
        if (gameObject.GetComponentInChildren<HealthBar>().currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void Update()
    {
        if (attacking)
        {
            if (target != null) 
            {
                transform.Translate((target.position - transform.position) * 2 * Time.deltaTime);

            }
            
        }
        else
        {
            transform.Translate((startPosition.position - transform.position) * 2 * Time.deltaTime);
        }
        if (finishAttack && Vector3.Distance(startPosition.position, transform.position)<5)
        {
            finishAttack = false;
            GetComponent<Animator>().SetTrigger("idle");
        }

        

    }

    public void returntoStart(){
        finishAttack = true;
        attacking = false;   
    }
    public void Attack()
    {
        

        Character[] characs = FindObjectsOfType<Character>();
        foreach (Character C in characs)
        {
            if (C.player == false)
            {
                attacking = true;
                target = C.hitPosition;
                GetComponent<Animator>().SetTrigger("exception");
                
                break;
            }
        }
    }


    public void applyDamage()
    {
        target.GetComponentInParent<Character>().updateHealth(-attack);
    }
    public void updateShield(int shield)
    {
        this.shield += shield;
        if (this.shield < 0)
        {
            updateHealth(this.shield);
            this.shield = 0;
            
        }
    }

    public int getCurrentHealth() 
    {
        return GetComponentInChildren<HealthBar>().currentHealth;
    }


}
