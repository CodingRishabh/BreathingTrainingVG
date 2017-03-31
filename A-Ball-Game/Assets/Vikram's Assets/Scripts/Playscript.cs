using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Playscript : MonoBehaviour {

	public void loadingthescene(string scene) 
	{
		SceneManager.LoadScene (scene);

	}
}
