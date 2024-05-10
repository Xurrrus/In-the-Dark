using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndGameDetect : MonoBehaviour
{
    MonsterController monster;

    bool fiJoc;
    float fadeOutValue = -1.5f;
    [SerializeField] Image fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        monster = FindAnyObjectByType<MonsterController>();
    }

    // Update is called once per frame
    void Update()
    {
       if(monster.GetHaAtacat())
        {
            fadeOut.color = new Color(0f, 0f, 0f,fadeOutValue);
            fiJoc = true;
            fadeOutValue += Time.deltaTime;
        }

       if(fadeOutValue >= 2f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
