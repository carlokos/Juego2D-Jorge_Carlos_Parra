using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_behaviour : MonoBehaviour
{
    //Script que controla las animaciones del enemigo Goblin
    private Enemy_mov mov;
    private Enemy enemy;
    private Animator anim;
    private float auxSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        mov = GetComponentInParent<Enemy_mov>();
        anim = GetComponentInParent<Animator>();
        auxSpeed = mov.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.IsFollowing && !enemy.IsAttacking)
        {
            anim.SetBool("Running", true);
            mov.Speed = auxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mov.Speed = 0;
            anim.SetBool("Running", false);
            anim.SetBool("Attaking", true);
            enemy.IsAttacking = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
