using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerBoss : MonoBehaviour
{
    [SerializeField] private GameObject[] activarObjetos;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject fondoDefault;
    [SerializeField] private GameObject bossHealth;
    [SerializeField] private BGMusic_manager sonido;
    [SerializeField] private Player player;
    [SerializeField] private CombatPlayer attack;
    
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

    private IEnumerator bossActivation()
    {
        player.Speed = 0;
        attack.CanAttack = false;
        yield return new WaitForSeconds(1.5f);
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
