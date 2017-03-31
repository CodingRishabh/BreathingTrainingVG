/*
 Authored by Vikramaditya Kumar
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class tilemanager1 : MonoBehaviour {


	public GameObject[] titlePrefabs;
	public float spawninzdirection = 500.0f;
	public float floorlength = 400.0f;
	public int nooftilesonscreen = 1;
	public float safezone = 300.0f;
	private Transform playertransform;
	private List<GameObject> floortilesactive;

	// Use this for initialization
	void Start () {
		floortilesactive = new List<GameObject>();
		playertransform = GameObject.FindGameObjectWithTag ("Player").transform;
		for (int i = 0; i < nooftilesonscreen; i++)
		{
			spawnfloor (); 
		}
	}

      
	// Update is called once per frame
	void Update () {
		
		if(playertransform.position.z - safezone > (spawninzdirection - nooftilesonscreen * floorlength ))
		{
			
			spawnfloor();
			deletefloor();
		}
	}

	private void spawnfloor(int prefabindex = -1)
	{
		
		GameObject tilenew;
		tilenew = Instantiate (titlePrefabs [0]) as GameObject;
		tilenew.transform.SetParent (transform);
		tilenew.transform.localScale += new Vector3 (5.0F,0,5.0F);
		tilenew.transform.position = (Vector3.forward * spawninzdirection);
		spawninzdirection += floorlength;
		floortilesactive.Add (tilenew);
	}

	private void deletefloor()
	{
		Destroy (floortilesactive [0]);
		floortilesactive.RemoveAt (0);
	}
}
