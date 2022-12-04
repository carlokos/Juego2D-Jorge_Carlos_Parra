using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private AudioSource attackSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackSound.Play();
            collision.gameObject.GetComponent<CombatPlayer>().TomarDaño(damage,
                collision.GetContact(0).normal);
        }
    }
}
