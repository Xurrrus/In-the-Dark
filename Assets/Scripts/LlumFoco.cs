using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlumFoco : MonoBehaviour
{
    private Light llumFoco;
 
    public void activarFocos()
    {
        llumFoco = gameObject.GetComponent<Light>();

        llumFoco.enabled = true;
    }
}
