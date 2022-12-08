using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRange : MonoBehaviour
{
    //script que activa el ataque del boss al entrar en el rango
    private Enemy boss;
    private Animator anim;

    private void Start()
    {
        boss = GetComponentInParent<Enemy>();
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !boss.IsAttacking)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Attacking", true);
            boss.IsAttacking = true;
            //desactivamos el objeto del rango para que solo ataque una vez 
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
