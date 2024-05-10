using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class MonsterController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target = null;
    Vector3 wayPoint;

    public float distDeVisio = 20f;
    public float angleDeVisio = 60f;

    public float distDeMoviment = 20f;
    public float radiEsfera = 10f;
    float minDist = Mathf.Infinity;
    public float temsAbansSeguentPunt = 2f;
    public float distAtack = 1f;
    public float tempsEntreAtacks = 2f;

    bool haAtacat = false;
    public GameObject mainCamera;
    public GameObject screamCamera;
    public AudioSource audioSorolls;
    public Animator animatorController;

    public TMP_Text textState;
    public Rigidbody rb;
    Vector3 lastPosition;
    public float elapsedTime = 0f;

    public List<AudioClip> llistaSorolls;

    //VELOCITATS DEL SOCI MONSTRE
    public float velocitatNormal = 4.0f;
    public float velocitatEnfocat = 2.0f; 
    private float multiplicadorVelocitat = 1.0f;

    private bool enfocat = false;
    public GameObject llanterna;
    bool viu = true;

    public ControladorMenu controladorMenu;

    void Start(){
        wayPoint = new Vector3(-100f, -100f, -100f);
        screamCamera.SetActive(false);
        animatorController.SetBool("Caminar", true);
        lastPosition = transform.position;
    }

    void Update(){
        if(viu){
            if(target == null){ //Si no segueix al jugador -> comprovar si el veu i Voltar
                target = ComprovarVeuPlayer();
                textState.text = "Estat: Voltant";
                Voltant();
            }
            else{ // Si segueix al jugador -> seguir
                //Si el jugador esta molt lluny -> deixar de seguir
                if(Vector3.Distance(transform.position, target.position) >= distDeVisio){
                    target = null;
                }
                else{ //Si el jugador esta aprop -> seguir
                    textState.text = "Estat: Seguint";
                    if(!audioSorolls.isPlaying){
                        audioSorolls.clip = llistaSorolls[1];
                        audioSorolls.Play();
                    }
                    Seguint();

                    if(Vector3.Distance(transform.position, target.position) < distAtack && !haAtacat){
                        haAtacat = true;
                        textState.text = "Estat: Atacant";
                        Atacant();
                        enabled = false;
                    }
                }

                llanternaMonstre();
            }
        }
    }

    Transform ComprovarVeuPlayer(){
        Transform resultTransform = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiEsfera);
        if(hitColliders.Length > 0){
            foreach (Collider hitCollider in hitColliders){
                if(hitCollider.CompareTag("Player")){
                    if(Vector3.Distance(hitCollider.transform.position, transform.position) < minDist){
                        minDist = Vector3.Distance(hitCollider.transform.position, transform.position);
                        resultTransform = hitCollider.transform;
                        Debug.Log("Ara el player esta aprop");
                    }
                }
            }
        }

        RaycastHit hit;
        for(float angle = -angleDeVisio / 2; angle <= angleDeVisio / 2; angle += 5f){
            Vector3 direccio = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.Raycast(transform.position, direccio, out hit, distDeVisio)){
                if (hit.collider.CompareTag("Player")){
                    resultTransform = hit.transform;
                }
            }
        }

        return resultTransform;
    }

    void Voltant(){
        if(wayPoint == new Vector3(-100f, -100f, -100f)){ //Si no tenim punt d'on anar -> buscar punt
            wayPoint = TrobarPuntDinsNavMesh(new Vector3(-100f, -100f, -100f));
        }
        else{ //Si tenim on anar -> comprovar que no estigui parat gaire rato i desplacar-se
            
            if(Vector3.Distance(transform.position, lastPosition) <= 0.1f){
                elapsedTime += Time.deltaTime;

                if(elapsedTime >= 2f){
                    wayPoint = new Vector3(-100f, -100f, -100f);
                    Debug.Log("Molta estona monstre parat");
                }
            }

            navMeshAgent.SetDestination(wayPoint);
            if(Vector3.Distance(wayPoint, transform.position) <= 1){
                wayPoint = new Vector3(-100f, -100f, -100f);
            }

            lastPosition = transform.position;
        }

        if (enfocat){
            navMeshAgent.speed = velocitatEnfocat * multiplicadorVelocitat;
        }
        else{
            navMeshAgent.speed = velocitatNormal * multiplicadorVelocitat;
        }
    }

    Vector3 TrobarPuntDinsNavMesh(Vector3 punt){
        elapsedTime = 0f;
        if(punt == new Vector3(-100f, -100f, -100f)){
            punt = new Vector3(transform.position.x + Random.Range(-distDeMoviment, distDeMoviment), 0f, transform.position.z + Random.Range(-distDeMoviment, distDeMoviment));
        }

        NavMeshHit hit;

        // Intenta trobar una posició vàlida dins del NavMesh
        if (NavMesh.SamplePosition(punt, out hit, 25f, NavMesh.AllAreas))
        {
            //Debug.Log(hit.position);
            return hit.position;
        }
        else
        {
            // Si no es pot trobar una posició vàlida, pots gestionar-ho de diverses maneres.
            // En aquest exemple, simplement retorna el punt original.
            Debug.Log("No es pot trobar una posició vàlida dins del NavMesh: " + punt);
            punt = new Vector3(-100f, -100f, -100f);
            return punt;
        }
    }

    void Seguint(){
        wayPoint = new Vector3(-100f, -100f, -100f);
        //Vector3 pos = TrobarPuntDinsNavMesh(target.position);
        navMeshAgent.SetDestination(target.position);
    }

    void Atacant(){
        navMeshAgent.Stop();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        mainCamera.SetActive(false);
        screamCamera.SetActive(true);
        animatorController.SetTrigger("Scream");
        
        audioSorolls.clip = llistaSorolls[0];
        audioSorolls.Play();
        if(animatorController.GetCurrentAnimatorStateInfo(0).IsName("Scream")){
            controladorMenu.Jugar();
        }
    }

    private bool MonstreApuntat(){
        Vector3 dirMonstreLlanterna = (transform.position - llanterna.transform.position).normalized;

        Vector3 dirLlanterna = llanterna.transform.forward;

        float DiferenciaDireccions = Vector3.Dot(dirMonstreLlanterna, dirLlanterna);
        // Debug.Log("DOT: " + DiferenciaDireccions);
        return DiferenciaDireccions > 0.7f; 
    }

    private void llanternaMonstre(){
        float distancia = Vector3.Distance(transform.position, llanterna.transform.position);
        bool direccions = MonstreApuntat();

        if (distancia < 20 && direccions) enfocat = true; 
        else enfocat = false;
    }

    void OnTriggerEnter(Collider collision){
        if (collision.gameObject.tag == "llumFoco" && viu){
            viu = false;
            navMeshAgent.Stop();
            animatorController.SetBool("Mort", true);
            audioSorolls.Stop();
            audioSorolls.clip = llistaSorolls[2];
            audioSorolls.Play();
        }
    }

    public bool GetHaAtacat()
    {
        return haAtacat;
    }
}
