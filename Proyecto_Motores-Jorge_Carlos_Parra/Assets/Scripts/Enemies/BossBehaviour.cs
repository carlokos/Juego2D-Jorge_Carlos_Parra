using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    [Header ("fundamental")]
    [SerializeField] private Player pj; 
    [SerializeField] private GameObject range; 
    [SerializeField] private GameObject hitbox;
    [SerializeField] private float cdCastTime;
    private Animator anim;
    private Enemy boss;
    private bool isCasting;
    private float cdCasting;
    private int chooser;

    [Header ("Teleport")]
    [SerializeField] private Transform[] positionsTP;
    [SerializeField] private AudioSource tpSound;
    [SerializeField] private float cdTeleportTime;
    private int lastPos;
    private float cdTeleport;

    [Header ("Magic Hand")]
    [SerializeField] private magicHand magicHand;
    [SerializeField] private float magicHandSpeed;
    [SerializeField] private AudioSource CastSound;

    [Header("Burning souls")]
    [SerializeField] private GameObject[] fireFloor;
    [SerializeField] private GameObject[] danger;

    [Header("Muerte")]
    [SerializeField] private AudioSource dead;
    [SerializeField] private TrigerBoss bossTrigger;

    private void Start()
    {
        boss = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        cdTeleport = cdTeleportTime;
        cdCasting = cdCastTime;
    }

    private void Update()
    {
        //cada cierto tiempo ira teletransportandose y lanzando hechizos
        cdCasting -= Time.deltaTime;
        if(cdCasting <= 0 && !isCasting && !boss.IsAttacking)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Attacking", false);
            anim.SetTrigger("StartCasting");
            AttackPatron();   
        }

        if(!boss.IsAttacking && !isCasting)
        {
            anim.SetBool("Attacking", false);
            anim.SetBool("Idle", true);
            cdTeleport -= Time.deltaTime;
            if (cdTeleport <= 0)
            {
                Teleport();
            }
        } 
    }

    //metodo que llamamos para elegir un ataque al azar
    private void AttackPatron()
    {
        chooser = Random.Range(0, 2);
        switch (chooser)
        {
            case 0:
                StartCoroutine(magicHandAttack());
                break;
            case 1:
                StartCoroutine(floorOnFire());
                break;
            default:
                Teleport();
                break;
        }
    }

    /*
     * Ataque que prepara una mano magica que persigue al jugador, primero el jefe prepara el hechizo donde el jugador
     * puede aprovechar y golpear al jefe de manera segura. Cuando el hechizo se ha acabado, la mano se queda en el lugar y ataca al jugador
     */
    private IEnumerator magicHandAttack()
    {
        /*
         * El truco esta en que el ataca ya esta en la escena para que pueda perseguir al jugador
         * solo lo activamos y le ponimos la posicion inicial y el script de magicHand se encarga del resto
         * tambien hay que volver a poner la velocidad para pueda pararse y moverse cuando se lo indiquemos
         */
        range.SetActive(false);
        CastSound.Play();
        Vector2 posInicial = new Vector2(pj.transform.position.x, pj.transform.position.y + 2);
        magicHand.Speed = magicHandSpeed;
        magicHand.activate();
        magicHand.transform.position = posInicial;
        isCasting = true;
        anim.SetBool("Casting", true);
        yield return new WaitForSeconds(3.7f);
        //el proyectil se para e intenta golpear en la situacion donde esta
        anim.SetBool("Casting", false);
        StartCoroutine(magicHand.endProjectile());
        finishAnimation();
    }

    private IEnumerator floorOnFire()
    {
        /*
         * Ataque que incendia en fuego 5 de las 6 plataformas donde puede situarse el jefe, primero
         * manda una señal de aviso de cuales va a incendiar para despues hacerlo, terminando con un teletransporte
         * del jefe a un lugar seguro, el jugador tiene que ser rapido para ir hacia donde esta el jefe preparando el hechizo
         * y golpearlo
         */
        range.SetActive(false);
        CastSound.Play();
        Teleport();
        isCasting = true;
        anim.SetBool("Casting", true);
        for(int i = 0; i < positionsTP.Length; i++)
        {
            if(i != lastPos)
            {
                danger[i].SetActive(true);
            }
        }
        yield return new WaitForSeconds(2.3f);
        for (int i = 0; i < positionsTP.Length; i++)
        {
            if (i != lastPos)
            {
                danger[i].SetActive(false);
            }
        }
        StartCoroutine(activateFire());
        anim.SetBool("Casting", false);
        tpProgramado();
        finishAnimation();
    }

    private void tpProgramado()
    {
        /*
         * Despues de activar el suelo de fuego, existe la posibilidad que el enemigo se mantenga en el sitio
         * haciendo que el jugador no tenga ningun sitio para esquivar el ataque, es por eso que el siguiente teletransporte
         * del enemigo tiene que seguir un patrón, para que el jugador pueda esquivar el ataque siempre.
         */
        switch (lastPos)
        {
            case <= 1:
                gameObject.transform.position = positionsTP[lastPos + 1].transform.position;
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;

            case 5:
                gameObject.transform.position = positionsTP[0].transform.position;
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            default:
                gameObject.transform.position = positionsTP[lastPos + 1].transform.position;
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
        }
    }
    //metodo para activar las plataformas de fuego
    private IEnumerator activateFire()
    {
        for(int i = 0; i< positionsTP.Length; i++)
        {
            if(i != lastPos)
            {
                fireFloor[i].SetActive(true);
            }
        }
        yield return new WaitForSeconds(4f);
        for(int i = 0; i < positionsTP.Length; i++)
        {
            fireFloor[i].SetActive(false);
        }
    }
    //coge una posicion de la array y se teletransporta a esa zona
    //guardamos la ultima posicion 
    private void Teleport()
    {
        int i = Random.Range(0, positionsTP.Length);
        lastPos = i;
        if(i <= 2)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        gameObject.transform.position = positionsTP[i].transform.position;
        tpSound.Play();
        cdTeleport = cdTeleportTime;
    }

    private void OnDestroy()
    {
        //quitamos todo lo relacionado al boss del escenario y su UI
        anim.SetBool("Attaking", false);
        anim.SetBool("Casting", false);
        for(int i = 0; i < positionsTP.Length; i++)
        {
            Destroy(fireFloor[i]);
            Destroy(danger[i]);
        }
        Destroy(magicHand.gameObject);
        bossTrigger.desactivateObjects();
    }

    private void finishAnimation()
    {
        isCasting = false;
        cdCasting = cdCastTime;
        anim.SetBool("Idle", true);
        range.SetActive(true);
    }
}
