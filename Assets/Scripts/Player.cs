using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] Canvas menu;
    [SerializeField] Animator frontLegs;
    [SerializeField] Animator backLegs;
    CircleCollider2D coll;

    private float y_offset = 0.4f;
    private float fallAccel = 0.001f;
    private float jumpVel = 0.05f;
    public bool input = false;
    private bool turbo = false;
    private bool moveLeft;
    private bool moveRight;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        menu.enabled = false;
    }
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
            frontLegs.SetFloat("Speed", 1);
            backLegs.SetFloat("Speed", 1);
            moveRight = false;
        }
        else if (moveLeft)
        {
            transform.position = transform.position + new Vector3(-speed, 0, 0);
            frontLegs.SetFloat("Speed", -1);
            backLegs.SetFloat("Speed", -1);
            moveLeft = false;
        }
        else
        {
            backLegs.SetFloat("Speed", 0);
            frontLegs.SetFloat("Speed", 0);
        }
    }


    public void fallDown()
    {
       input = false;
       int newY = path.nearestBranchBelow(transform.position.x, Mathf.RoundToInt(transform.position.y + y_offset));
       if(newY < 0)
       {
            // transform.position = transform.position + new Vector3(0, newY, 0);
            StartCoroutine(fallDown(transform.position.y + newY));
       }
       else
       {
            StartCoroutine(fallDown(-20, 2));
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
            if (dir < 0)
            {
                frontLegs.SetBool("MoveDown", true);
                StartCoroutine(fallDown(transform.position.y + dir, 1));
                Debug.Log("moving down");
            }
            else
            {
                backLegs.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                frontLegs.SetBool("MoveUp", true);
                StartCoroutine(moveUp(transform.position.y + dir));
            }
            //transform.position = transform.position + new Vector3(0, dir, 0);
            
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

    IEnumerator fallDown(float y, int options = 0)
    {
        if (options == 0) coll.enabled = false;
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
        frontLegs.SetBool("MoveDown", false);
        if (options == 0) StartCoroutine(resetCollider());
        if (options == 2) menu.enabled = true;
    }

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(1f);
        coll.enabled = true;
    }

    IEnumerator moveUp(float y)
    {
        input = false;
        float g = jumpVel;

        while (transform.position.y < y)
        {
            g -= fallAccel;
            transform.position = transform.position + new Vector3(0, g, 0);
            yield return new WaitForSeconds(0.01f);
        }


        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        input = true;
        backLegs.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        frontLegs.SetBool("MoveUp", false);

    }


}
