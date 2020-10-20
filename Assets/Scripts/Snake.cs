using System.Collections.Generic;
using UnityEngine;


public class Snake : Enemy
{
    [SerializeField] float speed;
    private List<float> limits = new List<float>();
    private float direction = 1;

    protected override void Awake()
    {
        int i = 0;

        base.Awake();
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            if(t.gameObject != this)
            {
                limits.Add(t.position.x);
            }
        }

        limits.Sort();

        Debug.Log(limits[0] + "," + limits[1]);

    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(direction * speed, 0, 0);

        if(direction == 1 && transform.position.x > limits[1])
        {
            direction = -1;
        }
        else if(direction == -1 && transform.position.x < limits[0])
        {
            direction = 1;
        }


    }

}
