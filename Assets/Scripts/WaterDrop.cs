using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    [SerializeField] int rate;
    [SerializeField] GameObject drop;

    float dt = 0f;
    private void FixedUpdate()
    {
        dt += Time.deltaTime;
        if(dt > rate)
        {
            dt -= rate;
            Instantiate(drop, transform);
        }
    }
}
