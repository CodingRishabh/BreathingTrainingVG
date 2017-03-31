/*
 * Author: Joel Prakash
*/

using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Rotate the object 15 degrees around x-axis, 30 degrees around y-axis and 45 degrees around z-axis
        transform.Rotate(new Vector3(30, 45, 60) * Time.deltaTime);
	
	}
}
