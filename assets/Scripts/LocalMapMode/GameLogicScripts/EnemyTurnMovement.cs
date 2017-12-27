using UnityEngine;
using System.Collections;

public class EnemyTurnMovement : MonoBehaviour {

    private Transform player;
    private Unit movement;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movement = GetComponent<Unit>();

    }

    // Update is called once per frame
    void Update () {
        movement.move(transform.position, player.transform.position);
    }
}
