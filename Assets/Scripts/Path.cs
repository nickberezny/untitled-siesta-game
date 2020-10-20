using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public int y;
    public int x1;
    public int x2;
}

public class Path : MonoBehaviour
{

    private List<Line> lines;
    

    private void Awake()
    {
        lines = new List<Line>();
        int i = 0;
        Line temp = new Line();

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.gameObject != this.gameObject)
            {
                if (i == 1)
                {
                    if(t.position.x > temp.x1) temp.x2 = Mathf.RoundToInt(t.position.x);
                    else
                    {
                        temp.x2 = temp.x1;
                        temp.x1 = Mathf.RoundToInt(t.position.x);
                    }
                    lines.Add(temp);
                    temp = new Line();
                    i = 0;
                }
                else
                {
                    temp.x1 = Mathf.RoundToInt(t.position.x);
                    temp.y = Mathf.RoundToInt(t.position.y);
                    i = 1;
                }
                    

            }
        }

    }

    public bool isPositionInBound(float x, int y)
    {
        foreach(Line line in lines)
        {
            if(line.y == y)
            {
                if (x <= line.x2 && x >= line.x1) return true;
            }
        }

        return false;
    }

    public bool isBranchReachable(float x, int y, int dir)
    {
        return isPositionInBound(x, y + dir);
    }

    public int nearestBranchBelow(float x, int y)
    {
        int nearest = -9999;
        foreach (Line line in lines)
        {
            Debug.Log(line.y + "," + nearest);
            if (line.y < y && x <= line.x2 && x >= line.x1)
            {
                if(y - line.y < y - nearest)
                {
                    nearest = line.y;
                }
            }

        }

        if (nearest == -9999) nearest = y;

        return nearest - y;
    }



}
