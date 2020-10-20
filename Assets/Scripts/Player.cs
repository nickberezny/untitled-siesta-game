using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;

    private float y_offset = 0.4f;
    private float fallAccel = 0.01f;
    private bool input = true;

    private void Update()
    {
        if(input)
        {
            if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) switchBranch(1);
            if (Input.GetKeyDown("down") || Input.GetKeyDown("s")) switchBranch(-1);
            if (Input.GetKey("right") || Input.GetKey("d")) move(1);
            if (Input.GetKey("left") || Input.GetKey("a")) move(-1);

            if (Input.GetKey("space"))
            {
                //turbo mode
            }
        }
        
       
    }

    public void fallDown()
    {
        
       int newY = path.nearestBranchBelow(transform.position.x, Mathf.RoundToInt(transform.position.y + y_offset));
       if(newY < 0)
       {
            // transform.position = transform.position + new Vector3(0, newY, 0);
            StartCoroutine(fallDown(transform.position.y + newY));
       }
       else
       {
           Debug.Log("Dead!");
       }
    }

    private void switchBranch(int dir)
    {
        input = false;
        //check if branch is avail
        if (path.isBranchReachable(transform.position.x , Mathf.RoundToInt(transform.position.y + y_offset),dir))
        {
            //start subroutine, cancel horizontal move
            transform.position = transform.position + new Vector3(0, dir, 0);
        }
        input = true;

    }

    private void move(int dir)
    {
        if(path.isPositionInBound(transform.position.x + dir*speed, Mathf.RoundToInt(transform.position.y + y_offset)))
        {
            transform.position = transform.position + new Vector3(dir * speed, 0, 0);
        }

    }

    IEnumerator fallDown(float y)
    {
        input = false;
        float g = 0;

        while(transform.position.y > y)
        {
            g += fallAccel;
            transform.position = transform.position - new Vector3(0, g, 0);
            yield return new WaitForSeconds(0.1f);
        }

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        input = true;
    }

}
