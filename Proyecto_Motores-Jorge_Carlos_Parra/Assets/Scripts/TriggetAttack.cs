using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggetAttack : MonoBehaviour
{
    [SerializeField] private Transform location;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float damage;
    [SerializeField] private float cdtime;
    private Player player;
    private float cd;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cd > 0)
        {
            cd -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && cd <= 0)
        {
            StartCoroutine(Attacking());
            Attack();
            cd = cdtime;
        }
    }

    private void Attack()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(location.position, radioGolpe);
        animator.SetTrigger("Attack");
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
                colisionador.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(location.position, radioGolpe);
    }

    private IEnumerator Attacking()
    {
        player.Speed = 0;
        player.Jump1 = false;
        yield return new WaitForSeconds(0.7f);
        player.Speed = 10f;
    }
}
