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

        //Debug.Log("New health:" + health);

        if (health <= 0)
        {
            //die, reset...
            //PlayerManager.Instance.spawnPlayer();
            health = _maxHealth;

        }

        //update health bar

    }


}

