using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControladorMenu : MonoBehaviour
{

    public GameObject MenuBotons;
    public GameObject MenuControls;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Controls()
    {
        MenuBotons.SetActive(false);
        MenuControls.SetActive(true);
    }

    public void Menu()
    {
        MenuBotons.SetActive(true);
        MenuControls.SetActive(false);
    }


    public void sortir()
    {
        Application.Quit();
    }
}
