using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class pause : MonoBehaviour {


	public string pausegame;

	public void pausingthegame()
	{
		Application.LoadLevel (pausegame);
	}

}