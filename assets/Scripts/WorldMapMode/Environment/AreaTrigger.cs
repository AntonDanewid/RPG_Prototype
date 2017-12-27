using UnityEngine;
using System.Collections;

public class AreaTrigger : MonoBehaviour {

    private bool showButton = false;



    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            showButton = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("player"))
        {
            showButton = false;
        }
    }

    void OnGUI()
    {
        if (showButton)
        {
            if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
                Debug.Log("Clicked the button with text");
        }
    }

}
