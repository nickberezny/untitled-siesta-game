using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMotor _playerMotor;
    private Vector2 dir;
    private bool fwd;

    private void Update()
    {
        bool input = false; 

        if(Input.GetKey("up"))
        {
            dir = new Vector2(0, 1);
            fwd = true;
            input = true;
        }
        if(Input.GetKey("down"))
        {
            dir = new Vector2(0, 1);
            fwd = false;
            input = true;
        }
        if(Input.GetKey("right"))
        {
            dir = new Vector2(1, 0);
            fwd = true;
            input = true;
        }
        if(Input.GetKey("left"))
        {
            dir = new Vector2(1, 0);
            fwd = false;
            input = true;
        }

        if (input) _playerMotor.movePlayer(dir, fwd);

    }
}
