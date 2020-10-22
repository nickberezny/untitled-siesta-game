using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] HealthBar healthBar;

    private int _maxHealth;

    public void Awake()
    {
        _maxHealth = health;
        
    }

    public void StartGame()
    {
        StartCoroutine(changeHealth(1f));
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

        healthBar.barDisplay = health / 100f; ;

    }

    IEnumerator changeHealth(float dt)
    {
        while(health > 0)
        {
            changeHealth(-1);
            yield return new WaitForSeconds(dt);
        }
    }


}

