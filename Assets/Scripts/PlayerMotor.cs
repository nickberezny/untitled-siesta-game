using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float displacementPerClick;

    private GameObject _player;
    private int _dir = 1;
    private bool newState = false;

    private const float dt = 0.01f;
    

    public playerState state { get; private set;}


    private void Awake()
    {
        _player = this.gameObject;
    }

    public void trySetState(playerState setValue)
    {
        Debug.Log(setValue);
        newState = true;
        state = setValue;
        
        if(state == playerState.moving)
        {
            StopAllCoroutines();
            StartCoroutine(movePlayer(_dir, displacementPerClick));
        }
    }

    public void setDir(int dir)
    {
        _dir = dir;
    }

    public void switchBranch()
    {

    }

   
    private IEnumerator movePlayer(int dir, float displacement)
    {
        float totalDisplacement = 0f;
        float dx = speed * dt;
        while (Mathf.Abs(totalDisplacement) < displacement)
        {
            totalDisplacement += dir*dx;
            _player.transform.position = _player.transform.position + new Vector3(dir * dx, 0f, 0f);
            yield return new WaitForSeconds(dt);

        }
        
    }


    public enum playerState
    {
        notMoving,
        moving,
        preparingToReverse,
        reversing,
        waiting

    }



}
