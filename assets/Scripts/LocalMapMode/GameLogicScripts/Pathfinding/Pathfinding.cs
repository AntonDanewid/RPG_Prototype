using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Pathfinding : MonoBehaviour {

    PathManager pathManager;

    Grid grid;
    void Awake()
    {
        pathManager = GetComponent<PathManager>();
        grid = GetComponent<Grid>();
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 endPos)
    {
        Vector3[] wayPoints = new Vector3[0];
        bool pathFound = false;
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node endNode = grid.NodeFromWorldPoint(endPos);

        if (startNode.walkable && endNode.walkable)
        {



            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);
                if (currentNode == endNode)
                {
                    pathFound = true;
                    break;
                }
                foreach (Node neighbour in grid.getNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue; // If the node is blocked or it has already been checked, move on
                    }
                    int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = getDistance(neighbour, endNode);
                        neighbour.parent = currentNode;
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                            openSet.UpdateItem(neighbour);

                        }
                    }
                }
            }
            yield return null;
            if (pathFound)
            {
                wayPoints = retracePath(startNode, endNode);

            }
        }
        pathManager.FinishedProcessingPath(wayPoints, pathFound);

    }

    public void StartFindPath(Vector3 start, Vector3 end)
    {
        StartCoroutine(FindPath(start, end));
    }

    Vector3[] retracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] wayponints = simpifyPath(path);
        path.Reverse();
        Array.Reverse(wayponints);
        return wayponints;

    }

    Vector3[] simpifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionnew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionnew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionnew;
        }

        return waypoints.ToArray();
    }

    int getDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);
        if(distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);

        } else
        {
            return 14 * distX + 10 * (distY - distX);

        }
    }
}
