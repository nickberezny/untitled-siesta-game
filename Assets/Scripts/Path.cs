using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Path : MonoBehaviour
{
    public static Path Instance { private set; get; }

    [SerializeField] PlayerMotor _playerMotor;

    private List<Vector3> positions;

    private int currentIndex = 0;
    private Vector3 nextPosition;
    private Vector3 direction;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        positions = new List<Vector3>();
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            if(t.gameObject != this.gameObject)
            {
                //Debug.Log(t.position);
                positions.Add(t.position);
                
            } 
        }

        //reachedNewDestination();
    }

    public void reachedNewDestination(int step)
    {
        Debug.Log(currentIndex + step + "," + positions.Count);
        if (currentIndex + step == positions.Count)
        {
            Debug.Log("win");
        }
        else if(currentIndex + step <= 0)
        {
            Debug.Log("at start");
        }
        else 
        {
            getNewLine(currentIndex + step);
            //animate to new position, wait
            //
        }

    }

    private void getNewLine(int index)
    {
        Debug.Log(index);
        if (index >= positions.Count) return;

        nextPosition = positions[index];
        currentIndex = index;

        direction = (positions[index] - positions[index - 1]).normalized;
        _playerMotor._direction = direction;
        _playerMotor._limits = new Vector2(Vector3.Dot(positions[index - 1] , absVector3(direction)), Vector3.Dot(positions[index] , absVector3(direction)));

        //Debug.Log(direction);
        //Debug.Log(_playerMotor._limits);

    }

    private Vector3 absVector3(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

 




}
