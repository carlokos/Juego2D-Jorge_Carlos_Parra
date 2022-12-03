using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject position;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CombatPlayer>().TomarDaño(1,
                collision.GetContact(0).normal);
            collision.gameObject.transform.position = position.transform.position;
        }
    }
}
