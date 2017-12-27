using UnityEngine;
using System.Collections;
using System;

public class Node : IHeapItem<Node> 
{
    public int hCost;
    public int gCost;
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public Node parent;
    public int index;

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }
    

    /* Nice feature C#*/
    public int fCost
    {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }


    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}
