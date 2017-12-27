using UnityEngine;
using System.Collections;

public class TurnHandler : MonoBehaviour {

	
    void Start()
    {
        //Get every actor in area 
        //Put them in circular list
        //Give turn to first actor that has priorty (stats or whatnot) 

    }

    void TurnDone()
    {
        //give turn rights to next in line
        //if all enemies are dead, turn over;
        //destroy 
        //give back RT controll to player

    }

    void CombatEnd()
    {
        enabled = false;
    }

}
