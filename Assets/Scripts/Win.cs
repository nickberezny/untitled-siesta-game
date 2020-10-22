﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] string NextLevel;
    [SerializeField] LevelManager levelManager;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision w/ " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //win
            //animation
            //next level
            levelManager.nextLevel(NextLevel);
        }
    }
}
