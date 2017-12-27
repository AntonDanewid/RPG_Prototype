using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public float speed;
    protected float distance = 1;
    protected float arrowSpeed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ZoomCamera();
        RotateCamera();
    }

    private void RotateCamera()
    {

        if (Input.GetKey(KeyCode.RightArrow)) CameraRotate(false);
        if (Input.GetKey(KeyCode.LeftArrow)) CameraRotate(true);
        if (Input.GetKey(KeyCode.UpArrow)) ZoomWithArrows(false);
        if (Input.GetKey(KeyCode.DownArrow)) ZoomWithArrows(true);
    }

    protected void CameraRotate(bool bLeft)
    {
        float step = arrowSpeed * Time.deltaTime;
        float fOrbitCircumfrance = 2F * distance * Mathf.PI;
        float fDistanceDegrees = (arrowSpeed / fOrbitCircumfrance) * 360;
        float fDistanceRadians = (arrowSpeed / fOrbitCircumfrance) * 2 * Mathf.PI;
        if (bLeft)
        {
            transform.RotateAround(transform.position, Vector3.up, -fDistanceRadians);
        }
        else
            transform.RotateAround(transform.position, Vector3.up, fDistanceRadians);
    }

    protected void ZoomWithArrows(bool bOut)
    {
        if (bOut) transform.Translate(0, 0, -arrowSpeed / 10, Space.Self);
        else
            transform.Translate(0, 0, arrowSpeed / 10, Space.Self);
    }


    private void MoveCamera()
    {
        float deltaX = 0;
        float deltaY = 0;
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        if (x < 20)
        {
            deltaX = -1 * speed;
        }
        else if (x > Screen.width - 20)
        {
            deltaX = 1 * speed;
            Debug.Log("Flyttar screen width");
        }

        if (y < 20)
        {
            deltaY = -1 * speed;
        }
        else if (y > Screen.height - 20)
        {
            deltaY = 1 * speed;
        }
        transform.position = new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z + deltaY);
    }



    private void ZoomCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
    }

}