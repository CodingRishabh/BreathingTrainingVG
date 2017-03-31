/*
 * Author: Joel Prakash
*/

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    Vector3 offset;

    // Use this for initialization
    void Start () {
        // Store the difference between the camera's position and the player's position
        offset = transform.position - player.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Gets executed only after all functions in Update() 
    // Enables Camera to move only after Player makes a move first.
    void LateUpdate()
    {
        // Makes the camera maintain the same distance between it and the player, while following the player around 
        transform.position = player.transform.position + offset;
    }
}
