/*
 * Author: Joel Prakash
*/
/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required to use "Text" object and other UI elements
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Linq;
using System.Diagnostics;

public class PlayerController : MonoBehaviour {
	
	public float movementSpeed = 800.0f; // public so we can change this value in the inspector
	public float jumpForce = 300.0f; // using the SPACE key
	public Text scoreText; // "scoreText"  will store our UI Text object
	public Text wintext; // Displays "You Win!" after collecting all cubes
	private int count = 0; // "count" will keep track of how many cubes we picked up
	public AudioSource audioFloat; // Audio file for floating up
	public AudioSource audioDrop; // Audio file for droping
	public AudioSource audioCollect; // Audio file for collecting cubes
	public AudioSource audioCalmOcean; // Audio file for background music "calm ocean"
	public AudioSource audioWin; // Audio file to play once you win the game
	public AudioSource audioStageStart; // Audio file to play at the begining of every stage
	private IAsyncResult currentAsyncResult;
	int diff_count=0;
	static int r_lung = 0;
	static int l_lung = 0;
	int temp = 0;
	int total = 0;
	int tempcalc=0;
	private UdpClient client = null;
	private IPEndPoint sender = null;
	private string mes = "";
	int realdiff;
	int diff_sum;
	int diff=0;
	int diff_avg;
	int currentdiff;
	int currentsum;
	int currentcount=0;
	int current_avg=0;
	int jumpcalc=0;
	int jumpsum=0;
	// static int count1 = 0;
	static string ip = "127.0.0.1";
	static int port = 6799;
	DateTime startTime;
	public CharacterController aircraft;
	public Vector3 movedirection;
	// Use this for initialization
	void Start () {



		aircraft = GetComponent<CharacterController>(); // Gets the player Info
		audioCalmOcean.Play();

	}



	public void recibir(System.IAsyncResult res)
	{

		// Waits for a receive from Begin till EndReceive is called
		byte[] bResp = client.EndReceive(res, ref sender);
		// Due to ReuseAddress parameter very fast close and reopen is allowed
		client.Close();
		count++;
		//Convert the data to a string
		mes = Encoding.UTF8.GetString(bResp); 

		// convert data to array
		//int[] bytesAsInts = Array.ConvertAll(bResp, c => (int)c);

		// mes = bytesAsInts[0] + ", "+ bytesAsInts[1];
		string[] subStrings = mes.Split(',');
		Int32.TryParse(subStrings[0], out l_lung);
		Int32.TryParse(subStrings[1], out r_lung);
		UnityEngine.Debug.Log(mes);
	}

	void OnGUI()
	{
		GUILayout.Label(""+count);
	}

	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup < 10f) {
			Time.timeScale = 0f;
			realdiff = Math.Abs(r_lung - l_lung);
			//jumpcalc = r_lung + l_lung;
			//	if (jumpcalc > tempcalc) {
			//		tempcalc = jumpcalc;
			//	}
			//realdiff = r_lung - l_lung;
			diff_sum += realdiff;
			if (diff_count != 0) {
				diff_avg = diff_sum / diff_count;
			}
			diff_count++;
		} else {
			Time.timeScale = 1f;
		}

		/* if (Input.GetKeyUp(KeyCode.Z)) { // If Z key is pressed
            jump();
        }
        else if (Input.GetKeyUp(KeyCode.X)) // If X key is pressed
        {
            drop();
        }


		if (client != null)
		{
			currentAsyncResult = null;
			client.Close();
		}

		// Binding the Socket to port
		client = new UdpClient();
		client.ExclusiveAddressUse = false;
		sender = new IPEndPoint(IPAddress.Parse(ip), port);
		client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
		client.Client.Bind(sender);
		// Binding to an IP Address
		currentAsyncResult  = client.BeginReceive(new System.AsyncCallback(recibir), sender);

	}

	void jump()
	{
		

	}

	void drop()
	{
		
	}

	// used for force physics controls, since these don't depend on a frame
	void FixedUpdate() // Infinite loop while we are in play mode
	{
		
		diff = Math.Abs(r_lung - l_lung);

		if (diff > diff_avg) {
			//jumped = false;
			if (r_lung > diff_avg + l_lung) 
			{
				movedirection = new Vector3(Input.GetAxis(-1, 0, 0)) * r_lung;
			} else if (r_lung < l_lung) {
				movedirection = new Vector3(Input.GetAxis(1, 0, 0)) * r_lung;
			}
		}

	}

	void OnTriggerEnter(Collider other) // When the ball enters a Trigger Collider
	{
		if(other.gameObject.tag == "PickUp") // If the object that was entered has the tag "PickUp"
		{
			audioCollect.Play();
			if (count >= 39)
			{
				wintext.gameObject.SetActive(true);
				audioWin.Play();
			}

			other.gameObject.SetActive(false); // Then set that object to be inactive (hide it)
			count += 1; // Adds 1 to count each time we pick up a cube
			scoreText.text = "Score: " + count + "\n\n" + "Buttons: Use Arrow Keys / WASD" + "\n" + "Jump: Space" + "\n\n" + "Objective: Collect All Cubes to win the game"; // Updates the text property of scoreText
		}


	}
}
*/