using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    [SerializeField] int healthGain;
    [SerializeField] AudioSource audioSource;
    private bool Collected = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !Collected)
        {
            Collected = true;
            audioSource.Play();
            if(healthGain > 0) collision.gameObject.GetComponent<Health>().changeHealth(healthGain);
            GetComponent<SpriteRenderer>().enabled = false;
            //Destroy(this.gameObject);
        }
    }
}

