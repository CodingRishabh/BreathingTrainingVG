using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour {


	public string playgame;
	public string score;

	public void startgame()
	{
		Application.LoadLevel (playgame);
       /* GameObject player = GameObject.Find("Jet");
        PlayerController playerControllerScript = player.GetComponent<PlayerController>();
        playerControllerScript.scoreText.gameObject.SetActive(true);*/
    }

	public void quitgame()
	{
		Application.Quit ();
	}
	public void checkscore()
	{
		Application.LoadLevel(score);
	}

}