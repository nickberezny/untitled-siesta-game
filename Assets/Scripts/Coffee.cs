using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] int healthGain;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(healthGain > 0) collision.gameObject.GetComponent<Health>().changeHealth(healthGain);
            Destroy(this.gameObject);
        }
    }
}

