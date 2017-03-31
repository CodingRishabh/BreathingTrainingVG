/*
 * Author: Joel Prakash
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required to use "Text" object and other UI elements
using System.Net.Sockets;
using System;
using System.Net;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;
    private GameObject floor;
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
    public GameObject inst;
    public GameObject gamestartedtext;



    private IAsyncResult currentAsyncResult;
    static int r_lung = 0;
    static int l_lung = 0;
   
    //int temp = 0;
    int total = 0;
    private UdpClient client = null;
    private IPEndPoint sender = null;
    private string mes = "";
    static int count1 = 0;
    //"172.17.88.251";
    static string ip = "127.0.0.1";
    static int port = 6799;
    public Vector3 mover;

    private float diff = 0;
    private int R_Lung_count = 0;
    private int L_Lung_count = 0;
    private int Up_count = 0;
    private int Down_count = 0;

    private bool moveVertical = false;
    private String laneChangeDirection = "";


    // Use this for initialization
    void Start () {

       // inst = GameObject.FindGameObjectWithTag("insturction");
        //gamestartedtext = GameObject.FindGameObjectWithTag("started");
        gamestartedtext.SetActive(false);

        rb = GetComponent<Rigidbody>(); // Gets the player Info
        audioCalmOcean.Play();

        floor = GameObject.Find("Ground");

    }
	
	// Update is called once per frame
	void Update () {

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
        currentAsyncResult = client.BeginReceive(new System.AsyncCallback(recibir), sender);

        if(rb.transform.position.y > 19)
        {
            Up_count = 0;
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.angularVelocity = Vector3.zero;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
            
        }
        else if (rb.transform.position.y < 3)
        {
            Down_count = 0;
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.angularVelocity = Vector3.zero;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.angularVelocity = Vector3.zero;
        }

        total = r_lung + l_lung;

        if ((725 < total && (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) )) || (Input.GetKey(KeyCode.UpArrow) && ( Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) ) ) )
        {
            Up_count++;
           
        }
        else if ((725 > total && (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) ) ) || (Input.GetKey(KeyCode.DownArrow) && (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) ) )) 
        {
            Down_count++;
            
        }
      
       // temp = total;

      if( Up_count > 1)
        {
            jump();
            Up_count = 0;
        }
      else if(Down_count > 1)
        {
            drop();
            Down_count = 0;
        }

      



    }

    void jump()
    {
       
        
            
                rb.AddForce(Vector3.up * jumpForce);
                audioFloat.Play();
            
    }

    void drop()
    {
       
            rb.AddForce(Vector3.down * jumpForce);
            audioDrop.Play();
       
    }

    // used for force physics controls, since these don't depend on a frame
    void FixedUpdate() // Infinite loop while we are in play mode
    {
        float moveHorizontal = 0.0f;
        diff = r_lung - l_lung;
        //laneChangeDirection = "";



       
        if ( ((diff > 8 && (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) ))  || (Input.GetKey(KeyCode.LeftArrow) && (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A)) )) && withinLane() )
            {
                L_Lung_count++;
                //moveHorizontal = -200f;
                if ((rb.transform.position.x < 0.5f) && (rb.transform.position.x > -0.5f))
                {
                    laneChangeDirection = "C-L";
                }

                else if (rb.transform.position.x > 40f)
                {
                    laneChangeDirection = "R-C";
                }


        }
            else if ( (((diff < -8) && (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A)) ) || (Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A)) )) && withinLane())
            {
                R_Lung_count++;
                //moveHorizontal = 200f;
                
                if( (rb.transform.position.x < 0.5f) && (rb.transform.position.x > -0.5f))
                {
                  laneChangeDirection = "C-R";
                }

                else if (rb.transform.position.x < -40f)
                {
                    laneChangeDirection = "L-C";
                }
        }
        else
        {
            moveHorizontal = 0f;
            laneChangeDirection = "";
        }

      


         
         if(L_Lung_count >= 5)
         {
             moveHorizontal = -200f;
             L_Lung_count = 0;
            R_Lung_count = 0;



        }
         else if (R_Lung_count >= 5)
         {
             moveHorizontal = 200f;
             R_Lung_count = 0;
            L_Lung_count = 0;

        }

        // If you try to move past the Side lanes
        if ( (moveHorizontal > 0.1f && rb.transform.position.x > 40f) || (moveHorizontal < -0.1f && rb.transform.position.x < -40f) )
        {
            moveHorizontal = 0f;
            rb.velocity = new Vector3(0.0f, 0.0f, rb.velocity.z);
            rb.angularVelocity = Vector3.zero;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
            laneChangeDirection = "";
        }

        
        if ( ((rb.transform.position.x > 40f) || (rb.transform.position.x < -40f)) && rb.velocity.x != 0f )
        {
            moveHorizontal = 0f;
            rb.velocity = new Vector3(0.0f, 0.0f, rb.velocity.z);
            rb.angularVelocity = Vector3.zero;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
            laneChangeDirection = "";
        }
     
        // Lock the 3 lanes in certain directions
        if ( (laneChangeDirection.Equals("L-C") || laneChangeDirection.Equals("R-C") || laneChangeDirection.Equals("")) && ((rb.transform.position.x < 0.5f) && (rb.transform.position.x > -0.5f)) )
            {
                rb.velocity = new Vector3(0.0f, 0.0f, rb.velocity.z);
                rb.angularVelocity = Vector3.zero;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                laneChangeDirection = "";
            }
            else if((laneChangeDirection.Equals("C-L") || laneChangeDirection.Equals("")) && (rb.position.x < -40f))
            {
                rb.velocity = new Vector3(0.0f, 0.0f, rb.velocity.z);
                rb.angularVelocity = Vector3.zero;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                laneChangeDirection = "";
        }
            else if ((laneChangeDirection.Equals("C-R") || laneChangeDirection.Equals("")) && (rb.position.x > 40f))
            {
                rb.velocity = new Vector3(0.0f, 0.0f, rb.velocity.z);
                rb.angularVelocity = Vector3.zero;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                laneChangeDirection = "";
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.5f);
        rb.AddForce(movement * movementSpeed * Time.deltaTime);



    }

    void OnTriggerEnter(Collider other) // When the ball enters a Trigger Collider
    {
        if(other.gameObject.gameObject.tag == "Gem_R" || other.gameObject.tag == "Gem_B") // If the object that was entered has the tag "PickUp"
        {
            scoreText.gameObject.SetActive(true);
            audioCollect.Play();
           

            other.gameObject.SetActive(false); // Then set that object to be inactive (hide it)
            if(other.gameObject.tag == "Gem_R")
            {
                count += 10;
            }
            else {
                count += 1; // Adds 1 to count each time we pick up a cube
            }
           
            scoreText.text = "Score: " + count + "\n\n" + "Buttons: Use Arrow Keys / WASD" + "\n" + "Jump: Space" + "\n\n" + "Objective: Collect All Cubes to win the game"; // Updates the text property of scoreText
        }

        
    }
    public void sensor()
    {
        float moveHorizontal = 0.0f;
        moveHorizontal = Input.GetAxis("Horizontal");
    }

    public void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;


        rb.angularVelocity = Vector3.zero;


        transform.rotation = Quaternion.identity;
    }

    public void recibir(System.IAsyncResult res)
    {

        // Waits for a receive from Begin till EndReceive is called
        byte[] bResp = client.EndReceive(res, ref sender);
        // Due to ReuseAddress parameter very fast close and reopen is allowed
        client.Close();
        count1++;
        //Convert the data to a string
        mes = System.Text.Encoding.UTF8.GetString(bResp);

        // convert data to array
        //int[] bytesAsInts = Array.ConvertAll(bResp, c => (int)c);

        // mes = bytesAsInts[0] + ", "+ bytesAsInts[1];
        string[] subStrings = mes.Split(',');
        Int32.TryParse(subStrings[0], out l_lung);
        Int32.TryParse(subStrings[1], out r_lung);
        Debug.Log(mes);
    }

    public bool withinLane()
    {
        if ( (rb.transform.position.x > 40f) || (rb.transform.position.x < -40f) || (rb.transform.position.x > -0.5f && rb.transform.position.x < 0.5f) )
        {
            return true;
        }

        return false;
    }

    private void OnGUI()
    {
        GUILayout.Label("" + rb.transform.position.x);
    }
}
