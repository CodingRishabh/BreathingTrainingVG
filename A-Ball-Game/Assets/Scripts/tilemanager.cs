using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class tilemanager : MonoBehaviour {


	public GameObject[] titlePrefabs;
	public float spawninzdirection = 100.0f;
	public float floorlength = 10.0f;
	public int nooftilesonscreen = 20;
	public float safezone = 15.0f;
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
		if(playertransform.position.z + safezone > (spawninzdirection - nooftilesonscreen * floorlength ))
		{
			spawnfloor();
            if (floortilesactive.Count == 20)
            {
                deletefloor();
            }
		}
	}

	private void spawnfloor(int prefabindex = -1)
	{
		
		GameObject tilenew;
		tilenew = Instantiate (titlePrefabs [0]) as GameObject;
		tilenew.transform.SetParent (transform);
		tilenew.transform.position = (transform.position + (Vector3.forward * spawninzdirection));
		spawninzdirection += floorlength;
		floortilesactive.Add (tilenew);
	}

	private void deletefloor()
	{
		Destroy (floortilesactive [0]);
		floortilesactive.RemoveAt (0);
	}
}
