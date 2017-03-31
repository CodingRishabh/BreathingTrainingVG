/*
 Authored by Vikramaditya Kumar
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;

public class Playerc : MonoBehaviour {

	public CharacterController x;
	private float speed = 20.0f;
	private float hori = 12.0f;
	private float verti = 10.0f;
	public GameObject[] titlePrefabs;
	public Vector3 mover;
	public GameObject inst;
	public GameObject gamestartedtext;


	void Start ()
	{
		x = GetComponent<CharacterController> ();
		inst = GameObject.FindGameObjectWithTag ("insturction");
		gamestartedtext = GameObject.FindGameObjectWithTag ("started");
		gamestartedtext.SetActive (false);

	}
	void Update ()
	{
		// x - Left and Right
		mover.x = Input.GetAxisRaw ("Horizontal") * hori;
		// Y - Up and down
		mover.y = Input.GetAxisRaw ("Vertical") * verti;
		// z - axis
		mover.z = speed;
		x.Move (mover * Time.deltaTime * hori);


		if (x.transform.position.z > 14000.0f) {
			inst.SetActive (false);
		}
		if (x.transform.position.y < 500.0f) {
			inst.SetActive (false);
		}
		if (x.transform.position.y < 10) {
			if (x.transform.position.y < 1000) {
				gamestartedtext.SetActive (true);
			}
		}
	}

}
 



