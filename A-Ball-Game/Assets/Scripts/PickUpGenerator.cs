using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PickUpGenerator : MonoBehaviour
{


    public GameObject gem_B;
    public GameObject gem_R;

    public float[] lanes_H;
    public float[] lanes_V;

    public int noOfLanes_H;
    public int noOfLanes_V;

    public int spawn_Lane_H = 0;
    public int spawn_Lane_V = 0;

    private bool shouldISpawnNow = false;

    private List<List<GameObject>> gems_BR_active;


    public float spawnDistance;
    private Transform playertransform;

    public float chain_Dist_From_OG;
    public float gem_In_Chain_Dist;

    private float sum_Dist_From_OG = 0.0f;
    private float sum_Dist_From_Parent_Chain = 0.0f;

    public float gem_Size;

    public int max_Chain_Size;

    public int chanceOfSpawningRedDiamond = 10;

    public Text statusText; // "statusText"  will store our UI Text object
    public Text HigherPromptText; // Displays "Go Higher" if Target Height is above player's current height
    public Text OnTargetPromptText; // Displays "On Target" if Target Height is equal to player's current height
    public Text LowerPromptText; // Displays "Go Lower" if Target Height is below player's current height
    public Text InstructionsText;
    public Text StartedText;

    public float time = 0.0f;


    // Random height generator between two doubles
    int rnd_Index(int r)
    {
        return Random.Range(0, r);

    }

    int rnd_Chain_Length()
    {
        return Random.Range(0, max_Chain_Size);
    }

    bool spawnRedDiamond()
    {
        bool chance = false;

        if( rnd_Index(chanceOfSpawningRedDiamond) == 0)
        {
            chance = true;
        }

        return chance;
    }

    // Use this for initialization
    void Start()
    {
        gems_BR_active = new List<List<GameObject>>();
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("spawnLaneInTime");
    }

    private void spawn_Chain(List<GameObject> gemChain)
    {
        sum_Dist_From_Parent_Chain = 0.0f;
      
        int chain_Length = 4;

        if (chain_Length > 0)
        {
            for (int i = 0; i < chain_Length; i++)
            {
                gemChain = spawnGem_Chain(gemChain);
            }

        }

        gems_BR_active.Add(gemChain);
    }

    // Update is called once per frame
    void Update()
    {
        genPrompts();
        if (shouldISpawnNow == true)
        {
            chain_Dist_From_OG = playertransform.position.z + spawnDistance;
                List<GameObject> gemChain = spawnGem_Parent();
                spawn_Chain(gemChain);
                shouldISpawnNow = false;
        }

        if (gems_BR_active.Count > 4)
        {
            deleteGemChain();
        }
    }

    private List<GameObject> spawnGem_Parent()
    {
        List<GameObject> gemChain = new List<GameObject>();
        GameObject gemNewParent;
        if (!spawnRedDiamond())
        {
            gemNewParent = Instantiate(gem_B) as GameObject;
        }
        else
        {
            gemNewParent = Instantiate(gem_R) as GameObject;
        }
        
        float lane_H = lanes_H[spawn_Lane_H];
        float lane_V = lanes_V[spawn_Lane_V];
        gemNewParent.transform.position =  new Vector3(lane_H, lane_V, chain_Dist_From_OG);
        gemChain.Add(gemNewParent);

        return gemChain;

       
    }

    // Child will have same X and Y cordinates
        private List<GameObject> spawnGem_Chain(List<GameObject> gemChain)
    {
        GameObject gemNewChild;
        if (!spawnRedDiamond())
        {
            gemNewChild = Instantiate(gem_B) as GameObject;
        }
        else
        {
            gemNewChild = Instantiate(gem_R) as GameObject;
        }
        sum_Dist_From_Parent_Chain += gem_In_Chain_Dist;
        gemNewChild.transform.position = gemChain[0].transform.position + new Vector3(0.0f, 0.0f, sum_Dist_From_Parent_Chain);
        gemChain.Add(gemNewChild);

        return gemChain;
    }

    private void deleteGemChain()
    {
        destroyGem_Chain(gems_BR_active[0]);
        gems_BR_active.RemoveAt(0);
    }

    private void destroyGem_Chain(List<GameObject> gemChain)
    {
        foreach( GameObject gem in gemChain){
            Destroy(gem);
        }
    }

    //spawn_Lane_H = rnd_Index(noOfLanes_H);
    // 5f = 5 Sec
    IEnumerator spawnLaneInTime()
    {
        for (;;)
        {
            spawn_Lane_H = rnd_Index(noOfLanes_H);
            spawn_Lane_V = rnd_Index(noOfLanes_V);
            yield return new WaitForSeconds(time);
            shouldISpawnNow = true;
            InstructionsText.gameObject.SetActive(false);
            StartedText.gameObject.SetActive(false);
        }
    }

    private void genPrompts()
    {
        statusText.gameObject.SetActive(true);
    

    statusText.text = "Current Height:" + playertransform.position.y + "\n" + "Target Height  :" + lanes_V[spawn_Lane_V]; // Updates the text property of statusText
        float diff_V = playertransform.position.y - lanes_V[spawn_Lane_V];
        if (diff_V < -playertransform.localScale.y)// Go Higher
        {
            HigherPromptText.gameObject.SetActive(true);
            OnTargetPromptText.gameObject.SetActive(false);
            LowerPromptText.gameObject.SetActive(false);
            
        }
        else if (diff_V > playertransform.localScale.y)// Go Lower
        {
            HigherPromptText.gameObject.SetActive(false);
            OnTargetPromptText.gameObject.SetActive(false);
            LowerPromptText.gameObject.SetActive(true);
        }
       

        else if(Mathf.Abs(diff_V) < 3)
        {
            HigherPromptText.gameObject.SetActive(false);
            OnTargetPromptText.gameObject.SetActive(true);
            LowerPromptText.gameObject.SetActive(false);
        }


    }

}
