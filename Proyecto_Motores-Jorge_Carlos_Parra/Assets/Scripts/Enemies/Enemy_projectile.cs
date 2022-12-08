using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_projectile : MonoBehaviour
{
    /*
     * Script para los proyectiles de los enemigos, tiene informacion como su velocidad
     * y su manera de disparar
     */
    [SerializeField] private GameObject projectile;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private float timeToShoot;
    private float shootColdDown;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private bool freqShooter;
    [SerializeField] private AudioSource fireSound;

    public bool FreqShooter { get => freqShooter; set => freqShooter = value; }


    // Start is called before the first frame update
    void Start()
    {
        shootColdDown = timeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (freqShooter)
        {
            shootColdDown -= Time.deltaTime;

            if (shootColdDown < 0)
            {
                Shoot();
            }
        }
    }

    //empieza en la posicion del jugador y va avanzando segun la direccion donde el jugador este mirando
    public void Shoot()
    {
        fireSound.Play();
        GameObject fireball = getFireball();
        fireball.transform.position = transform.position;

        if (transform.localRotation.y != 0)
        {
            fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-projectileSpeed, 0f), ForceMode2D.Force);
            fireball.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectileSpeed, 0f), ForceMode2D.Force);
            fireball.transform.localScale = new Vector3(1, 1, 1);
        }
        shootColdDown = timeToShoot;
    }
    /*
     * Sistema de reciclaje, comprueba si ya existe el objeto en la herarquia, si no existe crea uno
     * si existe lo vuelve activo
     */
    private GameObject getFireball()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject fireball = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        pool.Add(fireball);
        return fireball;
    }
}
