using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int healthDecrement;
    [SerializeField] AudioSource audioSource;

    private float fallAccel = 0.002f;
    public bool falling = false;

    protected virtual void Awake()
    {
        Debug.Log("enemy awake");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision w/ " + collision.gameObject.name);
        if(collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Health>().changeHealth(-healthDecrement);
            collision.gameObject.GetComponent<Player>().fallDown();
            //set animation
            //Destroy(this.gameObject);
            audioSource.Play();
            deleteObject();
            //StartCoroutine(fallDown());
        }
    }

    protected virtual void deleteObject()
    {
        return;
    }

    IEnumerator fallDown()
    {
        falling = true;
        float g = 0;

        while (transform.position.y > -20)
        {
            g += fallAccel;
            transform.position = transform.position - new Vector3(0, g, 0);
            yield return new WaitForSeconds(0.01f);
        }

    }
}
