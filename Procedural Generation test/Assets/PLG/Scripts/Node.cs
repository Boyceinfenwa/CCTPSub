using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPos;

    public int gcost;
    public int hcost;

    public int gridX;
    public int gridY;

    public Node parent;
    public Node(bool _walklable, Vector3 _worldpos, int _gridX, int _gridY)
    {
        walkable = _walklable;
        worldPos = _worldpos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fcost
    {
        get
        {
            return gcost + hcost;
        }
    }
    
}
