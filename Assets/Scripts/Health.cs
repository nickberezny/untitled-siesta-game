using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float healthDecayRate;
    [SerializeField] HealthBar healthBar;

    private int _maxHealth;
    private Player player;

    public void Awake()
    {
        _maxHealth = health;
        player = gameObject.GetComponent<Player>();
        
    }

    public void StartGame()
    {
        StartCoroutine(changeHealth(healthDecayRate));
    }

    public void changeHealth(int change)
    {
        health += change;
        if (health > _maxHealth) health = _maxHealth;

        //Debug.Log("New health:" + health);

        if (health <= 0)
        {
            //die, reset...
            player.forceDeath();
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

