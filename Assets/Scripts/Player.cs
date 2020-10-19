using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] float speed;

    private float y_offset = 0.4f;

    private void Update()
    {
        if (Input.GetKey("up")) switchBranch(1);
        if(Input.GetKey("down")) switchBranch(-1);
        if (Input.GetKey("right")) move(1);
        if (Input.GetKey("left")) move(-1);
    }

    private void switchBranch(int dir)
    {
        //check if branch is avail
        if (path.isBranchReachable(transform.position.x + dir * speed, Mathf.RoundToInt(transform.position.y + y_offset),dir))
        {
            //start subroutine, cancel horizontal move
            transform.position = transform.position + new Vector3(0, dir, 0);
        }

    }

    private void move(int dir)
    {
        if(path.isPositionInBound(transform.position.x + dir*speed, Mathf.RoundToInt(transform.position.y + y_offset)))
        {
            transform.position = transform.position + new Vector3(dir * speed, 0, 0);
        }

    }
}
