using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;

    private Light llumLlanterna;

    private bool on;
    private bool off;




    void Start()
    {
        off = true;
        on = false;

        llumLlanterna = flashlight.GetComponent<Light>();
    }


    void OnTriggerEnter(Collider collision)
    {
        
        if (off && collision.gameObject.tag == "mando")
        {
           
            llumLlanterna.enabled = true;
            on = true;
            off = false;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (on && collision.gameObject.tag == "mando")
        {
            
            llumLlanterna.enabled = false;
            on = false;
            off = true;
        }
    }

}
