using System.Collections.Generic;
using UnityEngine;


public class Spider : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float downSpeed;
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
                limits.Add(t.position.y);
            }
        }

        limits.Sort();

        Debug.Log(limits[0] + "," + limits[1]);

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


    }

}
