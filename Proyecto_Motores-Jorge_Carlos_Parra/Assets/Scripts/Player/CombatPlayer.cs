using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{
    private Player player;
    private float cd;
    private Animator animator;
    private SpriteRenderer sprite;

    [Header ("Ataque")]
    [SerializeField] private Transform location;
    [SerializeField] private Vector2 boxsize;
    [SerializeField] private float damage;
    [SerializeField] private float cdtime;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Estadisticas_jugador")]
    [SerializeField] private float vida;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        sprite = GetComponent<SpriteRenderer>();
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
        Collider2D[] foes = Physics2D.OverlapBoxAll(location.position, boxsize, 0, enemyLayers);
        animator.SetTrigger("Attack");
        foreach (Collider2D colisionador in foes)
        {
            colisionador.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(location.position, boxsize);
    }

    private IEnumerator Attacking()
    {
        player.Speed = 0;
        player.Jump1 = false;
        yield return new WaitForSeconds(0.7f);
        player.Speed = 10f;
    }

    public void TomarDaño(float damage, Vector2 posicion)
    {
        vida -= damage;
        animator.SetTrigger("Hurt");
        StartCoroutine(PerdoerControl());
        StartCoroutine(Invulnerabilidad());
        player.Knockback(posicion);
    }

    private IEnumerator PerdoerControl()
    {
        player.SePuedeMover = false;
        yield return new WaitForSeconds(0.7f);
        player.SePuedeMover = true;
    }

    private IEnumerator Invulnerabilidad()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(2.2f);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
