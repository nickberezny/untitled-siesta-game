using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMotor playerMotor;

    private bool[] keys = new bool[4];
    private bool switchKey;
    private bool timeKey;

    private int[] order = { 0, 1, 2, 3 };
    private int _dir = 1;
    private int _index = 0;

    void Update()
    {
        keys[0] = Input.GetKeyDown("a");
        keys[1] = Input.GetKeyDown("s");
        keys[2] = Input.GetKeyDown("d");
        keys[3] = Input.GetKeyDown("f");

        switchKey = Input.GetKeyDown("space");
        timeKey = Input.GetKeyDown("enter");

        if(switchKey == true)
        {
            //switch branch
        }

        if(timeKey == true)
        {
            //switch time scale
        }

        if (keys[order[getOrder(_index, _dir)]] == true)
        {
            //move in dir
            playerMotor.trySetState(PlayerMotor.playerState.moving);
            _index = clampIndex(_index + _dir);
        }
        else if (keys[order[getOrder(_index, -_dir)]] == true)
        {
            if(playerMotor.state != PlayerMotor.playerState.waiting)
            {
                //prep reverse
                playerMotor.trySetState(PlayerMotor.playerState.preparingToReverse);
            }
            
        }
        else if (keys[order[getOrder(_index, 2)]] == true)
        {
            if (playerMotor.state == PlayerMotor.playerState.preparingToReverse)
            {
                //reverse
                
                _dir = -_dir;
                playerMotor.setDir(_dir);
                playerMotor.trySetState(PlayerMotor.playerState.reversing);
                _index = clampIndex(_index+2);
            }
            else
            {
                playerMotor.trySetState(PlayerMotor.playerState.waiting);
                Debug.Log("not preparing....");
            }
            
        }
        else if(keys[order[_index]] == true)
        {
            playerMotor.trySetState(PlayerMotor.playerState.waiting);
        }
        else
        {
            //playerMotor.trySetState(PlayerMotor.playerState.notMoving);
        }

    }

    private int getOrder(int index, int value)
    {
        index = clampIndex(index + value);
        return order[index];
    }

    private int clampIndex(int Index)
    {
        while (Index > 3) Index = Index - 4;
        while (Index < 0) Index = Index + 4;
        return Index;
    }


}
