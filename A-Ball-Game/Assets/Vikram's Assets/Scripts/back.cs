using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class back : MonoBehaviour {


	public string menus;

	public void goback()
	{
		Application.LoadLevel(menus);
	}

}