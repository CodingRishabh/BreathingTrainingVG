/*
 * Author: Joel Prakash
*/

using UnityEngine;
using System.Collections;

public class TrafficLightController : MonoBehaviour {

    public GameObject player;
    Vector3 offset;

    // Use this for initialization
    void Start () {
        // Store the difference between the Traffic Light's position and the player's position
        offset = transform.position - player.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Gets executed only after all functions in Update() 
    // Enables Traffic Light to move only after Player makes a move first.
    void LateUpdate()
    {
        // Makes the Traffic Light maintain the same distance between it and the player, while following the player around 
        Vector3 newVector = new Vector3(transform.position.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
        transform.position = newVector;
    }
}
