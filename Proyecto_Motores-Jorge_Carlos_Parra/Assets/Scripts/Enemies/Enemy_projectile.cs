using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_projectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
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
        GameObject cross = Instantiate(projectile, transform.position, Quaternion.identity);

        if (transform.localRotation.y != 0)
        {
            cross.GetComponent<Rigidbody2D>().AddForce(new Vector2(-projectileSpeed, 0f), ForceMode2D.Force);
            cross.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            cross.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectileSpeed, 0f), ForceMode2D.Force);
            cross.transform.localScale = new Vector3(1, 1, 1);
        }
        shootColdDown = timeToShoot;
    }
}
