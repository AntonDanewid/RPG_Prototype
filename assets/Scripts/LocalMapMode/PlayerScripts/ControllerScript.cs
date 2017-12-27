using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {



    private Vector3 wayPoint;
    private Unit movement;
    static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);



    // Use this for initialization
    void Start () {
        movement = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update () {
	
        if(Input.GetMouseButton(0))
        {
            movement.move(transform.position, GetMousePositionOnXZPlane()); 
        }
	}


    public static Vector3 GetMousePositionOnXZPlane()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (XZPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            //Just double check to ensure the y position is exactly zero
            hitPoint.y = 0.5f;
            return hitPoint;
        }
        return Vector3.zero;
    }





}
