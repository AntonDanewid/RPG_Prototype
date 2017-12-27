using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Grid : MonoBehaviour {

    Node[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    public bool onlyDisplayPathGizmos;

    public LayerMask unWalkableMask;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public Transform player;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        CreateGrid();
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if(grid != null && onlyDisplayPathGizmos)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
        
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-0.1f));
            }
        }
    }

    /*Convert a world position in unity, into a grid position that can be handled by the algorithm*/
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];


    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x , y);
            }
        }
    }


    /*Check every neighbour for a node in the grid*/
    public List<Node> getNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for(int x = -1; x <= 1; x ++ )
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue; // Node at x= 0 and y= 0 is the current node. Skip this iteration
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }

        }
        return neighbours;
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }


}
