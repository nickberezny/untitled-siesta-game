using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;

    private float y_offset = 0.4f;
    private float fallAccel = 0.001f;
    public bool input = false;
    private bool turbo = false;
    private bool moveLeft;
    private bool moveRight;

    private void Update()
    {
        if(input)
        {
            if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) switchBranch(1);
            if (Input.GetKeyDown("down") || Input.GetKeyDown("s")) switchBranch(-1);
            if (Input.GetKey("right") || Input.GetKey("d")) move(1);
            if (Input.GetKey("left") || Input.GetKey("a")) move(-1);

            if (Input.GetKeyDown("space"))
            {
                //turbo mode
                if (turbo)
                {
                    turbo = false;
                    Time.timeScale = 1;
                    speed = speed / 3;
                }
                else
                {
                    turbo = true;
                    speed = speed * 3;
                    Time.timeScale = 3;
                }
            }
        }
        
       
    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            transform.position = transform.position + new Vector3(speed, 0, 0);
            moveRight = false;
        }
        if (moveLeft)
        {
            transform.position = transform.position + new Vector3(-speed, 0, 0);
            moveLeft = false;
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
            StartCoroutine(fallDown(-10));
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
            //transform.position = transform.position + new Vector3(dir * speed, 0, 0);
            if(dir == 1) moveRight = true;
            if (dir == -1) moveLeft = true;
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
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        input = true;
    }

}
