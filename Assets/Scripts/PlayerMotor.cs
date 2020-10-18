using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    [SerializeField] float speed;

    public Vector2 _direction;
    public Vector2 _limits;

    private GameObject _player;
    private float _lineProgress;
    

    private void Awake()
    {
        _player = this.gameObject;

        Path.Instance.reachedNewDestination(1);
        _lineProgress = 0f;

    }


    public void movePlayer(Vector2 dir, bool fwd)
    {

        //Debug.Log("Move player: " + Vector2.Distance(_direction, dir));

        if (Vector2.Distance(new Vector2(Mathf.Abs(_direction.x), Mathf.Abs(_direction.y)), dir) < 0.1)
        {
            
            if ((_lineProgress > 1.0f & sumVector(_direction) > 0 & fwd ) || ( _lineProgress > 1.0f & sumVector(_direction)<0 & !fwd ))
            {
                transitionLines(true);
                _lineProgress = 0f;
            }
            else if((_lineProgress < 0.0f & sumVector(_direction) < 0 & fwd) || (_lineProgress < 0.0f & sumVector(_direction) > 0 & !fwd))
            {
                transitionLines(false);
                _lineProgress = 0f;
            }
            else if (fwd)
            {
                //move fwd
                move(1);
                Debug.Log("move fwd");
            }
            else if (!fwd)
            {
                //move bkwd
                Debug.Log("Move bckwd");
                move(-1);
            }
        }
        else
        {
            //check for another branch...
        }
    }

    private void transitionLines(bool fwd)
    {
        if (fwd) Path.Instance.reachedNewDestination(1);
        else Path.Instance.reachedNewDestination(-1);
    }

    private void move(int fwd)
    {
        
        _player.transform.position = _player.transform.position + new Vector3(fwd*Mathf.Abs(_direction.x * speed), fwd* Mathf.Abs(_direction.y) * speed, 0f);
        _lineProgress = sumVector((Vector2.Scale(new Vector2(_player.transform.position.x, _player.transform.position.y), _direction) - _limits.x *  _direction)) / (_limits.y *  _direction - _limits.x * _direction).magnitude;
        Debug.Log(_lineProgress);
    }

    private float sumVector(Vector2 v)
    {
        Debug.Log(v.x +","+ v.y);
        return v.x + v.y; 
    }




}
