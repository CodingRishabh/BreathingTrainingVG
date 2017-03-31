using System;
using UnityEngine;
using System.Collections;

public class TrafficLightSignals : MonoBehaviour
{
    public TrafficLightSignals()
    {
    }

    private Shader shader1;
    private Shader shader2;

    public Renderer rend_C;
    public Renderer rend_R;
    public Renderer rend_L;

    

    public Light lt_C;
    public Light lt_R;
    public Light lt_L;

    public float duration = 1.0F;

    public Renderer rend_UL;
    public Renderer rend_CL;
    public Renderer rend_DL;

    public Light lt_UL;
    public Light lt_CL;
    public Light lt_DL;
  

    public Renderer rend_UR;
    public Renderer rend_CR;
    public Renderer rend_DR;

    public Light lt_UR;
    public Light lt_CR;
    public Light lt_DR;
  
    void Start()
    {
        shader1 = Shader.Find("Standard");
        shader2 = Shader.Find("FX/Flare");

        rend_L.material.shader = shader1;
        lt_L.intensity = 0;

        rend_C.material.shader = shader1;
        lt_C.intensity = 0;

        rend_R.material.shader = shader1;
        lt_R.intensity = 0;


        rend_UL.material.shader = shader1;
        lt_UL.intensity = 0;

        rend_CL.material.shader = shader1;
        lt_CL.intensity = 0;

        rend_DL.material.shader = shader1;
        lt_DL.intensity = 0;


        rend_UR.material.shader = shader1;
        lt_UR.intensity = 0;

        rend_CR.material.shader = shader1;
        lt_CR.intensity = 0;

        rend_DR.material.shader = shader1;
        lt_DR.intensity = 0;
    }
    void Update()
    {
        // float phi = Time.time / duration * 2 * Mathf.PI;
        //float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
      
        GameObject gem = GameObject.Find("Cubie");
        PickUpGenerator pickUpScript = gem.GetComponent<PickUpGenerator>();
        // pickUpScript.spawn_Lane_H;

        if (pickUpScript.spawn_Lane_V == 0)
        {
            rend_DL.material.shader = shader2;
            lt_DL.intensity = 8;

            rend_DR.material.shader = shader2;
            lt_DR.intensity = 8;

            rend_CL.material.shader = shader1;
            lt_CL.intensity = 0;

            rend_CR.material.shader = shader1;
            lt_CR.intensity = 0;

            rend_UL.material.shader = shader1;
            lt_UL.intensity = 0;

            rend_UR.material.shader = shader1;
            lt_UR.intensity = 0;


        }
        if (pickUpScript.spawn_Lane_V == 1)
        {
            rend_DL.material.shader = shader1;
            lt_DL.intensity = 0;

            rend_DR.material.shader = shader1;
            lt_DR.intensity = 0;

            rend_CL.material.shader = shader2;
            lt_CL.intensity = 8;

            rend_CR.material.shader = shader2;
            lt_CR.intensity = 8;

            rend_UL.material.shader = shader1;
            lt_UL.intensity = 0;

            rend_UR.material.shader = shader1;
            lt_UR.intensity = 0;
        }
        if (pickUpScript.spawn_Lane_V == 2)
        {
            rend_DL.material.shader = shader1;
            lt_DL.intensity = 0;

            rend_DR.material.shader = shader1;
            lt_DR.intensity = 0;

            rend_CL.material.shader = shader1;
            lt_CL.intensity = 0;

            rend_CR.material.shader = shader1;
            lt_CR.intensity = 0;

            rend_UL.material.shader = shader2;
            lt_UL.intensity = 8;

            rend_UR.material.shader = shader2;
            lt_UR.intensity = 8;
        }

        if (pickUpScript.spawn_Lane_H == 0)
        {
           
            rend_L.material.shader = shader2;
            lt_L.intensity = 8;

            rend_C.material.shader = shader1;
            lt_C.intensity = 0;

            rend_R.material.shader = shader1;
            lt_R.intensity = 0;
  
            
        }
        if (pickUpScript.spawn_Lane_H == 1)
        {
            rend_L.material.shader = shader1;
            lt_L.intensity = 0;

            rend_C.material.shader = shader2;
            lt_C.intensity = 8;

            rend_R.material.shader = shader1;
            lt_R.intensity = 0;
        }
        if (pickUpScript.spawn_Lane_H == 2)
        {
            rend_L.material.shader = shader1;
            lt_L.intensity = 0;

            rend_C.material.shader = shader1;
            lt_C.intensity = 0;

            rend_R.material.shader = shader2;
            lt_R.intensity = 8;
        }

    }

   

    
}
