using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty mostrarBoto;

    public Transform capJugador;
    public float spawnDistancia = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mostrarBoto.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = capJugador.position + new Vector3(capJugador.forward.x,0,capJugador.forward.z).normalized * spawnDistancia;
            menu.transform.LookAt(new Vector3(capJugador.position.x, menu.transform.position.y, capJugador.position.z));
            menu.transform.forward *= -1;
        }
        

    }
}
