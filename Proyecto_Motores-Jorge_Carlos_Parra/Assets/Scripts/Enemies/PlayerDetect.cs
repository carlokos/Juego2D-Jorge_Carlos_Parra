using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    [SerializeField] private bool Watcher;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && transform.GetComponentInParent<Enemy_mov>().CanFollow)
        {
            transform.GetComponentInParent<Enemy_mov>().StartFollowing();
        }

        if (collision.CompareTag("Player") && Watcher)
        {
            transform.GetComponentInParent<Enemy_projectile>().FreqShooter = true;
            transform.GetComponentInParent<Enemy_projectile>().Shoot();
        }
    }
}
