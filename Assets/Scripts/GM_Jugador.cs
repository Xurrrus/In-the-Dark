using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Jugador : MonoBehaviour
{

    public List<GameObject> piles;
    public int numeroPiles;

    public GameObject zonaMorir;
    public GameObject gm;

   
    void Start()
    {
       numeroPiles = 0;
        
    }

    
    void Update()
    {
       
    }

    public void afegirPila()
    {
        numeroPiles++;
    }

    public void posarPila()
    {
        gm.GetComponent<Haptic>().ActivarImpulse();
        for (int i = 0; i < numeroPiles; i++)
        {
            piles[i].SetActive(true);
        }

        //StartCoroutine(PassarSegons(numeroPiles, 1f));

        if (numeroPiles == 4)
        {

            GameObject[] llumF = GameObject.FindGameObjectsWithTag("llumFoco");
            for (int i = 0; i < 4; i++)
            {
                llumF[i].GetComponent<LlumFoco>().activarFocos();
            }
            zonaMorir.SetActive(true);
        }
    }

    /*IEnumerator PassarSegons(int numPiles, float segons){
        if(numeroPiles > 0){
            gm.GetComponent<Haptic>().ActivarImpulse();
            piles[numPiles-1].SetActive(true);
            yield return new WaitForSeconds(segons);
            StartCoroutine(PassarSegons(numeroPiles--, segons));
        }
        else yield return null;
    }*/


}
