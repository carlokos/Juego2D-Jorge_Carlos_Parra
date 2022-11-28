using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_projectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private float timeToShoot;
    private float shootColdDown;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private bool freqShooter;
    [SerializeField] private bool watcher;

    public bool Watcher { get => watcher; set => watcher = value; }


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

    public void Shoot()
    {
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
