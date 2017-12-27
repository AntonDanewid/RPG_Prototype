using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{


    public float speed;
    Vector3[] path;
    int targetIndex;
    public void move(Vector3 start, Vector3 end)
    {

        PathManager.RequestPath(start, end, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath"); 
        }
    }

    IEnumerator FollowPath()
    {
        if(path.Length > 0)
        {
            Debug.Log(path.Length);
            Vector3 currentWaypoint = path[0];
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                // transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z), speed * Time.deltaTime);

                yield return null;

            }
        }
      
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            Debug.Log("Är i gizmos");

            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

 
}