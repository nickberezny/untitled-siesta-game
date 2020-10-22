using System;
using System.Collections.Generic;
using UnityEngine;


public class Spider : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float downSpeed;
    [SerializeField] GameObject web;

    private List<float> limits = new List<float>();
    private float direction = 1;
    private bool deleted = false;

    protected override void Awake()
    {
        int i = 0;

        base.Awake();
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            if(t.gameObject != this.gameObject)
            {
                limits.Add(t.position.y);
            }
        }

        limits.Sort();

        Debug.Log(limits[0] + "," + limits[1]);

    }protected override void deleteObject()
    {
        Destroy(web);
        deleted = true;
    }

    private void FixedUpdate()
    {
        float dy = direction * speed;
        if (direction == -1) dy = dy*downSpeed;

        transform.position = transform.position + new Vector3(0, dy, 0);

        if(direction == 1 && transform.position.y > limits[1])
        {
            direction = -1;
        }
        else if(direction == -1 && transform.position.y < limits[0])
        {
            direction = 1;
        }

        if(!deleted)
        {
            web.transform.localScale = new Vector3(0.01f, (limits[1] - transform.position.y), 0.01f);
            web.transform.position = new Vector3(web.transform.position.x, limits[1] + ((transform.position.y - limits[1])/2), web.transform.position.z);

        }

    }

}
