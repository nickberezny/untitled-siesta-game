using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;

    private int _maxHealth;

    public void Awake()
    {
        _maxHealth = health;
    }

    public void changeHealth(int change)
    {
        health += change;
        if (health > _maxHealth) health = _maxHealth;

        if(health <= 0)
        {
            //die
        }

        //update health bar

    }
}
