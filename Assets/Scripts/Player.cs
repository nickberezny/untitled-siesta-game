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
    [SerializeField] Animator bed;
    CircleCollider2D coll;

    private float y_offset = 0.4f;
    private float fallAccel = 0.003f;
    private float jumpVel = 0.1f;
    public bool input = false;
    private bool turbo = false;
    private bool moveLeft;
    private bool moveRight;
    private bool switchingBranch = false;
    private bool scheduleFall = false;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        menu.enabled = false;
        turbo = true;
        speed = speed * 2;
        Time.timeScale = 2;

    }
    private void Update()
    {
        if (scheduleFall) fallDown();
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
                    speed = speed / 2;
                }
                else
                {
                    turbo = true;
                    speed = speed * 2;
                    Time.timeScale = 2;
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

        if (transform.position.y < -20) forceDeath();
    }

    public void forceDeath()
    {
        frontLegs.SetBool("hit", true);
        input = false;
        StartCoroutine(fall(-20, 2));
        Debug.Log("Dead!");
    }

    public void Win()
    {
        backLegs.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        frontLegs.SetBool("MoveUp", true);
        StartCoroutine(moveUp(transform.position.y + 1, true));
    }


    public void fallDown()
    {
        if(switchingBranch)
        {
            scheduleFall = true;
            return;
        }

       scheduleFall = false;
        coll.enabled = false;
        frontLegs.SetBool("hit", true);
       input = false;
       int newY = path.nearestBranchBelow(transform.position.x, Mathf.RoundToInt(transform.position.y + y_offset));
        if (switchingBranch) scheduleFall = true;
       if(newY < 0)
       {
            // transform.position = transform.position + new Vector3(0, newY, 0);
            StartCoroutine(fall(transform.position.y + newY));
            
       }
       else
       {
            StartCoroutine(fall(-20, 2));
            Debug.Log("Dead!");
            
        }
    }

    private void switchBranch(int dir)
    {
        input = false;
        //check if branch is avail
        if (path.isBranchReachable(transform.position.x , Mathf.RoundToInt(transform.position.y + y_offset),dir))
        {
            switchingBranch = true;
            //start subroutine, cancel horizontal move
            if (dir < 0)
            {
                frontLegs.SetBool("MoveDown", true);
                StartCoroutine(fall(transform.position.y + dir, 1));
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
        else
        {
            input = true;
        }
        

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

    IEnumerator fall(float y, int options = 0)
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
        
        frontLegs.SetBool("MoveDown", false);
        if (options == 0)
        {
            frontLegs.SetBool("hit", false);
            StartCoroutine(resetCollider());
        }
        if (options == 2) menu.enabled = true;
        input = true;
        switchingBranch = false;

    }

    IEnumerator resetCollider()
    {
        yield return new WaitForSeconds(1f);
        coll.enabled = true;
    }

    IEnumerator moveUp(float y, bool win = false)
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
        switchingBranch = false;

        if (!win)
        {
            frontLegs.SetBool("MoveUp", false);
            backLegs.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            bed.SetBool("win", true);
            Destroy(this.gameObject);
        }



       }


}
