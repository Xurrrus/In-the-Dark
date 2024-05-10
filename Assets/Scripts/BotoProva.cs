using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoProva : MonoBehaviour
{
    public GM_Jugador gm;

    public void BotoClicat(){

        gm.afegirPila();
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gm.afegirPila();
            Destroy(gameObject);
        }
    }
}
