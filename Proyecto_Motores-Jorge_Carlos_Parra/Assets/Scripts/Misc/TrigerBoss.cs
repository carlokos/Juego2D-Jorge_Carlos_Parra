using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerBoss : MonoBehaviour
{
    [SerializeField] private GameObject[] activarObjetos;
    [SerializeField] private GameObject boss;
    [SerializeField] private AudioSource deadBoss;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject fondoDefault;
    [SerializeField] private GameObject bossHealth;
    [SerializeField] private BGMusic_manager sonido;
    [SerializeField] private Player player;
    [SerializeField] private CombatPlayer attack;
    [SerializeField] private PauseMenuManager PMM;
    
    private void activateObjects()
    {
        //desactivamos la camara principal y el fondo
        mainCamera.enabled = false;
        fondoDefault.SetActive(false);
        //activamos todos los componentes que ayudan al escenario del boss
        foreach(GameObject cosas in activarObjetos)
        {
            cosas.SetActive(true);
        }
        //llamamos a una couritina para activar las cosas del boss
        StartCoroutine(bossActivation());
    }

    public void desactivateObjects()
    {
        deadBoss.Play();
        mainCamera.enabled = true;
        fondoDefault.SetActive(true);
        bossHealth.SetActive(false);
        sonido.BossFinish();
        foreach (GameObject cosas in activarObjetos)
        {
            cosas.SetActive(false);
        }
    }

    private IEnumerator bossActivation()
    {
        PMM.GetComponent<PauseMenuManager>().HaveSpecialUI = true;
        player.Speed = 0;
        attack.CanAttack = false;
        yield return new WaitForSeconds(1.5f);
        boss.SetActive(true);
        bossHealth.SetActive(true);
        sonido.BossStart();
        attack.CanAttack = true;
        player.Speed = 10;
        this.gameObject.SetActive(false);
        Debug.Log("Boss Activado");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activateObjects();
        }
    }
}
